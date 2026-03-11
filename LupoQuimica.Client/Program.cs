using Blazored.LocalStorage;
using LupoQuimica.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Verifique se esta linha aponta para a porta correta da sua API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7212/") });
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();