using CafesApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace CafesApi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddBsonMapper();

            string connectionString = configuration["DbConnections:CafesDb:ConnectionString"];
            string dbName = configuration["DbConnections:CafesDb:DbName"];

            services.Configure<DbSettings>(options => {
                options.ConnectionString = connectionString;
                options.DbName = dbName;
            });

            services.AddScoped<CafesDbContext>();

            return services;
        }

        public static IServiceCollection AddBsonMapper(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<Cafe>(cm => {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            BsonClassMap.RegisterClassMap<CreatedCafeDto>(cm => {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            return services;
        }
    }
}
