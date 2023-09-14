using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.IDUser).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.IDCTSP).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.GiaKhuyenMai).HasColumnType("decimal");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.HasOne(x => x.Cart).WithMany(x => x.CartDetail).HasForeignKey(x => x.IDUser);
            builder.HasOne(x => x.ProductDetail).WithMany(x => x.CartDetail).HasForeignKey(x => x.IDCTSP);
        }
    }
}
