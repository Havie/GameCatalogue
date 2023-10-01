using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GameCatalogue.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//was the client
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//change to the server (GameCatalogue.Server->Properties->launchSettings.json->profiles applicationUrl)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5030") });
//inject a "static" instance of the GameClient class. Basically a singleton
builder.Services.AddScoped<GameClient>();

await builder.Build().RunAsync();
