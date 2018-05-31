#define DEBUG
#undef RELEASE

using ExsalesMobileApp.lang;
using ExsalesMobileApp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExsalesMobileApp.pages.functions;

namespace ExsalesMobileApp.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FunctionsPage : ContentPage
	{
        Person user;
		public FunctionsPage (Person _user)
		{
			InitializeComponent ();
            user = _user;

            /*Блок перевода*/
#if(DEBUG)
            lb_titlePage.Text = "The functions available to you,"+" " + user.FirstName;
            bt_back.Text = "Back";
            string text = "Company management:Management of a distribution network:Management of a retail location:Personnel management:Product/service management:Bonus point management:Sales:Sales monitoring:Retail location organization (regjobber)";
#else
            lb_titlePage.Text = LangResources.FunctionsPageTitleLabelText + " " + user.FirstName;
            bt_back.Text = LangResources.PageBack;
            string text = LangResources.FunctionsPageFunctionsNames;
#endif

            var functions = text.Split(new char[] { ':' });

            foreach (var item in user.Functions)
            {
                TagButton temp = new TagButton { Text = functions[item.Id - 1], Tag=item.Id};
                temp.Clicked += bt_Function_Clicked;
                functionsContainer.Children.Add(temp);
            }

            /*Обработчики событий*/
            bt_back.Clicked += Bt_back_Clicked;

        }//c_tor

        //нажатие на кнопку функции
        private async void bt_Function_Clicked(object sender, EventArgs e)
        {

            int id = ((TagButton)sender).Tag;
            switch (id)
            {
                case 1:
                    await Navigation.PushModalAsync(new CompanyManagementPage(user,id ), true);
                    break;
                default:
                    await Navigation.PopModalAsync();
                    break;
            }
        }

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }//class
}//namespace