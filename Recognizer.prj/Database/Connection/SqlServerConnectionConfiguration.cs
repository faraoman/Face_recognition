using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer.Database
{
	public sealed class SqlServerConnectionConfiguration : IConnectionStringProvider
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

		public string AttachedDbFileName { get; set; }

		public AuthType AuthType { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		#endregion

		#region Methods

		public string GetConnectionString()
		{
			var sb = new SqlConnectionStringBuilder();

			sb.InitialCatalog = DatabaseName;
			sb.DataSource = Hostname;
			sb.AttachDBFilename = AttachedDbFileName;
			sb.IntegratedSecurity = AuthType == AuthType.Windows;
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
			AttachedDbFileName = Defaults.AttachedDbFileName;
			Username = Defaults.Username;
			Password = Defaults.Password;
		}

		#endregion

		#region Defaults

		public static class Defaults
		{
			public const string DatabaseName = "FaceRecognizer";

			public const string Hostname = @"(localdb)\mssqllocaldb";

			public static readonly string AttachedDbFileName = Path.Combine(Services.DatabaseDirectory.FullPath, "FaceRecognizer.mdf");

			public static readonly AuthType AuthType = AuthType.Windows;

			public const string Username = "";

			public const string Password = "";
		}
		#endregion
	}

	public enum AuthType : int
	{
		Windows = 0,
		Server = 1
	}
}
