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
	public partial class SubdivisionsListPage : ContentPage
	{

        ObservableCollection<Subdivision> subdivisionList;
        CompanyData currentCompany;

		internal SubdivisionsListPage (CompanyData company)
		{
			InitializeComponent ();
            ai_ind.IsVisible = false;
            ai_ind.IsRunning = false;
            lb_title.Text = "Current subdivision";
            

            pc_company.ItemsSource = App.APP.CompanyCollection;
            currentCompany = company != null ? company : App.APP.CompanyCollection[0];
            pc_company.SelectedItem = currentCompany;
            bt_add.Text = "+";
            bt_back.Text = "Back";
            bt_add.Clicked += Bt_add_Clicked;
            bt_back.Clicked += Bt_back_Clicked;
            lv_subdivisions.ItemSelected += Lv_subdivisions_ItemSelected;
            pc_company.SelectedIndexChanged += Pc_company_SelectedIndexChanged;

        }//SubdivisionsListPage

        //добавление нового элемента
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddSubdivisionPage(currentCompany));
        }

        //изменение текущей компании
        private void Pc_company_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCompany = (CompanyData)pc_company.SelectedItem;
            LoadCompanyData();
        }

        //клик по элементу
        private void Lv_subdivisions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected async override void OnAppearing()
        {

            LoadCompanyData();

            base.OnAppearing();
        }//OnAppearing

        //подгрузка подразделений для компании
        async void LoadCompanyData()
        {
            ai_ind.IsVisible = true;
            ai_ind.IsRunning = true;
            if (currentCompany != null)
            {

                ApiService api = new ApiService { Url = ApiService.URL_GET_SUBDIVISION };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", App.APP.CurrentUser.AuthKey);
                data.Add("id", currentCompany.Id.ToString());
                api.AddParams(data);
                subdivisionList = await api.GetSubdivisions();

                lv_subdivisions.ItemsSource = subdivisionList;


            }//if

            ai_ind.IsVisible = false;
            ai_ind.IsRunning = false;
        }//LoadCompanyData

    }//class
}//namespace