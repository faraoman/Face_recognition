using System;
using Mallenom;
using Mallenom.AppServices;

namespace Recognizer.Logs
{
	/// <summary>Сервис логгирования.</summary>
	public class LoggingService : ILoggingService, IDisposable
	{
		#region Static

		/// <summary>Лог.</summary>
		public static readonly ILog Log = DefaultLogger.Log;

		#endregion

		#region .ctor

		/// <summary>Создание <see cref="LoggingService"/>.</summary>
		/// <param name="defaultFileName">Имя для файлов логгера.</param>
		/// <param name="directory">Директория для хранения логов.</param>
		/// <param name="appenders">Дополнительные аппендеры.</param>
		/// <exception cref="ArgumentNullException"><paramref name="directory"/> == <c>null</c>.</exception>
		public LoggingService(IApplicationProfileDirectory directory)
		{
			Verify.Argument.IsNotNull(directory, nameof(directory));

			Directory = directory;

			Initialize();
		}

		#endregion

		#region Properties

		/// <summary>Возвращает директорию, в которой хранятся логи.</summary>
		/// <value>Директория, в которой хранятся логи.</value>
		public IApplicationProfileDirectory Directory { get; }

		#endregion

		#region Methods

		private void Initialize()
		{
			Directory.EnsureExists();
			WriteStartupMessages();
		}

		/// <summary>Выводит сообщения о запуске программы.</summary>
		private static void WriteStartupMessages()
		{
			Log.Info("Application started");
		}

		#endregion

		#region IDisposable Members

		/// <summary>Возвращает флаг ликвидированного объекта.</summary>
		/// <value><c>true</c>, если объект ликвидирован, иначе - <c>false</c>.</value>
		public bool IsDisposed { get; private set; }

		/// <summary>Освобождает ресурсы сервиса логгирования.</summary>
		public void Dispose()
		{
			if(!IsDisposed)
			{
				Log.Info("Application shutdown");

				IsDisposed = true;
			}
		}

		#endregion
	}
}
