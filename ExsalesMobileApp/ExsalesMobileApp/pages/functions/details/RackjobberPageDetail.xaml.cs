using ExsalesMobileApp.model;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using ExsalesMobileApp.view;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class RackjobberPageDetail : ContentPage
	{
        List<Branch> branchesList;

		public RackjobberPageDetail ()
		{

            InitializeComponent ();

            bt_add.Text = "Add";
            bt_back.Text = "Back";

            bt_add.Clicked += Bt_add_Clicked;
            bt_back.Clicked += Bt_back_Clicked;

            lv_container.HasUnevenRows = true;
            //    lv_container.ItemSelected += async(x, y) => { await Navigation.PushModalAsync(new AddBranchPage((Branch)y.SelectedItem),true); };
            lv_container.ItemSelected += async(x, y) => { await Navigation.PushModalAsync(new NotificationListPage((Branch)y.SelectedItem),true); };


        }//c_tor

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        //добавление 
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddBranchPage(null),true);
        }

        //подгрузка данных
        protected async override void OnAppearing()
        {
            try
            {
                ApiService api = new ApiService {Url = ApiService.URL_GET_BRANCH };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey }
                };
                api.AddParams(data);

                branchesList = await api.GetBranches();

            }catch(Exception ex)
            {
                branchesList = new List<Branch>();
            }

            lv_container.ItemsSource = branchesList;

            lv_container.ItemTemplate = new DataTemplate(() =>
            {
                BranchListViewCell customCell = new BranchListViewCell ();
                customCell.SetBinding(BranchListViewCell.TitleProperty, "Title");
                Binding companyBinding = new Binding { Path = "Company", StringFormat = "Company : {0}" };
                customCell.SetBinding(BranchListViewCell.CompanyProperty, companyBinding);
                Binding retailerBinding = new Binding { Path = "Retailer", StringFormat = "Network : {0}" };
                customCell.SetBinding(BranchListViewCell.RetailerProperty, retailerBinding);
                return customCell;
            });

            base.OnAppearing();
        }//OnAppearing

    }//class
}//namespace