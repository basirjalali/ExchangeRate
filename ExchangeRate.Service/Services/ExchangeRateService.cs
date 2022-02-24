using ExchangeRate.Service.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Service.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private static readonly IList<string> _supportedCurrencies = new ReadOnlyCollection<string>(
            new string[]
            {
                "EUR",
                "GBP",
                "USD"
            }
        );

        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<ExchangeRateResponse> GetExchangeRateAsync(double price, string sourceCurrency, string targetCurrency)
        {
            if (price <= 0)
                throw new Exception("Price cannot be zero or negative value.");
            if (string.IsNullOrEmpty(sourceCurrency))
                throw new Exception("Source currency cannot be null or empty.");
            if (string.IsNullOrEmpty(targetCurrency))
                throw new Exception("Target Currency cannot be null or empty.");

            if (!_supportedCurrencies.Contains(sourceCurrency))
                throw new Exception("Source currency is not supported.");
            if (!_supportedCurrencies.Contains(targetCurrency))
                throw new Exception("Target Currency is not supported.");



            var exchangeRateUrl = $"{_configuration["ExchangeRate:URL"]}{_configuration["ExchangeRate:Currency:Path"]}{sourceCurrency}.json";
            var exchangeRate = await _httpClient.GetAsync(exchangeRateUrl);
            var exchangeRateContent = await exchangeRate.Content.ReadAsStringAsync();
            var exchangeRateDTO = JsonConvert.DeserializeObject<ExchangeRateDTO>(exchangeRateContent);
            return new ExchangeRateResponse
            {
                TargetCurrency = targetCurrency,
                ConvertedPrice = price * (double)exchangeRateDTO.Rates[targetCurrency.ToUpper()]
            };
        }
    }
}
