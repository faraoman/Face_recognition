using System.Collections.Generic;
using System.IO;
using Mallenom.Imaging;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;

namespace Recognizer.Detector
{
	public sealed class FaceDetector
	{
		#region Constants
		private const int IMG_WIDTH = 300;
		private const int IMG_HEIGHT = 300; 
		#endregion

		#region .ctor
		/// <summary> 
		/// Создаёт объект <see cref="FaceDetector"/>
		/// </summary>
		/// <param name="imagePath"> Путь к файлу с изображением. </param>
		public FaceDetector(string imagePath)
		{
			//current
			//если будет использоваться, то убрать COPY в названии создаваемого файла
			var xmlfile_string = Properties.Resources.haarcascade_frontalface_default;

			Directory.CreateDirectory(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples"));

			File.WriteAllText(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"haarcascade_frontalface_defaultCOPY.xml"), xmlfile_string);
			//current


			//удалить строку снизу, удалить слеши комментария
			Classifier = new CascadeClassifier(
				Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					//"haarcascade_frontalface_default.xml"));
					"haarcascade_frontalface_defaultCOPY.xml"));

			InputMatrix = new Mat(
				imagePath,
				LoadMode.AnyColor);

			OutputMatrix = InputMatrix.Clone();

			FacesRepository = new List<Mat>();
		}


		//
		/**/

		/// <summary> 
		/// Создаёт объект <see cref="FaceDetector"/>
		/// </summary>
		/// <param name="colorMatrix"> Цветная матрица изображения. </param>
		public FaceDetector(ColorMatrix colorMatrix)
		{
			Classifier = new CascadeClassifier(
				Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"haarcascade_frontalface_default.xml"));

			//getting bitmap from colorMatrix
			System.Drawing.Bitmap bitmap = colorMatrix.CreateBitmap();

			//converting System.Drawing.Bitmap -> OpenCVSharp.CPlusPlus.Mat
			Mat mat = BitmapConverter.ToMat(bitmap);

			InputMatrix = mat;
			OutputMatrix = InputMatrix.Clone();

			FacesRepository = new List<Mat>();
		}


		/// <summary> 
		/// Создаёт объект <see cref="FaceDetector"/>
		/// </summary>
		/// <param name="videoImage"> Изображение. </param>
		public FaceDetector(VideoImage videoImage)
		{
			Classifier = new CascadeClassifier(
				Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"haarcascade_frontalface_default.xml"));

			//creating bitmap
			System.Drawing.Bitmap bitmap = videoImage.Matrix.CreateBitmap();

			//converting System.Drawing.Bitmap -> OpenCVSharp.CPlusPlus.Mat
			if(bitmap != null) { 
				Mat mat = BitmapConverter.ToMat(bitmap);
				InputMatrix = mat;
				OutputMatrix = InputMatrix.Clone();
			}

			FacesRepository = new List<Mat>();
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
		public Mat InputMatrix { get; set; }

		/// <summary> 
		/// Изображение с распознанными лицами. 
		/// </summary>
		public Mat OutputMatrix { get; set; }

		/// <summary> 
		/// Массив прямоугольных областей <seealso cref="Rect"/>, внутри которых обнаружены лица.
		/// </summary>
		public Rect[] DetectedFaces { get; set; }

		/// <summary>
		/// Список распознанных лиц
		/// </summary>
		public List<Mat> FacesRepository { get; set; }

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
				image: InputMatrix,
				scaleFactor: 1.35,
				minNeighbors: 5,
				flags: HaarDetectionType.ScaleImage,
				minSize: new Size(30, 30),
				maxSize: InputMatrix.Size());


			foreach(var faceRect in DetectedFaces)
			{
				Mat face = new Mat(InputMatrix, roi: faceRect)
					.ResizeImage(IMG_WIDTH, IMG_HEIGHT)
					.ConvertToGray();

				FacesRepository.Add(face);
			}			

			return DetectedFaces;
		}
	
		/// <summary>
		/// Рисует найденные прямоугольники <seealso cref="Rect"/>. 
		/// </summary>
		public void DrawFaceRectangles()
		{
			if(OutputMatrix != null)
			{
				foreach(var faceRectangle in DetectedFaces)
				{
					OutputMatrix.DrawRect(faceRectangle);
				}
			}
		}

		/// <summary>
		/// Обновляет свойства InputMatrix и OutputMatrix новыми значениями, полученными из bitmap. 
		/// </summary>
		public void UpdateImages(System.Drawing.Bitmap bitmap)
		{
			if(bitmap != null)
			{
				Mat mat = BitmapConverter.ToMat(bitmap);

				InputMatrix = mat;
				OutputMatrix = InputMatrix.Clone();
			}
		}

		/// <summary>
		/// Обновляет свойства InputMatrix и OutputMatrix новыми значениями, полученными из colorMatrix. 
		/// </summary>
		public void UpdateImages(ColorMatrix colorMatrix)
		{
			if(colorMatrix != null)
			{
				InputMatrix = ColorMatrixExtensions.ToMat(colorMatrix);
				OutputMatrix = InputMatrix.Clone();
			}
		}
		#endregion

	}
}
