using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalSystem.Repository;
using PortalSystem.Service.ClassService;
using PortalSystem.Service.UserServices;
using System.Text;


namespace PortalSystem.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPortalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClassService, ServiceClass>();
            services.AddPortalRepositories(configuration);
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            services.AddAuthentication().AddJwtBearer(options =>
            {
                var jwtConfig = services.BuildServiceProvider().GetService<IOptions<JwtConfiguration>>()?.Value;
                if (jwtConfig != null)
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Token))
                    };
                }
                else
                {
                    throw new InvalidOperationException("JwtConfiguration is not properly configured.");
                }
            });

            return services;
        }
    }
}
