using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortalRentCar.Client;
using PortalRentCar.Client.Auth;
using Scrutor;
using Syncfusion.Blazor;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BackendUrl")!) });

var backendUrl = builder.Configuration.GetValue<string>("BackendUrl");

if (string.IsNullOrEmpty(backendUrl))
{
    throw new ArgumentNullException("BackendUrl", "The BackendUrl configuration value is missing or empty.");
}

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(backendUrl) });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();
builder.Services.AddBlazorBootstrap();
builder.Services.AddSyncfusionBlazor();


builder.Services.Scan(selector => selector
    .FromAssemblies(Assembly.GetExecutingAssembly())
    .AddClasses(false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime());

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
