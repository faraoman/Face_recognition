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

namespace Recognizer
{
	public partial class MainForm : Form
	{
		// список изображений
		private static LinkedList<Mat> _images = new LinkedList<Mat>();
		// список меток
		private static LinkedList<int> _labels = new LinkedList<int>();
		IVideoSourceProvider _provider;
		IVideoSource _videoSource;

		public MainForm()
		{
			InitializeComponent();
		
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\angry.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\happy.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\normal.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\sad.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\smiled.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\surprised.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\wow.png", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\test.png", LoadMode.GrayScale));
			//_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\leftlighted.png", LoadMode.GrayScale));
			//_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\gray\e.png", LoadMode.GrayScale));

			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\2\happy.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\2\normal.jpg", LoadMode.GrayScale));
			_images.AddLast(new Mat(@"E:\Study\C#\FaceRecognition\Faces\2\surprised.jpg", LoadMode.GrayScale));

			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			_labels.AddLast(1);
			//_labels.AddLast(1);
			//_labels.AddLast(1);

			_labels.AddLast(2);
			_labels.AddLast(2);
			_labels.AddLast(2);

			_provider = new IPCameraSourceProvider();
			_videoSource = _provider.CreateVideoSource();

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

				FisherFaceRecognizer recognizer = new FisherFaceRecognizer();
				recognizer.Train(_images, _labels);
				recognizer.Save();

				using(var window = new Window("Picture", detector.OutputMatrix))
				{
					Cv.WaitKey();
				}

				foreach(var face in detector.FacesRepository)
				{
					int result = recognizer.Recognize(face);
					//face.SaveImage(@"E:\Study\C#\FaceRecognition\Faces\gray\wow.png");

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
