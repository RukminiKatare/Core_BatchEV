using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Core_WebEV.Models
{
    public class CoreAppDBContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public CoreAppDBContext(DbContextOptions<CoreAppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //navigation with 1-M relationship & foreign key
            modelBuilder.Entity<Product>()
                .HasOne(P => P.Category)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.CategoryRowId);
            base.OnModelCreating(modelBuilder);
        }
    }

    

  
}
