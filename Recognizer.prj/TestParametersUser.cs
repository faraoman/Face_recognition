using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mallenom.AppServices;

namespace Recognizer
{
	class TestParametersUser
	{
		public TestParametersUser(IParametersSource<TestParameters> testParametersSource, IParametersProvider<TestParameters> testParametersProvider)
		{
			testParametersSource.Parameters = new TestParameters(testParameter1: "new value");
		}
	}
}
