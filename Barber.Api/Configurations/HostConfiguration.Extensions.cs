using System.Reflection;
using System.Text;
using Barber.Api.Data;
using Barber.Api.Hubs;
using Barber.Application.Auth.Services;
using Barber.Application.Barbers.Services;
using Barber.Application.Booking.Service;
using Barber.Application.Common.Settings;
using Barber.Application.Dashboard;
using Barber.Application.Dashboard.Service;
using Barber.Application.Images.Service;
using Barber.Application.Location.Service;
using Barber.Application.Payments.Click.Service;
using Barber.Application.Reviews.Services;
using Barber.Application.Servises.Sarvices;
using Barber.Application.Users.Services;
using Barber.Domain.Entities;
using Barber.Infrastructure.Auth.Services;
using Barber.Infrastructure.Barbers.Services;
using Barber.Infrastructure.Booking.Services;
using Barber.Infrastructure.Common.Caching;
using Barber.Infrastructure.Dashboard.Service;
using Barber.Infrastructure.Images.Service;
using Barber.Infrastructure.Location.Sevice;
using Barber.Infrastructure.Payments.Click.Service;
using Barber.Infrastructure.Reviews.Service;
using Barber.Infrastructure.Servises.Services;
using Barber.Infrastructure.Users.Services;
using Barber.Persistence.Caching.Brokers;
using Barber.Persistence.Caching.Models;
using Barber.Persistence.DataContexts;
using Barber.Persistence.Repositories;
using Barber.Persistence.Repositories.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
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

        // Auth
        builder.Services.AddScoped<IAuthService, AuthService>();

        // Review

        builder.Services.AddScoped<IReviewService, ReviewService>();
        // dashboard
        builder.Services.AddScoped<IDashboardService, DashboardService>();
        // Images
        builder.Services.AddScoped<IImageService, ImageService>();
        // Location
        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<ILocationRepository, LocationRepositories>();

        // payments

        builder.Services.AddScoped<IPaymentService, PaymentService>();

        builder.Services.AddScoped<TimeScheduleGenerator>();
        builder.Services.AddHttpClient();


        #region JWT Bearer

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "Lesson Auth", Version = "v1.0.0", Description = "Lesson Auth API" });
            var securitySchema = new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);
            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securitySchema, new[] { "Bearer" } }
            };
            c.AddSecurityRequirement(securityRequirement);
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = GetTokenValidationParameters(builder.Configuration);

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = (context) =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IsTokenExpired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        TokenValidationParameters GetTokenValidationParameters(ConfigurationManager configuration)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ClockSkew = TimeSpan.Zero,
            };
        }

        #endregion

        return builder;
    }

    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => { conf.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

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
        builder.Services.AddSignalR();

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
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


        builder.Services.AddSignalR();
        return builder;
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // Configure CacheSettings from the app settings.
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // Register the Memory Cache service.
        builder.Services.AddLazyCache();

        // Register the Memory Cache as a singleton.
        builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();


        return builder;
    }


    /// <summary>
    /// Enables CORS middleware in the web application pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors(options =>
        {
            options.WithOrigins("http://10.0.0.34:8081") // Faqat frontend uchun ruxsat berish
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // Cookie va auth tokenlar bilan ishlash uchun
        });

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
        // app.UseMiddleware<GlobalException>();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<BookingHub>("/bookingHub");

        return app;
    }
}