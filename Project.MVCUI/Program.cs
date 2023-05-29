using Project.BLL.ServiceInjections;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextService();
builder.Services.AddRepManServices();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "addOrderForm",
    pattern: "Order/AddOrderForm",
    defaults: new { controller = "Order", action = "AddOrderForm" }
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=ListCategories}/{id?}");

app.Run();
