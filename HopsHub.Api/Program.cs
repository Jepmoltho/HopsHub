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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

if (environment == "Development")
{
    Env.Load("../.env");
}

string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("Database host not set");
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("Database user not set");
string dbPassword = environment == "Production"
    ? File.ReadAllText("/run/secrets/db_password").Trim()
    : Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("Database password not set");
string testUserPassword = environment == "Production"
    ? File.ReadAllText("/run/secrets/testuser_password").Trim()
    : Environment.GetEnvironmentVariable("TESTUSER_PASSWORD") ?? throw new InvalidOperationException("Test user password not set");

string defaultConnection = $"Server={dbHost},1433;Database=BeerDb;User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";

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

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        //Development environment uses https. Docker containers in Production use http.
        policy.WithOrigins(
            "https://localhost:7148",
            "http://localhost:7148") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//Todo: Delete
//Cookie authentification login/logout
//builder.Services.AddAuthentication("Cookies")
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login";
//        options.LogoutPath = "/Logout";
//        //Not sure i need this
//        //options.Cookie.HttpOnly = true;
//        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS required
//        options.Cookie.SameSite = SameSiteMode.None;
//    });

//Token authentification
var key = builder.Configuration["JwtSettings:SecretKey"];
var keyBytes = Encoding.UTF8.GetBytes(key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorization();

//Register services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IBeerService, BeerService>();
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

//Create DB and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BeerContext>();

    if (await context.Database.CanConnectAsync())
    {
        Console.WriteLine("Database exists. Applying migrations...");
        await context.Database.MigrateAsync(); 
    }
    else
    {
        Console.WriteLine("Database does not exist. Creating and seeding...");
        await context.Database.EnsureCreatedAsync(); 
        await DataSeeder.SeedData(services, testUserPassword); 
    }
}


//Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply the CORS policy
app.UseCors("AllowFrontend"); 

//Setup ASP.NET login authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.UseRateLimiter();

app.Run();

