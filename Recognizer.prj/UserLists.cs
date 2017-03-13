using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Mallenom.Video;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using Recognizer.Database;
using Recognizer.Database.Data;
using Recognizer.Entities;
using Recognizer.Logs;
using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public partial class UserLists : Form
	{
		private IComponentContext _container;
		private IVideoSource _videoSource;
		private RecognitionLogController _recognitionLogController;

		public UserLists()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		public UserLists(IComponentContext container, IVideoSource videoSource)
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;

			_videoSource = videoSource;

			_container = container;
			_recognitionLogController = container.Resolve<RecognitionLogController>();
			_recognitionLogController.DataGridView = _dataGridView;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			MinimumSize = Size;
		}

		private Task GetEmployeesAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
					var employeeRecords = employeeLogRepository.FetchRecords();

					string str = string.Empty;

					foreach(var employee in employeeRecords)
					{
						str += employee + "\n";
					}
				}

				catch(Exception exc)
				{
					Log.Error("Database initialization error", exc);
				}
			});
		}

		private async void _btnShowEmployees_Click(object sender, EventArgs e)
		{
			await GetEmployeesAsync();
		}

		private void _btnAddEmployee_Click(object sender, EventArgs e)
		{
			using(var addEmployee = new AddNewEmployeeForm(_container, _videoSource))
			{
				if(addEmployee.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						Debug.WriteLine("AddEmployeeForm created OK");

					}
					catch(Exception exc)
					{
						Log.Error("Database initialization error", exc);
					}
				}
			}

		}
	}
}
