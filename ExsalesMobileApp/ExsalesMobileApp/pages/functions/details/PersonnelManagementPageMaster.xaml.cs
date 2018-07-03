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
	public partial class PersonnelManagementPageMaster : ContentPage
	{
        List<FunctionData> functions;
        PersonnelManagementPage parent;

        public PersonnelManagementPageMaster (List<FunctionData> _functions, PersonnelManagementPage parentPage)
		{

			InitializeComponent ();
            parent = parentPage;
            functions = _functions;
            Title = "Master";

            lb_title.Text = "Available subfunctions";

            foreach (var item in functions)
            {
                TagButton temp = new TagButton { Tag = item.Id, Text = item.Functions };
                temp.Clicked += subfunction_Clicked;

                functionContainer.Children.Add(temp);
            }

        }//c_tor

        private async void subfunction_Clicked(object sender, EventArgs e)
        {
            int id = ((TagButton)sender).Tag;
            switch (id)
            {
                case 22:
                    ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
                    var types = await api.GetCompanyTypes();
                    parent.Detail = new PersonnelAdditionPage(null);
                    break;
                //case 23:
                //    api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
                //    types = await api.GetCompanyTypes();
                //    parent.Detail = new CompanyAdditionPage(user, types, null);
                //    break;
                //case 24:
                //    api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };

                //    parent.Detail = new SubdivisionsListPage(null);
                //    break;
                //case 25:
                //    api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };

                //    parent.Detail = new SubdivisionsListPage(null);
                //    break;
                default:
                    parent.Detail = new PersonnelManagementPageDetail();
                    break;
            }



        }

    }//class
}//namespace