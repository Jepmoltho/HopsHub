using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HopsHub.Frontend;
using HopsHub.Frontend.Services;
using HopsHub.Frontend.Services.Interfaces;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using static System.Formats.Asn1.AsnWriter;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var environment = builder.HostEnvironment.Environment;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

//Development environment uses https. Docker containers in roduction use http
builder.Services.AddScoped(sp =>
{
    //using var scope = sp.CreateScope();

    //var localStorage = scope.ServiceProvider.GetRequiredService<ILocalStorageService>();

    //var authTokenTask = localStorage.GetItemAsync<string>("authToken");

    var baseUri = environment == "Development" ?
        "https://localhost:8080/" :
        "http://localhost:8080/";

    return new HttpClient
    {
        BaseAddress = new Uri(baseUri),
        DefaultRequestHeaders = { { "Accept", "application/json" } }
    };
});


// Determine baseUri logic based on the environment
//var baseUri = environment == "Development"
//    ? "https://localhost:8080/"
//    : "http://localhost:8080/";

// Register HttpClient as a singleton
//builder.Services.AddSingleton(sp =>
//{
//    var httpClient = new HttpClient
//    {
//        BaseAddress = new Uri(baseUri),
//        DefaultRequestHeaders = { { "Accept", "application/json" } }
//    };

//    // Set default headers
//    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//    return httpClient;
//});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<AuthenticationStateService>();

//Preserve logged in state after refreshing page
//var authenticationStateService = builder.Services.BuildServiceProvider().GetService<AuthenticationStateService>();
//await authenticationStateService.InitializeAsync();

await builder.Build().RunAsync();

