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
	public partial class RackjobberPageMaster : ContentPage
	{
        RackjobberManagementPage parent;

        public RackjobberPageMaster (RackjobberManagementPage _parent)
		{
			InitializeComponent ();
            parent = _parent;
            Title = "Reckjobber";

		}//c_tor
	}//class
}//namespace