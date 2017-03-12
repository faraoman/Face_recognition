using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Mallenom;
using Mallenom.Diagnostics.Logs;
using Mallenom.Imaging;
using Mallenom.Video;
using Mallenom.Video.DirectShow;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using Recognizer.Database.Data;
using Recognizer.Detector;
using Recognizer.Entities;
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

		private int _frameCounter;
		private int _skipFrames = 5;

		#endregion

		/**/
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
			this.FormClosing += OnAddNewEmployeeFormClosing;

			_container = container;

			Log = _container.Resolve<ILog>();
			IsPictureTaken = false;

			InitializeRecognizer();
			InitializeVideoSource();
		}

		//public AddNewEmployeeForm(IComponentContext container, IVideoSourceProvider provider)
		//{
		//	Verify.Argument.IsNotNull(container, nameof(container));
		//	Verify.Argument.IsNotNull(provider, nameof(provider));

		//	InitializeComponent();
		//	this.FormClosing += OnAddNewEmployeeFormClosing;

		//	_container = container;
		//	_videoSourceProvider = provider;

		//	Log = _container.Resolve<ILog>();

		//	InitializeRecognizer();
		//	InitializeVideoSource();
		//}

		#endregion

		#region Properties

		ILog Log { get; }

		bool IsPictureTaken { get; set; }

		Mat PictureTaken { get; set; }

		string FirstNameText
		{
			get
			{
				return _textBoxFirstname.Text;
			}
		}

		string LastNameText
		{
			get
			{
				return _textBoxLastname.Text;
			}
		}

		string PatronymicText
		{
			get
			{
				return _textBoxPatronymic.Text;
			}
		}

		#endregion

		#region Methods
		private void InitializeRecognizer()
		{
			_detector = _container.Resolve<FaceDetector>();
			_recognizer = _container.Resolve<LBPFaceRecognizer>();

			var trainDataPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFaces.xml");

			_recognizer.Load(trainDataPath);

			_recognizer.FaceRecognized += OnFaceRecognized;
		}

		private void InitializeVideoSource()
		{
			_videoSourceProvider = new DXCaptureSourceProvider();
			_videoSource = _videoSourceProvider.CreateVideoSource();
			_matrix = new ColorMatrix();

			_frameImage.Matrix = _matrix;
			_frameImage.ManualUpdateRendererCache = true;
			_frameImage.SizeMode = ImageSizeMode.Zoom;

			_videoSource.AttachMatrix(_matrix);
			_videoSource.MatrixUpdated += OnMatrixUpdated;

			try
			{
				_videoSource.Open();
				_videoSource.Start();
			}
			catch
			{
			}
		}

		private void DrawRectangles(Rect[] rectangles)
		{
			_frameImage.Frames.Clear();
			foreach(var rect in rectangles)
			{
				Rect rect_ = Shakalization(rect);

				_frameImage.Frames.Add(new Frame(rect_.X, rect_.Y, rect_.Width, rect_.Height)
				{
					Locked = true,
					Visible = true
				});
			}
			_frameImage.InvalidateCache();
		}


		#endregion

		#region EventHandlers

		/// <summary> Обработчик события <see cref="LBPFaceRecognizer.FaceRecognized"/>. </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFaceRecognized(object sender, FaceRecognizedEventArgs e)
		{
			try
			{
				var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
				var filter = _container.Resolve<EmployeesLogRepositoryFilter>();
				filter.PersonLabel = e.Label;

				var employeeRecord = employeeLogRepository.FetchRecords(filter)[0];
				var resultStr = "Распознан " + employeeRecord;

				Log.Info(resultStr);
			}
			catch(Exception exc)
			{
				Log.Error("Ошибка в запросе к БД", exc);
			}
		}

		private void OnAddNewEmployeeFormClosing(object sender, FormClosingEventArgs e)
		{
			if(_videoSource != null)
			{
				_videoSource.DetachAllMatrices();
				_videoSource.MatrixUpdated -= OnMatrixUpdated;
				_videoSource.Stop();
				_videoSource.Close();
			}

			_recognizer.FaceRecognized -= OnFaceRecognized;
		}

		private void OnMatrixUpdated(object sender, MatrixUpdatedEventArgs e)
		{
			var mi = new MethodInvoker(() =>
			{
				if(!IsDisposed)
				{
					_frameImage.InvalidateCache();
				}
			});

			if(_frameImage.InvokeRequired)
			{
				if(!IsDisposed)
				{
					try
					{
						_frameImage.BeginInvoke(mi);
					}
					catch(ObjectDisposedException)
					{
					}
				}
			}
			else
			{
				mi();
			}

			if(_frameCounter >= _skipFrames)
			{
				//устанавливаем кадр, который требуется проанализировать
				_detector.UpdateImages(_matrix);

				var rects = _detector.DetectFaces();

				mi = new MethodInvoker(() => DrawRectangles(rects));
				if(_frameImage.InvokeRequired)
				{
					if(!IsDisposed)
					{
						try
						{
							_frameImage.BeginInvoke(mi);
						}
						catch(ObjectDisposedException)
						{
						}
					}
				}
				else
				{
					mi();
				}

				FaceProcessing();

				_frameCounter = 0;
			}
			_frameCounter++;
		}

		private Rect Shakalization(Rect rect)
		{
			int frameWidth = _frameImage.Matrix.Width,
				frameHeigth = _frameImage.Matrix.Height,
				componentWidth = _frameImage.Width,
				componentHeigth = _frameImage.Height;

			double shakalizationFactor = (double)componentWidth / frameWidth;

			int shiftY = (int)(componentHeigth - (frameHeigth * shakalizationFactor)) / 2;

			rect.X = (int)(rect.X * shakalizationFactor);
			rect.Y = (int)((rect.Y * shakalizationFactor) + shiftY);

			rect.Width = (int)(rect.Width * shakalizationFactor);
			rect.Height = (int)(rect.Height * shakalizationFactor);

			return rect;
		}

		private void FaceProcessing()
		{
			foreach(var face in _detector.FacesRepository)
			{
				lock(this)
				{
					//_recognizer.Recognize(face);

					//double avgBrightness = face.AverageBrightness();

					//Log.Info(avgBrightness);
					PictureTaken = face;
				}
			}

			_detector
					.FacesRepository
					.Clear();
		}

		private void OnButtonTakePicture_Click(object sender, EventArgs e)
		{
			if(/*_detector.FaceCounter == 1*/ PictureTaken != null)
			{

				_frameImage.Frames.Clear();
				//var imageMatrix = _detector.FacesRepository[0];
				var imageMatrix = PictureTaken;

				_videoSource.Stop();

				var showingMatrix = imageMatrix
					.CvtColor(ColorConversion.GrayToBgr)
					.ToImage();

				_frameImage.Matrix = showingMatrix;

				IsPictureTaken = true;
				PictureTaken = imageMatrix;
			}
		}

		#endregion

		private void OnButtonDropPicture_Click(object sender, EventArgs e)
		{
			if(_videoSource.State != VideoSourceState.Running)
			{
				_frameImage.Matrix = _matrix;
				_videoSource.Start();
				IsPictureTaken = false;
			}
		}

		private void OnButtonAddNewEmployee_Click(object sender, EventArgs e)
		{
			if (IsPictureTaken)
			{
				if(_recognizer.Recognize(PictureTaken) != -1)
				{
					switch(MessageBox.Show("Возможно, что лицо уже есть в базе. Продолжить?", "", MessageBoxButtons.YesNo))
					{
						case DialogResult.Yes:
							{
								AddEmployee();
								break;
							}
						case DialogResult.No:
							{
								this.Close();
								return;
							}
					}
				}

				else AddEmployee();

			}
			
		}

		private void InitXmlFile(Mat img, long label)
		{
			_recognizer.Train(new List<Mat> { img }, new List<int> { (int) label });
			_recognizer.Save(Path.Combine(
					Directory.GetCurrentDirectory(),
					"Samples",
					"initialized.xml"));
		}

		private void AddEmployee()
		{
			try
			{
				var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
				Employee employee;
				long maxLabel = -1;

				if(FirstNameText != string.Empty && LastNameText != string.Empty && PatronymicText != string.Empty)
				{
					maxLabel = employeeLogRepository.findMaxLabel();

					employee = new Employee
					{
						FirstName = FirstNameText,
						LastName = LastNameText,
						Patronymic = PatronymicText,
						PersonLabel = maxLabel + 1
					};
				}
				else
				{
					MessageBox.Show(this, "Заполните все поля", "Ахтунг!", MessageBoxButtons.OK);
					return;
				}

				employeeLogRepository.AddRecord(employee);

				_recognizer.Update(PictureTaken, maxLabel + 1);

				string dest;
				_recognizer.Save(dest = Path.Combine(
						Directory.GetCurrentDirectory(),
						"Samples",
						"LBPFaces.xml"));

				Log.Info("Запись добавлена");

				_recognizer.Load(dest);
				this.Close();
			}
			catch(Exception exc)
			{
				Log.Error("Ошибка в запросе к БД", exc);
			}
		}

		private void OnButtonPictureFromFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Выберите картинку";
			openFile.InitialDirectory = Path.Combine(
					Directory.GetCurrentDirectory(),
					"Photo");
			openFile.Filter = "Images|*.jpg;*.png;*.bmp|All files|*.*";

			if(openFile.ShowDialog() == DialogResult.OK)
			{
				var detector = new FaceDetector(openFile.FileName);
				detector.DetectFaces();

				foreach(var face in detector.FacesRepository)
				{
					var showingMatrix = face
						.CvtColor(ColorConversion.GrayToBgr)
						.ToImage();

					_frameImage.Matrix = showingMatrix;

					IsPictureTaken = true;
					PictureTaken = face;

					break;
				}

				
			}
		}
	}
}
