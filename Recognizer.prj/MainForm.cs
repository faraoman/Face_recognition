using Autofac;

using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Mallenom.Imaging;
using Mallenom.Setup;
using Mallenom.Video;
using Mallenom.Video.FFmpeg;

using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

using Recognizer.Database.Data;
using Recognizer.Detector;
using Recognizer.Recognition;
using Recognizer.Logs;

using Mallenom;

//Написать фейковый класс, который вернул бы какие-то записи из БД (Для Сони).
namespace Recognizer
{
	public partial class MainForm : Form
	{
		private IComponentContext _container;

		private IVideoSourceProvider _videoSourceProvider;
		private IVideoSource _videoSource;
		private IImageMatrix _matrix;

		private FaceDetector _detector;
		private LBPFaceRecognizer _recognizer;

		private RecognitionLogController _recognitionLogController;

		public MainForm(IComponentContext container)
		{
			Verify.Argument.IsNotNull(container, nameof(container));

			InitializeComponent();
			this.FormClosing += OnMainFormClosing;

			_container = container;
			Log = _container.Resolve<ILog>();

			InitializeRecognizer();
			InitializeVideoSource();

			_recognitionLogController = container.Resolve<RecognitionLogController>();
			_recognitionLogController.DataGridView = dataGridView1;

			//oldest version by artemd
			/**
			_videoSourceProvider = videoSourceProvider;
			_videoSource = videoSource;
			_matrix = new ColorMatrix();

			_frameImage.Matrix = _matrix;
			_frameImage.ManualUpdateRendererCache = true;
			_frameImage.SizeMode = ImageSizeMode.Zoom;

			_videoSource.AttachMatrix(_matrix);
			_videoSource.MatrixUpdated += OnMatrixUpdated;
			_videoSource.Open();
			/**/

			//тестирование видео без зума, Frames рисуются нормально
			/**
			_videoSourceProvider = videoSourceProvider;
			_frameImage.SizeMode = ImageSizeMode.Zoom;
			var source = new FFmpegVideoSource();
			_matrix = new ColorMatrix();
			source.StreamUrl = "testing_video.avi";
			_frameImage.ManualUpdateRendererCache = true;
			source.RepeatAfterMediaEnded = true;
			_videoSource = source;
			_videoSource.MatrixUpdated += OnMatrixUpdated;
			_frameImage.Matrix = _matrix;
			_detector = new FaceDetector(_frameImage);
			_videoSource.AttachMatrix(_matrix);
			_videoSource.Open();
			_videoSource.Start();
			/**/
		}

		#region Properties
		ILog Log { get; } 

		#endregion

		private void InitializeRecognizer()
		{
			_detector   = _container.Resolve<FaceDetector>();
			_recognizer = _container.Resolve<LBPFaceRecognizer>();

			var trainDataPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFaces.xml");

			_recognizer.Load(trainDataPath);
		}

		private void InitializeVideoSource()
		{
			_videoSourceProvider = _container.Resolve<IVideoSourceProvider>();
			_videoSource         = _videoSourceProvider.CreateVideoSource();

			_matrix              = new ColorMatrix();

			_frameImage.Matrix                    = _matrix;
			_frameImage.ManualUpdateRendererCache = true;
			_frameImage.SizeMode                  = ImageSizeMode.Zoom;

			/*Вставка видео*/
			//var c = new FFmpegVideoSource();
			//c.RepeatAfterMediaEnded = true;
			//c.StreamUrl = "testing_video.avi";
			//c.AttachMatrix(_matrix);

			_videoSource.AttachMatrix(_matrix);
			_videoSource.MatrixUpdated += OnMatrixUpdated;

			try
			{
				_videoSource.Open();
				_videoSource.Start();
			}
			catch
			{
			}
		}

		private void OnMainFormClosing(object sender, FormClosingEventArgs e)
		{
			if(_videoSource != null)
			{
				_videoSource.DetachAllMatrices();
				_videoSource.MatrixUpdated -= OnMatrixUpdated;
			}
		}

		private int _frameCounter;
		private int _skipFrames = 15;

		private void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
		{
			var mi = new MethodInvoker(()=>
			{
				if(!IsDisposed)
				{
					_frameImage.InvalidateCache();
				}
			});

			if(_frameImage.InvokeRequired)
			{
				if(!IsDisposed)
				{
					try
					{
						_frameImage.BeginInvoke(mi);
					}
					catch(ObjectDisposedException)
					{
					}
				}
			}
			else
			{
				mi();
			}

			/**/
			if(_frameCounter >= _skipFrames)
			{
				//устанавливаем кадр, который требуется проанализировать
				//_detector.UpdateImages(_matrix.CreateBitmap());

				//пытаемся распознать человека FaceRecognizer'ом
				//_detector.DetectFaces();
				//_detector.DrawFaceRectangles();
				_detector.UpdateImages(_matrix);

				var rects = _detector.DetectFaces();

				if(_frameImage.InvokeRequired)
				{
					_frameImage.BeginInvoke(new MethodInvoker(() =>
					{
						DrawRectangles(rects);
					}));
				}
				/*await*/
				FaceProcessing(/*_detector.FacesRepository*/);
				
				_frameCounter = 0;
			}
			_frameCounter++;
			/**/
		}

		private /*async*/ void OnButtonTestOpenCV_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Выберите картинку";
			openFile.InitialDirectory = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Photo");
			openFile.Filter = "Images|*.jpg;*.png;*.bmp|All files|*.*";

			if(openFile.ShowDialog() == DialogResult.OK)
			{
				/**/
				var detector = new FaceDetector(openFile.FileName);

				detector.DetectFaces();
				detector.DrawFaceRectangles();
				/**/

				//string trainDataPath = Path.Combine(
				//	Directory.GetCurrentDirectory(),
				//	"Samples",
				//	"LBPFaces.xml");


				//_recognizer.Train(_images, _labels);
				//_recognizer.Save(trainDataPath);

				//!Примерно вот так можно скопировать из ресурсов строку во вновь созданный файл
				/**/
				var facesxml_string = Properties.Resources.LBPFaces;


				File.WriteAllText(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFacesCOPY.xml"), facesxml_string);

				var trainDataPath = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFaces.xml");

				foreach(var face in detector.FacesRepository)
				{
					int result = _recognizer.Recognize(face);
					string resultStr = string.Empty;

					if(result != -1)
					{
						try
						{
							var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
							var filter = _container.Resolve<EmployeesLogRepositoryFilter>();
							filter.PersonLabel = result;

							var employeeRecord = employeeLogRepository.FetchRecords(filter)[0];

							resultStr = "Распознан " + employeeRecord;

							Log.Info(resultStr);
						}
						catch(Exception exc)
						{
							Log.Error("Ошибка в запросе к БД", exc);
						}
					}

					else
					{
						resultStr = "Лицо не распознано";

						Log.Info(resultStr);
					}

					MessageBox.Show(resultStr, "Recognizer", MessageBoxButtons.OK);
				}

				detector
					.FacesRepository
					.Clear();
			}
		}

		private void DrawRectangles(Rect[] rectangles)
		{
			_frameImage.Frames.Clear();
			foreach(var rect in rectangles)
			{
				_frameImage.Frames.Add(new Frame(rect.X, rect.Y, rect.Width, rect.Height)
				{
					Locked = true,
					Visible = true
				});
			}
			_frameImage.InvalidateCache();
		}

		private void FaceProcessing()
		{	
			foreach(var face in _detector.FacesRepository)
			{
				int result = _recognizer.Recognize(face);
				string resultStr = string.Empty;

				if(result != -1)
				{
					try
					{
						var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
						var filter = _container.Resolve<EmployeesLogRepositoryFilter>();
						filter.PersonLabel = result;

						var employeeRecord = employeeLogRepository.FetchRecords(filter)[0];

						resultStr = "Распознан " + employeeRecord;

						Log.Info(resultStr);
					}
					catch(Exception exc)
					{
						Log.Error("Ошибка в запросе к БД", exc);
					}
				}

				else
				{
					resultStr = "Лицо не распознано";

					Log.Info(resultStr);
				}

				//MessageBox.Show(resultStr, "Recognizer", MessageBoxButtons.OK);
			}

			_detector
					.FacesRepository
					.Clear();
		}

		private void OnButtonTestCamera_Click(object sender, EventArgs e)
		{
			IplImage frame = null;
			//IplImage src = null;
			CvCapture capture = null;
			
			//CvWindow windowCapture = new CvWindow("Camera capture");

			using(CvWindow windowCapture = new CvWindow("Camera capture", WindowMode.KeepRatio))
			{
				try
				{
					capture = new CvCapture(0);
				}
				catch
				{
					Debug.WriteLine("Error: can't open camera.");
				}

				if(capture != null)
				{

					double width = capture.GetCaptureProperty(CaptureProperty.FrameWidth);
					double height = capture.GetCaptureProperty(CaptureProperty.FrameHeight);

					Debug.WriteLine("Width: " + width);
					Debug.WriteLine("Height: " + height);

					int counter = 0;
					while(true)
					{
						capture.GrabFrame();
						frame = capture.RetrieveFrame();

						windowCapture.ShowImage(frame);

						int key = CvWindow.WaitKey(33);
						if(key == 27) // ESC 
						{
							windowCapture.Close();
							break;

						}
						else if(key == 13) //Enter 
						{
							frame.SaveImage($"Image{ counter}.jpg");
							counter++;
						}
					}
				}
				else
				{
					Debug.WriteLine("Error: can't open camera.");
				}
				Debug.WriteLine("End");
			}				
		}

		private void _btnTestBtn_Click(object sender, EventArgs e)
		{
			using(TestForm testForm = new TestForm())
			{
				if(testForm.ShowDialog() != DialogResult.Cancel)
				{

				}
			}
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var setupForm = new SingleControlSetupForm(_videoSourceProvider.CreateSetupControl(_videoSource)))
			{
				if(setupForm.ShowDialog(this) == DialogResult.OK)
				{
					_videoSource.Close();
					_videoSource.Open();
					_videoSource.Start();
				}
			}
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void спискиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var formLists = new UserLists(_container))
			{
				formLists.ShowDialog(this);
			}					
		}

		private void _btnTestVideoFromFile_Click(object sender, EventArgs e)
		{
			var m = new ColorMatrix().ToMat();
		}
	}
}
