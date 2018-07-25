using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
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
	public partial class AddBonusPage : ContentPage
	{
        Product CurrentProduct { get; set; }
		internal AddBonusPage (Product product)
		{
			InitializeComponent ();

            CurrentProduct = product;


            if(CurrentProduct != null)
            {
                en_title.Text = CurrentProduct.Title;
                en_ean.Text = CurrentProduct.EAN;
                en_bonus.Text = CurrentProduct.Bonus.ToString();
            }
            else
            {
                bt_save.IsVisible = false;
            }

            bt_save.Text = "Save";
            bt_back.Text = "Back";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_save.Clicked += async (x, y) => { await SaveBonus(); };

        }

        private async Task SaveBonus()
        {
            try
            {
                ApiService api = new ApiService {Url = ApiService.URL_EDIT_PRODUCT };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey },
                    {"id", CurrentProduct.Id.ToString() },
                    {"title", CurrentProduct.Title},
                    {"ean", CurrentProduct.EAN},
                    {"bonus", en_bonus.Text}
                };

                var res = await api.Post(data);

                if(res == System.Net.HttpStatusCode.OK)
                {
                    await DisplayAlert("Success", "Bonus was added", "OK");
                }
                else
                {
                    await DisplayAlert("Warning", "Bonus was not added", "Done");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }
    }
}