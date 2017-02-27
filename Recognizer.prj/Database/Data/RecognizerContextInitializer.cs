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
					FirstName = "Pavel",
					Patronymic = "Ivanovich",
					LastName = "Shevelev",
					PersonLabel = 1
				},

				new Employee
				{
					FirstName = "Andrey",
					Patronymic = "Alekseevich",
					LastName = "Okomin",
					PersonLabel = 3
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
