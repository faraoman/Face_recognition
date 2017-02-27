using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognizer.Entities;

namespace Recognizer.Database.Data
{
	/// <summary> Класс, достающий из базы данных записи <seealso cref="EmployeesLogRecord"/> по фильтру <see cref="EmployeesLogRepositoryFilter"/></summary>
	public class EmployeesLogRepository
	{
		#region .ctor

		public EmployeesLogRepository(IDbConnectionFactory dbConnectionFactory)
		{
			DbConnectionFactory = dbConnectionFactory;
		}
		#endregion

		#region Properties

		IDbConnectionFactory DbConnectionFactory { get; }
		#endregion

		#region Methods

		/// <summary> Выполняет запрос к базе данных и достаёт оттуда записи, соответствующие фильтру <see cref="EmployeesLogRepositoryFilter"/>. </summary>
		/// <param name="filter"> Фильтр для записей из базы данных. </param>
		/// <returns> Возвращает список c записями, соответствующими фильтру. </returns>
		public IReadOnlyList<EmployeesLogRecord> FetchRecord(EmployeesLogRepositoryFilter filter)
		{
			var records = new List<EmployeesLogRecord>();

			using(var dbContext = new RecognizerContext(DbConnectionFactory, contextOwnsConnection: true))
			{
				var employees = dbContext
					.Employees
					.Where(e => e.PersonLabel == filter.PersonLabel)
					.ToList();

				foreach(var employee in employees)
				{
					records.Add(new EmployeesLogRecord
					{
						Record = $"{employee.LastName} {employee.FirstName} {employee.Patronymic}"
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
		public string Record { get; set; }

		public override string ToString()
		{
			return Record;
		}
	}

	/// <summary> Фильтр записей из базы данных по метке <paramref name="PersonLabel"/>. </summary>
	public class EmployeesLogRepositoryFilter
	{
		public long PersonLabel { get; set; }
	}
}
