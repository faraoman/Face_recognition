using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mallenom.AppServices;
using Recognizer.Database;
using Recognizer.Logs;

namespace Recognizer
{
	public static class Services
	{
		static Services()
		{
			ApplicationProfileService = new ApplicationProfileService("Mallenom", "FaceRecognizer");
			LogDirectory = DefaultLogger.LogDirectory;
			LoggingService = new LoggingService(LogDirectory);

			DatabaseDirectory = ApplicationProfileService
				.LocalMachine
				.RegisterDirectory("Database", ApplicationProfileDirectoryUsage.Database);

			DatabaseDirectory.EnsureExists();

			DatabaseService = new DatabaseService(new SqlServerConnectionFactory(new SqlServerConnectionConfiguration()));
		}

		public static IApplicationProfileService ApplicationProfileService { get; }
		public static IApplicationProfileDirectory LogDirectory { get; }
		public static IApplicationProfileDirectory DatabaseDirectory { get; }

		public static LoggingService LoggingService { get; }
		public static DatabaseService DatabaseService { get; }
	}
}
