using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Mallenom;
using Mallenom.AppServices;

namespace Recognizer
{
	public sealed class AppCriticalServices
	{
		private readonly IApplicationProfileService _applicationProfileService;
		private readonly IApplicationProfileDirectory _logsDirectory;

		public AppCriticalServices()
		{
			_applicationProfileService = new ApplicationProfileService("Mallenom", "FaceRecognizer");
			_logsDirectory = _applicationProfileService.LocalMachine.RegisterDirectory(@"Logs", ApplicationProfileDirectoryUsage.Logs);
		}

		public void Register(ContainerBuilder builder)
		{
			Assert.IsNotNull(builder);

			builder
				.RegisterInstance(_applicationProfileService)
				.As<IApplicationProfileService>()
				.SingleInstance();

			builder.RegisterInstance(_logsDirectory).Named<IApplicationProfileDirectory>("logs").ExternallyOwned();
		}
	}
}
