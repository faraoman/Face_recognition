using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Recognizer.Database
{
	public class DatabaseService
	{
		public DbContext CreateContext()
		{
			var dbConnection = new SqlServerConnectionFactory(new SqlServerConnectionConfiguration());
			
			return new DbContext(dbConnection.CreateConnection(), contextOwnsConnection: true);
		}
	}
}
