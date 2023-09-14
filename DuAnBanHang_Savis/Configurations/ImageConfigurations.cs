using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            //     public Guid Id { get; set; }
            //public Guid? IdProductDetail { get; set; }
            //public string? TenAnh { get; set; }
            //public byte[]? DuongDan { get; set; }
            //public int? TrangThai { get; set; }

            //public virtual ProductDetail ProductDetail { get; set; }

            builder.HasKey(x => x.Id);
            builder.Property(x => x.TenAnh).HasColumnType("nvarchar(250)");

            builder.HasOne(x => x.ProductDetails).WithMany(y => y.Image).
            HasForeignKey(c => c.IdProductDetail);
        }
    }
}
