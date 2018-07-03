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

namespace ExsalesMobileApp.pages.functions.details
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkPageDetail : ContentPage
	{
        List<RetailPoint> points;
		public NetworkPageDetail ()
		{
			InitializeComponent ();

            lb_title.Text = "Distribution Management";
            bt_add.Text = "Add";
            bt_back.Text = "Back";

            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Clicked += Bt_add_Clicked;

		}//c_tor

        //нажатие на кнопку добавить
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddNetworkPage(null), true);
        }

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)=> await Navigation.PopModalAsync(true);

        protected async override void OnAppearing()
        {
            ai_ind.IsVisible = true;
            ai_ind.IsRunning = true;
            lv_container.IsVisible = false;
            try
            {

                ApiService api = new ApiService { Url = ApiService.URL_RETAIL_AT_USER };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key",App.APP.CurrentUser.AuthKey }
                };
                api.AddParams(data);

                var res = await api.GetRetailPoints();

                lv_container.ItemsSource = res;
                lv_container.ItemSelected += Lv_container_ItemSelected;

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
            finally
            {
                ai_ind.IsVisible = false;
                ai_ind.IsRunning = false;
                lv_container.IsVisible = true;
            }

            base.OnAppearing();
        }//OnAppearing

        //нажатие на строку list view
        private async void Lv_container_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddNetworkPage((RetailPoint)e.SelectedItem), true);
        }

    }//class
}//namespace