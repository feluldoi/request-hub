using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RequestHub.Client;
using RequestHub.Client.Services.AuthServiceClient;
using RequestHub.Client.Services.EmailServiceClient;
using RequestHub.Client.Services.FileUploadServiceClient;
using RequestHub.Client.Services.TicketServiceClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();//local storage NuGet Package


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ITicketServiceClient, TicketServiceClient>();
builder.Services.AddScoped<IAuthServiceClient, AuthServiceClient>();
builder.Services.AddScoped<IEmailServiceClient, EmailServiceClient>();
builder.Services.AddScoped<IFileUploadServiceClient, FileUploadServiceClient>();

builder.Services.AddOptions();//CustomAuthStateProvider
builder.Services.AddAuthorizationCore();//CustomAuthStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

await builder.Build().RunAsync();
