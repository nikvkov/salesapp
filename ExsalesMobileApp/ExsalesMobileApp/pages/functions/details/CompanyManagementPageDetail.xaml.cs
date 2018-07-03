using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using static ExsalesMobileApp.services.ApiService;
using ExsalesMobileApp.library;
using ExsalesMobileApp.view;
using System.Collections.ObjectModel;

namespace ExsalesMobileApp.pages.functions.details
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyManagementPageDetail : ContentPage
	{
        ObservableCollection<CompanyData> companyData;
        Person user;

		public CompanyManagementPageDetail (Person _user)
		{
			InitializeComponent ();
            user = _user;


            lb_title.Text = "Company management";
            lb_swipeInfo.Text = "To access the subfunctions, swipe from the right edge of the screen to the left";
            lb_description.Text = "Here you can manage companies";
            bt_addCompany.Text = "+";
            bt_back.Text = "Back";

            bt_back.Clicked += Bt_back_Clicked;
            bt_addCompany.Clicked += Bt_addCompany_Clicked;
            lv_companies.ItemSelected += Lv_companies_ItemSelected;
            

            //mainContainer.Children.Add(new CompanyAdditionView());
		}

        //нажатие на строку кампании
        private async void Lv_companies_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
            var types = await api.GetCompanyTypes();
            await Navigation.PushModalAsync(new CompanyAdditionPage(user, types, (CompanyData)lv_companies.SelectedItem), true);
        }

        //нажатие на кнопку добавить кампанию
        private async void Bt_addCompany_Clicked(object sender, EventArgs e)
        {
            ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/types" };
            var types = await api.GetCompanyTypes();
            await Navigation.PushModalAsync( new CompanyAdditionPage(user, types, null),true);
        }

        //нажатие на кнопку back
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected async override void OnAppearing()
        {
            ai_ind.IsVisible = true;
            ai_ind.IsRunning = true;
            lv_companies.IsVisible = false;

            try
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/get" };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", user.AuthKey);
                api.AddParams(data);
                companyData = await api.GetCompany();
            }
            catch (Exception ex)
            {
                companyData = new ObservableCollection<CompanyData>();
            }

            App.APP.CompanyCollection = companyData.ToList();

            try { 
            lv_companies.HasUnevenRows = true;
            // Определяем источник данных
            lv_companies.ItemsSource = companyData;

            // Определяем формат отображения данных
            lv_companies.ItemTemplate = new DataTemplate(() =>
            {
                CompanyListViewCell customCell = new CompanyListViewCell(user);
                customCell.SetBinding(CompanyListViewCell.TitleProperty, "Title");
                Binding typeBinding = new Binding { Path = "Type", StringFormat = "Тип компании {0}" };
                customCell.SetBinding(CompanyListViewCell.TypeProperty, typeBinding);
                Binding addressBinding = new Binding { Path = "Address", StringFormat = "Адрес {0}" };
                customCell.SetBinding(CompanyListViewCell.AddressProperty, addressBinding);
                Binding countryBinding = new Binding { Path = "Country", StringFormat = "Country: {0}" };
                customCell.SetBinding(CompanyListViewCell.CountryProperty, countryBinding);

                Binding cityBinding = new Binding { Path = "City", StringFormat = "City: {0}" };
                customCell.SetBinding(CompanyListViewCell.CityProperty, cityBinding);

                Binding dateBinding = new Binding { Path = "Date", StringFormat = "Date: {0:D}" };
                customCell.SetBinding(CompanyListViewCell.DateProperty, dateBinding);


                customCell.SetBinding(CompanyListViewCell.CurrentCompanyProperty, "Id");


                //customCell.SetBinding(CompanyListViewCell.ImagePathProperty, "ImagePath");
                return customCell;

            });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            
            }
            finally
            {
                ai_ind.IsVisible = false;
                ai_ind.IsRunning = false;
                lv_companies.IsVisible = true;
            }
        }

    }
}