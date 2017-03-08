using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Mallenom;
using Mallenom.Diagnostics.Logs;
using Mallenom.Imaging;
using Mallenom.Video;
using Recognizer.Detector;
using Recognizer.Recognition;

namespace Recognizer
{
	public partial class AddNewEmployeeForm : Form
	{
		#region Data

		private IComponentContext _container;

		private IVideoSourceProvider _videoSourceProvider;
		private IVideoSource _videoSource;
		private IImageMatrix _matrix;

		private FaceDetector _detector;
		private LBPFaceRecognizer _recognizer;

		//private RecognitionLogController _recognitionLogController;

		private int _frameCounter;
		private int _skipFrames = 5;

		#endregion


		public AddNewEmployeeForm()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}


		#region .ctor
		public AddNewEmployeeForm(IComponentContext container)
		{
			Verify.Argument.IsNotNull(container, nameof(container));

			InitializeComponent();
			this.FormClosing += OnMainFormClosing;

			_container = container;

			// fixme
			//var loggingService = _container.Resolve<ILoggingService>();
			Log = LogManager.GetLog(typeof(LoggingService));

			InitializeRecognizer();
			InitializeVideoSource();

			_logView.Appender = new LogViewAppender();
			_logView.Appender.DoAppend(new LogEvent(Level.Info, "Hello, bitch", new Exception(), DateTime.Now, "root"));

			//_recognitionLogController = container.Resolve<RecognitionLogController>();
			//_recognitionLogController.DataGridView = dataGridView1;
		}
		#endregion
	}
}
