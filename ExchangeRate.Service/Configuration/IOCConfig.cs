using System;
using System.Collections.Generic;
using System.Text;
using ExchangeRate.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRate.Service.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
            return services;
        }
    }
}
