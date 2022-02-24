using ExchangeRate.API.Controllers;
using ExchangeRate.Service.Models;
using ExchangeRate.Service.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Test
{
    public class ExchangeRateControllerTests
    {
        private readonly ExchangeRateController _exchangeRateController;
        private readonly ExchangeRateResponse exchangeRateResponse = new ExchangeRateResponse
        {
            ConvertedPrice = 1.2,
            TargetCurrency = "GBP"
        };

        public ExchangeRateControllerTests()
        {
            var exchangeRateService = new Mock<IExchangeRateService>();
            exchangeRateService.Setup(s => s.GetExchangeRateAsync(It.IsAny<double>(), It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(exchangeRateResponse);
            _exchangeRateController = new ExchangeRateController(exchangeRateService.Object);

        }

        [Fact]
        public async Task Should_return_ok_object_when_calling_get_exchange_rate()
        {
            var response = await _exchangeRateController.GetExchangeRate(1, "EUR", "GBP");

            this.ShouldSatisfyAllConditions(
                () => response.ShouldBeAssignableTo<ActionResult<ExchangeRateResponse>>(),
                () => ((OkObjectResult)(response.Result)).Value.ShouldBe(exchangeRateResponse));
        }

    }
}
