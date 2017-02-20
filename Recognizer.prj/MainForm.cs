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

//Написать фейковый класс, который вернул бы какие-то записи из БД (Для Сони).
namespace Recognizer
{
	public partial class MainForm : Form
	{
		IVideoSourceProvider _provider;
		IVideoSource _videoSource;
		IImageMatrix _matrix;

		FaceDetector _faceDetector;

		public MainForm()
		{
			InitializeComponent();

			_provider = new IPCameraSourceProvider();
			_videoSource = _provider.CreateVideoSource();
			_matrix = new ColorMatrix();

			_videoImage.Matrix = _matrix;
			_videoImage.ManualUpdateRendererCache = true;
			_videoImage.SizeMode = ImageSizeMode.Zoom;

			_videoSource.AttachMatrix(_matrix);
			_videoSource.MatrixUpdated += OnMatrixUpdated;
			_videoSource.Open();

			//НУЖНО получить из потока изображение	
			_faceDetector = new FaceDetector(_videoImage);

		}

		private int _frameCounter;
		private int _skipFrames = 3;

		private void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
		{
			if(_frameCounter >= _skipFrames)
			{
				//устанавливаем кадр, который требуется проанализировать
				_faceDetector.UpdateImages(_matrix.CreateBitmap());

				//пытаемся распознать человека FaceRecognizer'ом
				_faceDetector.DetectFaces();
				_faceDetector.DrawFaceRectangles();
				
				_frameCounter = 0;
			}
			_frameCounter++;
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
				var detector = new FaceDetector(openFile.FileName);

				detector.DetectFaces();
				detector.DrawFaceRectangles();

				LBPFaceRecognizer recognizer = new LBPFaceRecognizer();

				string trainDataPath = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFaces.xml");


				//recognizer.Train(_images, _labels);
				//recognizer.Save(trainDataPath);

				//!Примерно вот так можно скопировать из ресурсов строку во вновь созданный файл
				/**/
				var facesxml_string = Properties.Resources.LBPFaces;


				File.WriteAllText(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFacesCOPY.xml"), facesxml_string);

				trainDataPath = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"LBPFacesCOPY.xml");
				//recognizer.Load();
				/**/

				recognizer.Load(trainDataPath);
				

				using(var window = new Window("Picture", detector.OutputMatrix))
				{
					Cv.WaitKey();
				}

				foreach(var face in detector.FacesRepository)
				{
					int result = recognizer.Recognize(face);

					using(var faceWindow = new Window("Picture", face))
					{
						Cv.WaitKey();
					}
				}
			}
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
			using(var setupForm = new SingleControlSetupForm(_provider.CreateSetupControl(_videoSource)))
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
	}
}
