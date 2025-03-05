using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Appiconnect.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Appiconnect.Web.Data
{
    public class DataContext:IdentityDbContext<User>
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasIndex(x=> x.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
