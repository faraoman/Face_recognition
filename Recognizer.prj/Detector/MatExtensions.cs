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
	}
}
