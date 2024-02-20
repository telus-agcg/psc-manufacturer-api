using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSC.Manufacturer.API.Core.Entities;

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

            modelBuilder.Entity<Core.Entities.Manufacturer>()
                .ToTable(x => x.HasTrigger("utr_Update_Manufacturer"));

            modelBuilder.Entity<Vendor>()
                .ToTable("Vendor")
                .HasKey(x => x.Vendor_Key);

            modelBuilder.Entity<ApiLog>(entity =>
            {
                entity.ToTable("Api_Log", "mdmrecon");
                entity.HasKey(x => x.ApiLogKey).HasName("PK_Mdmrecon_Api_Log");

                entity.Property(x => x.ApiLogKey)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn()
                    .HasColumnType("bigint")
                    .HasColumnName("Api_Log_Key");
            });
        }

        public DbSet<Core.Entities.Manufacturer> Manufacturers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ApiLog> ApiLogs { get; set; }
    }
}
