using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

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
