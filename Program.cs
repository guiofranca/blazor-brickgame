using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor;
using Services;
using Models;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<GameEngine>();
builder.Services.AddScoped<Ball>();
builder.Services.AddScoped<Board>();
builder.Services.AddScoped<Bar>();

await builder.Build().RunAsync();
