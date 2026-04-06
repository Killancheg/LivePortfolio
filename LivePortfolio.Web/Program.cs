using LivePortfolio.Infrastructure;
using LivePortfolio.Infrastructure.Identity;
using LivePortfolio.Web;
using LivePortfolio.Web.Components;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApp(app);
await SeedData(app.Services);

app.Run();

static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddRazorComponents().AddInteractiveServerComponents();

    services.AddPostgresDb(configuration);

    services.AddAppIdentity();
}

static void ConfigureApp(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseAntiforgery();
    app.MapStaticAssets();

    app.MapIdentity();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
}

static async Task SeedData(IServiceProvider serviceProvider)
{
    using IServiceScope scope = serviceProvider.CreateScope();

    await RoleSeeder.SeedAsync(scope.ServiceProvider);
}