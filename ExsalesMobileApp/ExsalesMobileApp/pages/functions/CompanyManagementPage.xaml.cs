#define DEBUG
#undef RELEASE

using ExsalesMobileApp.library;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyManagementPage : ContentPage
	{
        Person user;
        int functionId = 0;
		public CompanyManagementPage (Person _user, int _id)
		{
			InitializeComponent ();
            user = _user;
            functionId = _id;

            lb_title.Text = functionId.ToString();

            var x = GetFunctionData();

            DisplayAlert("", x.ToString(), "OK");

        }//c_tor

        async Task<string> GetFunctionData()
        {
            try
            {
                //задаем url отправки
                ApiService api = new ApiService
                {
                    Url = "https://www.exsales.net/api/v1/user/functions"
                };

                //добавляем параметы к запросу
                Dictionary<string, string> data = new Dictionary<string, string>();
                //добавляем идентификатор пользователя
                data.Add("auth_key", user.AuthKey);

                //идентификатор функции
                data.Add("func", functionId.ToString() );
                api.AddParams(data);

                if (!String.IsNullOrEmpty(user.AuthKey) && functionId!=0)
                {
                    //отправляем запрос и ждем результат
                    string res = await api.Function();
                    return res;
                }
                else
                {
                    await DisplayAlert("Warning", "Ошибка идентификаторов", "OK");
                    return null;
                }

                //выводим ответ

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }
	}
}