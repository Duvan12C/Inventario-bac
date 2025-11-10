using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class BacDbContext : DbContext
    {
        public BacDbContext(DbContextOptions<BacDbContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Product -> CreatedBy / UpdatedBy
            modelBuilder.Entity<Product>()
                .HasOne(p => p.EmployeeCreated)
                .WithMany(e => e.ProductsCreated)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.EmployeeUpdated)
                .WithMany(e => e.ProductsUpdated)
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Seller)
                .WithMany(e => e.Sales)
                .HasForeignKey(s => s.IdSeller)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.IdSale)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleDetail>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.IdProduct)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
