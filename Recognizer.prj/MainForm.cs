using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Mallenom.Imaging;
using Mallenom.Setup;
using Mallenom.Video;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using Recognizer.Detector;
using Recognizer.Recognition;
using Mallenom.FFmpeg;
using Mallenom.Video.FFmpeg;

using static Recognizer.Logs.LoggingService;
using Recognizer.Database.Data;
using Mallenom.Diagnostics.Logs;
using System.Threading.Tasks;

//Написать фейковый класс, который вернул бы какие-то записи из БД (Для Сони).
namespace Recognizer
{
	public partial class MainForm : Form
	{
		IVideoSourceProvider _videoSourceProvider;
		IVideoSource _videoSource;
		IImageMatrix _matrix;

		/**
		IVideoSourceProvider _testVideoSourceProvider;
		IVideoSource _testVideoSource;
		IImageMatrix _testVideoMatrix;
		/**/

		FaceDetector _faceDetector;
		object facesLock = new object();

		public MainForm(IVideoSourceProvider videoSourceProvider, IVideoSource videoSource)
		{
			InitializeComponent();
			this.FormClosing += OnMainFormClosing;
			/**
			_videoSourceProvider = videoSourceProvider;
			_videoSource = videoSource;
			_matrix = new ColorMatrix();

			//currnet
			//_matrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
			//current

			_videoImage.Matrix = _matrix;
			_videoImage.ManualUpdateRendererCache = true;
			_videoImage.SizeMode = ImageSizeMode.Zoom;

			_videoSource.AttachMatrix(_matrix);
			_videoSource.MatrixUpdated += OnMatrixUpdated;
			_videoSource.Open();

			//НУЖНО получить из потока изображение	
			_faceDetector = new FaceDetector(_videoImage);

			/**/


			//current
			/**/

			_faceDetector = new FaceDetector(_videoImage);
			_videoSourceProvider = videoSourceProvider;
			_videoSource = new FFmpegVideoSource();

			_matrix = new ColorMatrix();
			//_videoSource.AttachMatrix(_matrix);


			var c = new Mallenom.Video.FFmpeg.FFmpegVideoSource();
			c.RepeatAfterMediaEnded = true;
			c.StreamUrl = "testing_video.avi";
			c.AttachMatrix(_matrix);

			_videoSource = c;

			_videoImage.Matrix = _matrix;
			_videoImage.ManualUpdateRendererCache = true;
			_videoImage.SizeMode = ImageSizeMode.Zoom;


			_videoSource.MatrixUpdated += OnMatrixUpdated;

			c.Open();
			c.Start();
			/**

			Debug.WriteLine(_videoSourceProvider.Description);
			Debug.WriteLine(c.StreamUrl);
			/**/


			/**
			Debug.WriteLine(_videoImage.Width);
			Debug.WriteLine(_videoImage.Height);
			_videoImage.Load("1234.bmp");
			Debug.WriteLine(_videoImage.Matrix.Stride);

			Debug.WriteLine(_videoImage.Matrix.DataFormat);
			Debug.WriteLine(_videoSource.State);
			/**/

			//_logView.Appender = new LogViewAppender();
			/**/


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

		private /*async*/ void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
		{
			MethodInvoker mi = new MethodInvoker(()=>
			{
				_videoImage.InvalidateCache();
			});

			_videoImage.BeginInvoke(mi);

			/**/
			if(_frameCounter >= _skipFrames)
			{
				//устанавливаем кадр, который требуется проанализировать
				//_faceDetector.UpdateImages(_matrix.CreateBitmap());

				//пытаемся распознать человека FaceRecognizer'ом
				//_faceDetector.DetectFaces();
				//_faceDetector.DrawFaceRectangles();

				/*await*/ FaceProcessingAsync(/*_faceDetector.FacesRepository*/);
				
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


				//recognizer.Train(_images, _labels);
				//recognizer.Save(trainDataPath);

				//!Примерно вот так можно скопировать из ресурсов строку во вновь созданный файл
				/**/
				var facesxml_string = Properties.Resources.LBPFaces;


				File.WriteAllText(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFacesCOPY.xml"), facesxml_string);

				//trainDataPath = Path.Combine(
				//	Directory.GetCurrentDirectory(),
				//	"Samples",
				//	"LBPFacesCOPY.xml");
				//recognizer.Load();
				/**/				

				//using(var window = new Window("Picture", detector.OutputMatrix))
				//{
				//	Cv.WaitKey(1000);
				//}

				/*await*/ FaceProcessingAsync(/*detector.FacesRepository*/);
			}
		}

		private Task FaceProcessingAsync(/*List<Mat> faces*/)
		{
			return Task.Run(() =>
			{
				//var detector = new FaceDetector(openFile.FileName);
				lock(facesLock)
				{
					_faceDetector.UpdateImages(_matrix.CreateBitmap());
					_faceDetector.DetectFaces();
					_faceDetector.DrawFaceRectangles();

					LBPFaceRecognizer recognizer = new LBPFaceRecognizer();

					var trainDataPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFacesCOPY.xml");

					recognizer.Load(trainDataPath);				

					foreach(var face in _faceDetector.FacesRepository)
					{
						int result = recognizer.Recognize(face);
						string resultStr = string.Empty;

						if(result != -1)
						{
							try
							{
								var employeeLogRepository = new EmployeesLogRepository(Services.DatabaseService.DbConnectionFactory);

								var filter = new EmployeesLogRepositoryFilter
								{
									PersonLabel = result
								};

								var employeeRecord = employeeLogRepository
									.FetchRecord(filter)[0];

								resultStr = "Распознан " + employeeRecord;

								Log.Info(resultStr);
								//_logView.Text += $"{resultStr}{Environment.NewLine}";
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
							//_logView.Text += $"{resultStr}{Environment.NewLine}";
						}

						//MessageBox.Show(resultStr, "Recognizer", MessageBoxButtons.OK);
					} 
				}
			});
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
					_videoSource.Stop();
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
			using(var formLists = new UserLists())
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
