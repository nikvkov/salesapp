using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.model
{
    class Branch
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("company_id")]
        public int CompanyId { get; set; }

        [JsonProperty("retailer_id")]
        public int RetailerId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("retailer")]
        public string Retailer { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
