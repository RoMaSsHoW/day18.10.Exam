using Exam.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Application
{
    public static class AddApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
