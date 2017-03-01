using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Recognizer.Database;
using Recognizer.Database.Data;
using Recognizer.Entities;
using Recognizer.Logs;
using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public partial class UserLists : Form
	{
		private IContainer _container;

		public UserLists()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		public UserLists(IContainer container)
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;

			_container = container;
			Log = _container.Resolve<ILog>();
		}

		#region Properties
		ILog Log { get; }

		#endregion

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
			try
			{
				var employeeLogRepository = _container.Resolve<EmployeesLogRepository>();
				var vasya = new Employee
				{
					FirstName = "Вася",
					LastName = "Пупкин",
					Patronymic = "Петрович",
					PersonLabel = 5
				};

				employeeLogRepository.AddRecord(vasya);
			}
			catch(Exception exc)
			{
				Log.Error("Database initialization error", exc);
			}
		}
	}
}
