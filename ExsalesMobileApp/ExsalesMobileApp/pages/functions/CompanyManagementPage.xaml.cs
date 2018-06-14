#define DEBUG
#undef RELEASE

using ExsalesMobileApp.library;

using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExsalesMobileApp.pages.functions.details;

namespace ExsalesMobileApp.pages.functions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyManagementPage : MasterDetailPage
    {
        Person user;
        List<FunctionData> parts;
		public CompanyManagementPage (Person _user, List<FunctionData> functionsPart)
		{
			InitializeComponent ();
            user = _user;
            parts = functionsPart;

            Master = new CompanyManagementPageMaster(user, parts, this);
            Detail = new CompanyManagementPageDetail(user);
            //lb_title.Text = parts.Count.ToString();

        }//c_tor

        
	}
}