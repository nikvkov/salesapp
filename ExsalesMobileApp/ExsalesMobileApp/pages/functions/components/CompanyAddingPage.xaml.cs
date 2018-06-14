using ExsalesMobileApp.library;
using ExsalesMobileApp.services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
	public partial class CompanyAdditionPage : ContentPage
	{
        List<CountryData> countryNames;
        List<CityData> cityNames;
        List<CompanyType> types;
        Person user;
        CompanyData company;

        internal CompanyAdditionPage(Person _user, List<CompanyType> _types, CompanyData _company)
		{
			InitializeComponent ();
            user = _user;
            types = _types;

            company = _company;
            lb_title.Text = "Company addition";

            lb_nameText.Text = "Company name";

            en_name.Placeholder = "Put name for company";
            lb_typeText.Text = "Select type";
            lb_countryText.Text = "Select country";
            lb_cityText.Text = "Select city";
            en_country.Placeholder = "Your country";
            en_city.Placeholder = "City";
            bt_editCountry.Text = "Edit";
            bt_editCity.Text = "Edit";
            lb_addressText.Text = "Address";
            en_adress.Placeholder = "Put address";
            bt_add.Clicked += Bt_add_Clicked;
            bt_edit.Clicked += Bt_edit_Clicked;
            bt_del.Clicked += Bt_del_Clicked;
           

            if (types != null && types.Count>0)
            {
                pc_type.ItemsSource = types;
                pc_type.SelectedIndex = 0;
                if (company != null)
                {
                    pc_type.SelectedIndex = types.Where(x => x.Type == company.Type).Select(x => x.Id).First()-1;
                }
            }

            ai_country.IsRunning = false;
            ai_city.IsRunning = false;
            en_country.Focused += En_country_Focused;
            en_city.Focused += En_city_Focused;
            bt_editCountry.IsEnabled = false;
            bt_editCity.IsEnabled = false;
            bt_editCountry.Clicked += Bt_editCountry_Clicked;
            bt_editCity.Clicked += Bt_editCity_Clicked;
            bt_back.Clicked += Bt_back_Clicked;
            bt_add.Text = "Add company";
            bt_edit.Text = "Edit";
            bt_del.Text = "X";
            if (company != null)
            {
                bt_edit.IsVisible = true;
                bt_del.IsVisible = true;
                bt_add.IsVisible = false;
                en_city.IsEnabled = false;
                en_country.IsEnabled = false;
                bt_editCity.IsEnabled = true;
                bt_editCountry.IsEnabled = true;
            }
            bt_back.Text = "Back";
            if (company != null)
            {
                en_name.Text = company.Title;
                en_city.Text = company.City;
                en_country.Text = company.Country;
                en_adress.Text = company.Address;

            }
        }

        //удаление кампании
        private async void Bt_del_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/remove" };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", user.AuthKey);
                data.Add("id", company.Id.ToString());

                api.AddParams(data);

                JObject res = await api.DelCompany();
                if ((bool)res["status"])
                {
                    await DisplayAlert("Success", "Company deleted", "OK");
                }
                else
                {
                    await DisplayAlert("Warning", "Company not deleted", "Done");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //редактирование кампании
        private async void Bt_edit_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/edit" };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", user.AuthKey);
                data.Add("id", company.Id.ToString());
                if (en_name.Text.Length == 0) throw new Exception("Укажите название компании");
                data.Add("title", en_name.Text);

                if (en_city.Text != null && en_city.Text.Length > 0)
                {
                    data.Add("city_id", cityNames.Where(x => x.City == en_city.Text).Select(x => x.Id).First().ToString());
                }

                data.Add("type_id", ((CompanyType)pc_type.SelectedItem).Id.ToString());

                //await DisplayAlert("Success", ((CompanyType)pc_type.SelectedItem).Id.ToString(), "OK");

                if (en_adress.Text != null && en_adress.Text.Length > 0)
                {
                    data.Add("address", en_adress.Text);
                }

                var res = await api.Post(data);
                //var stream = await res.Content.ReadAsStreamAsync();
                //await DisplayAlert("Success", new StreamReader(stream).ReadToEnd(), "OK");
                if (res == HttpStatusCode.OK)
                {
                    await DisplayAlert("Success", "Изменения сохранены", "OK");
                }
                else
                {
                    await DisplayAlert("Warning", "Изменения не сохранены! Проверьте указанные данные или свяжитесь с администратором", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ОК");
            }
        }

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        //запрос на добавление компании
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/company/add" };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", user.AuthKey);

                if (en_name.Text.Length == 0) throw new Exception("Укажите название компании");
                data.Add("title", en_name.Text);

                if (en_city.Text != null && en_city.Text.Length > 0)
                {
                    data.Add("city_id", cityNames.Where(x => x.City == en_city.Text).Select(x => x.Id).First().ToString());
                }

                data.Add("type_id", ((CompanyType)pc_type.SelectedItem).Id.ToString());

                //await DisplayAlert("Success", ((CompanyType)pc_type.SelectedItem).Id.ToString(), "OK");

                if (en_adress.Text!=null && en_adress.Text.Length > 0)
                {
                    data.Add("address", en_adress.Text);
                }

                var res = await api.Post(data);
                //var stream = await res.Content.ReadAsStreamAsync();
                //await DisplayAlert("Success", new StreamReader(stream).ReadToEnd(), "OK");
                if (res == HttpStatusCode.OK)
                {
                    await DisplayAlert("Success", "Компания добавлена", "OK");
                }
                else
                {
                    await DisplayAlert("Warning", "Компания не добавлена! Проверьте указанные данные или свяжитесь с администратором", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ОК");
            }
        }


        //включение редактирования города
        private void Bt_editCity_Clicked(object sender, EventArgs e)
        {
            en_city.IsEnabled = true;
        }

        //включение редактирования страны
        private void Bt_editCountry_Clicked(object sender, EventArgs e)
        {
            en_country.IsEnabled = true;
        }


        //получение фокуса полем ввода страны
        private async void En_country_Focused(object sender, FocusEventArgs e)
        {
            ai_country.IsRunning = true;
            en_country.IsVisible = false;
            if (countryNames == null) { 
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/country/all" };
                countryNames = await api.GetCountries();

            }
            var countries = countryNames.Select(x => x.Country).ToArray();
            var action = await DisplayActionSheet("Select country", "Cancel", null, countries);

            if (action != "Cancel")
            {
                ((Entry)sender).Text = action;
            }

            ai_country.IsRunning = false;
            en_country.IsVisible = true;

            ((Entry)sender).IsEnabled = false;
            bt_editCountry.IsEnabled = true;
            cityNames = null;
            en_city.Text = "";

        }

        //получение фокуса полес ввода города
        private async void En_city_Focused(object sender, FocusEventArgs e)
        {
            ai_city.IsRunning = true;
            en_city.IsVisible = false;
            if (cityNames == null)
            {
                ApiService api = new ApiService { Url = "https://www.exsales.net/api/v1/city/cic" };
                Dictionary<string, string> data = new Dictionary<string, string>();

                int id = countryNames.Where(x=>x.Country == en_country.Text).Select(x=>x.Id).First();
                //await DisplayAlert("dwdew", id.ToString(), "OK");
                //lb_title.Text = id.ToString();
                data.Add("id", id.ToString());
                api.AddParams(data);
                cityNames = await api.GetCities();

            }

            if (cityNames != null && cityNames.Count > 0)
            {
                var cities = cityNames.Select(x => x.City).ToArray();
                var action = await DisplayActionSheet("Select city", "Cancel", null, cities);

                if (action != "Cancel")
                {
                    ((Entry)sender).Text = action;
                }

                ((Entry)sender).IsEnabled = false;
                bt_editCity.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Warning", "С выбранной страной не связано ни одного города. Для добавления интересующего города свяжитесь с админитратором!", "OK");
                ((Entry)sender).IsEnabled = false;
            }
            ai_city.IsRunning = false;
            en_city.IsVisible = true;
        }
    }
}