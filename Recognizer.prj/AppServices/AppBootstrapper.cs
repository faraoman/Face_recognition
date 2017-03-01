using System;
using System.Windows.Forms;
using Autofac;
using Mallenom.Video;
using Recognizer.AppServices;
using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public sealed class AppBootstrapper : IAppBootstrapper
	{
		IVideoSourceProvider _videoSourceProvider;
		IVideoSource _videoSource;

		public AppBootstrapper(IVideoSourceProvider videoSourceProvider, IVideoSource videoSource)
		{
			_videoSourceProvider = videoSourceProvider;
			_videoSource = videoSource;
		}

		public IContainer Container { get; set; }

		public void Run()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(Container));
		}

		public void Dispose()
		{

		}
	}
}
