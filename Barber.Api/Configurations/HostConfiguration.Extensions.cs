using System.Reflection;
using System.Text;
using Barber.Application.Barbers.Services;
using Barber.Application.Booking.Service;
using Barber.Application.Servises.Sarvices;
using Barber.Application.Users.Services;
using Barber.Domain.Entities;
using Barber.Infrastructure.Barbers.Services;
using Barber.Infrastructure.Booking.Services;
using Barber.Infrastructure.Servises.Services;
using Barber.Infrastructure.Users.Services;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunarmand.Application.Common.Settings;

namespace Barber.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        builder.Services.AddValidatorsFromAssemblies(Assemblies);        
        return builder;
    }
    
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }
    
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));
        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // user
        builder.Services.AddScoped<IUserRepository, UserRepositories>();
        builder.Services.AddScoped<IUserService, UserService>();
        // barber
        builder.Services.AddScoped<IBarberService, BarberService>();
        builder.Services.AddScoped<IBarberRepository, BarberRepositories>();
        // Service
        builder.Services.AddScoped<IService, Servicee>();
        builder.Services.AddScoped<IServiceRepository, ServiceRepositories>();
        // Booking
        builder.Services.AddScoped(typeof(IBookingService), typeof(BookingService));
        builder.Services.AddScoped(typeof(IBookingRepositoriess), typeof(BookingRepositories));
        
        return builder;
    }
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => {conf.RegisterServicesFromAssemblies(Assemblies.ToArray());});
        
        return builder;
    }

    /// <summary>
    /// Adds client-related infrastructure services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>Application builder</returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
    
    /// <summary>
    ///  Configures exposers including controllers and routing.
    /// </summary>
    /// <param name="builder">>Application builder</param>
    /// <returns>Application builder</returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(
            options => { options.SuppressModelStateInvalidFilter = true; }
        );
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }


    /// <summary>
    /// Enables CORS middleware in the web application pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");

        return app;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}