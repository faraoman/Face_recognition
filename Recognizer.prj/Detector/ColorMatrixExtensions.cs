using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mallenom.Imaging;
using OpenCvSharp.CPlusPlus;

namespace Recognizer.Detector
{
	public static class ColorMatrixExtensions
	{
		public static Mat ToMat(this ColorMatrix colorMatrix)
		{
			var result = new Mat(new Size(colorMatrix.Width, colorMatrix.Height), MatType.CV_8UC3);
			using(var matrixData = colorMatrix.LockData())
			{
				Native
					.Kernel32
					.CopyMemory(matrixData.Scan0, result.Data, matrixData.Stride * matrixData.Height);
			}
			return result;
		}
	}
}
