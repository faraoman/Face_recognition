using System;
using System.IO;

namespace Recognizer.Logs
{
	public sealed class RecognizerLogConfigurations : ILogConfiguration
	{

		#region .ctor

		public RecognizerLogConfigurations()
		{
			SetDefaults();
		}
		#endregion

		#region Properties

		/// <summary>Путь к файлу лога.</summary>
		public string FilePath { get; set; }

		/// <summary>Путь к папке с логами.</summary>
		public string Directory { get; set; }

		/// <summary>Информация о каталоге.</summary> 
		public DirectoryInfo DirectoryInfo { get; set; }

		/// <summary>Информация о файле.</summary>
		public FileInfo FileInfo { get; set; }

		public int FileSize { get; set; }
		#endregion

		#region Methods

		public void SetDefaults()
		{
			Directory = DefaultLogConfiguration.Directory;
			FilePath = DefaultLogConfiguration.FilePath;
			DirectoryInfo = DefaultLogConfiguration.DirectoryInfo;
			FileInfo = DefaultLogConfiguration.FileInfo;
			FileSize = DefaultLogConfiguration.FileSize;
		} 
		#endregion

		#region Config

		/// <summary> Конфигурация лога по умолчанию. </summary>
		public class DefaultLogConfiguration
		{
			public static readonly string Directory = Path.Combine(
				Environment.GetFolderPath(
					Environment
					.SpecialFolder
					.CommonApplicationData),
				@"Mallenom\FaceRecognizer\Logs");

			public static readonly string FilePath = Path.Combine(Directory, "logs.log");

			public static readonly DirectoryInfo DirectoryInfo = new DirectoryInfo(Directory);

			public static readonly FileInfo FileInfo = new FileInfo(FilePath);

			public static readonly int FileSize = 102400;
		} 
		#endregion
	}
}
