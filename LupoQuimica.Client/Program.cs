using Blazored.LocalStorage;
using LupoQuimica.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUri = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:7212"
    : "https://lupo-quimica-production.up.railway.app/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUri) });
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();