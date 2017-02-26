using Autofac;
using Mallenom;
using Mallenom.AppServices;
using Recognizer.Logs;

namespace Recognizer
{
	public sealed class AppCriticalServices
	{
		private readonly IApplicationProfileService _applicationProfileService;
		private readonly IApplicationProfileDirectory _logsDirectory;
		private readonly IApplicationProfileDirectory _dbDirectory;

		private readonly LoggingService _logService;

		public AppCriticalServices()
		{
			_applicationProfileService = Services.ApplicationProfileService;
			_logsDirectory = DefaultLogger.LogDirectory;
			_dbDirectory = Services.DatabaseDirectory;

			_logService = new LoggingService(_logsDirectory);
		}

		public void Register(ContainerBuilder builder)
		{
			Assert.IsNotNull(builder);

			builder
				.RegisterInstance(_applicationProfileService)
				.As<IApplicationProfileService>()
				.SingleInstance();

			builder
				.RegisterInstance(_logService)
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
