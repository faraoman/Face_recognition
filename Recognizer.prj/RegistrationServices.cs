using Autofac;
using Mallenom;
using Recognizer.Database;

namespace Recognizer
{
	public static class RegistrationServices
	{
		public static IContainer CreateContainer(AppCriticalServices appCriticalServices)
		{
			Verify.Argument.IsNotNull(appCriticalServices, nameof(appCriticalServices));
			
			var containerBuilder = new ContainerBuilder();

			appCriticalServices.Register(containerBuilder);

			containerBuilder
				.RegisterType<AppBootstrapper>()
				.SingleInstance()
				.AsSelf();

			containerBuilder
				.RegisterType<DatabaseService>()
				.SingleInstance()
				.AsSelf();

			containerBuilder.RegisterModule<ConfigurationModule>();
			containerBuilder.RegisterModule<DatabaseModule>();

			return containerBuilder.Build();
		}
	}
}
