using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductPage : ContentPage
	{

        internal Product CurProduct { get; private set; }
        internal CompanyData CurrentCompany { get; private set; }

        internal ProductPage (Product prod, CompanyData company)
		{
            try
            {
                InitializeComponent();
                CurProduct = prod;
                CurrentCompany = company;
                if (CurProduct != null)
                {

                    this.BindingContext = CurProduct;
                    bt_save.IsVisible = false;
                }
                else
                {
                    bt_edit.IsVisible = false;
                    bt_del.IsVisible = false;
                }

                bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };

                bt_save.Clicked += async (x, y) => { await SaveProduct(); };

                bt_edit.Clicked += async (x, y) => { await EditProduct(); };

                bt_del.Clicked += async (x, y) => { await DelProduct(); };
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Done");
            }
        }

        private async Task DelProduct()
        {
            try
            {
                if (CurProduct != null )
                {
                    ApiService api = new ApiService { Url = ApiService.URL_REMOVE_PRODUCT};
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", CurProduct.Id.ToString() },
                    };

                    api.AddParams(data);

                    var res = await api.GetRequest();

                    if ((bool) res["status"])
                    {
                        await DisplayAlert("Success", "Product was deleted", "OK");
                        await Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Product was not deleted", "Done");
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        private async Task EditProduct()
        {
            try
            {
                if (CurProduct != null && en_title.Text.Length > 0 && en_ean.Text.Length > 0)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_EDIT_PRODUCT };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", CurProduct.Id.ToString() },
                        {"title", en_title.Text },
                        {"ean", en_ean.Text },
                    };
                    var res = await api.Post(data);

                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "Product was updated", "OK");
                        await Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Product was not updated", "Done");
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        private async Task SaveProduct()
        {
            try
            {
                if (CurrentCompany != null && en_title.Text.Length > 0 && en_ean.Text.Length > 0)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_SAVE_PRODUCT };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"company_id", CurrentCompany.Id.ToString() },
                        {"title", en_title.Text },
                        {"ean", en_ean.Text },
                    };
                    var res = await api.Post(data);

                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "Product was added", "OK");
                        await Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Product was not added", "Done");
                    }
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }
    }
}