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

		IAppender[] appenders = new Appender[]
		{
			new LogViewAppender(102400, "logView")
		};

		public AppCriticalServices()
		{
			_applicationProfileService = Services.ApplicationProfileService;
			_logsDirectory = DefaultLogger.LogDirectory;
			_dbDirectory = Services.DatabaseDirectory;

			_loggingService = new LoggingService(_logsDirectory, "logs.log", appenders);
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
				.AsSelf();

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
