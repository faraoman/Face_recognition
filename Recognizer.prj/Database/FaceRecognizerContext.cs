using System.Data.Entity;
using Mallenom;
using Recognizer.Entities;

namespace Recognizer.Database
{
	public sealed class FaceRecognizerContext : DbContext
	{
		public FaceRecognizerContext() : base ("FaceRecognizerConnectionString")
		{
			Database.CreateIfNotExists();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Assert.IsNotNull(modelBuilder);

			var configurations = modelBuilder.Configurations;

			configurations.Add(Employees.CreateConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
