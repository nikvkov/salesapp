using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSubdivisionPage : ContentPage
	{
        CompanyData currentCompany;
        Subdivision currentSubdivision;

		internal AddSubdivisionPage (CompanyData _company, Subdivision _subdivision)
		{
			InitializeComponent ();

            currentCompany = _company != null ? _company : App.APP.CompanyCollection[0];

            pc_company.ItemsSource = App.APP.CompanyCollection;
            pc_company.SelectedItem = currentCompany;

            currentSubdivision = _subdivision;

            bt_add.Text = "Add";
            bt_edit.Text = "Edit";
            bt_delete.Text = "Remove";
            bt_back.Text = "Back";
            lb_title.Text = "Add new subdivision";
            lb_item_title.Text = "New title";
            en_item_title.Placeholder = "Put title";
            lb_item_company.Text = "New company";
            lb_item_user.Text = "New user";

            if (currentSubdivision != null)
            {
                bt_add.IsVisible = false;
                en_item_title.Text = currentSubdivision.Title;
            }
            else
            {
       
                bt_edit.IsVisible = false;
                bt_delete.IsVisible = false;
                
            }

            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Clicked += Bt_add_Clicked;
            bt_delete.Clicked += Bt_delete_Clicked;
            bt_edit.Clicked += Bt_edit_Clicked;

            pc_company.IsEnabled = false;
        }//c_tor

        //редактирование
        private async void Bt_edit_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_EDIT_SUBDIVISION };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                if (en_item_title.Text.Length == 0) throw new Exception("You must fill title!");
                data.Add("title", en_item_title.Text);
                data.Add("company_id", currentCompany.Id.ToString());
                data.Add("user_id", ((SubdivisionUser)pc_user.SelectedItem).Id.ToString());

                var res = await api.Post(data);
                if (res == HttpStatusCode.OK)
                {
                    await DisplayAlert("Succees", "Subdivision was updated", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Subdivision was not updated", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //удаление подразделения
        private async void Bt_delete_Clicked(object sender, EventArgs e)
        {
            try
            {

                ApiService api = new ApiService { Url = ApiService.URL_REMOVE_SUBDIVISION };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                if (currentSubdivision == null) throw new Exception("Not find current subdivision");
                data.Add("id", currentSubdivision.Id.ToString());
                api.AddParams(data);

                var res = await api.GetRequest();
                if ((bool)res["status"])
                {
                    await DisplayAlert("Succees", "Subdivision was deleted", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Subdivision was not deleted", "OK");
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //сохранение данных
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_ADD_SUBDIVISION };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                if (en_item_title.Text.Length == 0) throw new Exception("You must fill title!");
                data.Add("title", en_item_title.Text);
                data.Add("company_id", currentCompany.Id.ToString());
                data.Add("user_id", ((SubdivisionUser)pc_user.SelectedItem).Id.ToString());

                var res = await api.Post(data);
                if(res == HttpStatusCode.OK)
                {
                    await DisplayAlert("Succees", "Subdivision was added", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Subdivision was not added", "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//t_add_Clicked

        //при старте окна
        protected async override void OnAppearing()
        {
            //получение списка пользователей
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_USERS_AT_COMPANY };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                data.Add("id", currentCompany.Id.ToString());
                api.AddParams(data);
                var users = await api.GetSubdivisionUsers();
                pc_user.ItemsSource = users;
                pc_user.SelectedIndex = 0;

                if(currentSubdivision!=null && currentSubdivision.User.Length > 0)
                {
                    pc_user.SelectedItem = users.Where(x => (x.Firstname + " " + x.Lastname) == currentSubdivision.User).First();
                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Warning", "Users not load", "Done");
            }
            base.OnAppearing();

        }//OnAppearing()

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }


    }//class
}//namespace