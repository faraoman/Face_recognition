using System.Collections.Generic;
using System.Data.Entity;
using Recognizer.Entities;

namespace Recognizer.Database
{
	public class RecognizerContextInitializer : CreateDatabaseIfNotExists<RecognizerContext>
	{
		protected override void Seed(RecognizerContext context)
		{
			var employees = new[]
			{
				new Employee
				{
					FirstName = "Павел",
					Patronymic = "Иванович",
					LastName = "Шевелев",
					PersonLabel = 0
				},

				new Employee
				{
					FirstName = "Андрей",
					Patronymic = "Алексеевич",
					LastName = "Окомин",
					PersonLabel = 1
				},

				new Employee
				{
					FirstName = "Матвей",
					Patronymic = "Иванович",
					LastName = "Чевычелов",
					PersonLabel = 2
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
