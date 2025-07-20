using CompanySystem.DataAccessLayer.Common.Enums;
using CompanySystem.DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.DataAccessLayer.Persistence.Data.Configurations.Employees
{
    class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E =>E.Gender)
                .HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender),gender)
                );

            builder.Property(E => E.EmployeeType)
                .HasConversion(
                   (type) => type.ToString(),
                  (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                 );

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETDATE()");

        }
    }
}
