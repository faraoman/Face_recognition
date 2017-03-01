using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mallenom.Imaging;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Detector
{
	public static class ImageMatrixExtensions
	{
		public static Mat ToMat(this IImageMatrix colorMatrix)
		{
			var result = new Mat(new Size(colorMatrix.Width, colorMatrix.Height), MatType.CV_8UC3);
			using(var matrixData = colorMatrix.LockData())
			{
				Native
					.Kernel32
					.CopyMemory(result.Data, matrixData.Scan0, matrixData.Stride * matrixData.Height);
			}
			return result;
		}
	}
}
