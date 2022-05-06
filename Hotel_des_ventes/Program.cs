using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<API.DataAccess.Hotel_des_ventesContext>();
builder.Services.AddAutoMapper(typeof(API.Repositories.AutomapperProfiles));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IMarketRepository, MarketRepository>();
builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMarketService, MarketService>();
builder.Services.AddTransient<IItemService, ItemService>();

builder.Services.AddAutoMapper(typeof(Hotel_des_ventes.Models.AutomapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
