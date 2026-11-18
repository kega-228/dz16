using WebApiP41.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Middleware;
using WebApiP41.Models.DTO;
using WebApiP41.Services;
using System.Reflection;
using System.IO;
using System.Collections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite(connectionString)); // Використовуємо SQLite

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

// додати контролер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Компілятор кладе XML-файл поруч із dll: <ІмʼяПроєкту>.xml
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddSingleton<RouteCounterService>(sp =>
{
    var dataFile =builder.Configuration["RouteCounterService:RouteCounterDataFilePath"];
    return new RouteCounterService(dataFile);
});

builder.Services.AddOpenApi();
builder.Services.AddScoped<UserService>();




var app = builder.Build();
app.UseMiddleware<CheckUsersMiddleware>();
app.UseMiddleware<RouteCounterMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "Messages API V1");
       
    });
}

app.UseStaticFiles();
app.MapControllers();
app.Run();


