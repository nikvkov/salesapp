using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNetworkPage : ContentPage
	{
		public AddNetworkPage ()
		{

			InitializeComponent ();

            lb_title.Text = "Add new network";
            lb_item_title.Text = "New title";
            en_item_title.Text = "Put title";
            lb_item_network.Text = "New network";

		}//c_tor
	}//class
}//namespace