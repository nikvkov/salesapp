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


        public string Company{ get; set; }
        public string User{ get; set; }
    }//class

}//ExsalesMobileApp.model
