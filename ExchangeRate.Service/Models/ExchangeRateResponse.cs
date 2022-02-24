using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Service.Models
{
    public class ExchangeRateResponse
    {
        public double ConvertedPrice { get; set; }
        public string TargetCurrency { get; set; }
    }
}
