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
	public partial class NotificationListPage : ContentPage
	{
        Branch branch;

        List<CustomNotification> customNotes;

		internal NotificationListPage (Branch _branch)
		{
			InitializeComponent ();

            bt_add.Text = "Add";
            bt_back.Text = "Back";
            branch = _branch;

            try
            {
                bt_add.Clicked += async (x, y) => { await Navigation.PushModalAsync(new AddNotificationPage(branch, null), true); };
                bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
                lv_container.ItemSelected += async (x, y) => { await Navigation.PushModalAsync(new AddNotificationPage(branch, (CustomNotification)y.SelectedItem), true); };
            }catch(Exception ex)
            {
                DisplayAlert("dede", ex.Message, "OK");
            }

        }

        protected async override void OnAppearing()
        {
            try
            {
                ApiService api = new ApiService {Url = ApiService.URL_GET_NOTIFICATION };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey },
                    {"link_id", branch.Id.ToString()},
                };
                api.AddParams(data);
                customNotes = await api.GetNotifications();
            }catch(Exception ex)
            {
                customNotes = new List<CustomNotification>();
            }

            lv_container.ItemsSource = customNotes;

            base.OnAppearing();
        }

    }//class
}//namespace