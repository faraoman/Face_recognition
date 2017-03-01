using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer.Database
{
	public interface IDbContextFactory
	{
		DbContext CreateContext();

		Task<DbContext> CreateContextAsync();
	}
}
