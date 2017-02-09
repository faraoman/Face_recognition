using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Blob;
using OpenCvSharp.CPlusPlus;

namespace Recognizer
{
	public sealed class FaceDetector
	{
		#region .ctor
		/// <summary> 
		/// Создаёт объект <see cref="FaceDetector"/>
		/// </summary>
		/// <param name="imagePath"> Путь к файлу с изображением. </param>
		public FaceDetector(string imagePath)
		{
			Classifier = new CascadeClassifier(
				Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"haarcascade_frontalface_default.xml"));

			ImageMatrix = new Mat(
				imagePath,
				LoadMode.AnyColor);
		}
		#endregion

		#region Properties

		/// <summary>
		/// Классификатор каскадов для обнаружения объекта.
		/// </summary>
		public CascadeClassifier Classifier { get; set; }

		/// <summary> 
		/// Матрица полученного изображения.
		/// </summary>
		public Mat ImageMatrix { get; set; }

		/// <summary> 
		/// Изображение с распознанными лицами. 
		/// </summary>
		public IplImage OutputImage { get; set; }

		/// <summary> 
		/// Массив прямоугольных областей <seealso cref="Rect"/>, внутри которых обнаружены лица.
		/// </summary>
		public Rect[] DetectedFaces { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Обнаружение лиц на изображении.
		/// </summary>
		/// <returns>
		/// Возвращает массивы прямоугольников <seealso cref="Rect"/> обнаруженных лиц. 
		/// </returns>
		public Rect[] DetectFaces()
		{
			// Detect faceRectangles
			DetectedFaces = Classifier.DetectMultiScale(
				ImageMatrix, 1.095, 6, HaarDetectionType.ScaleImage, new Size(30, 30));
			OutputImage = ImageMatrix.ToIplImage();

			return DetectedFaces;
		}
	
		/// <summary>
		/// Рисует найденные прямоугольники <seealso cref="Rect"/>. 
		/// </summary>
		public void DrawFaceRectangles()
		{
			if(OutputImage != null)
			{
				foreach(var faceRectangle in DetectedFaces)
				{
					OutputImage.DrawRect(
						faceRectangle,
						new CvScalar(
							140,
							60,
							170,
							10),
						2);
				}
			}
		} 
		#endregion

	}
}
