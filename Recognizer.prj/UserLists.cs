using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Recognizer.Database;
using Recognizer.Entities;

using static Recognizer.Logs.LoggingService;

namespace Recognizer
{
	public partial class UserLists : Form
	{
		public UserLists()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			MinimumSize = Size;

			var employees = GetEmployeesFromDb();

			string str = string.Empty;

			foreach(var employee in employees)
			{
				str += employee.ToString() + "\n";
			}

			MessageBox.Show(this, str, "Список сотрудников", MessageBoxButtons.OK);
		}

		private List<Employee> GetEmployeesFromDb()
		{
			try
			{
				var context = new RecognizerContext(Services.DatabaseService.DbConnectionFactory, contextOwnsConnection: true);

				return context
					.Set<Employee>()
					.ToList();
			}
			catch(Exception exc)
			{
				Log.Error("Database initialization error", exc);
				return new List<Employee>();
			}
		}
	}
}
