using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.model
{
    class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("company_id")]
        public int CompanyId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("ean")]
        public string EAN { get; set; }

    }
}
