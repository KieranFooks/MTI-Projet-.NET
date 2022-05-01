using API.Repositories;
using API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkSqlServer()
	.AddDbContext<API.DataAccess.Hotel_des_ventesContext>();
builder.Services.AddAutoMapper(typeof(API.Repositories.AutomapperProfiles));

builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

app.Run();