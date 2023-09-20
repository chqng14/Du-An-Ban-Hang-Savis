using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BaoHanh).HasColumnType("nvarchar(250)").IsRequired(false);
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(max)").IsRequired(false);

            builder.HasOne(x => x.Products).WithMany(y => y.ProductDetails).
            HasForeignKey(c => c.IdProduct);

            builder.HasOne(x => x.Color).WithMany(y => y.ProductDetail).
            HasForeignKey(c => c.IdColor);

            builder.HasOne(x => x.Size).WithMany(y => y.ProductDetails).
            HasForeignKey(c => c.IdSize);


            builder.HasOne(x => x.TypeProduct).WithMany(y => y.ProductDetails).
            HasForeignKey(c => c.IdTypeProduct);


            builder.HasOne(x => x.Material).WithMany(y => y.ProductDetails).
            HasForeignKey(c => c.IdMaterial);



        }
    }
}
