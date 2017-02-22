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

		private readonly RecognizerLogger _logger;

		public AppCriticalServices()
		{
			_applicationProfileService = new ApplicationProfileService("Mallenom", "FaceRecognizer");
			_logsDirectory = _applicationProfileService
				.LocalMachine
				.RegisterDirectory(@"Logs", ApplicationProfileDirectoryUsage.Logs);

			_logsDirectory.EnsureExists();

			var logger = new RecognizerLogger(_logsDirectory);
		}

		public void Register(ContainerBuilder builder)
		{
			Assert.IsNotNull(builder);

			builder
				.RegisterInstance(_applicationProfileService)
				.As<IApplicationProfileService>()
				.SingleInstance();

			builder
				.RegisterInstance(_logger)
				.AsSelf();

			builder
				.RegisterInstance(_logsDirectory)
				.Named<IApplicationProfileDirectory>("logs")
				.ExternallyOwned();
		}
	}
}
