using System.Collections.Generic;
using System.Diagnostics;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Recognition
{
	/// <summary> 
	/// Класс, реализующий метод распознавания лиц с помощью 
	/// линейного дискриминантного анализа (линейный дискриминант Фишера или LDA). 
	/// </summary>
	public class FisherFaceRecognizer
	{
		
		private FaceRecognizer _recognizer;

		public FisherFaceRecognizer()
		{
			_recognizer = FaceRecognizer
				.CreateFisherFaceRecognizer(numComponents: 1, threshold: 10000);
		}

		public void Train(IEnumerable<Mat> images, IEnumerable<int> labels)
		{
			_recognizer.Train(images, labels);
		}

		public void Save(/*string filePath*/)
		{
			_recognizer.Save(@"E:\Study\C#\FaceRecognition\Faces\gray\FisherFaces.xml");
		}

		public void Load(/*string filePath*/)
		{
			_recognizer.Load(@"E:\Study\C#\FaceRecognition\Faces\gray\FisherFaces.xml");
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

			Debug.WriteLine($"Label:{label}, Confidence is {confidence}");

			return label;
		}
	}
}
