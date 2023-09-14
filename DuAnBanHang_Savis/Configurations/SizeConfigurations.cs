using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App_Data.Models;

namespace App_Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Size");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Ma).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.Size1).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.Property(c => c.Cm).HasColumnType("decimal").IsRequired(true);
            builder.Property(c => c.TrangThai).HasColumnType("int");
        }
    }
}
