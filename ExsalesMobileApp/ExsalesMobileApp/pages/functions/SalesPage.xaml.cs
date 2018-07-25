using ExsalesMobileApp.model;
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
	public partial class SalesPage : ContentPage
	{
		public SalesPage ()
		{
			InitializeComponent ();

            lb_company.Text = "Company";
            lb_quant.Text = "Quantity";
            en_quant.Text = "1";

            st_quant.Minimum = 1;
            st_quant.Maximum = 100;
            st_quant.Increment = 1;
            st_quant.ValueChanged += (x, y) => { en_quant.Text = y.NewValue.ToString(); };
            pc_company.SelectedIndexChanged += async (x, y) => { await GetProducts(); };
            lv_product.ItemSelected += async (x, y) => { await AddSell((Product) y.SelectedItem); };
            en_quant.IsEnabled = false;

            lv_product.HasUnevenRows = true;

            lv_product.ItemTemplate = new DataTemplate(() =>
            {
                SalesViewCell customCell = new SalesViewCell();
                customCell.SetBinding(SalesViewCell.TitleProperty, "Title");
                customCell.SetBinding(SalesViewCell.EANProperty, "EAN");
                Binding bonusBinding = new Binding { Path = "Bonus", StringFormat = "{0}" };
                customCell.SetBinding(SalesViewCell.BonusProperty, bonusBinding);

                return customCell;
            });
        }

        private async Task AddSell(Product curProd)
        {
            bool result = await DisplayAlert("Add sale", "Add new sale?", "Yes", "Cancel");

            if (result)
            {
                try
                {
                    ApiService api = new ApiService { Url = ApiService.URL_ADD_SALE };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"product_id", curProd.Id.ToString() },
                        {"quantity", ((int)st_quant.Value).ToString() }
                    };

                    var res = await api.Post(data);

                    if(res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", $"Sale {curProd.Title} , quantity {en_quant.Text} was added", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Sale was not added", "Done");
                    }

                }catch(Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Done");
                }
            }
        }

        protected async override void OnAppearing()
        {

            //получаем списов компаний
            if (App.APP.CompanyCollection == null)
            {

                try
                {
                    ApiService api = new ApiService { Url = ApiService.URL_GET_COMPANIES };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey }
                    };

                    api.AddParams(data);

                    App.APP.CompanyCollection = (await api.GetCompany()).ToList();

                }
                catch (Exception ex)
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
                if (((CompanyData)pc_company.SelectedItem) != null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_GET_PRODUCTS };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"company_id", ((CompanyData)pc_company.SelectedItem).Id.ToString() },
                    };

                    api.AddParams(data);

                    lv_product.ItemsSource = await api.GetProducts();

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

    }
}