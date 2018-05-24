using ExsalesMobileApp.lang;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
        //ключ авторизации
        string auth_key;
		public StartPage ()
		{
			InitializeComponent ();

            //инициализация элементов формы
            logo_image.Source = @"logo.jpg";

            //блок локализации
            en_login.Placeholder = /*LangResources.StartPageLoginPlaceholder;*/"Login";
            en_password.Placeholder = /*LangResources.StartPageLoginPassword;*/"Password";
            bt_sign_in.Text = /*LangResources.StartPageBtSignIn;*/"Sign In";
            label_register.Text = /*LangResources.StartPageLabelRegister;*/"Create new account?";
            bt_register.Text = /*LangResources.StartPageBtRegister;*/"Register";

            //тестирование
            en_login.Text = "seller@test.com";
            en_password.Text = "NYACRgFgNx";

            //приязка обработчиков событий
            bt_register.Clicked += Bt_register_Clicked;
            bt_sign_in.Clicked += Bt_sign_in_Clicked;

        }//c_tor

        //обработка нажатия на кнопку входа
        private async void Bt_sign_in_Clicked(object sender, EventArgs e)
        {
            try
            {
                //задаем url отправки
                ApiService api = new ApiService
                {
                    Url = "https://www.exsales.net/api/user/auth"
                };

                //добавляем параметы к запросу
                Dictionary<string, string> data = new Dictionary<string, string>();
                //добавляем почту
                data.Add("email", ApiService.StringUrlEncode(en_login.Text));

                //добавляем роль
                data.Add("password", en_password.Text);
                api.AddParams(data);

                if (!String.IsNullOrEmpty(en_login.Text) && !String.IsNullOrEmpty(en_password.Text))
                {
                    //отправляем запрос и ждем результат
                    AuthData res = await api.Auth();
                    //await DisplayAlert("Message", res.AuthKey, "OK");
                    //получаем ключ авторизации пользователя
                    if (res.Status)
                    {
                        auth_key = res.AuthKey;

                        await Navigation.PushModalAsync(new AccountPage(auth_key), true);
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Ошибка авторизации. Проверьте указанные данные!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Warning", "Проверьте указанные данные!", "OK");
                }

                //выводим ответ

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        //обработка нажатия на кнопку регистрации
        private async void Bt_register_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new RegisrationPage(),true);
            }
            catch(Exception ex)
            {
               await DisplayAlert("Ошибка", ex.Message, "ОK");
            
            }

        }//Bt_register_Clicked
    }//class
}//namespace