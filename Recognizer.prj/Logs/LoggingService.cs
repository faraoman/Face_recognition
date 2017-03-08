using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mallenom;
using Mallenom.AppServices;
using Mallenom.Diagnostics.Logs;

namespace Recognizer.Logs
{
	/// <summary>Сервис логгирования.</summary>
	public class LoggingService : ILoggingService, IDisposable
	{
		#region Static

		/// <summary>Лог.</summary>
		public static readonly ILog Log = LogManager.GetLog(typeof(LoggingService));

		#endregion

		#region Data

		/// <summary>Список аппендеров, зарегистрированных через сервис.</summary>
		private readonly List<IAppender> _appenders;

		private readonly string _defaultFileName;

		#endregion

		#region .ctor

		/// <summary>Создание <see cref="LoggingService"/>.</summary>
		/// <param name="defaultFileName">Имя для файлов логгера.</param>
		/// <param name="directory">Директория для хранения логов.</param>
		/// <param name="appenders">Дополнительные аппендеры.</param>
		/// <exception cref="ArgumentNullException"><paramref name="directory"/> == <c>null</c>.</exception>
		public LoggingService(IApplicationProfileDirectory directory, params IAppender[] appenders)
		{
			Verify.Argument.IsNotNull(directory, nameof(directory));

			Directory = directory;

			_appenders = new List<IAppender>();

			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
			_defaultFileName = Path.GetFileName(assembly.Location);

			try
			{
				Initialize(appenders);
			}
			catch(Exception exc)
			{
				Log.Error("Failed to initialize logging service.", exc);
			}
		}

		/// <summary>Создание <see cref="LoggingService"/>.</summary>
		/// <param name="defaultFileName">Имя для файлов логгера.</param>
		/// <param name="directory">Директория для хранения логов.</param>
		/// <param name="appenders">Дополнительные аппендеры.</param>
		/// <exception cref="ArgumentNullException"><paramref name="directory"/> == <c>null</c>.</exception>
		public LoggingService(IApplicationProfileDirectory directory, string defaultFileName, params IAppender[] appenders)
		{
			Verify.Argument.IsNotNull(directory, nameof(directory));
			Verify.Argument.IsNeitherNullNorWhitespace(defaultFileName, nameof(defaultFileName));

			Directory = directory;

			_appenders = new List<IAppender>();
			_defaultFileName = defaultFileName;

			try
			{
				Initialize(appenders);
			}
			catch(Exception exc)
			{
				Log.Error("Failed to initialize logging service.", exc);
			}
		}

		#endregion

		#region Properties

		/// <summary>Возвращает директорию, в которой хранятся логи.</summary>
		/// <value>Директория, в которой хранятся логи.</value>
		public IApplicationProfileDirectory Directory { get; }

		#endregion

		#region Methods

		private void Initialize(IAppender[] appenders)
		{
			LogManager.GetRepository().RootLogger.EffectiveLevel = Level.All;

			if(appenders != null)
			{
				foreach(var appender in appenders)
				{
					if(appender != null)
					{
						RegisterAppender(appender);
					}
				}
			}

			Directory.EnsureExists();

			RegisterDefaultFileAppenders(_defaultFileName);
			WriteStartupMessages();
		}

		/// <summary>Регистрирует основные лог-файлы.</summary>
		private void RegisterDefaultFileAppenders(string fileName)
		{
			Verify.Argument.IsNeitherNullNorWhitespace(fileName, nameof(fileName));

			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

			// общий
			this.RegisterAppender(_appenders[0]);
		}

		/// <summary>Выводит сообщения о запуске программы.</summary>
		private static void WriteStartupMessages()
		{
			// Log.Info(Доброе утро)
		}

		/// <summary>Регистрирует аппендер для лога.</summary>
		/// <param name="appender">Регистрируемый аппендер.</param>
		/// <exception cref="ArgumentNullException"><paramref name="appender"/> == <c>null</c>.</exception>
		/// <exception cref="ObjectDisposedException">Сервис был ликвидирован.</exception>
		public void RegisterAppender(IAppender appender)
		{
			Verify.Argument.IsNotNull(appender, nameof(appender));
			Verify.State.IsNotDisposed(this, IsDisposed);

			LogManager.GetRepository().RootLogger.AddAppender(appender);
			_appenders.Add(appender);
		}

		/// <summary>Отменяет регистрацию аппендера.</summary>
		/// <param name="appender">Аппендер, зарегистрированный ранее вызовом <see cref="RegisterAppender"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="appender"/> == <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="appender"/> не был зарегистрирован ранее.</exception>
		/// <exception cref="ObjectDisposedException">Сервис был ликвидирован.</exception>
		public void UnregisterAppender(IAppender appender)
		{
			Verify.Argument.IsNotNull(appender, nameof(appender));
			Verify.State.IsNotDisposed(this, IsDisposed);

			if(!_appenders.Remove(appender))
			{
				throw new ArgumentException("Specified appender is not registered.", nameof(appender));
			}
			LogManager.GetRepository().RootLogger.RemoveAppender(appender);
			(appender as IDisposable)?.Dispose();
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
				Log.Info("Application shutdown.");
				if(_appenders != null)
				{
					var rootLogger = LogManager.GetRepository().RootLogger;
					foreach(var appender in _appenders)
					{
						try
						{
							rootLogger.RemoveAppender(appender);
							(appender as IDisposable)?.Dispose();
						}
						catch(Exception exc)
						{
							Log.Error($"Failed to remove log appender '{appender.GetType().Name}'.", exc);
						}
					}
					_appenders.Clear();
				}
				IsDisposed = true;
			}
		}

		#endregion
	}
}
