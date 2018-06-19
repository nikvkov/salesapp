using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.model
{
    class Subdivision
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title{ get; set; }

        [JsonProperty("company")]
        public string Company{ get; set; }

        [JsonProperty("user")]
        public string User{ get; set; }
    }//class

}//ExsalesMobileApp.model
