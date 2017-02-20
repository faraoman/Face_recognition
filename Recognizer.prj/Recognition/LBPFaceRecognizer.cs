using System.Collections.Generic;
using System.Diagnostics;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Recognition
{
	/// <summary> 
	/// Класс, реализующий метод распознавания лиц с помощью 
	/// Local Binary Patterns (LPB). 
	/// </summary>
	public class LBPFaceRecognizer
	{
		
		private FaceRecognizer _recognizer;

		public LBPFaceRecognizer()
		{
			_recognizer = FaceRecognizer
				.CreateLBPHFaceRecognizer(radius: 1, neighbors: 8, gridX: 8, gridY: 8, threshold: 80);
		}

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

			// Если confidence меньше чем величина threshold, то считается, что лицо распознано.
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
			}

			Debug.WriteLine($"Label:{label}, this is {name}. Confidence is {confidence}");

			return label;
		}
	}
}
