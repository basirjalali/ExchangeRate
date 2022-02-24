using ExchangeRate.API.Controllers;
using ExchangeRate.Service.Models;
using ExchangeRate.Service.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq.Protected;
using System.Threading;

namespace ExchangeRate.Test
{
    public class ExchangeRateServiceTests
    {
        private readonly ExchangeRateService _exchangeRateService;
        private readonly IConfiguration _configuration;
        private readonly ExchangeRateDTO exchangeRateDTO = new ExchangeRateDTO
        {
            Base = "GBP",
            Date = DateTime.Now,
            TimeLastUpdated = 1,
            Rates = new RateDTO
            {
                EUR = 1.168852,
                GBP = 1,
                USD = 1.384935
            }

        };

        public ExchangeRateServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "ExchangeRate:URL", "https://www.test.com/" },
                { "ExchangeRate:Currency:Path", "test" }
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();


            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(exchangeRateDTO)),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            var exchangeRateService = new Mock<IExchangeRateService>();
            _exchangeRateService = new ExchangeRateService(httpClient, _configuration);

        }

        [Fact]
        public async Task Should_return_result_when_calling_get_exchange_rate()
        {
            var response = await _exchangeRateService.GetExchangeRateAsync(1, "GBP", "USD");
            var expectedResponse = new ExchangeRateResponse
            {
                ConvertedPrice = 1.384935,
                TargetCurrency = "USD"
            };
            response.ShouldNotBeNull();
            response.TargetCurrency.ShouldBe(expectedResponse.TargetCurrency);
            response.ConvertedPrice.ShouldBe(expectedResponse.ConvertedPrice);
        }

    }
}
