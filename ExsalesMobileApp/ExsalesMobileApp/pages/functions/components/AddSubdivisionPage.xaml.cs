using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSubdivisionPage : ContentPage
	{
        CompanyData currentCompany;
		internal AddSubdivisionPage (CompanyData _company)
		{
			InitializeComponent ();

            currentCompany = _company != null ? _company : App.APP.CompanyCollection[0];
		}
	}//class
}//namespace