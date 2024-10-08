using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var env = builder.HostEnvironment;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7244") });

builder.Services.AddHttpClient("AssessmentApi", option =>
{
    option.BaseAddress = new Uri("https://localhost:7244");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddExtensions();

await builder.Build().RunAsync();
