using System;
using Autofac;
using Mallenom.AppServices;

namespace Recognizer
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			var appServcies = new AppCriticalServices();
			using(var container = RegistrationServices.CreateContainer(appServcies))
			using(var appBootstrapper = container.Resolve<AppBootstrapper>())
			{
				var appConfiguration = container.Resolve<IApplicationConfiguration>();
				appConfiguration.Load();
				// Fix-me: удалите
				var tpu = container.Resolve<TestParametersUser>();
				appBootstrapper.Run();
				appConfiguration.Save();
			}	
		}
	}
}
