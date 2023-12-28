
using APIStart.Core.Entities;
using APIStart.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APIStart.Data.DAL
{
    public class AppDbContext :DbContext
    {
       public  AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
          
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet <ProfessionEmployee> ProfessionEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
