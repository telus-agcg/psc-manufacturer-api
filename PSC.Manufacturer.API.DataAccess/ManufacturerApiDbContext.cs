using Microsoft.EntityFrameworkCore;
using PSC.Manufacturer.API.Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace PSC.Manufacturer.API.DataAccess
{
    public class ManufacturerApiDbContext:DbContext
    {
        public ManufacturerApiDbContext(DbContextOptions<ManufacturerApiDbContext> options)
            :base(options)
        {          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Core.Entities.Manufacturer>()
                .ToTable("Manufacturer")
                .HasKey(x=>x.Mfg_Key);

            modelBuilder.Entity<Core.Entities.Manufacturer>()
                .Property(x => x.Mfg_Key).UseIdentityColumn();

            modelBuilder.Entity<Core.Entities.Manufacturer>()
                .Property(x => x.State_Code).HasColumnType("char (2)");

            modelBuilder.Entity<Vendor>()
                .ToTable("Vendor")
                .HasKey(x => x.Vendor_Key);
        }

        public DbSet<Core.Entities.Manufacturer> Manufacturers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
