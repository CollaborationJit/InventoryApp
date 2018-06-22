using System;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Model
{
    public class ApplicationDbContext:  DbContext
    {
        #region Public Properties
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<InventoryItem> Items { get; set; }

        #endregion

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=tcp:inventoryhelper.database.windows.net,1433;Initial Catalog=InventoryHelper;Persist Security Info=False;User ID=inventoryadmin;Password=Huurb101!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Vendor>();
            modelBuilder.Entity<InventoryItem>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
