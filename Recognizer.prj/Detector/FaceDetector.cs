using System.IO;

using OpenCvSharp;
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
			// Detect face rectangles
			DetectedFaces = Classifier.DetectMultiScale(
				image: ImageMatrix,
				scaleFactor: 1.095,
				minNeighbors: 3,
				flags: HaarDetectionType.ScaleImage,
				minSize: new Size(30, 30));
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
						rect: faceRectangle,
						color: new CvScalar(140, 60, 170, 10),
						thickness: 2);
				}
			}
		} 
		#endregion

	}
}
