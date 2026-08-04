using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using QuestPDF.Infrastructure;
using VidrieriaPresupuestos.Web.Components;
using VidrieriaPresupuestos.Web.Data;
using VidrieriaPresupuestos.Web.Services;

QuestPDF.Settings.License = LicenseType.Community;

var culturaApp = new CultureInfo("es-CL");
CultureInfo.DefaultThreadCurrentCulture = culturaApp;
CultureInfo.DefaultThreadCurrentUICulture = culturaApp;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();

// Add services to the container.
builder.Services.AddDbContext<VidrieriaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VidrieriaDatabase")));

builder.Services.AddMudServices();
builder.Services.AddScoped<PresupuestoPdfService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
