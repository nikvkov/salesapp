using ExsalesMobileApp.library;
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
	public partial class NetworkPageMaster : ContentPage
	{
        List<FunctionData> functions;
        NetworkManagementPage parent;
        public NetworkPageMaster (List<FunctionData> _functions, NetworkManagementPage parentPage)
		{
			InitializeComponent ();

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
        }//c_tor

        private async void subfunction_Clicked(object sender, EventArgs e)
        {
            int id = ((TagButton)sender).Tag;
            switch (id)
            {
                //    case 15:
                //        ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
                //        var types = await api.GetCompanyTypes();
                //        parent.Detail = new CompanyAdditionPage(user, types, null);
                //        break;
                //    case 16:
                //        api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
                //        types = await api.GetCompanyTypes();
                //        parent.Detail = new CompanyAdditionPage(user, types, null);
                //        break;
                //    case 17:
                //        api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };

                //        parent.Detail = new SubdivisionsListPage(null);
                //        break;
                default:
                    parent.Detail = new NetworkPageDetail();
            break;
        }

    }


    }//class
}//namespace