using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HopsHub.Frontend;
using HopsHub.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Todo: Setup baseadress and CORS policy in different environments
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("http://localhost:8080/")
});

//Register services
builder.Services.AddScoped<BeerService>();

//Todo: Enforce https only in frontend and backend
//Todo: Clear the hopshub.shared project reference

await builder.Build().RunAsync();

