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
	public partial class RegisrationPage : ContentPage
	{
		public RegisrationPage ()
		{
			InitializeComponent ();

            //инициализация элементов формы
            logo_image.Source = @"logo.jpg";

            //блок локализации
            role_pc.Title = "Role";
            role_pc.Items.Add("Manager");
            role_pc.Items.Add("Seller");
            lb_reg_text.Text = "The password will be sent to the specified email";
            en_email.Placeholder = /*LangResources.StartPageLoginPlaceholder;*/"Email";
            bt_send.Text = "Send";
            bt_back.Text = "Back";
        }//c_tor
	}//class
}//namespace