using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using Mallenom;

namespace Recognizer.Database
{
	public sealed class DatabaseService
	{
		public DatabaseService(IDbConnectionFactory dbConnectionFactory)
		{
			Verify.Argument.IsNotNull(dbConnectionFactory, nameof(dbConnectionFactory));

			DbConnectionFactory = dbConnectionFactory;
		}

		public IDbConnectionFactory DbConnectionFactory { get; }

		public DbContext CreateContext()
		{	
			return new DbContext(DbConnectionFactory.CreateConnection(), contextOwnsConnection: true);
		}
	}
}
