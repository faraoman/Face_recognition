using System.IO;

namespace Recognizer.Logs
{
	public interface ILogConfiguration
	{
		#region Properties

		/// <summary>Путь к файлу лога.</summary>
		string FilePath { get; set; }

		/// <summary>Путь к папке с логами.</summary>
		string Directory { get; set; }

		/// <summary>Информация о каталоге.</summary> 
		DirectoryInfo DirectoryInfo { get; set; }

		/// <summary>Информация о файле.</summary>
		FileInfo FileInfo { get; set; }

		/// <summary>Размер файла лога в байтах.</summary>
		int FileSize { get; set; }
		#endregion
	}
}
