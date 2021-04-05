using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            AddMediatr(services);
            AddFluentValidation(services);
        }

        private static void AddMediatr(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        private static void AddFluentValidation(IServiceCollection services)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
        }
    }
}
