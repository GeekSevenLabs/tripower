using TriPower.Identity.Application;
using TriPower.Identity.Application.Shared;
using TriPower.Identity.IoC;
using TriPower.Presentation.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddTriHandlerMediatorForServer();
builder.Services.AddKernelServerServices();
builder.Services.AddTriIdentityInfrastructure(builder.Configuration);

// 1. Services and configurations for Identity

builder.AddHandlerRequestServicesForServer(
    requestRegistries: [TriIdentityRequestContext.ConfigureRequests],
    handlerRegistries: [TriIdentityHandlersContext.ConfigureHandlers],
    serviceRegistries: [TriIdentityServicesContext.ConfigureServices]
);

var app = builder.Build();

// 1. Endpoints for Identity

app.MapHandlerRequestEndpoints(
    [TriIdentityHandlersContext.ConfigureHandlers]
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForErrors: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TriPower.Presentation.Web.Client._Imports).Assembly);

app.Run();