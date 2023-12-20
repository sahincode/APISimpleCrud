using APIStart.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APIStart.data
{
    public class AppDbContext :DbContext
    {
       public  AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
          
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
