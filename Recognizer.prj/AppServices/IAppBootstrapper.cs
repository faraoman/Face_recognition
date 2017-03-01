using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Recognizer.AppServices
{
	public interface IAppBootstrapper : IDisposable
	{
		void Run();

		IContainer Container { get; set; }
	}
}
