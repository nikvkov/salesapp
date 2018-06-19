using ExsalesMobileApp.library;
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
	public partial class PersonnelManagementPage : ContentPage
	{
		public PersonnelManagementPage (List<FunctionData> data)
		{
			InitializeComponent ();
		}
	}//class
}//namespace