using Microsoft.EntityFrameworkCore;
using Mordor.Domain.Interfaces;
using Mordor.Infrastructure.Repositories;
using Mordor.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<DapperDbContext>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new DapperDbContext(configuration);
});

// Register UserRepository as a scoped service
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add controllers
builder.Services.AddControllers();

// Configure Entity Framework DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
