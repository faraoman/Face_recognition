using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer.Database
{
	public class SqlServerConnectionConfiguration : IConnectionStringProvider
	{
		#region .ctor

		public SqlServerConnectionConfiguration()
		{
			SetDefaults();
		}

		#endregion

		#region Properties

		public string DatabaseName { get; set; }

		public string Hostname { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		#endregion

		#region Methods

		public string GetConnectionString()
		{
			var sb = new SqlConnectionStringBuilder();

			sb.InitialCatalog = DatabaseName;
			sb.DataSource = Hostname;
			sb.UserID = Username;
			sb.Password = Password;

			return sb.ToString();
		}

		#endregion

		#region Config

		public void SetDefaults()
		{
			DatabaseName = Defaults.DatabaseName;
			Hostname = Defaults.Hostname;
			Username = Defaults.Username;
			Password = Defaults.Password;
		}

		public void Save()
		{

		}

		public void Load()
		{

		}

		#endregion

		#region Defaults

		public static class Defaults
		{
			public const string DatabaseName = "Emloyees";

			public const string Hostname = @"localhost\SQLServer";

			//public static readonly AuthType AuthType = AuthType.Windows;

			public const string Username = "";

			public const string Password = "";
		}

		#endregion
	}
}
