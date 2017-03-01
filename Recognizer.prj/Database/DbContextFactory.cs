using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mallenom;

namespace Recognizer.Database
{
	class DbContextFactory : IDbContextFactory
	{
		public DbContextFactory(IDbConnectionFactory dbConnectionFactory)
		{
			Verify.Argument.IsNotNull(dbConnectionFactory, nameof(dbConnectionFactory));

			DbConnectionFactory = dbConnectionFactory;
		}

		[NotNull]
		private IDbConnectionFactory DbConnectionFactory { get; }

		public DbContext CreateContext() => new RecognizerContext(DbConnectionFactory.CreateConnection(), true);

		public Task<DbContext> CreateContextAsync() => Task.Run(() => CreateContext());
	}
}
