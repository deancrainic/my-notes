using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyNotes.Domain.Entities;
using MyNotes.Infrastructure.Data;
using System.Text;
using Microsoft.OpenApi.Models;

namespace MyNotes.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services
                .AddIdentityCore<User>(q =>
                {
                    q.Password.RequireDigit = true;
                    q.Password.RequiredLength = 8;
                    q.Password.RequireLowercase = true;
                    q.Password.RequireUppercase = true;
                    q.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
                });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder
                .AddEntityFrameworkStores<MyNotesDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = configuration.GetSection("Jwt:Key").Value;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidAudience = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                    options.SaveToken = true;
                });
        }

        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                                    Enter 'Bearer' [space] and then your token in the next input below.
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "0auth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyNotes", Version = "v1" });
            });
        }
    }
}
