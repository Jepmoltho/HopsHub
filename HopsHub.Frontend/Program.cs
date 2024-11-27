using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HopsHub.Frontend;
using HopsHub.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("http://localhost:8080/")
    //BaseAddress = new Uri("http://backend:8080/")
    //BaseAddress = new Uri("https://localhost:7277/")
    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

//Register services
builder.Services.AddScoped<BeerService>();

//Todo: Enforce only https in frontend and backend
//Todo: Clear the hopshub.shared project reference

await builder.Build().RunAsync();

