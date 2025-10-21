using Exam.Application;
using Exam.Application.Models;
using Exam.Domain.Entities;
using Exam.Infrastructure;
using Exam.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Exam.Api.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);

            ConfigureIdentity(services);

            services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));

            ConfigureJwtAuthenticationAndAuthorization(services, configuration);

            services.AddApplicationServices();

            return services;
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ExamDbContext>()
            .AddDefaultTokenProviders();
        }

        private static void ConfigureJwtAuthenticationAndAuthorization(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();

            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
            {
                throw new InvalidOperationException("JWT settings are not configured properly.");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = signingKey
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var json = JsonSerializer.Serialize(new
                            {
                                errors = new
                                {
                                    message = "Пользователь не авторизован. Пожалуйста, войдите в систему."
                                }
                            });

                            await context.Response.WriteAsync(json);
                        },

                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var json = JsonSerializer.Serialize(new
                            {
                                errors = new
                                {
                                    message = "Доступ запрещён. У вас нет прав для выполнения этого действия."
                                }
                            });

                            await context.Response.WriteAsync(json);
                        },

                        OnAuthenticationFailed = async context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var json = JsonSerializer.Serialize(new
                            {
                                errors = new
                                {
                                    message = "Ошибка аутентификации: " + context.Exception.Message
                                }
                            });

                            await context.Response.WriteAsync(json);
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
