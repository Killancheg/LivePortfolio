using LivePortfolio.Infrastructure;
using LivePortfolio.Web.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    var connectionString =
    configuration.GetConnectionString("AppDbConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'AppDbConnection' not found.");

    services.AddRazorComponents().AddInteractiveServerComponents();

    //DataBase
    services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, 
        b => b.MigrationsAssembly("LivePortfolio.Infrastructure")));
}