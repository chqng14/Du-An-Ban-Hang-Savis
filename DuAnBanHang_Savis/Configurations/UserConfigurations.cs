using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Ma).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(c => c.Ten).HasColumnType("nvarchar(300)").IsRequired();
            builder.Property(c => c.GioiTinh).HasColumnType("int").IsRequired();
            builder.Property(c => c.NgaySinh).HasColumnType("datetime").IsRequired();
            builder.Property(c => c.DiaChi).HasColumnType("nvarchar(MAX)").IsRequired(false);
            builder.Property(c => c.Sdt).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(c => c.MatKhau).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(c => c.Email).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(c => c.TaiKhoan).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(c => c.TrangThai).HasColumnType("int").IsRequired();
            builder.HasOne(x => x.Roles).WithMany(y => y.Users).
            HasForeignKey(c => c.IdRole);
        }
    }
}
