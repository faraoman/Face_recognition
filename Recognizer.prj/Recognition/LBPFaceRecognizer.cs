using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mallenom;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Recognition
{
	public class FaceRecognizedEventArgs : EventArgs
	{
		public FaceRecognizedEventArgs(int label)
		{
			Label = label;
		}

		public int Label { get; }
	}

	/// <summary> 
	/// Класс, реализующий метод распознавания лиц с помощью 
	/// Local Binary Patterns (LPB). 
	/// </summary>
	public class LBPFaceRecognizer
	{
		#region Data
		private FaceRecognizer _recognizer;
		#endregion

		#region Events
		public event EventHandler<FaceRecognizedEventArgs> FaceRecognized;
		#endregion

		#region .ctor

		public LBPFaceRecognizer()
		{
			_recognizer = FaceRecognizer
				.CreateLBPHFaceRecognizer(radius: 1, neighbors: 8, gridX: 8, gridY: 8, threshold: 80);
		}

		#endregion

		#region Handlers

		private void OnFaceRecognized(FaceRecognizedEventArgs e)
		{
			Assert.IsNotNull(e);

			FaceRecognized?.Invoke(this, e);
		}

		#endregion

		#region Methods

		public void Train(IEnumerable<Mat> images, IEnumerable<int> labels)
		{
			_recognizer.Train(images, labels);
		}

		public void Save(string filePath)
		{
			_recognizer.Save(filePath);
		}

		public void Load(string filePath)
		{
			_recognizer.Load(filePath);
		}

		public void Load()
		{
			var filePath = Recognizer.Properties.Resources.LBPFaces;
			_recognizer.Load(filePath);
		}

		public int Recognize(Mat image)
		{
			// Метка
			int label = -1;
			// "Уверенность"
			double confidence = 0.0;

			// Если confidence меньше, чем величина threshold, то считается, что лицо распознано.
			_recognizer.Predict(src: image,
				label: out label,
				confidence: out confidence);

			string name = string.Empty;

			switch(label)
			{
				default:
					name = "unknown person";
					break;
				case 1:
					name = "Pavel";
					break;
				case 3:
					name = "Andrey";
					break;
				case 4:
					name = "Matvey";
					break;

				case 7:
					name = "Valery";
					break;
			}

			if(label != -1)
			{
				OnFaceRecognized(new FaceRecognizedEventArgs(label));
			}

			Debug.WriteLine($"Label:{label}, this is {name}. Confidence is {confidence}");

			return label;
		}

		public void Update(IEnumerable<Mat> images, IEnumerable<int> labels)
		{
			_recognizer.Update(images, labels);
		}
		#endregion
	}
}
