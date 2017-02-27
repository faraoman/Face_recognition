using System.Collections.Generic;
using System.Data.Entity;
using Recognizer.Entities;

namespace Recognizer.Database
{
	public class RecognizerContextInitializer : CreateDatabaseIfNotExists<RecognizerContext>
	{
		protected override void Seed(RecognizerContext context)
		{
			var employees = new List<Employee>()
			{
				new Employee
				{
					FirstName = "Павел",
					Patronymic = "Иванович",
					LastName = "Шевелев",
					PersonLabel = 1
				},

				new Employee
				{
					FirstName = "Андрей",
					Patronymic = "Алексеевич",
					LastName = "Окомин",
					PersonLabel = 3
				},

				new Employee
				{
					FirstName = "Матвей",
					Patronymic = "Иванович",
					LastName = "Чевычелов",
					PersonLabel = 4
				}
			};

			context
				.Employees
				.AddRange(employees);

			context.SaveChanges();

			base.Seed(context);
		}
	}
}
