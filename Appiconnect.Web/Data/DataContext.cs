using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Appiconnect.Shared;

namespace Appiconnect.Web.Data
{
    public class DataContext:DbContext
    {
        public DbSet <City> Cities { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasIndex(x=> x.Name).IsUnique();
        }
    }
}
