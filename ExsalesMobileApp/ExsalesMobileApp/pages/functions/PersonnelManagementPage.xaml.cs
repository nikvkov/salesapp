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
	public partial class PersonnelManagementPage : MasterDetailPage
	{
        List<FunctionData> parts;
        public PersonnelManagementPage (List<FunctionData> data)
		{
			InitializeComponent ();

            parts = data;

            Master = new PersonnelManagementPageMaster(parts, this);
            Detail = new PersonnelManagementPageDetail();

        }
	}//class
}//namespace