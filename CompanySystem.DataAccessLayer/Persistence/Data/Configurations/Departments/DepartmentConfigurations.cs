using CompanySystem.DataAccessLayer.Models.Departments;

namespace CompanySystem.DataAccessLayer.Persistence.Data.Configurations.Departments
{
    class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(E  => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
