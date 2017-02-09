using System;
using System.Windows.Forms;

namespace Recognizer
{
	public partial class TestForm : Form
	{
		//FaceDetector detect = new FaceDetector();
		public TestForm()
		{
			InitializeComponent();
		}

		private void _btnLoadImage_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Выберите картинку";
			openFile.FileName = "*.jpg";
			if(openFile.ShowDialog() == DialogResult.OK)
			{
				
			}

		}
	}
}
