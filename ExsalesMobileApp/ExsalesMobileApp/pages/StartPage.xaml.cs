#define DEBUG
#undef RELEASE

using ExsalesMobileApp.lang;
using ExsalesMobileApp.services;
using Newtonsoft.Json.Linq;
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
            try
            {
                InitializeComponent();

                //инициализация элементов формы
                logo_image.Source = @"logo.jpg";
                ai_ind.IsVisible = false;

                //блок локализации
#if (DEBUG)
                en_login.Placeholder = "Login";
                en_password.Placeholder = "Password";
                bt_sign_in.Text = "Sign In";
                label_register.Text = "Create new account?";
                bt_register.Text = "Register";
#else
                en_login.Placeholder = LangResources.StartPageLoginPlaceholder;
                en_password.Placeholder = LangResources.StartPageLoginPassword;
                bt_sign_in.Text = LangResources.StartPageBtSignIn;
                label_register.Text = LangResources.StartPageLabelRegister;
                bt_register.Text = LangResources.StartPageBtRegister;
#endif
                //тестирование
                en_login.Text = "mng@gmail.com";
                en_password.Text = "PfiMzeKR7o";

                //приязка обработчиков событий
                bt_register.Clicked += Bt_register_Clicked;
                bt_sign_in.Clicked += Bt_sign_in_Clicked;
            }catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }

        }//c_tor

        //обработка нажатия на кнопку входа
        private async void Bt_sign_in_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                ai_ind.IsVisible = true;
                bt_sign_in.IsVisible = false;
                bt_register.IsVisible = false;
                //задаем url отправки
                ApiService api = new ApiService
                {
                    Url = "https://www.exsales.net/api/v1/user/auth"
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
                    JObject res = await api.Auth();
              //      await DisplayAlert("Message", res/*bool.Parse(res["status"].ToString()).ToString()*/, "OK");
               //     получаем ключ авторизации пользователя
                    if (bool.Parse(res["status"].ToString()))
                    {
                        auth_key = res["auth_key"].ToString();
                        //await DisplayAlert("Message", auth_key, "OK");
                        await Navigation.PushModalAsync(new AccountPage(res), true);
                    }
                    else
                    {
#if (DEBUG)
                        await DisplayAlert("Warning", "Ошибка авторизации. Проверьте указанные данные!", "OK");

#else
                        await DisplayAlert("Warning", LangResources.StartPageEnterMessageWarning, "OK"); 
#endif
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
            finally
            {
                ai_ind.IsVisible = false;
                bt_sign_in.IsVisible = true;
                bt_register.IsVisible = true;
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