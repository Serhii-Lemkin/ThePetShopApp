using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Repositories;
using ThePetShop.Servises.Interface;
using ThePetShop.Servises.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<IDbDataRepository, DbDataRepository>();
builder.Services.AddTransient<IImageManager, ImageManager>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAnimalService, AnimalService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IFilteringService, FilteringService>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)    
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AnimalContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
