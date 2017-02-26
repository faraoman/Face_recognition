using System;
using System.IO;
using Mallenom.AppServices;

namespace Recognizer.Logs
{
	public sealed class Logger : ILog
	{

		private readonly string _defaultFileName = "logs.log";
		private string _fileName;

		#region .ctor

		public Logger(IApplicationProfileDirectory directory)
		{
			Directory = directory;
			_fileName = _defaultFileName;
			FullPath = Path.Combine(Directory.FullPath, _defaultFileName);
		}

		public Logger(IApplicationProfileDirectory directory, string fileName)
		{
			Directory = directory;
			_fileName = fileName;
			FullPath = Path.Combine(Directory.FullPath, fileName);
		}
		#endregion

		#region Properties

		IApplicationProfileDirectory Directory { get; set; }
		string FullPath { get; }
		
		#endregion

		#region Methods

		public void Info(string message)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][INFO: \"{message}\".]");
			}
		}

		public void Info(string message, Exception exception)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][INFO \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Error(string message)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][ERROR: \"{message}\".]");
			}
		}

		public void Error(string message, Exception exception)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][ERROR: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Warning(string message)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][WARNING: \"{message}\".]");
			}
		}

		public void Warning(string message, Exception exception)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][WARNING: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}

		public void Fatal(string message)
		{
			DateTime now = DateTime.Now;
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][FATAL: \"{message}\".]");
			}
		}

		public void Fatal(string message, Exception exception)
		{
			using(StreamWriter sw = File.AppendText(FullPath))
			{
				sw.WriteLine($"[Time: {DateTime.Now}][FATAL: \"{message}\".][Description: \"{exception.Message}\" in method \"{exception.TargetSite}\".]");
			}
		}
		#endregion
	}
}
