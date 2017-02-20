using System;

namespace Recognizer.Logs
{
	static class Log
	{
		private static ILog _logger;
		private static ILogConfiguration _logConfiguration;

		static Log()
		{
			_logConfiguration = new RecognizerLogConfigurations();
			_logger = new RecognizerLogFactory(_logConfiguration).CreateLog();
		}

		#region Methods
		
		public static void Info(string message)
		{
			_logger.Info(message);
		}

		public static void Info(string message, Exception exception)
		{
			_logger.Info(message, exception);
		}

		public static void Error(string message)
		{
			_logger.Error(message);
		}

		public static void Error(string message, Exception exception)
		{
			_logger.Error(message, exception);
		}

		#endregion
	}
}
