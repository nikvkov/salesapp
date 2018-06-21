using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddBranchPage : ContentPage
	{
        List<CompanyData> companyData;

        internal AddBranchPage (Branch _branch)
		{
			InitializeComponent ();

            lb_title.Text = "Add new branch";
            lb_item_title.Text = "New branch title";
            en_item_title.Placeholder = "Put new title";
            lb_item_company.Text = "Select company";
            lb_item_network.Text = "Select network";
            bt_add.Text = "Add";
            bt_edit.Text = "Edit";
            bt_delete.Text = "Remove";
            bt_back.Text = "Back";

            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Clicked += Bt_add_Clicked;

        }//c_tor

        //добавление 
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (en_item_title.Text.Length != 0 && pc_company.SelectedItem!=null && pc_network.SelectedItem!=null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_ADD_BRANCH };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"title", en_item_title.Text },
                        {"company_id", ((CompanyData)pc_company.SelectedItem).Id.ToString() },
                        {"retailer_id", ((RetailPoint)pc_network.SelectedItem).Id.ToString() }
                    };

                    var res = await api.Post(data);
                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "Branch was added", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Branch was not added", "Done");
                    }
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//Bt_add_Clicked

        protected async override void OnAppearing()
        {
            //заполняем список компаний
            if (App.APP.CompanyCollection != null && App.APP.CompanyCollection.Count > 0)
            {
                companyData = App.APP.CompanyCollection;
            }
            else
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/get" };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                api.AddParams(data);
                companyData = (await api.GetCompany()).ToList();
                App.APP.CompanyCollection = companyData;
            }
            pc_company.ItemsSource = companyData;
            pc_company.SelectedIndex = 0;

            //заполняем список сетей
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_RETAIL_AT_USER };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key",App.APP.CurrentUser.AuthKey }
                };
                api.AddParams(data);

                var res = await api.GetRetailPoints();

                pc_network.ItemsSource = res;
                pc_network.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }


            base.OnAppearing();
        }//OnAppearing

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync(true);
 
    }//class
}//namespace