using ExsalesMobileApp.library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.model
{
    class User
    {
        
        [JsonProperty("id")]
        public int Id { get; set; }

        string firstname;
        [JsonProperty("firstname")]
        public string Firstname { get { return firstname; } set { firstname = value == "" || value == null ? "Username" : value; } }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        string phone;
        [JsonProperty("phone")]
        public string Phone { get { return phone; } set { phone = value == "" || value == null ? "phone not added" : value; } }

      ////  [JsonProperty("functions")]
      // // [JsonArray(true, Type.GetType("FunctionData"), "functions")]
      //  [JsonArray(true, NamingStrategyType = typeof(FunctionData),)]
      //  public List<FunctionData> Functions { get; set; }

        public override string ToString()
        {
            return Lastname;
        }


    }//class
}//namespase
