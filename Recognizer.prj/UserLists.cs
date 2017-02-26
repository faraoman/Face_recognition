using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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
		}

		private static Task GetEmployeesAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					var dbConnectionFactory = Services.DatabaseService.DbConnectionFactory;
					var context = new RecognizerContext(dbConnectionFactory, contextOwnsConnection: true);

					var employees = context
						.Set<Employee>()
						.ToList();

					string str = string.Empty;

					foreach(var employee in employees)
					{
						str += employee + "\n";
					}

					MessageBox.Show(str, "Список сотрудников", MessageBoxButtons.OK);

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
	}
}
