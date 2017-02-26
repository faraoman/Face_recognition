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
	}
}