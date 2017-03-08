using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace Recognizer.Entities
{
	public sealed class Employee
	{
		#region Configuration

		private sealed class EmployeesConfiguration : EntityTypeConfiguration<Employee>
		{
			public EmployeesConfiguration()
			{
				ToTable("Employees");

				HasKey(e => e.Id);

				Property(e => e.Id)
					.IsRequired()
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(e => e.FirstName)
					.IsRequired()
					.IsUnicode()
					.HasColumnName("Имя")
					.HasMaxLength(30);

				Property(e => e.LastName)
					.IsRequired()
					.IsUnicode()
					.HasColumnName("Фамилия")
					.HasMaxLength(30);

				Property(e => e.Patronymic)
					.IsRequired()
					.IsUnicode()
					.HasColumnName("Отчество")
					.HasMaxLength(30);

				Property(e => e.PersonLabel)
					.IsRequired()
					.HasColumnName("Метка")
					.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_DatabasePlateIndex", 1) { IsUnique = true }));

			}
		}

		public static EntityTypeConfiguration<Employee> CreateConfiguration() => new EmployeesConfiguration();

		#endregion

		#region Properties

		public long Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public long PersonLabel { get; set; }

		#endregion

		public override string ToString() => $"Id: {Id}, Фамилия: {LastName}, Имя: {FirstName}, Отчество: {Patronymic}, Метка: {PersonLabel}.";
	}
}
