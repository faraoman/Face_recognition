using Mallenom.AppServices;

namespace Recognizer.Logs
{
	public static class DefaultLogger
	{
		static DefaultLogger()
		{

			LogDirectory = Services
				.ApplicationProfileService
				.LocalMachine
				.RegisterDirectory(@"Logs", ApplicationProfileDirectoryUsage.Logs);

			Log = new Logger(LogDirectory, "logs.log");
		}

		public static IApplicationProfileDirectory LogDirectory { get; }
		public static ILog Log { get; }

		//public static ILog GetLog()
		//{
		//	return Log;
		//}
	}
}
