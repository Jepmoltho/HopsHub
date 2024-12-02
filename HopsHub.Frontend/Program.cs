using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HopsHub.Frontend;
using HopsHub.Frontend.Services;
using HopsHub.Frontend.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var environment = builder.HostEnvironment.Environment;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Development environment uses https. Docker containers in roduction use http
builder.Services.AddScoped(sp =>
{
    var baseUri = environment == "Development" ?
        "https://localhost:8080/" :
        "http://localhost:8080/";

    return new HttpClient { BaseAddress = new Uri(baseUri) };
});

builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITypeService, TypeService>();

await builder.Build().RunAsync();

