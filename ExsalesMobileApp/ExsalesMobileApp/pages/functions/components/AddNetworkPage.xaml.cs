using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XLabs.Forms.Controls;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNetworkPage : ContentPage
	{
		public AddNetworkPage ()
		{
            
			InitializeComponent ();

            lb_title.Text = "Add new network";
            lb_item_title.Text = "New title";
            en_item_title.Placeholder = "Put title";
            lb_item_network.Text = "New network";
            lb_item_retail.Text = "New Retail";
            en_item_retail.Placeholder = "Select retail point";
            bt_add.Text = "Add";
            bt_edit.Text = "Edit";
            bt_delete.Text = "Remove";
            bt_back.Text = "Back";

            en_item_retail.Focused += En_item_retail_Focused;
            pc_item_network.Focused += Pc_item_network_Focused;
            en_item_title.Focused+= Pc_item_network_Focused;
            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Clicked += Bt_add_Clicked;

        }//c_tor

        //добавление
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = "" };

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//Bt_add_Clicked

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private void Pc_item_network_Focused(object sender, FocusEventArgs e)
        {
            en_item_retail.IsEnabled = true;
        }

        //фокус на поле ввода торговой точки
        private async void En_item_retail_Focused(object sender, FocusEventArgs e)
        {
            ApiService api = new ApiService { Url = ApiService.URL_GET_RETAIL };
            Dictionary<string,string> data = new Dictionary<string, string>();
            data.Add("id", ((Network)pc_item_network.SelectedItem).Id.ToString());
            api.AddParams(data);
            var retailPoints = await api.GetRetailPoints();
            string[] names = retailPoints.Select(x => ("Title : " + x.Title + "\n" + "Address : " + x.Address)).ToArray();
            var item = await DisplayActionSheet("Select retail point", "Cancel", null, names);

            if (item != "Cancel")
            {
                en_item_retail.Text = item;
                en_item_retail.IsEnabled = false;
            }
            else
            {
                en_item_title.Focus();
            }

        }//En_item_retail_Focused

        //действия при входе на страницу
        protected async override void OnAppearing()
        {
            //получаем список торговых сетей
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_COMPANY_AT_TYPE };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("id","2");
                api.AddParams(data);
                var networks = await api.GetNetworks();
                pc_item_network.ItemsSource = networks;
                pc_item_network.SelectedIndex = 0;

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }


            base.OnAppearing();
        }//OnAppearing()


    }//class
}//namespace