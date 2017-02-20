namespace Recognizer.Logs
{
	public sealed class RecognizerLogFactory
	{
		#region .ctor

		public RecognizerLogFactory(ILogConfiguration logConfiguration)
		{
			LogConfiguration = logConfiguration;
		}

		#endregion

		#region Properties

		public ILogConfiguration LogConfiguration { get; }

		#endregion

		#region Methods

		public ILog CreateLog()
		{
			return new RecognizerLogger(LogConfiguration);
		}

		#endregion
	}
}
