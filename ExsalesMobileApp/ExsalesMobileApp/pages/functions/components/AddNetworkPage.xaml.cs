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
        List<RetailPoint> retailPoints;
        List<Network> networks;
        RetailPoint currentPoint;
        internal AddNetworkPage (RetailPoint _point)
		{
            
			InitializeComponent ();

            currentPoint = _point;

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
            bt_delete.Clicked += Bt_delete_Clicked;

            bt_edit.IsVisible = false;

        }//c_tor


        //удаление 
        private async void Bt_delete_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (currentPoint != null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_REMOVE_RETAIL };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", currentPoint.Id.ToString() }
                    };
                    api.AddParams(data);
                    var res = await api.GetRequest();
                    if ((bool)res["status"])
                    {
                        await DisplayAlert("Success", "Network was removed", "OK");
                        await Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Network was not removed", "Done");
                        await Navigation.PopModalAsync(true);
                    }
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //добавление
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_ADD_RETAIL };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                if (en_item_title.Text == null || en_item_title.Text.Length == 0) throw new Exception("Title must be fill!");
                data.Add("title", en_item_title.Text);
                if(retailPoints==null || retailPoints.Count==0) throw new Exception("Retails not found!");

                RetailPoint point = retailPoints.Where(x => (x.Title == en_item_retail.Text.Split('\n')[0])).FirstOrDefault();
                if(point==null) throw new Exception("Retail point not found!");
                data.Add("retailer_id", point.Id.ToString());
                var res = await api.Post(data);
                if(res == System.Net.HttpStatusCode.OK)
                {
                    await DisplayAlert("Succees", "Network was added", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Network was not added", "Done");
                }
            }
            catch(Exception ex)
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
            retailPoints = await api.GetRetailPoints();
            string[] names = retailPoints.Select(x => (x.Title + "\n" + "Address : " + x.Address)).ToArray();
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
                networks = await api.GetNetworks();
                pc_item_network.ItemsSource = networks;
                pc_item_network.SelectedIndex = 0;

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }

            if (currentPoint != null)
            {
                bt_add.IsVisible = false;
                en_item_title.Text = currentPoint.Title;
                if (networks != null)
                    pc_item_network.SelectedItem = networks.Where(x => x.Id == currentPoint.DistributorId).First();
            }
            else
            {
                bt_delete.IsVisible = false;
            }


            base.OnAppearing();
        }//OnAppearing()


    }//class
}//namespace