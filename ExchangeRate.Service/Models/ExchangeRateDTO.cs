using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExchangeRate.Service.Models
{
    public class ExchangeRateDTO
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("time_last_updated")]
        public long TimeLastUpdated { get; set; }


        [JsonProperty("rates")]
        public RateDTO Rates { get; set; }
    }

    public class RateDTO
    {
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(RateDTO);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(RateDTO);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }

        [JsonProperty("GBP")]
        public double GBP { get; set; }

        [JsonProperty("EUR")]
        public double EUR { get; set; }

        [JsonProperty("USD")]
        public double USD { get; set; }
    }
}
