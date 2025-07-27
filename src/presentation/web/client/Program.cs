using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TriPower;
using TriPower.Identity.Application.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Register Handler and Request services ==================

builder.Services.AddTriHandlerMediatorForClient();

builder.Services.AddHandlerRequestServicesForClient(
    TriIdentityRequestContext.ConfigureRequests
);

// End of Handler and Request services registration =======

await builder.Build().RunAsync();
