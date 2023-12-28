
using APIStart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStart.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FullName).
                IsRequired().HasMaxLength(50);
            builder.Property(e => e.Description).
                 IsRequired().HasMaxLength(200);
           
            builder.Property(e => e.InstaLink).
                            IsRequired().HasMaxLength(100);
            builder.Property(e => e.FaceLink).
                            IsRequired().HasMaxLength(100);
            builder.Property(e => e.TwitLink).
                            IsRequired().HasMaxLength(100);
            builder.Property(e => e.ImageUrl).
                          IsRequired().HasMaxLength(100);
            builder.Property(e => e.LinkLink).
                          IsRequired().HasMaxLength(100);
           


        }
    }
}
