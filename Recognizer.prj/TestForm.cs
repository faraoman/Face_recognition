using System;
using System.Linq;
using System.Windows.Forms;
using Recognizer.Database;
using Recognizer.Entities;
using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		private void _btnLoadImage_Click(object sender, EventArgs e)
		{
			var dbService = Services.DatabaseService;

			//Employee employee = new Employee
			//{
			//	FirstName = "Pavel",
			//	Patronymic = "Ivanovich",
			//	LastName = "Shevelev",
			//	PersonLabel = 1
			//};

			Employee employee = new Employee
			{
				FirstName = "Andrey",
				Patronymic = "Alekseevich",
				LastName = "Okomin",
				PersonLabel = 3
			};

			try
			{
				var context = new RecognizerContext();

				var employees = context
					.Set<Employee>()
					.ToList();

				context
					.Set<Employee>()
					.RemoveRange(employees);

				context.SaveChanges();
			}
			catch(Exception exc)
			{
				Log.Error("Database initialization error", exc);
			}
		}
	}
}
