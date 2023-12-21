using HelloCodeaholics.Data;
using HelloCodeaholics.Infrastructure;
using HelloCodeaholics.Services.Application;
using HelloCodeaholics.Services.Interfaces;


using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("HelloCodeEntities");
builder.Services.AddDbContextPool<HelloCodeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(DataRepository<>));
builder.Services.AddScoped<IPharmacyService, PharmacyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

var corsOrigins = builder.Configuration["Cors:Origins"];
var originList = new List<string>();
foreach (var origin in corsOrigins.Split(','))
{
    if (!string.IsNullOrEmpty(origin))
    {
        originList.Add(origin);
    }
}
app.UseCors(builder =>
{
    builder.WithOrigins(originList.ToArray())
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
