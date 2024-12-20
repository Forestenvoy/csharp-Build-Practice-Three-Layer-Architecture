﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Practice.Repository.Entities;
using Practice.Repository.Common;

namespace Practice.Repository.Persistence
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

        // 管理員
        public virtual DbSet<Admin> Admins { get; set; }

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

                entity.Property(e => e.Create)
                    .HasColumnType("timestamp(3)")
                    .HasConversion(
                        e => e, 
                        e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

                entity.Property(e => e.LastModified)
                    .IsConcurrencyToken()
                    .HasColumnType("timestamp(3)")
                        .HasConversion(
                        e => e,
                        e => e.HasValue ? DateTime.SpecifyKind(e.Value, DateTimeKind.Utc) : null);
            });

            OnModelCreatingPartial(modelBuilder);

            // 種子資料
            Seed(modelBuilder);
        }

        ///// <summary>
        ///// SaveChangesAsync 擴展
        ///// </summary>
        ///// <remarks>
        ///// 檢查 追蹤狀態且是繼承於 AuditableEntity 的實體，並且狀態是修改的，統一設定最後修改時間
        ///// 廣義上任何欄位變動都算，所以斟酌使用，因為有些表不是每個欄位的變動都算在資料異動裡面
        ///// </remarks>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    // 設定最後修改時間 
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.SetLastModified();
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString ="Server=mysql;User ID=root;Password=P@ssw0rd;Database=Practice;TreatTinyAsBoolean=False";

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

