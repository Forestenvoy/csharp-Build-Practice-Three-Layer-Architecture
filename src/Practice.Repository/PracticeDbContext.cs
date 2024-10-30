using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Practice.Repository.Entities;

namespace Practice.Repository
{
    public partial class PracticeDbContext : DbContext, IDataProtectionKeyContext
    {
        public PracticeDbContext() 
        {
        }

        public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
        {
        }

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        // Test
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 指定資料庫的定序與字符集
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Account)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(e => e.AddTime)
                    .HasColumnType("timestamp(3)")
                    .HasConversion(e => e, e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

                entity.Property(e => e.UpdateTime)
                    .IsConcurrencyToken()
                    .HasColumnType("timestamp(3)")
                    .HasConversion(e => e, e => DateTime.SpecifyKind(e, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                
                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.AddTime)
                    .HasColumnType("timestamp(3)")
                    .HasConversion(e => e, e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

                entity.Property(e => e.UpdateTime)
                    .IsConcurrencyToken()
                    .HasColumnType("timestamp(3)")
                    .HasConversion(e => e, e => DateTime.SpecifyKind(e, DateTimeKind.Utc));
            });

            OnModelCreatingPartial(modelBuilder);

            // 種子資料
            Seed(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString =
                    "Server=mysql;User ID=root;Password=P@ssw0rd;Database=Practice;TreatTinyAsBoolean=False";

                ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

                optionsBuilder.UseMySql(connectionString, serverVersion,
                    op =>
                    {
                        op.DefaultDataTypeMappings(m => m.WithClrBoolean(MySqlBooleanType.Bit1));
                        op.DefaultDataTypeMappings(m => m.WithClrDateTime(MySqlDateTimeType.Timestamp6));
                    });
            }
        }
    }
}

