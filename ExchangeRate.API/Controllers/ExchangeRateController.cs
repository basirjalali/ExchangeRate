using ExchangeRate.Service.Models;
using ExchangeRate.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("{price}/{sourceCurrency}/{targetCurrency}")]
        public async Task<ActionResult<ExchangeRateResponse>> GetExchangeRate(double price, string sourceCurrency, string targetCurrency) => Ok(await _exchangeRateService.GetExchangeRateAsync(price, sourceCurrency, targetCurrency));

    }
}
