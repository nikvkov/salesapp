using ExsalesMobileApp.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
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

            //приязка обработчиков событий
            bt_register.Clicked += Bt_register_Clicked;

        }//c_tor

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