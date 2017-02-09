using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Recognizer.DataBase
{
	public sealed class Employees
	{
		#region Configuration

		private sealed class EmployeesConfiguration : EntityTypeConfiguration<Employees>
		{
			public EmployeesConfiguration()
			{
				ToTable("Employees");

				Property(e => e.Id)
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(e => e.FirstName)
					.IsRequired()
					.IsUnicode()
					.HasMaxLength(30);

				Property(e => e.LastName)
					.IsRequired()
					.IsUnicode()
					.HasMaxLength(30);

				Property(e => e.Patronymic)
					.IsRequired()
					.IsUnicode()
					.HasMaxLength(30);

				Property(e => e.MaskPhoto)
					.IsRequired();

				Property(e => e.SourcePhoto)
					.IsOptional();
			}
		}

		public static EntityTypeConfiguration<Employees> CreateConfiguration() => new EmployeesConfiguration();

		#endregion

		#region Properties

		public long Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public byte[] MaskPhoto { get; set; }

		public byte[] SourcePhoto { get; set; }

		#endregion

	}
}
