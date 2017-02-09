using System;
using System.Windows.Forms;

namespace Recognizer
{
	public sealed class AppBootstrapper : IDisposable
	{
		public void Run()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		public void Dispose()
		{
			
		}
	}
}
