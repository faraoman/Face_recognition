using Autofac;
using Autofac.Builder;
using Autofac.Core;

using Mallenom;
using Mallenom.AppServices;

namespace Recognizer
{
	public static class AutofacExtensions
	{
		public static IRegistrationBuilder<SerializableConfiguration<T>, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterSerializableConfiguration<T>(this ContainerBuilder containerBuilder,
			string configurationGroup, string configurationName, IParametersSerializer<T> serializer, T defaults)
			where T : class
		{
			Verify.Argument.IsNotNull(containerBuilder, nameof(containerBuilder));
			Verify.Argument.IsNeitherNullNorWhitespace(configurationGroup, nameof(configurationGroup));
			Verify.Argument.IsNeitherNullNorWhitespace(configurationName, nameof(configurationName));
			Verify.Argument.IsNotNull(serializer, nameof(serializer));

			containerBuilder
				.RegisterInstance(serializer)
				.As<IParametersSerializer<T>>()
				.ExternallyOwned();

			containerBuilder
				.RegisterInstance(defaults)
				.Named<T>("defaults")
				.ExternallyOwned();

			return containerBuilder
				.RegisterType<SerializableConfiguration<T>>()
				.WithParameter(new NamedParameter(@"configurationGroup", configurationGroup))
				.WithParameter(new NamedParameter(@"configurationName", configurationName))
				.WithParameter(new NamedParameter(@"defaults", defaults))
				.As<ISerializableConfiguration>()
				.As<IParametersSource<T>>()
				.As<IParametersProvider<T>>()
				.SingleInstance();
		}
	}

	class ConfigurationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder
				.RegisterType<ApplicationConfiguration>()
				.As<IApplicationConfiguration>()
				.SingleInstance();

			builder.Register(
				context => context
					.Resolve<IApplicationProfileService>()
					.LocalMachine
					.RegisterDirectory(@"Configuration", ApplicationProfileDirectoryUsage.Configuration))
				.Named<IApplicationProfileDirectory>(@"configuration")
				.SingleInstance();

			builder.Register(
				context => context
					.Resolve<IApplicationProfileService>()
					.LocalMachine
					.RegisterDirectory(@"Database", ApplicationProfileDirectoryUsage.Database))
				.Named<IApplicationProfileDirectory>(@"database")
				.SingleInstance();

			builder.RegisterSerializableConfiguration("Core", "TestParameters", TestParameters.Serializer, TestParameters.Defaults);

			builder
				.RegisterType<TestParametersUser>()
				.SingleInstance()
				.AsSelf();

			builder
				.RegisterType<RootApplicationConfigurationFile>()
				.As<IApplicationConfigurationFile>()
				.WithParameter(ResolvedParameter.ForNamed<IApplicationProfileDirectory>(@"configuration"))
				.WithParameter(new NamedParameter(@"fileName", "FaceRecognizer.cfg"))
				.SingleInstance();
		}
	}
}
