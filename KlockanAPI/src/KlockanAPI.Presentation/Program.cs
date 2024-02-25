using Asp.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
//using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation.AspNetCore;
using Mapster;

using KlockanAPI.Infrastructure;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.CrossCutting.Authentication;
using KlockanAPI.Application;
using KlockanAPI.Presentation.Middlewares;
using KlockanAPI.Application.Mappings;

namespace KlockanAPI.Presentation;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder = ConfigureServicesAndMiddlewares(builder);


        var app = builder.Build();
        app.UseResponseCompression();
        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler();
        app.UseCors(c =>
        {
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
            if(allowedOrigins is not null)
                c.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
        });

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHealthChecks("/health");
        app.Run();
    }

    public static WebApplicationBuilder ConfigureServicesAndMiddlewares(WebApplicationBuilder builder)
    {
        // ***********  API CONTROLLERS AND RESPONSES ************

        builder.Services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;
                configure.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                configure.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                configure.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            }).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

        // ***********  FLUENT VALIDATION ************
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // ***********  API SWAGGER GEN ************
        builder.Services.AddSwaggerGen(c =>
        {
            // General information
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Klockan API",
                Version = "v1",
                Description = "This API lets you access to Klockan Project.",
                Contact = new()
                {
                    Name = "Klockan Project",
                    Email = "klockanporject@gmail.com"
                },
                License = new()
                {
                    Name = "MIT License",
                },
            });
/*
            // Keycloak
            c.AddSecurityDefinition("KeyCloakBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
                In = ParameterLocation.Header
            });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        Array.Empty<string>()
                    }
            };

            c.AddSecurityRequirement(securityRequirement);
*/
        });

        // ***********  API VERSIONING ************
        var apiVersioningBuilder = builder.Services.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;

        });
        apiVersioningBuilder.AddApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        // ***********  GZIP COMPRESSION ************
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });

        // ***********  KEYCLOAK ************
        builder.Services.AddKeyCloakJWTAuthentication(builder.Configuration.GetSection("KeyCloakJwt"), builder.Environment);

        // ***********  KEYCLOAK ************
        builder.Services.AddMappingRegistrations();

        // ***********  EXCEPTIONS ************
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        // ***********  DEPENDENCY INJECTION ************
        builder.Services.AddApplicationServices();
        builder.Services.AddInfraestructureRepositories();

        // ***********  HEALTHCHECK ************
        builder.Services.AddHealthChecks();

        // ***********  DBCONTEXT ************
        builder.Services.AddDbContext<KlockanContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        return builder;
    }
}
