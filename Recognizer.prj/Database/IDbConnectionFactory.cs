using System.Data.Common;

namespace Recognizer.Database
{
	public interface IDbConnectionFactory
	{
		DbConnection CreateConnection();
	}
}
