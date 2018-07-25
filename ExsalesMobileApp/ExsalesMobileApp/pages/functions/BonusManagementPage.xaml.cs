using ExsalesMobileApp.model;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using ExsalesMobileApp.view;
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
	public partial class BonusManagementPage : ContentPage
	{
		public BonusManagementPage ()
		{
			InitializeComponent ();

            lb_title.Text = "Bonuses";
            bt_add.Text = "Add";
            bt_back.Text = "Back";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_add.Clicked += async (x, y) => { await Navigation.PushModalAsync(new AddBonusPage(null),true); };
            lv_bonus.ItemSelected += async (x, y) => { await Navigation.PushModalAsync(new AddBonusPage((Product)y.SelectedItem), true); };
            pc_company.SelectedIndexChanged += async (x, y) => { await GetProducts(); };


            lv_bonus.HasUnevenRows = true;

            lv_bonus.ItemTemplate = new DataTemplate(() =>
            {
                SalesViewCell customCell = new SalesViewCell();
                customCell.SetBinding(SalesViewCell.TitleProperty, "Title");
                customCell.SetBinding(SalesViewCell.EANProperty, "EAN");
                Binding bonusBinding = new Binding { Path = "Bonus", StringFormat = "{0}" };
                customCell.SetBinding(SalesViewCell.BonusProperty, bonusBinding);

                return customCell;
            });

        }

        protected async override void OnAppearing()
        {

            //получаем списов компаний
            if(App.APP.CompanyCollection == null)
            {

                try
                {
                    ApiService api = new ApiService {Url = ApiService.URL_GET_COMPANIES };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey }
                    };

                    api.AddParams(data);

                    App.APP.CompanyCollection = (await api.GetCompany()).ToList();

                }
                catch(Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Done");
                    App.APP.CompanyCollection = new List<ApiService.CompanyData>();
                }

            }

            pc_company.ItemsSource = App.APP.CompanyCollection;
            pc_company.SelectedIndex = 0;

            //получаем список товаров
            await GetProducts();





            base.OnAppearing();
        }

        private async Task GetProducts()
        {
            try
            {
                if(((CompanyData)pc_company.SelectedItem) != null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_GET_PRODUCTS };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"company_id", ((CompanyData)pc_company.SelectedItem).Id.ToString() },
                    };

                    api.AddParams(data);

                    lv_bonus.ItemsSource = await api.GetProducts();

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Done");
            }
        }
    }
}