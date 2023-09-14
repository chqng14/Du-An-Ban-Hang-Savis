using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdBill).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.IdProductDetail).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.DonGia).HasColumnType("decimal");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.Bill).WithMany(x => x.BillDetails).HasForeignKey(x => x.IdBill);
            builder.HasOne(x => x.ProductDetail).WithMany(x => x.BillDetail).HasForeignKey(x => x.IdProductDetail);
        }
    }
}
