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

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OwnerSalesMonitoring : ContentPage
	{
        List<Sale> Sales { get; set; }
		public OwnerSalesMonitoring ()
		{
			InitializeComponent ();

           // Sales = new List<Sale>();

            this.Title = "Own sales";

            lb_sales.Text = "Your sales";
            lb_all_bonus.Text = "Your bonuses";
            bt_back.Text = "Back";
            bt_new_sale.Text = "New sale";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_new_sale.Clicked += async (x, y) => { await Navigation.PushModalAsync(new SalesPage()); };
            lv_sales_container.ItemSelected += async (x, y) => { await RemoveSale((Sale)y.SelectedItem); };

        }

        private async Task RemoveSale(Sale selectedItem)
        {
            bool result = await DisplayAlert("Remove sale", "Remove sale?", "Yes", "Cancel");

            if (result)
            {
                try
                {
                    ApiService api = new ApiService { Url = ApiService.URL_REMOVE_SALE };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", selectedItem.Id.ToString()},
                    };

                    var res = await api.Post(data);

                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", $"Sale {selectedItem.Product} was deleted", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Sale was not deleted", "Done");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Done");
                }
                finally
                {
                    await GetSales();
                }
            }
        }

        protected async override void OnAppearing()
        {

            await GetSales();
            base.OnAppearing();
        }

        async Task GetSales()
        {
            ai_ind.IsVisible = true;
            ai_ind.IsRunning = true;
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_ALL_USER_SALES };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey }
                };
                api.AddParams(data);
                var sales = await api.GetOwnSales();
                lv_sales_container.ItemsSource = sales;

                int bonus = sales.Sum(x => x.Bonus);

                lb_quant_bonus.Text = bonus.ToString();

                //lv_sales_container.HasUnevenRows = true;
                //lv_sales_container.ItemTemplate = new DataTemplate(() =>
                //{
                //    OwnSaleCell customCell = new OwnSaleCell();
                //    customCell.SetBinding(OwnSaleCell.ProductProperty, "Product");
                //    Binding quantityBinding = new Binding { Path = "Quantity", StringFormat = "Quantity : {0}" };
                //    customCell.SetBinding(OwnSaleCell.QuantityProperty, quantityBinding);
                //    Binding bonusBinding = new Binding { Path = "Bonus", StringFormat = "Bonus : {0}" };
                //    customCell.SetBinding(OwnSaleCell.BonusProperty, bonusBinding);

                //    return customCell;
                //});

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
            finally
            {
                ai_ind.IsVisible = false;
                ai_ind.IsRunning = false;
            }
        }
    }
}