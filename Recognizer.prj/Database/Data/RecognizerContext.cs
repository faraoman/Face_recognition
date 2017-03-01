using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mallenom;
using Recognizer.Entities;

namespace Recognizer.Database
{
	public sealed class RecognizerContext : DbContext
	{
		static RecognizerContext()
		{
			System
				.Data
				.Entity
				.Database
				.SetInitializer(new RecognizerContextInitializer());
		}

		public RecognizerContext(DbConnection dbConnection, bool contextOwnsConnection = true)
			: base (dbConnection, contextOwnsConnection)
		{
		}

		public DbSet<Employee> Employees { get; set; }

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
