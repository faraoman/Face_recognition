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
		}

		public static IApplicationProfileDirectory LogDirectory { get; }
	}
}
