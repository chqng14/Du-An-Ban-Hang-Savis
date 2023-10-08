﻿// <auto-generated />
using System;
using App_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App_Data.Migrations
{
    [DbContext(typeof(DbContextModel))]
    [Migration("20231008144619_Update_VoucherV3")]
    partial class Update_VoucherV3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App_Data.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid?>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdVoucher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("NgayNhan")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("NgayShip")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("NgayThanhToan")
                        .HasColumnType("DateTime");

                    b.Property<string>("Sdt")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal?>("SoTienGiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenNguoiNhan")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal?>("TienShip")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.HasIndex("IdVoucher");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("App_Data.Models.BillDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("DonGia")
                        .HasColumnType("decimal");

                    b.Property<Guid?>("IdBill")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("IdProductDetail")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdBill");

                    b.HasIndex("IdProductDetail");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("App_Data.Models.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenBlog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("App_Data.Models.Cart", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("IdUser");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("App_Data.Models.CartDetails", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("GiaKhuyenMai")
                        .HasColumnType("decimal");

                    b.Property<Guid>("IDCTSP")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("IDUser")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDCTSP");

                    b.HasIndex("IDUser");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("App_Data.Models.Color", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("App_Data.Models.Images", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DuongDan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdProductDetail")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TenAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProductDetail");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("App_Data.Models.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("App_Data.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App_Data.Models.ProductDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BaoHanh")
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal?>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GiaNhap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("IdColor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdMaterial")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdProduct")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdSize")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdTypeProduct")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsNoiBat")
                        .HasColumnType("bit");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongDaBan")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongTon")
                        .HasColumnType("int");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdColor");

                    b.HasIndex("IdMaterial");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdSize");

                    b.HasIndex("IdTypeProduct");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("App_Data.Models.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("LoaiHinhKm")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MoTa")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal?>("MucGiam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,0)")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("NgayBatDau")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayKetThuc")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("TrangThai")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.ToTable("Sale", (string)null);
                });

            modelBuilder.Entity("App_Data.Models.SaleDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid?>("IdChiTietSp")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdChiTietSP");

                    b.Property<Guid?>("IdSale")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdSale");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("TrangThai")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("IdChiTietSp");

                    b.HasIndex("IdSale");

                    b.ToTable("DetailSale", (string)null);
                });

            modelBuilder.Entity("App_Data.Models.Size", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cm")
                        .HasColumnType("decimal");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Size1")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Size", (string)null);
                });

            modelBuilder.Entity("App_Data.Models.TypeProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TypeProducts");
                });

            modelBuilder.Entity("App_Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<int>("GioiTinh")
                        .HasColumnType("int");

                    b.Property<Guid>("IdRole")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TaiKhoan")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdRole");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("App_Data.Models.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DieuKien")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("LoaiHinhKm")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("MucUuDai")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("NgayBatDau")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayKetThuc")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<int?>("SoLuongTon")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("App_Data.Models.Bill", b =>
                {
                    b.HasOne("App_Data.Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("IdUser");

                    b.HasOne("App_Data.Models.Voucher", "Voucher")
                        .WithMany("Bills")
                        .HasForeignKey("IdVoucher");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("App_Data.Models.BillDetails", b =>
                {
                    b.HasOne("App_Data.Models.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("IdBill");

                    b.HasOne("App_Data.Models.ProductDetails", "ProductDetail")
                        .WithMany("BillDetail")
                        .HasForeignKey("IdProductDetail");

                    b.Navigation("Bill");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("App_Data.Models.Cart", b =>
                {
                    b.HasOne("App_Data.Models.User", "Users")
                        .WithOne("Carts")
                        .HasForeignKey("App_Data.Models.Cart", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("App_Data.Models.CartDetails", b =>
                {
                    b.HasOne("App_Data.Models.ProductDetails", "ProductDetail")
                        .WithMany("CartDetail")
                        .HasForeignKey("IDCTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data.Models.Cart", "Cart")
                        .WithMany("CartDetail")
                        .HasForeignKey("IDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("App_Data.Models.Images", b =>
                {
                    b.HasOne("App_Data.Models.ProductDetails", "ProductDetails")
                        .WithMany("Image")
                        .HasForeignKey("IdProductDetail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.ProductDetails", b =>
                {
                    b.HasOne("App_Data.Models.Color", "Color")
                        .WithMany("ProductDetail")
                        .HasForeignKey("IdColor");

                    b.HasOne("App_Data.Models.Material", "Material")
                        .WithMany("ProductDetails")
                        .HasForeignKey("IdMaterial");

                    b.HasOne("App_Data.Models.Product", "Products")
                        .WithMany("ProductDetails")
                        .HasForeignKey("IdProduct");

                    b.HasOne("App_Data.Models.Size", "Size")
                        .WithMany("ProductDetails")
                        .HasForeignKey("IdSize");

                    b.HasOne("App_Data.Models.TypeProduct", "TypeProduct")
                        .WithMany("ProductDetails")
                        .HasForeignKey("IdTypeProduct");

                    b.Navigation("Color");

                    b.Navigation("Material");

                    b.Navigation("Products");

                    b.Navigation("Size");

                    b.Navigation("TypeProduct");
                });

            modelBuilder.Entity("App_Data.Models.SaleDetail", b =>
                {
                    b.HasOne("App_Data.Models.ProductDetails", "ProductDetail")
                        .WithMany("DetailSale")
                        .HasForeignKey("IdChiTietSp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data.Models.Sale", "Sale")
                        .WithMany("DetailSales")
                        .HasForeignKey("IdSale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDetail");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("App_Data.Models.User", b =>
                {
                    b.HasOne("App_Data.Models.Role", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("App_Data.Models.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("App_Data.Models.Cart", b =>
                {
                    b.Navigation("CartDetail");
                });

            modelBuilder.Entity("App_Data.Models.Color", b =>
                {
                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("App_Data.Models.Material", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.Product", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.ProductDetails", b =>
                {
                    b.Navigation("BillDetail");

                    b.Navigation("CartDetail");

                    b.Navigation("DetailSale");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("App_Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("App_Data.Models.Sale", b =>
                {
                    b.Navigation("DetailSales");
                });

            modelBuilder.Entity("App_Data.Models.Size", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.TypeProduct", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("App_Data.Models.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Carts")
                        .IsRequired();
                });

            modelBuilder.Entity("App_Data.Models.Voucher", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
