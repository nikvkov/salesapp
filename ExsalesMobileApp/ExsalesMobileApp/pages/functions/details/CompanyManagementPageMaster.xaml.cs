using ExsalesMobileApp.library;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
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
	public partial class CompanyManagementPageMaster : ContentPage
	{
        Person user;
        List<FunctionData> functions;
        CompanyManagementPage parent;

        public CompanyManagementPageMaster (Person _user, List<FunctionData> _functions, CompanyManagementPage parentPage)
		{
			InitializeComponent ();
            user = _user;
            functions = _functions;
            Title = "Master";
            parent = parentPage;

            lb_title.Text = "Available subfunctions";

            foreach (var item in functions)
            {
                TagButton temp = new TagButton { Tag = item.Id, Text = item.Functions };
                temp.Clicked += subfunction_Clicked;

                functionContainer.Children.Add(temp);
            }
             
            

        }

        private async void subfunction_Clicked(object sender, EventArgs e)
        {
            int id = ((TagButton)sender).Tag;
            switch (id)
            {
                case 10:
                    ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
                    var types = await api.GetCompanyTypes();
                    parent.Detail = new CompanyAdditionPage(user, types);
                    break;
                default:
                    parent.Detail = new CompanyManagementPageDetail();
                    break;
            }

          
            
        }
    }
}