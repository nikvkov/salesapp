using ExsalesMobileApp.library;
using ExsalesMobileApp.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static string URL_GET_SUBDIVISION = "https://www.exsales.net/api/v1/subdivision/get";
        public static string URL_ADD_SUBDIVISION = "https://www.exsales.net/api/v1/subdivision/add";
        public static string URL_EDIT_SUBDIVISION = "https://www.exsales.net/api/v1/subdivision/edit";
        public static string URL_REMOVE_SUBDIVISION = "https://www.exsales.net/api/v1/subdivision/remove";
        public static string URL_COMPANY_AT_TYPE = "https://www.exsales.net/api/v1/company/by-type";
        public static string URL_USERS_AT_COMPANY = "https://www.exsales.net/api/v1/user/all-in-company";
        public static string URL_GET_RETAIL = "https://www.exsales.net/api/v1/company/retails";
    

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
        public async Task<List<FunctionData>> Function()
        {
            JObject o = JObject.Parse(await Get());
            var result = JsonConvert.DeserializeObject<List<FunctionData>>(o["data"].ToString());
            return result;

        }

        //получаем список стран
        public async Task<List<CountryData>> GetCountries()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<List<CountryData>>(o["data"].ToString());
        }

        //получаем список городов
        public async Task<List<CityData>> GetCities()
        {
            JObject o = JObject.Parse(await Get());
           // if ((bool)o["status"])
           // {
                return JsonConvert.DeserializeObject<List<CityData>>(o["data"].ToString());
           // }
          //  else return null;
        }

        //получаем список типов компаний
        public async Task<List<CompanyType>> GetCompanyTypes()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<List<CompanyType>>(o["data"].ToString());
        }

        //получаем список кампаний
        public async Task<ObservableCollection<CompanyData>> GetCompany()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<ObservableCollection<CompanyData>>(o["data"].ToString());
        }

        //получаем список пользователей для кампании
        public async Task<ObservableCollection<SubdivisionUser>> GetSubdivisionUsers()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<ObservableCollection<SubdivisionUser>>(o["data"].ToString());
        }

        //получаем список подразделений для кампании
        public async Task<ObservableCollection<Subdivision>> GetSubdivisions()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<ObservableCollection<Subdivision>>(o["data"].ToString());
        }

        //удаление кампании
        public async Task<JObject> DelCompany()
        {
            JObject o = JObject.Parse(await Get());
            return o;
        }

        //отправка get запроса
        public async Task<JObject> GetRequest()
        {
            JObject o = JObject.Parse(await Get());
            return o;
        }

        //получаем список торговых сетей
        public async Task<List<Network>> GetNetworks()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<List<Network>>(o["data"].ToString());
        }

        //получаем список торговых точек
        public async Task<List<RetailPoint>> GetRetailPoints()
        {
            JObject o = JObject.Parse(await Get());
            return JsonConvert.DeserializeObject<List<RetailPoint>>(o["data"].ToString());
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

        public class CountryData
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }
        }

        public class CityData
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }
        }

        public class CompanyType
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            public override string ToString()
            {
                return Type;
            }
        }

        public class SubdivisionUser
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("firstname")]
            public string Firstname { get; set; }

            [JsonProperty("lastname")]
            public string Lastname { get; set; }

            public override string ToString()
            { 
                return Lastname + " " + Firstname;
            }
        }

        public class CompanyData
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }

            public override string ToString()
            {
                return Title/*+" : "+ Type*/;
            }
        }

    }
}
