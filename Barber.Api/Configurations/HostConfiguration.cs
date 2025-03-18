using Barber.Api.Middleware;
using Microsoft.AspNetCore.Diagnostics;

namespace Barber.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures application builder
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns>Application builder</returns>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMappers()
            .AddValidators()
            .AddPersistence()
            .AddIdentityInfrastructure()   
            .AddMediator()
            .AddDevTools()
            .AddExposers();

        return new(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app.UseCors();
        app.UseMiddleware<GlobalException>();
        await app.SeedDataAsync();
        app
            .UseDevTools()
            .UseExposers();

        return app;
    }
}