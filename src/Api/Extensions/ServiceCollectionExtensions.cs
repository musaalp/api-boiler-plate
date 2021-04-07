using Data.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Helpers.SerializerHelper;

namespace Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IOrderDbConnection>(s =>
                new OrderDbConnection(configuration.GetConnectionString("OrderDbConnection")));

            services.AddSingleton<ISerializerHelper, SerializerHelper>();
        }
    }
}
