using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TriPower;
using TriPower.Identity.Application.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddTriHandlerMediatorForClient();
builder.Services.AddKernelClientServices();

// Register Handler and Request services ==================

builder.Services.AddTriHandlerMediatorForClient();

builder.Services.AddHandlerRequestServicesForClient(
    TriIdentityRequestContext.ConfigureRequests
);

// End of Handler and Request services registration =======

// HttpClient configuration ===============================
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
// End of HttpClient configuration ========================

await builder.Build().RunAsync();
