using ExsalesMobileApp.library;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using ExsalesMobileApp.view;
using Newtonsoft.Json.Linq;
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
	public partial class PersonnelManagementPageDetail : ContentPage
	{
		public PersonnelManagementPageDetail ()
		{
			InitializeComponent ();

            bt_back.Text = "Back";
            bt_addPerson.Text = "Add";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_addPerson.Clicked += async (x, y) => { await Navigation.PushModalAsync(new PersonnelAdditionPage(null)); };

		}//c_tor

        protected async override void OnAppearing()
        {

            ai_ind.IsRunning = true;
            ai_ind.IsVisible = true;
            try
            {
                ApiService api = new ApiService {Url = ApiService.URL_GET_USERS };

                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey }
                };

                api.AddParams(data);

                JObject o = await api.GetRequest();

               // await DisplayAlert("Success", o["data"].ToString(), "OK");

                var res = await api.GetUsers();
              //  var users = Person.GetUsers(res);
                lv_container.ItemsSource = res;
                lv_container.HasUnevenRows = true;
                lv_container.ItemTemplate = new DataTemplate(() =>
                {
                    UserListViewCell customCell = new UserListViewCell();
                    customCell.SetBinding(UserListViewCell.FirstNameProperty, "Firstname");
                    customCell.SetBinding(UserListViewCell.LastNameProperty, "Lastname");
                    Binding emailBinding = new Binding { Path = "Email", StringFormat = "Email : {0}" };
                    customCell.SetBinding(UserListViewCell.EmailProperty, emailBinding);
                    Binding phoneBinding = new Binding { Path = "Phone", StringFormat = "Phone : {0}" };
                    customCell.SetBinding(UserListViewCell.PhoneProperty, phoneBinding);
                    return customCell;
                });


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                ai_ind.IsRunning = false;
                ai_ind.IsVisible = false;
            }
            base.OnAppearing();
        }


    }//class
}//namespace