using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PortalSystem.Models;
using PortalSystem.Repository.Abstractions;

namespace PortalSystem.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPortalRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoDatabase>(provider =>
            {
                var connectionString = "mongodb+srv://preethel:azam448040@cluster0.378gpzh.mongodb.net/";
                var mongoClient = new MongoClient(connectionString);
                return mongoClient.GetDatabase("PortalSystem");
            });

            services.AddScoped<IMongoCollection<Class>>(provider =>
            {
                var database = provider.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<Class>("Classes");
            });
            services.AddScoped<IMongoCollection<User>>(provider =>
            {
                var database = provider.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<User>("Users");
            });
            services.AddScoped<IMongoCollection<RefreshToken>>(provider =>
            {
                var database = provider.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<RefreshToken>("RefreshToken");
            });
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
