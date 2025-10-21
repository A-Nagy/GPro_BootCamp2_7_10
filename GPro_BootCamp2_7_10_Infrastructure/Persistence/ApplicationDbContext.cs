using GPro_BootCamp2_7_10_Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Infrastructure.Persistence
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }


        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> productImages => Set<ProductImage>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<CartItem> CartItems => Set<CartItem>();



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Product> ().HasQueryFilter (x => !x.IsDeleted);
            builder.Entity<Supplier>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ProductImage>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<OrderItem>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<CartItem> ().HasQueryFilter (x => !x.IsDeleted);

            builder.Entity<Category>().Property(c => c.RowVersion).IsRowVersion();
            builder.Entity<Product> ().Property (p => p.RowVersion).IsRowVersion();
            builder.Entity<Order>().Property(o => o.RowVersion).IsRowVersion();
            builder.Entity<OrderItem>().Property(oi => oi.RowVersion).IsRowVersion();

            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");

        }

    }
}
