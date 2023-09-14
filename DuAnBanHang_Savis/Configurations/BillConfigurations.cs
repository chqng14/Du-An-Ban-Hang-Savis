using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;
namespace App_Data.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.Bills).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Voucher).WithMany(x => x.Bills).HasForeignKey(x => x.IdVoucher);
            builder.Property(x => x.Ma).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.NgayThanhToan).HasColumnType("DateTime");
            builder.Property(x => x.NgayShip).HasColumnType("DateTime");
            builder.Property(x => x.NgayNhan).HasColumnType("DateTime");
            builder.Property(x => x.TenNguoiNhan).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.Sdt).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(1000)");

        }
    }
}
