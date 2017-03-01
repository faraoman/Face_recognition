using System.Data.Entity;
using Autofac;

using Recognizer.Database.Data;
using Recognizer.Entities;

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
				.RegisterType<RecognizerContextInitializer>()
				.As<IDatabaseInitializer<RecognizerContext>>();

			builder
				.RegisterType<SqlServerConnectionConfiguration>()
				.As<IConnectionStringProvider>();

			builder
				.RegisterType<Employee>()
				.AsSelf();

			builder
				.RegisterType<EmployeesLogRepository>()
				.AsSelf();

			builder
				.RegisterType<EmployeesLogRepositoryFilter>()
				.AsSelf();

			builder
				.RegisterType<DbContextFactory>()
				.As<IDbContextFactory>()
				//.OnActivating(c =>
				//{
				//	using(var context = c.Instance.CreateContext())
				//	{
				//		context.Database.CreateIfNotExists();
				//	}
				//})
				.SingleInstance();

			builder
				.RegisterType<EmployeesLogRepository>()
				.AsSelf()
				.SingleInstance();
		}
	}
}
