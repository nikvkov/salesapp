using Newtonsoft.Json;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using ExsalesMobileApp.services;

namespace ExsalesMobileApp.model
{
    class CustomNotification
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        public List<string> Pictures { get; set; }


        static public CustomNotification GetNotice(JObject _obj)
        {
            //return new CustomNotification
            //{
            //    Id = (int)_obj["data"]["id"],
            //    Header = _obj["data"]["header"].ToString(),
            //    Content = _obj["data"]["content"].ToString(),
            //    Media = _obj["data"]["media"].ToString(),
            //    Date = (DateTime)_obj["data"]["date"]
            //};
            return ApiService.GetCurrentNotification(_obj);
        }

        public override string ToString()
        {
            return Header;
        }
    }//class
}//namespace
