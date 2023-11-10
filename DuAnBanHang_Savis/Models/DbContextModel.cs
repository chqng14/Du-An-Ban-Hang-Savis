using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace App_Data.Models
{
    public class DbContextModel : DbContext
    {
        public DbContextModel()
        {

        }

        public DbContextModel(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SaleDetail> DetailSales { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-OF-KHAI;Initial Catalog=Savis;Integrated Security=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Role>().HasData(
               new Role { Id = Guid.Parse("7430128c-674e-4c3d-9f6a-9c4025ae22fe"), Ma = "ADMIN", Ten = "admin", TrangThai = 0 },
               new Role { Id = Guid.NewGuid(),Ma = "USER", Ten = "user", TrangThai = 0 }
               //new Role { Id = Guid.NewGuid(),Ma = "STAFF", Ten = "staff", TrangThai = 0 }
               );
            modelBuilder.Entity<User>().HasData(
                new User{Id = Guid.NewGuid(), IdRole = Guid.Parse("7430128c-674e-4c3d-9f6a-9c4025ae22fe"), Ma = "ADMIN", Ten = "Admin", GioiTinh = 0, NgaySinh = DateTime.Now, DiaChi = " ", Sdt = "0987654321", TaiKhoan = "admin", MatKhau = "admin", Email = "admin@gmail.com", TrangThai = 0  }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
