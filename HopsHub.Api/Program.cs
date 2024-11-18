using HopsHub.Api.Data;
using HopsHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HopsHub.Api.Services;
using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Repositories;
using HopsHub.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using DotNetEnv;
using HopsHub.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

//Get the default connection
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (defaultConnection == null) throw new InvalidOperationException("Could not get default connection");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load passwords from .env
Env.Load();
string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("Database host not set");
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("Database user not set");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("Database password not set");
string testUserPassword = Environment.GetEnvironmentVariable("TESTUSER_PASSWORD") ?? throw new InvalidOperationException("Test user password not set"); ;

//Todo: Rethink how you store the testuser password as it works differently 
//Replace the variable in connection string with loaded password from .env
defaultConnection = defaultConnection
    .Replace("{DB_HOST}", dbHost)
    .Replace("{DB_USER}", dbUser)
    .Replace("{DB_PASSWORD}", dbPassword);

//Register the DB context
builder.Services.AddDbContext<BeerContext>(options =>
    options.UseSqlServer(defaultConnection));

// Add ASP.NET Core Identity Service
builder.Services.AddIdentity <User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})    
.AddEntityFrameworkStores<BeerContext>()
.AddDefaultTokenProviders();

//Sign in settings
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//    options.SignIn.RequireConfirmedAccount = false;
//});

//Register services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRatingsService, RatingService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IBrewerService, BrewerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Add controllers and configure JSON serialisation to ignore cycles
builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

//Add ratelimiters
builder.Services.AddRateLimiter(options =>
{
    void ConfigureFixedWindowLimiter(string name, int permitLimit)
    {
        options.AddFixedWindowLimiter(name, opt =>
        {
            opt.Window = TimeSpan.FromMinutes(1);
            opt.PermitLimit = permitLimit;
            opt.QueueLimit = 2;
            opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        });
    }

    ConfigureFixedWindowLimiter("HardMaxRequestPolicy", 10);
    ConfigureFixedWindowLimiter("NormalMaxRequestPolicy", 100);
    ConfigureFixedWindowLimiter("SoftMaxRequestPolicy", 1000);
});

var app = builder.Build();

//Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await DataSeeder.SeedData(services, testUserPassword);
}

// Configure the HTTP request pipeline.

//Todo: Configure environment settings
//Todo: Add a readme file
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

app.UseRateLimiter();

app.Run();

