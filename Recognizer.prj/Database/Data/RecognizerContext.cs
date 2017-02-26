using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mallenom;
using Recognizer.Entities;

namespace Recognizer.Database
{
	public sealed class RecognizerContext : DbContext
	{

		public RecognizerContext()
			: base("FaceRecognizer")
		{
			System
				.Data
				.Entity
				.Database
				.SetInitializer(new RecognizerInitializer());

			Database.CreateIfNotExists();
		}

		public RecognizerContext(IDbConnectionFactory connectionFactory, bool contextOwnsConnection = true)
			: base (connectionFactory.CreateConnection(), contextOwnsConnection)
		{
			System
				.Data
				.Entity
				.Database
				.SetInitializer(new RecognizerInitializer());

			Database.CreateIfNotExists();
		}

		//public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Assert.IsNotNull(modelBuilder);

			modelBuilder
				.Configurations
				.Add(Employee.CreateConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
