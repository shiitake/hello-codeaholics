using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Infrastructure;
using HelloCodeaholics.Services.Application;


using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("HelloCodeEntities");
builder.Services.AddDbContextPool<HelloCodeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPharmacyService, PharmacyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
