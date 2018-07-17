using ExsalesMobileApp.model;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductManagementPage : ContentPage
	{
		public ProductManagementPage ()
		{
			InitializeComponent ();

            bt_add.Clicked += async (x, y) =>{ await Navigation.PushModalAsync(new ProductPage(null, (CompanyData) pc_company.SelectedItem)); };
            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            pc_company.SelectedIndexChanged += async (x, y) => { await GetProducts(); };
            productsList.ItemSelected += async (x, y) => { await Navigation.PushModalAsync(new ProductPage((Product)y.SelectedItem, (CompanyData)pc_company.SelectedItem)); };
            bt_back.Text = "Back";
        }

        protected async override void OnAppearing()
        {
            try
            {
                if (App.APP.CompanyCollection == null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_GET_COMPANIES };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey }
                    };
                    api.AddParams(data);
                    App.APP.CompanyCollection = (await api.GetCompany()).ToList();
                }
                pc_company.ItemsSource = App.APP.CompanyCollection;
                pc_company.SelectedIndex = 0;

                await GetProducts();

            }
            catch (Exception ex)
            {
               await DisplayAlert("Error", ex.Message, "Done");
            }
            base.OnAppearing();
        }

        async Task GetProducts()
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_GET_PRODUCTS };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey },
                    {"company_id", ((CompanyData) pc_company.SelectedItem).Id.ToString() },
                };
                api.AddParams(data);

                productsList.ItemsSource = await api.GetProducts();

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }



    }
}