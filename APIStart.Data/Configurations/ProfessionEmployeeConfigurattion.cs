using APIStart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Data.Configurations
{
    public class ProfessionEmployeeConfigurattion : IEntityTypeConfiguration<ProfessionEmployee>
    {
        public void Configure(EntityTypeBuilder<ProfessionEmployee> builder)
        {
           
                builder.HasOne(wp => wp.Profession).WithMany(wp => wp.ProfessionEmployees).HasForeignKey(wp => wp.ProfessionId).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(wp => wp.Employee).WithMany(wp => wp.Professions).HasForeignKey(wp => wp.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
