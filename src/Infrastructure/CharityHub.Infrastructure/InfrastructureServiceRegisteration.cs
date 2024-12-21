using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;
using CharityHub.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CharityHub.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {

            //Swagger Gn
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v2", new OpenApiInfo
                    {
                        Title = "CharityHub API",
                        Version = "v2",
                        Description = "CharityHub API by AE",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Ahmed Elbaradey",
                            Email = "elbaradeyahmed1985@gmail.com",
                            Url = new Uri("https://linkedin.com/ahmedelbaradey"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "CharityHub API LICX",
                            Url = new Uri("https://example.com/license"),
                        }
                    });
               
                s.EnableAnnotations();

                s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "Jwt Authentication header using the Bearer scheme (....)"
                ,
                    Name = "Authorization"
                ,
                    In = ParameterLocation.Header
                ,
                    Type = SecuritySchemeType.ApiKey
                ,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });


                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtBearerDefaults.AuthenticationScheme
                         }
                     },
                     Array.Empty<string>()
                   }
                });
            });


            return services;
        }
    }
}
