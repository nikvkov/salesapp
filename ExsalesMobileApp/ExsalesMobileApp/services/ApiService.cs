using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExsalesMobileApp.services
{
    class ApiService
    {
        string url; //адрес запроса

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new Exception("Bad request url");
                url = value;
            }
        }

        // настройка клиента
        private HttpClient GetClientJson()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        // получаем всех друзей
        async Task<string> Get()
        {
            HttpClient client = GetClientJson();
            string result = await client.GetStringAsync(Url);
            //return JsonConvert.DeserializeObject<IEnumerable<Friend>>(result);
            return result;
        }

        //регистрация пользователя 
        public async Task<string> Registration()
        {
           var result = JsonConvert.DeserializeObject<RegistrationData>(await Get());

            if (result == null) return "Registration error";
            return result.Data;
        }

        //авторизация
        public async Task<AuthData> Auth()
        {
             var result = JsonConvert.DeserializeObject<AuthData>(await Get());
             return result;
           // var detail = JsonConvert.DeserializeObject<AuthDetail>(result.Data);

           //  return detail;
            //return await Get();
        }

        //добавляем параметры к запросу
        public void AddParams(Dictionary<string,string> data)
        {
            if (data.Count == 0) return;
            this.url += "?";
            int temp = 1;
            foreach (var item in data)
            {
                if (temp != 1) this.url += "&";
                this.url += item.Key + "=" + item.Value;
                temp += 1;
            }
        }

        //кодируем строку
        public static string StringUrlEncode(string url)
        {
            return System.Net.WebUtility.UrlEncode(url);
            //return System.Web.HttpUtility.UrlEncode(url,Encoding.UTF8);
            //return url;
        }


        class RegistrationData
        {

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }

        }
        public class AuthData
        {

            [JsonProperty("status")]
            public bool Status { get; set; }

            [JsonProperty("role")]
            public string Role { get; set; }


            [JsonProperty("auth_key")]
            public string AuthKey { get; set; }

            //public class AuthDetail
            //{
            //    [JsonProperty("role")]
            //    public string Role { get; set; }

            //    [JsonProperty("auth_key")]
            //    public string AuthKey { get; set; }
            //}

        }


    }
}
