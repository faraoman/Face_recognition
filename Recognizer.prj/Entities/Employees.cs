using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Recognizer.Entities
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

				Property(e => e.PersonLabel)
					.IsRequired()
					.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_DatabasePlateIndex", 1) { IsUnique = true }));

			}
		}

		public static EntityTypeConfiguration<Employees> CreateConfiguration() => new EmployeesConfiguration();

		#endregion

		#region Properties

		public long Id { get; set; }

		[Column("Имя")]
		public string FirstName { get; set; }

		[Column("Фамилия")]
		public string LastName { get; set; }

		[Column("Отчество")]
		public string Patronymic { get; set; }

		[Column("Метка")]
		public long PersonLabel { get; set; }

		#endregion

	}
}
