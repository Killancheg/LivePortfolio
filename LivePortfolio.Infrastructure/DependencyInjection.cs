using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using LivePortfolio.Infrastructure.Identity;

namespace LivePortfolio.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgresDb(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString =
                configuration.GetConnectionString("AppDbConnection")
                    ?? throw new InvalidOperationException("Connection string"
                    + "'AppDbConnection' not found.");


            //DataBase
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("LivePortfolio.Infrastructure")));

            return services;
        }

        public static IServiceCollection AddAppIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&$%";
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }
    }
}
