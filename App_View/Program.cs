using Hangfire;
using App_View.IServices;
using App_View.Services;
using System.Configuration;
using App_Data.IRepositories;
using App_Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7165") });
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IVoucherServices, VoucherServices>();
builder.Services.AddScoped<ITypeProductRepo, TypeProductRepo>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=LAPTOP-OF-KHAI;Initial Catalog=Savis;Integrated Security=True")); //Đoạn này ai chạy lỗi thì đổi đường dẫn trong này nha
builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
var promotionService = new PromotionService();
RecurringJob.AddOrUpdate("CheckPromotions", () => promotionService.CheckNgayKetThuc(), "*/5 * * * * *");
RecurringJob.AddOrUpdate("UpdateVoucher", () => promotionService.UpdateExpiredVouchers(), "*/5 * * * * *");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
