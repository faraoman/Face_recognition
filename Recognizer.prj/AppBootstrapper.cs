using System;
using System.Windows.Forms;
using Mallenom.Video;

namespace Recognizer
{
	public sealed class AppBootstrapper : IDisposable
	{
		IVideoSourceProvider _videoSourceProvider;
		IVideoSource _videoSource;

		public AppBootstrapper(IVideoSourceProvider videoSourceProvider, IVideoSource videoSource)
		{
			_videoSourceProvider = videoSourceProvider;
			_videoSource = videoSource;
		}

		public void Run()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(_videoSourceProvider, _videoSource));
		}

		public void Dispose()
		{
			
		}
	}
}
