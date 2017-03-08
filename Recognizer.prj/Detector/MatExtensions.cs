using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Detector
{
	/// <summary> 
	/// Класс для обработки матриц изображений, содержащий Extension-методы для <see cref="Mat"/>. 
	/// </summary>
	static class MatExtensions
	{
		/// <summary>
		/// Изменяет размер матрицы, изменяя размер изображения.
		/// </summary>
		/// <param name="src"> Обрабатываемая матрица <seealso cref="Mat"/>. </param>
		/// <param name="width"> Новая ширина изображения. </param>
		/// <param name="height"> Новая высота изображения. </param>
		/// <returns></returns>
		public static Mat ResizeImage(this Mat src, int width, int height)
		{
			return src.Resize(
				new Size(width, height),
				fx: 1.0,
				fy: 1.0,
				interpolation: Interpolation.Cubic);
		}

		/// <summary>
		/// Преобразует матрицу из цветовой палитры RGB в черно-белую.
		/// </summary>
		/// <param name="src"> Обрабатываемая матрица <seealso cref="Mat"/>. </param>
		/// <returns> Возвращает матрицу, преобразованную в черно-белую цветовую палитру. </returns>
		public static Mat ConvertToGray(this Mat src)
		{
			return src
				.Clone()
				.CvtColor(ColorConversion.RgbaToGray);
		}

		/// <summary>
		/// Рисует прямоугольник в области <paramref name="rect"/>. 
		/// </summary>
		/// <param name="src"> Матрица <seealso cref="Mat"/>, на которой рисуется прямоугольник. </param>
		/// <param name="rect"> Прямоугольная область <seealso cref="Rect"/>. </param>
		/// <returns> Возвращает обработанную матрицу с нарисованной прямоугольной областью.</returns>
		public static Mat DrawRect(this Mat src, Rect rect)
		{
			src.Rectangle(
				rect,
				color: new Scalar(50, 255, 50, 10),
				thickness: 2);

			return src;
		}

		public static double AverageBrightness(this Mat srcImgMat)
		{
			//srcImgMat.CvtColor(ColorConversion.GrayToBgr, 3);
			double brightness = 0;

			for(int i = 0; i < srcImgMat.Height; ++i)
			{
				for(int j = 0; j < srcImgMat.Width; ++j)
				{
					int b = srcImgMat.At<Vec3b>(i, j)[0],
						g = srcImgMat.At<Vec3b>(i, j)[1],
						r = srcImgMat.At<Vec3b>(i, j)[2];

					brightness += (b + g + r) / 3.0;
				}
			}

			return brightness / (srcImgMat.Height * srcImgMat.Width);
		}
	}
}
