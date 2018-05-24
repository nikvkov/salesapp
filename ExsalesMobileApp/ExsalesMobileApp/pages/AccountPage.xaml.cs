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
		public AccountPage (string apiKey)
		{
			InitializeComponent ();
            personalKey = apiKey;
            testLabel.Text = personalKey;
		}
	}
}