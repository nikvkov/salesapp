#define DEBUG
#undef RELEASE

using ExsalesMobileApp.lang;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ExsalesMobileApp.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisrationPage : ContentPage
	{
		public RegisrationPage ()
		{
			InitializeComponent ();

            //инициализация элементов формы
            logo_image.Source = @"logo.jpg";

            //блок локализации
#if (DEBUG)
            role_pc.Title = "Role";

            string text = "Manager:Seller";       
            string[] roles = text.Split(new char[] {':'});
            foreach (var item in roles)
            {
                role_pc.Items.Add(item);
            }

            lb_reg_text.Text = "The password will be sent to the specified email"; 
             en_email.Placeholder = "Email";
            bt_send.Text = "Send";         
            bt_back.Text = "Back";   

            role_pc.Title = "Role";
#else
            //перевод
            string text = LangResources.RegPageRoles;
            string[] roles = text.Split(new char[] { ':' });
            foreach (var item in roles)
            {
                role_pc.Items.Add(item);
            }

            lb_reg_text.Text = LangResources.RegPageLabelRegText;
            en_email.Placeholder = LangResources.RegPageEmailPlaceholder;
            bt_send.Text = LangResources.RegPageBtSend;
            bt_back.Text = LangResources.PageBack;
#endif

            //добавление обработчиков событий
            bt_back.Clicked += Bt_back_Clicked;
            bt_send.Clicked += Bt_send_Clicked;

        }//c_tor

        //обработка нажатия на кнопку отправить
        private async void Bt_send_Clicked(object sender, EventArgs e)
        {

            //activ_ind.IsRunning = true;
            //activ_ind.Color = Color.AliceBlue;
            //Thread.Sleep(3000);
            //activ_ind.IsRunning = false;
            try
            {
                //задаем url отправки
                ApiService api = new ApiService
                {
                    Url = "https://www.exsales.net/api/v1/user/registration"
                };

                //добавляем параметы к запросу
                Dictionary<string, string> data = new Dictionary<string, string>();
                //добавляем почту
                data.Add("email", ApiService.StringUrlEncode(en_email.Text));

                //добавляем роль
                // data.Add("role",role_pc.SelectedIndex.ToString());
                data.Add("role", "0");
                api.AddParams(data);

                if (!String.IsNullOrEmpty(en_email.Text) /*&& role_pc.SelectedIndex !=-1*/)
                {
                    //отправляем запрос и ждем результат
                    string res = await api.Registration();
                    await DisplayAlert("Message", res, "OK");
                }
                else
                {
                    //await DisplayAlert("Warning", LangResources.RegPageDataError, "OK");
                    await DisplayAlert("Warning", "Проверьте указанные данные и выберите роль!", "OK");
                }
              
                //выводим ответ
                
            }
            catch (Exception ex)
            {
               await DisplayAlert("Error", ex.Message, "OK");
            }

        }

        //обработка нажатия на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }//class
}//namespace