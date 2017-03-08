using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using Recognizer.Database;
using Recognizer.Detector;
using Recognizer.Entities;
using Recognizer.Recognition;
using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public partial class TestForm : Form
	{
		IComponentContext _container;

		public TestForm(IComponentContext container)
		{
			InitializeComponent();

			_container = container;

		}

		private void _btnLoadImage_Click(object sender, EventArgs e)
		{
			var recogizer = _container.Resolve<LBPFaceRecognizer>();

			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Выберите картинку";
			openFile.InitialDirectory = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Photo");
			openFile.Filter = "Images|*.jpg;*.png;*.bmp|All files|*.*";

			

			if(openFile.ShowDialog() == DialogResult.OK)
			{
				var faceDetector = new FaceDetector(openFile.FileName);
				Mat cop = faceDetector.OutputMatrix.ConvertToGray();
				//Mat mat = new Mat(openFile.FileName, LoadMode.AnyColor);
				int label = 7;

				var mats = new List<Mat>();
				var labels = new List<int>();

				mats.Add(cop);
				labels.Add(label);

				recogizer.Update(mats, labels);

				var trainDataPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFaces.xml");

				recogizer.Save(trainDataPath);
			}
						
		}
	}
}
