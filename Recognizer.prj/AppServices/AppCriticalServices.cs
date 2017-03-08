using System.IO;
using Autofac;
using Mallenom;
using Mallenom.AppServices;
using Mallenom.Diagnostics.Logs;
using Recognizer.Logs;

namespace Recognizer
{
	public sealed class AppCriticalServices
	{
		private readonly IApplicationProfileService _applicationProfileService;
		private readonly IApplicationProfileDirectory _logsDirectory;
		private readonly IApplicationProfileDirectory _dbDirectory;

		private readonly ILoggingService _loggingService;
		private readonly ILog _log;

		private const string DefaultFileName = "logs.log";

		public AppCriticalServices()
		{
			_applicationProfileService = Services.ApplicationProfileService;
			_logsDirectory = DefaultLogger.LogDirectory;
			_dbDirectory = Services.DatabaseDirectory;

			var fileAppender = new FileAppender(Path.Combine(_logsDirectory.FullPath, DefaultFileName))
			{
				MaxFileCount = 4,
				MaxFileSize = 1024 * 1024 * 10,
				Layout = new FileLayout()
			};

			var logViewAppender = new LogViewAppender(1024 * 1024 * 10, "LogViewApender");

			_loggingService = new LoggingService(_logsDirectory, DefaultFileName, fileAppender, logViewAppender);

			_log = LogManager.GetLog(typeof(LoggingService));
		}

		public void Register(ContainerBuilder builder)
		{
			Assert.IsNotNull(builder);

			builder
				.RegisterInstance(_applicationProfileService)
				.As<IApplicationProfileService>()
				.SingleInstance();

			builder
				.RegisterInstance(_loggingService)
				.As<ILoggingService>();

			builder
				.RegisterInstance(_log)
				.As<ILog>();

			builder
				.RegisterInstance(_logsDirectory)
				.Named<IApplicationProfileDirectory>("logs")
				.ExternallyOwned();

			builder
				.RegisterInstance(_dbDirectory)
				.Named<IApplicationProfileDirectory>("database")
				.ExternallyOwned();
		}
	}
}
