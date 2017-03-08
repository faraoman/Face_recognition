using System;
using System.Windows.Forms;
using Autofac;
using Recognizer.AppServices;

namespace Recognizer
{
	public sealed class AppBootstrapper : IAppBootstrapper
	{
		public AppBootstrapper()
		{
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
