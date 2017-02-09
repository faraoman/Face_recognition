﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Mallenom;

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

			containerBuilder.RegisterModule<ConfigurationModule>();

			return containerBuilder.Build();
		}
	}
}