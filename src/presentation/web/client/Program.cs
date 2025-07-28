using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TriPower;
using TriPower.Identity.Application.Shared;
using TriPower.Presentation.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddTriHandlerMediatorForClient();
builder.Services.AddKernelClientServices(builder.HostEnvironment.BaseAddress);
builder.Services.AddScoped<AuthService>();

// Register Handler and Request services ==================

builder.Services.AddTriHandlerMediatorForClient();

builder.Services.AddHandlerRequestServicesForClient(
    TriIdentityRequestContext.ConfigureRequests
);

// End of Handler and Request services registration =======

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

await builder.Build().RunAsync();
