using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognizer.Database.Data;

using Mallenom;
using System.Windows.Forms;

namespace Recognizer
{
	class RecognitionLogController
	{
		private DataGridView _dataGridView;

		public RecognitionLogController(EmployeesLogRepository repository)
		{
			Verify.Argument.IsNotNull(repository, nameof(repository));

			Repository = repository;
		}

		[NotNull]
		private EmployeesLogRepository Repository { get; }

		[CanBeNull]
		public DataGridView DataGridView
		{
			get { return _dataGridView; }
			set
			{
				if(_dataGridView != value)
				{
					if(_dataGridView != null)
					{
						_dataGridView.DataSource = null;
					}
					_dataGridView = value;
					{
						_dataGridView.DataSource = Repository.FetchRecords();
					}
				}
			}
		}
	}
}
