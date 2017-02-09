using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using OpenCvSharp;

namespace Recognizer
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
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

				var img = detector.OutputImage;

				using(var window = new CvWindow("Picture", img))
				{
					Cv.WaitKey();
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
	}
}
