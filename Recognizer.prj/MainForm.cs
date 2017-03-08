using Autofac;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Mallenom;
using Mallenom.Diagnostics.Logs;
using Mallenom.Imaging;
using Mallenom.Setup;
using Mallenom.Video;

using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

using Recognizer.Database.Data;
using Recognizer.Detector;
using Recognizer.Recognition;
using Recognizer.Logs;

namespace Recognizer
{
	public partial class MainForm : Form
	{
		#region Data

		private IComponentContext _container;

		private IVideoSourceProvider _videoSourceProvider;
		private IVideoSource _videoSource;
		private IImageMatrix _matrix;

		private FaceDetector _detector;
		private LBPFaceRecognizer _recognizer;

		//private RecognitionLogController _recognitionLogController;

		private int _frameCounter;
		private int _skipFrames = 5;

		#endregion

		#region .ctor

		public MainForm(IComponentContext container)
		{
			Verify.Argument.IsNotNull(container, nameof(container));

			InitializeComponent();
			this.FormClosing += OnMainFormClosing;

			_container = container;

			// fixme
			//var loggingService = _container.Resolve<ILoggingService>();
			Log = LogManager.GetLog(typeof(LoggingService));

			InitializeRecognizer();
			InitializeVideoSource();

			_logView.Appender = new LogViewAppender();
			_logView.Appender.DoAppend(new LogEvent(Level.Info, "Hello, bitch", new Exception(), DateTime.Now, "root"));

			//_recognitionLogController = container.Resolve<RecognitionLogController>();
			//_recognitionLogController.DataGridView = dataGridView1;
		}

		#endregion

		#region Properties

		ILog Log { get; }

		#endregion

		#region Methods
		private void InitializeRecognizer()
		{
			_detector = _container.Resolve<FaceDetector>();
			_recognizer = _container.Resolve<LBPFaceRecognizer>();

			var trainDataPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFaces.xml");

			_recognizer.Load(trainDataPath);

			_recognizer.FaceRecognized += OnFaceRecognized;
		}

		private void InitializeVideoSource()
		{
			_videoSourceProvider = _container.Resolve<IVideoSourceProvider>();
			_videoSource = _videoSourceProvider.CreateVideoSource();

			_matrix = new ColorMatrix();

			_frameImage.Matrix = _matrix;
			_frameImage.ManualUpdateRendererCache = true;
			_frameImage.SizeMode = ImageSizeMode.Zoom;

			/*Вставка видео*/
			//var c = new FFmpegVideoSource();
			//c.RepeatAfterMediaEnded = true;
			//c.StreamUrl = "testing_video.avi";
			//c.AttachMatrix(_matrix);

			//current
			//var cur = new DXCaptureSource();
			//_videoSource = cur;

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

		private void DrawRectangles(Rect[] rectangles)
		{
			_frameImage.Frames.Clear();
			foreach(var rect in rectangles)
			{
				Rect rect_ = Shakalization(rect);

				_frameImage.Frames.Add(new Frame(rect_.X, rect_.Y, rect_.Width, rect_.Height)
				{
					Locked = true,
					Visible = true
				});
			}
			_frameImage.InvalidateCache();
		}

		private Rect Shakalization(Rect rect)
		{
			int frameWidth = _frameImage.Matrix.Width,
				frameHeigth = _frameImage.Matrix.Height,
				componentWidth = _frameImage.Width,
				componentHeigth = _frameImage.Height;

			double shakalizationFactor = (double)componentWidth / frameWidth;

			int shiftY = (int)(componentHeigth - (frameHeigth * shakalizationFactor)) / 2;

			rect.X = (int)(rect.X * shakalizationFactor);
			rect.Y = (int)((rect.Y * shakalizationFactor) + shiftY);

			rect.Width = (int)(rect.Width * shakalizationFactor);
			rect.Height = (int)(rect.Height * shakalizationFactor);

			return rect;
		}

		private void FaceProcessing()
		{
			foreach(var face in _detector.FacesRepository)
			{
				lock(this)
				{
					_recognizer.Recognize(face); 
				}
			}

			_detector
					.FacesRepository
					.Clear();
		}
		#endregion

		#region EventHandlers
		
		/// <summary> Обработчик события <see cref="LBPFaceRecognizer.FaceRecognized"/>. </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFaceRecognized(object sender, FaceRecognizedEventArgs e)
		{
			try
			{
				var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
				var filter = _container.Resolve<EmployeesLogRepositoryFilter>();
				filter.PersonLabel = e.Label;

				var employeeRecord = employeeLogRepository.FetchRecords(filter)[0];
				var resultStr = "Распознан " + employeeRecord;

				Log.Info(resultStr);

				var mi = new MethodInvoker(() =>
				{
					if(!IsDisposed)
					{
						_listBox.Items.Add(resultStr);
					}
				});

				if(_listBox.InvokeRequired)
				{
					if(!IsDisposed)
					{
						try
						{
							_listBox.BeginInvoke(mi);
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
			}
			catch(Exception exc)
			{
				Log.Error("Ошибка в запросе к БД", exc);
			}
		}

		private void OnMainFormClosing(object sender, FormClosingEventArgs e)
		{
			if(_videoSource != null)
			{
				_videoSource.DetachAllMatrices();
				_videoSource.MatrixUpdated -= OnMatrixUpdated;
			}

			_recognizer.FaceRecognized -= OnFaceRecognized;
		}

		private void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
		{
			var mi = new MethodInvoker(() =>
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
				_detector.UpdateImages(_matrix);

				var rects = _detector.DetectFaces();

				mi = new MethodInvoker(() => DrawRectangles(rects));
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

				FaceProcessing();

				_frameCounter = 0;
			}
			_frameCounter++;
			/**/
		}

		private void OnButtonTestOpenCV_Click(object sender, EventArgs e)
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

		private void OnButtonTestCamera_Click(object sender, EventArgs e)
		{

			_videoSource.Stop();
			_videoSource.Close();

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

			_videoSource.Open();
			_videoSource.Start();
		}

		private void _btnTestBtn_Click(object sender, EventArgs e)
		{
			// stop videosource
			_videoSource.Stop();
			_videoSource.Close();
			using(TestForm testForm = new TestForm(_container))
			{
				if(testForm.ShowDialog() != DialogResult.Cancel)
				{

				}
			}
			// start videosource
			_videoSource.Open();
			_videoSource.Start();
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
			_videoSource.Stop();
			_videoSource.Close();

			using(var formLists = new UserLists(_container))
			{
				formLists.ShowDialog(this);
			}

			_videoSource.Open();
			_videoSource.Start();

		}

		private void _btnTestVideoFromFile_Click(object sender, EventArgs e)
		{
			var m = new ColorMatrix().ToMat();
		} 
		#endregion
	}
}
