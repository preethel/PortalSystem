using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortalSystem.Client;
using PortalSystem.Client.Services.AuthService;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    // Add authorization header here if needed
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddBlazoredLocalStorage();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
