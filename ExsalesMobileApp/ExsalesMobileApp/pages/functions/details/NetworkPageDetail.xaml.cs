using ExsalesMobileApp.pages.functions.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions.details
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkPageDetail : ContentPage
	{
		public NetworkPageDetail ()
		{
			InitializeComponent ();

            lb_title.Text = "Distribution Management";
            bt_add.Text = "Add";
            bt_back.Text = "Back";

            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Clicked += Bt_add_Clicked;

		}//c_tor

        //нажатие на кнопку добавить
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddNetworkPage(), true);
        }

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)=> await Navigation.PopModalAsync(true);

    }//class
}//namespace