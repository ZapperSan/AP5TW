using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using m3_zapletal.Eshop.Areas.Admin.Models.Database.Entity;
using m3_zapletal.Eshop.Models.Database.Configuration;
using m3_zapletal.Eshop.Models.Entity;
using m3_zapletal.Eshop.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace m3_zapletal.Eshop.Areas.Admin.Models.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<CarouselItem> CarouselItem {get; set;}

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());

            var entityTypes = builder.Model.GetEntityTypes();
            foreach(var entity in entityTypes)
            {
                entity.SetTableName(entity.GetTableName().Replace("AspNet", ""));
            }
        }
    }
}
