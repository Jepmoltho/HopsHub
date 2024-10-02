using HopsHub.Api.Data;
using HopsHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BeerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BeerContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();



var app = builder.Build();

//Seed test users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentityService.SeedRoles(services);

    await IdentityService.SeedUsers(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

