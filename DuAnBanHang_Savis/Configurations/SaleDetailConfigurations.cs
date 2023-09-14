using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class DetailSaleConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.ToTable("DetailSale");
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.IdChiTietSp).HasColumnName("IdChiTietSP");
            builder.Property(e => e.IdSale).HasColumnName("IdSale");
            builder.Property(e => e.TrangThai).HasDefaultValueSql("((0))");
            builder.Property(e => e.MoTa).HasMaxLength(500);
            builder.HasOne(d => d.ProductDetail).WithMany(p => p.DetailSale)
                .HasForeignKey(d => d.IdChiTietSp);

            builder.HasOne(d => d.Sale)
                .WithMany(p => p.DetailSales)
                .HasForeignKey(d => d.IdSale);
        }
    }
}
