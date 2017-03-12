using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognizer.Entities;

using Mallenom;

namespace Recognizer.Database.Data
{
	/// <summary> Класс, достающий из базы данных записи <seealso cref="EmployeesLogRecord"/> по фильтру <see cref="EmployeesLogRepositoryFilter"/></summary>
	public class EmployeesLogRepository
	{
		#region .ctor

		public EmployeesLogRepository(IDbContextFactory dbContextFactory)
		{
			Verify.Argument.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			DbContextFactory = dbContextFactory;
		}

		#endregion

		#region Properties

		IDbContextFactory DbContextFactory { get; }

		#endregion

		#region Methods

		public void AddRecord(Employee employee)
		{
			using(var dbContext = DbContextFactory.CreateContext())
			{
				dbContext
					.Set<Employee>()
					.Add(employee);

				dbContext.SaveChanges();
			}
		}

		public long findMaxLabel()
		{
			using(var dbContext = DbContextFactory.CreateContext())
			{
				var label = dbContext
					.Set<Employee>()
					.Max(e => e.PersonLabel);

				return label;
			}
		}

		/// <summary> Выполняет запрос к базе данных и достаёт оттуда все записи. </summary>
		/// <returns> Возвращает список cо всеми записями из БД. </returns>
		public IReadOnlyList<EmployeesLogRecord> FetchRecords()
		{
			var records = new List<EmployeesLogRecord>();

			using(var dbContext = DbContextFactory.CreateContext())
			{
				var employees = dbContext
					.Set<Employee>()
					.ToList();

				foreach(var employee in employees)
				{
					records.Add(new EmployeesLogRecord
					{
						Id = employee.Id,
						LastName = employee.LastName,
						FirstName = employee.FirstName,
						Patronymic = employee.Patronymic
					});
				}
			}

			return records;
		}

		/// <summary> Выполняет запрос к базе данных и достаёт оттуда записи, соответствующие фильтру <see cref="EmployeesLogRepositoryFilter"/>. </summary>
		/// <param name="filter"> Фильтр для записей из базы данных. </param>
		/// <returns> Возвращает список c записями, соответствующими фильтру. </returns>
		public IReadOnlyList<EmployeesLogRecord> FetchRecords(EmployeesLogRepositoryFilter filter)
		{
			var records = new List<EmployeesLogRecord>();

			using(var dbContext = DbContextFactory.CreateContext())
			{
				var employees = dbContext
					.Set<Employee>()
					.Where(e => e.PersonLabel == filter.PersonLabel)
					.ToList();

				foreach(var employee in employees)
				{
					records.Add(
						new EmployeesLogRecord
						{
							Id = employee.Id,
							LastName = employee.LastName,
							FirstName = employee.FirstName,
							Patronymic = employee.Patronymic
						});
				}
			}

			return records;
		} 
		#endregion
	}

	/// <summary> Запись из таблицы с сотрудниками. </summary>
	public class EmployeesLogRecord
	{
		#region .ctor
		static EmployeesLogRecord()
		{
			СolumnNames = new string[] {
				"Id", "Фамилия", "Имя", "Отчество"};
		}
		#endregion

		#region Properties
		public long Id { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Patronymic { get; set; }

		public static string[] СolumnNames { get; }
		#endregion

		#region Methods
		public override string ToString() => $"{LastName} {FirstName} {Patronymic}"; 
		#endregion
	}

	/// <summary> Фильтр записей из базы данных по метке <paramref name="PersonLabel"/>. </summary>
	public class EmployeesLogRepositoryFilter
	{
		public long PersonLabel { get; set; }
	}
}
