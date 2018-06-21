using ExsalesMobileApp.pages.functions.details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RackjobberManagementPage : MasterDetailPage
	{
		public RackjobberManagementPage ()
		{
			InitializeComponent ();

            Master = new RackjobberPageMaster(this);
            Detail = new RackjobberPageDetail();
        }//c_tor
	}//class
}//namespace