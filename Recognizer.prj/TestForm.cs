using System;
using System.Windows.Forms;
using Recognizer.Logs;

namespace Recognizer
{
	public partial class TestForm : Form
	{
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
				try
				{
					int i = 1;
					int a = 2 / --i;
				}
				catch(Exception exc)
				{
					Log.Error($"Не удалось открыть изображение {openFile.FileName}", exc);
				}
			}

		}
	}
}
