using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mallenom.AppServices;
using Mallenom.Storage;
using Mallenom.Video;

namespace Recognizer.Configuration
{
	class VideoSourceConfiguration : ISerializableConfiguration
	{
		private readonly IVideoSource _videoSource;
		private readonly IVideoSourceConfiguration _configuration;

		public VideoSourceConfiguration(IVideoSource videoSource, IVideoSourceConfiguration configuration)
		{
			_videoSource = videoSource;
			_configuration = configuration;
		}

		public string ConfigurationGroup => "Video";

		public string ConfigurationName => "VideoSourceConfiguration";

		public void Load(IObjectStorageReader reader)
		{
			_configuration.LoadVideoSourceConfiguration(_videoSource, reader);
		}

		public void Save(IObjectStorageWriter writer)
		{
			_configuration.SaveVideoSourceConfiguration(_videoSource, writer);
		}

		public void SetDefaults()
		{	
		}
	}
}
