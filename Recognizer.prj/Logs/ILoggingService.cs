using Mallenom.AppServices;
using Mallenom.Diagnostics.Logs;

namespace Recognizer.Logs
{
	/// <summary>Интерфейс сервиса логгирования.</summary>
	public interface ILoggingService
	{
		/// <summary>Возвращает директорию, в которой хранятся логи.</summary>
		/// <value>Директория, в которой хранятся логи.</value>
		IApplicationProfileDirectory Directory { get; }

		/// <summary>Регистрирует аппендер для лога.</summary>
		/// <param name="appender">Регистрируемый аппендер.</param>
		/// <exception cref="ArgumentNullException"><paramref name="appender"/> == <c>null</c>.</exception>
		void RegisterAppender(IAppender appender);
		
		/// <summary>Отменяет регистрацию аппендера.</summary>
		/// <param name="appender">Аппендер, зарегистрированный ранее вызовом <see cref="RegisterAppender"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="appender"/> == <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="appender"/> не был зарегистрирован ранее.</exception>
		void UnregisterAppender(IAppender appender);
	}
}