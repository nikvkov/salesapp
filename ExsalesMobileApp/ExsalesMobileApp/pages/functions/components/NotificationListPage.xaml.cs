using ExsalesMobileApp.model;
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
	public partial class NotificationListPage : ContentPage
	{
		internal NotificationListPage (Branch _branch)
		{
			InitializeComponent ();
		}
	}
}