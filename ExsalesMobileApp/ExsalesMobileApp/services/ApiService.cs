using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
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

        // получаем результат запроса
        async Task<string> Get()
        {
            HttpClient client = GetClientJson();
            string result = await client.GetStringAsync(Url);
            //return JsonConvert.DeserializeObject<IEnumerable<Friend>>(result);
            return result;
        }

        //отправка POST запроса
        public async Task<HttpStatusCode> Post(Dictionary<string,string> data)
        {
            HttpClient client = GetClientJson();
            HttpContent content = new FormUrlEncodedContent(data);
           
            var response = await client.PostAsync(Url, content);
            return response.StatusCode;
        
        }

        //регистрация пользователя 
        public async Task<string> Registration()
        {
           var result = JsonConvert.DeserializeObject<RegistrationData>(await Get());

            if (result == null) return "Registration error";
            return result.Data;
        }

        //авторизация
        public async Task<JObject> Auth()
        {
            // var result = JsonConvert.DeserializeObject<AuthData>(await Get());
            // return result;
            JObject o = JObject.Parse(await Get());
            return o;

        }

        //данные о функции
        public async Task<string> Function()
        {
            // var result = JsonConvert.DeserializeObject<AuthData>(await Get());
            // return result;
            //JObject o = JObject.Parse(await Get());
            //return o;
            return await Get();
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

        //public class AuthData
        //{

        //    [JsonProperty("status")]
        //    public bool Status { get; set; }

        //    [JsonProperty("role")]
        //    public string Role { get; set; }


        //    [JsonProperty("auth_key")]
        //    public string AuthKey { get; set; }

        //    [JsonProperty("firstName")]
        //    public string FirstName { get; set; }

        //    [JsonProperty("lastName")]
        //    public string LastName { get; set; }

        //    [JsonProperty("email")]
        //    public string Email { get; set; }

        //    [JsonProperty("phone")]
        //    public string Phone { get; set; }

        //    [JsonProperty("company")]
        //    public string Company { get; set; }

        //    [JsonProperty("referal")]
        //    public string Referal { get; set; }

        //    [JsonProperty("functions")]
        //    public string Functions { get; set; }


        //    //public class AuthDetail
        //    //{
        //    //    [JsonProperty("role")]
        //    //    public string Role { get; set; }

        //    //    [JsonProperty("auth_key")]
        //    //    public string AuthKey { get; set; }
        //    //}

        //}


    }
}
