using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HopsHub.Frontend;
using HopsHub.Frontend.Services;

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

builder.Services.AddScoped<BeerService>();

await builder.Build().RunAsync();

