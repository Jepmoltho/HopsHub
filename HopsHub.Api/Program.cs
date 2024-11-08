using HopsHub.Api.Data;
using HopsHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Services;
using HopsHub.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register the DB context
builder.Services.AddDbContext<BeerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add ASP.NET Core Identity Service
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BeerContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default SignIn settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//    options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//Register services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRatingsService, RatingService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IBrewerService, BrewerService>();
builder.Services.AddScoped<IAccountService, AccountService>();

//Add controllers and configure JSON serialisation to ignore cycles
builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


var app = builder.Build();

//Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentityService.SeedRoles(services);

    await IdentityService.SeedUsers(services);

    await DataSeeder.SeedData(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Setup ASP.NET login authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

