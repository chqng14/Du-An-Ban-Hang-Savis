using App_Api.Helpers.CustomJson;
using App_Data.IRepositories;
using App_Data.Models;
using App_Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = new CustomNamingPolicy();
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAllRepo<ProductDetails>, AllRepo<ProductDetails>>();
builder.Services.AddScoped<IAllRepo<Blog>, AllRepo<Blog>>();
builder.Services.AddScoped<IAllRepo<Bill>, AllRepo<Bill>>();
builder.Services.AddScoped<IAllRepo<BillDetails>, AllRepo<BillDetails>>();
builder.Services.AddScoped<IAllRepo<Cart>, AllRepo<Cart>>();
builder.Services.AddScoped<IAllRepo<CartDetails>, AllRepo<CartDetails>>();
builder.Services.AddScoped<IAllRepo<App_Data.Models.Color>, AllRepo<App_Data.Models.Color>>();
builder.Services.AddScoped<IAllRepo<SaleDetail>, AllRepo<SaleDetail>>();
builder.Services.AddScoped<IAllRepo<Images>, AllRepo<Images>>();
builder.Services.AddScoped<IAllRepo<Material>, AllRepo<Material>>();
builder.Services.AddScoped<IAllRepo<Product>, AllRepo<Product>>();
builder.Services.AddScoped<IAllRepo<Role>, AllRepo<Role>>();
builder.Services.AddScoped<IAllRepo<Sale>, AllRepo<Sale>>();
builder.Services.AddScoped<IAllRepo<SaleDetail>, AllRepo<SaleDetail>>();
builder.Services.AddScoped<IAllRepo<TypeProduct>, AllRepo<TypeProduct>>();
builder.Services.AddScoped<IAllRepo<User>, AllRepo<User>>();
builder.Services.AddScoped<IAllRepo<App_Data.Models.Size>, AllRepo<App_Data.Models.Size>>();
builder.Services.AddScoped<IAllRepo<Voucher>, AllRepo<Voucher>>();
builder.Services.AddScoped<IAllRepo<Suppliers>, AllRepo<Suppliers>>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
