using Project.BLL.ServiceInjections;
using System.Net;
using Project.COMMON.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextService();
builder.Services.AddRepManServices();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "addOrderForm",
    pattern: "Order/AddOrderForm",
    defaults: new { controller = "Order", action = "AddOrderForm" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shopping}/{action=ShoppingList}/{id?}");

app.Run();
