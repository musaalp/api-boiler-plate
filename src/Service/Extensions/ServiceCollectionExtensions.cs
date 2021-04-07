using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Helpers.CryptoHelper;
using Service.Utils.Helpers;
using System.Reflection;

namespace Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            AddMediatr(services);
            AddFluentValidation(services);

            services.AddSingleton<IBinWidthCalculator, BinWdithCalculator>();
            services.AddSingleton<ICryptoHelper, CryptoHelper>();
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
