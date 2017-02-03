using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenCvSharp;

namespace Recognizer.prj
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void OnButtonTestOpenCV_Click(object sender, EventArgs e)
		{
			using(IplImage image = new IplImage(256, 256, BitDepth.U8, 1))
			{
				image.Zero();
				for(int x = 0; x < image.Width; x++)
				{
					for(int y = 0; y < image.Height; y++)
					{
						int offset = y * image.WidthStep + x;
						byte value = (byte)(x + y);
						Marshal.WriteByte(image.ImageData, offset, value);
					}
				}
				using(CvWindow window = new CvWindow("Success!", WindowMode.AutoSize, image))
				{
					CvWindow.WaitKey();
				}
			}
		}

		private void OnButtonTestCamera_Click(object sender, EventArgs e)
		{
			IplImage frame = null;
			IplImage src = null;
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
	}
}
