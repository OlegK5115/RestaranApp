using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RestaranApp;
using RestaranApp.Services;

var builder = WebApplication.CreateBuilder(args);

// получение строки подключения из conf файла, добавление бд
var connectionString = builder.Configuration.GetConnectionString("Base");
builder.Services.AddDbContext<RestaranContext>(o => o.UseNpgsql(connectionString));

// добавление сервисов
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRestaranService, RestaranService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// добавление контроллеров
builder.Services.AddControllers();

var app = builder.Build();

// обработка ошибок
app.UseMiddleware<ExceptionMiddleware>();

app.Environment.EnvironmentName = "Production";

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();