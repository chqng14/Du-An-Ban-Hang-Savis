using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;
using System.Reflection.Emit;

namespace App_Data.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {

            builder.ToTable("Sale");

            //builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.Ten).HasMaxLength(500);
            builder.Property(e => e.DuongDanAnh).HasMaxLength(1000);
            builder.Property(e => e.Ma).HasColumnType("nvarchar(20)");
            builder.Property(e => e.MucGiam).HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");
            builder.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            builder.Property(e => e.NgayBatDau).HasColumnType("datetime");
            builder.Property(e => e.LoaiHinhKm).HasMaxLength(500);
            builder.Property(e => e.MoTa).HasMaxLength(500).IsRequired(false);
            builder.Property(e => e.TrangThai).HasDefaultValueSql("((0))");
        }
    }
}
