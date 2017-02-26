using System.Data.Entity;
using Autofac;

namespace Recognizer.Database
{
	class DatabaseModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder
				.RegisterType<SqlServerConnectionFactory>()
				.As<IDbConnectionFactory>();

			builder
				.RegisterType<RecognizerContext>()
				.As<DbContext>();

			builder
				.RegisterType<SqlServerConnectionConfiguration>()
				.As<IConnectionStringProvider>();
		}
	}
}
