using System;
using System.IO;
using Mallenom.AppServices;

namespace Recognizer.Logs
{
	public sealed class RecognizerLogger : ILog
	{
		#region .ctor

		public RecognizerLogger(IApplicationProfileDirectory directory)
		{
			Directory = directory;
		}
		#endregion

		#region Properties

		IApplicationProfileDirectory Directory { get; set; }
		#endregion

		#region Methods

		private void LogFileBuilder()
		{
			if (!File.Exists(Configuration.FilePath))
			{
				File
					.Create(Configuration.FilePath)
					.Close();
			}
			
			if(Configuration.FileInfo.Length > Configuration.FileSize)
			{
				string newFile = $@"{Configuration.Directory}\{DateTime.Now.ToString("ddMMyyHHmmss")}.old";

				if(File.Exists(newFile))
				{
					File.Delete(newFile);
				}
				else
				{
					File.Move(Configuration.FilePath, newFile);
					File.SetCreationTime(newFile, DateTime.Now);
				}
			}
		}

		public void Info(string message)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][INFO: \"{message}\".]");
			}
		}

		public void Info(string message, Exception exception)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][INFO \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Error(string message)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][ERROR: \"{message}\".]");
			}
		}

		public void Error(string message, Exception exception)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][ERROR: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Warning(string message)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][WARNING: \"{message}\".]");
			}
		}

		public void Warning(string message, Exception exception)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][WARNING: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Fatal(string message)
		{
			LogFileBuilder();
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][FATAL: \"{message}\".]");
			}
		}

		public void Fatal(string message, Exception exception)
		{
			using(StreamWriter sw = File.AppendText(Configuration.FilePath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][FATAL: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}
		#endregion
	}
}
