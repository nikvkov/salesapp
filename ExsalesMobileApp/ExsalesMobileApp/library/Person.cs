using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExsalesMobileApp.library
{
    public class Person
    {
        JObject jsonObj;
        public JObject JsonPersonData
        {
            get
            {
                return jsonObj;
            }
            set
            {
                jsonObj = value;
            }
        }

        //bool status;
        //public bool Status { get{ return status; } set { status = value; } }

        int role;
        public int Role { get{ return role; } set { role = value; } }

        string authKey;
        public string AuthKey { get { return authKey; } set { authKey = value; } }

        string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        int referal;
        public int Referal
        {
            get { return referal; }
            set { referal = value; }
        }

        List<FunctionData> functions;
        public List<FunctionData> Functions { get{ return functions; } set { functions = value; } }

        public Person(JObject data)
        {
            jsonObj = data;

            this.role = (int)data["role"];
            firstName =String.IsNullOrEmpty(data["firstName"].ToString())&& data["firstName"].ToString().Length==0? "User":data["firstName"].ToString();
            lastName = String.IsNullOrEmpty(data["lastName"].ToString()) && data["lastName"].ToString().Length ==0 ? "Surname": data["lastName"].ToString() ;
            authKey = data["auth_key"].ToString();
            email = data["email"].ToString();
            phone = String.IsNullOrEmpty(data["phone"].ToString()) || data["phone"].ToString().Length == 0 ? "+1 111 111 11 11": data["phone"].ToString();
            company = data["company"].ToString();
            referal = (int)data["referal"];

            functions =  JsonConvert.DeserializeObject<List<FunctionData>>(data["functions"].ToString());
            //foreach (JToken tkn in data["functions"])
            //{
            //    foreach (IJEnumerable<JToken> ter in tkn.Values())
            //    {
            //        functions.Add(ter.ToString());
            //    }
            //}
        }


        public static List<Person> GetUsers(List<JObject> res)
        {
            List<Person> data = new List<Person>();
            foreach (var item in res)
            {
                data.Add(new Person(item));
            }
            return data;
        }
    }

    public enum PersonRole
    {
        Manager,
        Seller,
        Undefined
    }

    public class FunctionData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("function")]
        public string Functions { get; set; }

        public override string ToString()
        {
            return Id+" "+Functions;
        }
    }
}
