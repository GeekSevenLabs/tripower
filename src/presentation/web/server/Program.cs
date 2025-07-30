using Scalar.AspNetCore;
using TriPower.Electrical.Application;
using TriPower.Electrical.Application.Shared;
using TriPower.Electrical.Infrastructure;
using TriPower.Identity.Application;
using TriPower.Identity.Application.Shared;
using TriPower.Identity.IoC;
using TriPower.Presentation.Web.Client.Services;
using TriPower.Presentation.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();
builder.Services.AddOutputCache();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTriHandlerMediatorForServer();
builder.Services.AddKernelServerServices();
builder.Services.AddScoped<AuthService>();

builder.Services.AddTriIdentityInfrastructure(builder.Configuration);
builder.Services.AddTriIdentityAuthenticationAndAuthorization(builder.Configuration);

// 1. Services and configurations for Identity

builder.AddHandlerRequestServicesForServer(
    requestRegistries: [
        TriIdentityRequestContext.ConfigureRequests,
        TriElectricalRequestContext.ConfigureRequests
    ],
    handlerRegistries: [
        TriIdentityHandlersContext.ConfigureHandlers,
        TriElectricalHandlersContext.ConfigureHandlers
    ],
    serviceRegistries: [
        TriIdentityServicesContext.ConfigureServices,
        
        TriElectricalServicesContext.ConfigureServices,
        TriElectricalInfrastructureServicesContext.ConfigureServices
    ],
    serializerContexts: [
        TriIdentitySerializerContext.Default,
        TriElectricalSerializerContext.Default,
    ]
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.MapScalarApiReference();
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

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseOutputCache();
app.MapStaticAssets();

// 1. Endpoints for Identity

app.MapHandlerRequestEndpoints(
    [
        TriIdentityHandlersContext.ConfigureHandlers,
        TriElectricalHandlersContext.ConfigureHandlers
    ]
);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TriPower.Presentation.Web.Client._Imports).Assembly);

app.Run();