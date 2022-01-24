using Microsoft.EntityFrameworkCore;
using Targv20Shop.Core.Domain;

namespace Targv20Shop.Data
{
    public class Targv20ShopDbContext : DbContext
    {
        public Targv20ShopDbContext(DbContextOptions<Targv20ShopDbContext> options)
            : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<ExistingFilePath> ExistingFilePath { get; set; }
        public DbSet<Spaceship> Spaceship { get; set; }
        public DbSet<FileToDatabase> FileToDatabase { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spaceship>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }*/
    }
}

