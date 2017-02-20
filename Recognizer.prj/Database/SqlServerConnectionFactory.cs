using System.Data.Common;
using System.Data.SqlClient;

namespace Recognizer.Database
{
	public sealed class SqlServerConnectionFactory : IDbConnectionFactory
	{
		#region .ctor

		public SqlServerConnectionFactory(IConnectionStringProvider connectionStringProvider)
		{
			ConnectionStringProvider = connectionStringProvider;
		}

		#endregion

		#region Properties

		public IConnectionStringProvider ConnectionStringProvider { get; }

		#endregion

		#region Methods

		public DbConnection CreateConnection()
		{
			return new SqlConnection(ConnectionStringProvider.GetConnectionString());
		} 

		#endregion
	}
}
