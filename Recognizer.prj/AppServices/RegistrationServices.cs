using Autofac;
using Mallenom;
using Mallenom.Diagnostics.Logs;
using Mallenom.Imaging;
using Mallenom.Video;
using Mallenom.Video.DirectShow;

using Recognizer.AppServices;
using Recognizer.Database;
using Recognizer.Detector;
using Recognizer.Logs;
using Recognizer.Recognition;

namespace Recognizer
{
	public static class RegistrationServices
	{
		public static IContainer CreateContainer(AppCriticalServices appCriticalServices)
		{
			Verify.Argument.IsNotNull(appCriticalServices, nameof(appCriticalServices));
			
			var containerBuilder = new ContainerBuilder();

			appCriticalServices.Register(containerBuilder);

			containerBuilder
				.RegisterType<AppBootstrapper>()
				.SingleInstance()
				.As<IAppBootstrapper>();

			containerBuilder
				.RegisterType<DatabaseService>()
				.SingleInstance()
				.AsSelf();

			containerBuilder
				.RegisterType<FaceDetector>()
				.SingleInstance()
				.AsSelf();

			containerBuilder
				.RegisterType<LBPFaceRecognizer>()
				.SingleInstance()
				.AsSelf();

			containerBuilder
				.RegisterType<FrameImage>()
				.SingleInstance()
				.As<VideoImage>();

			// webcam source
			/**/
			containerBuilder
				.RegisterType<DXCaptureSourceProvider>()
				.As<IVideoSourceProvider>()
				.SingleInstance();
			/**/

			containerBuilder
				.RegisterType<RecognitionLogController>()
				.AsSelf();

			containerBuilder
				.RegisterAssemblyModules(typeof(RegistrationServices).Assembly);

			return containerBuilder.Build();
		}
	}
}
