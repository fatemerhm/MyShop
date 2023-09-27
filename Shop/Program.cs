using Microsoft.EntityFrameworkCore;
using Shop.Core.ApplicationService;
using Shop.Core.Contracts;
using Shop.Core.Domain;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.EF;
using Shop.Models;
using Shop.PaymentSystem;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProdctFacade, ProductFacade>();
builder.Services.AddScoped<IOrderRepository, OrederRepository>();
builder.Services.AddScoped<IOrderFacade, OrderFacade>();
builder.Services.AddScoped<IDPay>();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
