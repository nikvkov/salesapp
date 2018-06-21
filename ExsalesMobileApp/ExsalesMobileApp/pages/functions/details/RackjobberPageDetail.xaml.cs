using ExsalesMobileApp.pages.functions.components;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class RackjobberPageDetail : ContentPage
	{
		public RackjobberPageDetail ()
		{

            InitializeComponent ();

            bt_add.Text = "Add";
            bt_back.Text = "Back";

            bt_add.Clicked += Bt_add_Clicked;
            bt_back.Clicked += Bt_back_Clicked;

        }//c_tor

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddBranchPage(null),true);
        }
    }//class
}//namespace