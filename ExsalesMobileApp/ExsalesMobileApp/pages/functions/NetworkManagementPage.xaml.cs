using ExsalesMobileApp.library;
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
	public partial class NetworkManagementPage : MasterDetailPage
	{
        List<FunctionData> parts;
        public NetworkManagementPage (List<FunctionData> functionsPart)
		{

			InitializeComponent ();

            parts = functionsPart;

            Master = new NetworkPageMaster(parts, this);
            Detail = new NetworkPageDetail();

        }//c_tor

	}//class
}//namespace