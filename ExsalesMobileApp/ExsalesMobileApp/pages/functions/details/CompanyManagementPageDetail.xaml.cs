using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExsalesMobileApp.pages.functions.components;

namespace ExsalesMobileApp.pages.functions.details
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyManagementPageDetail : ContentPage
	{
		public CompanyManagementPageDetail ()
		{
			InitializeComponent ();

            ContentView test = new ContentView();

            lb_title.Text = "Company management";
            lb_swipeInfo.Text = "To access the subfunctions, swipe from the right edge of the screen to the left";
            lb_description.Text = "Here you can manage companies";

            

            //mainContainer.Children.Add(new CompanyAdditionView());
		}
	}
}