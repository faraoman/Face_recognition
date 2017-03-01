using Autofac;
using Mallenom;
using Recognizer.Database;

using Mallenom.Video;
using Recognizer.Configuration;
using Mallenom.AppServices;
using Recognizer.Logs;
using Recognizer.AppServices;
using Recognizer.Detector;
using Recognizer.Recognition;
using Mallenom.Imaging;

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
				.RegisterType<LoggingService>()
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

			//var videoSourceProvider = new IPCameraSourceProvider();
			//var configuration = videoSourceProvider.TryGetConfiguration();
			//var videoSource = videoSourceProvider.CreateVideoSource();

			containerBuilder
				.RegisterType<Mallenom.Video.FFmpeg.FFmpegVideoSourceProvider>()
				.As<IVideoSourceProvider>()
				.SingleInstance();

			//containerBuilder
			//	.RegisterInstance(new VideoSourceConfiguration(videoSource, configuration))
			//	.As<ISerializableConfiguration>();

			//containerBuilder
			//	.RegisterInstance(videoSource)
			//	.As<IVideoSource>();

			containerBuilder
				.RegisterType<RecognitionLogController>()
				.AsSelf();

			containerBuilder
				.RegisterAssemblyModules(typeof(RegistrationServices).Assembly);

			return containerBuilder.Build();
		}
	}
}
