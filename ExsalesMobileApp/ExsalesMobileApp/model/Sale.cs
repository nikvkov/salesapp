using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.model
{
    class Sale
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        //[JsonProperty("date")]
        //public DateTime Date { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("bonus")]
        public int Bonus { get; set; }

        [JsonProperty("accepted")]
        public bool Accepted { get; set; }

    }
}
