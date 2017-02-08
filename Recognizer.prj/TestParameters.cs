using Mallenom;
using Mallenom.AppServices;
using Mallenom.Storage;

namespace Recognizer
{
	class TestParameters
	{
		sealed class TestParametersSerializer : IParametersSerializer<TestParameters>
		{
			public TestParameters Load(IObjectStorageReader reader)
			{
				return new TestParameters(reader.TryReadParameter("TestParameter1", "Value", Defaults.TestParameter1));
			}

			public void Save(TestParameters parameters, IObjectStorageWriter writer)
			{
				writer.WriteParameter("TestParameter1", "Value", parameters.TestParameter1);
			}
		}

		public TestParameters(string testParameter1)
		{
			Verify.Argument.IsNotNull(testParameter1, nameof(testParameter1));

			TestParameter1 = testParameter1;
		}

		public static TestParameters Defaults = new TestParameters("TestValue");

		public static IParametersSerializer<TestParameters> Serializer = new TestParametersSerializer();

		public string TestParameter1 { get; }
	}
}
