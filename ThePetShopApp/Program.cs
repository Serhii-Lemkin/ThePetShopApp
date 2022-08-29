using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Repositories;
using ThePetShopApp.Servises;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddTransient<IDbDataRepository, DbDataRepository>();
builder.Services.AddTransient<IImageManager, ImageManager>();
builder.Services.AddTransient<IDataManagerService, DataManagerService>();
builder.Services.AddDbContext<AnimalContext>(options => options.UseSqlServer(connectionString));

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
