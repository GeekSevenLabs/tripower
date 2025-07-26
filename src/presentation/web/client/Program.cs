using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TriPower;
using TriPower.Identity.Application.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTriHandlerMediatorForClient();

builder.Services.AddHandlerRequestServicesForClient(
    TriIdentityRequestContext.ConfigureRequests
);

await builder.Build().RunAsync();
