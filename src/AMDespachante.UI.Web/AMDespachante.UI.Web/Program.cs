using AMDespachante.UI.Web.Components;
using AMDespachante.UI.Web.Configuraiton;
using Hangfire;
using Microsoft.AspNetCore.Localization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "pt-BR" };
    options.SetDefaultCulture(supportedCultures[0])
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);

    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CustomRequestCultureProvider(context =>
    {
        return Task.FromResult(new ProviderCultureResult("pt-BR"));
    }));
});

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddIdentityConfig();

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddMudServices();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

// Add Hangfire services.
builder.Services.AddHangfire(builder.Configuration);

builder.Services.AddHttpClient();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/404");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHangfireDashboard();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AMDespachante.UI.Web.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();

// Add Hangfire Jobs.
app.AddHangfireJobs();

app.Run();
