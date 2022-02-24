using ExchangeRate.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Service.Services
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> GetExchangeRateAsync(double price, string sourceCurrency, string targetCurrency);
    }
}
