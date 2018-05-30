using ExsalesMobileApp.lang;
using ExsalesMobileApp.library;
using ExsalesMobileApp.services;
using Newtonsoft.Json.Linq;
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
	public partial class AccountPage : ContentPage
	{
        string personalKey;
        Person user;
		public AccountPage (JObject person)
		{
			InitializeComponent ();
            personalKey = person["auth_key"].ToString();

            user = new Person(person);

            //инициализация элементов формы
            logo_image.Source = @"logo.jpg";


            //блок локализации
            string text = "Manager:Seller";        /*LangResources.RegPageRoles;*/
            string[] roles = text.Split(new char[] { ':' });

            WelcomeLabel.Text = "Welcome, "+user.FirstName;
            RoleTextLabel.Text = "Your current role is " ;
            RoleLabel.Text = roles[user.Role];
            lb_personalDataText.Text = "Enter your personal information";
            bt_PersonalData.Text = "Personal data";
            lb_functionsText.Text = "Get Started";
            bt_Functions.Text = "Functions";
            bt_back.Text = "Back";           /*LangResources.PageBack;*/

            //раскоментировать при переводе
            /*    string text = LangResources.RegPageRoles;
                string[] roles = text.Split(new char[] { ':' });

                WelcomeLabel.Text = LangResources.AccountPageWelcomeText + user.FirstName;
                RoleTextLabel.Text = LangResources.AccountPageRoleTextLabel;
                RoleLabel.Text = roles[user.Role];
                lb_personalDataText.Text = LangResources.AccountPagePersonalDataLabelText;
                bt_PersonalData.Text = LangResources.AccountPageButtonPersonalData;
                lb_functionsText.Text = LangResources.AccountPageFunctionLabelText;
                bt_Functions.Text = LangResources.AccountPageButtonFunction;
                bt_back.Text = LangResources.PageBack;
            */

            //добавляем обработку событий
            bt_back.Clicked += Bt_Back_Clicked;
            bt_PersonalData.Clicked += Bt_PersonalData_Clicked;


        }//c_tor

        //Нажатие на кнопку назад
        private async void Bt_Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        //переход на страницу персональных данных
        private async void Bt_PersonalData_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PersonPage(user), true);
        }
    }
}