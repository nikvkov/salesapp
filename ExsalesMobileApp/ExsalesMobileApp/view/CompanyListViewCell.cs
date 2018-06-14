using ExsalesMobileApp.library;
using ExsalesMobileApp.pages.functions.components;
using ExsalesMobileApp.services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace ExsalesMobileApp.view
{
    class CompanyListViewCell : ViewCell
    {

        Label titleLabel, dateLabel, typeLabel, countryLabel, cityLabel, addressLabel;
       // Button buttonEdit, buttonDel;
        Person user;
        string currentCompany;
        //ActivityIndicator indicator;

        //Image image;

        public CompanyListViewCell(Person _user)
        {
            
            titleLabel = new Label { FontSize = 18 };
            dateLabel = new Label();
            typeLabel = new Label();
            countryLabel = new Label();
            cityLabel = new Label();
            addressLabel = new Label();
            user = _user;

            StackLayout cell = new StackLayout();
            cell.Orientation = StackOrientation.Horizontal;

            StackLayout titleDetailLayout = new StackLayout();
            titleDetailLayout.Children.Add(titleLabel);

            StackLayout buttonLayout = new StackLayout();
            buttonLayout.Orientation = StackOrientation.Vertical;
            buttonLayout.Children.Add(typeLabel);
            buttonLayout.Children.Add(countryLabel);
            buttonLayout.Children.Add(cityLabel);
            buttonLayout.Children.Add(addressLabel);
            buttonLayout.Children.Add(dateLabel);

            cell.Children.Add(titleDetailLayout);
            cell.Children.Add(buttonLayout);
            View = cell;
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(CompanyListViewCell), "");

        public static readonly BindableProperty CurrentCompanyProperty =
            BindableProperty.Create("CurrentCompany", typeof(string), typeof(CompanyListViewCell), "");
        public static readonly BindableProperty TypeProperty =
            BindableProperty.Create("Type", typeof(string), typeof(CompanyListViewCell), "");
        public static readonly BindableProperty AddressProperty =
            BindableProperty.Create("Address", typeof(string), typeof(CompanyListViewCell), "");
        public static readonly BindableProperty CityProperty =
            BindableProperty.Create("City", typeof(string), typeof(CompanyListViewCell), "");
        public static readonly BindableProperty DateProperty =
            BindableProperty.Create("Date", typeof(string), typeof(CompanyListViewCell), "");
        public static readonly BindableProperty CountryProperty =
            BindableProperty.Create("Country", typeof(string), typeof(CompanyListViewCell), "");



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public string Country
        {
            get { return (string)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        public string City
        {
            get { return (string)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
        }

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public string CurrentCompany
        {
            get { return (string)GetValue(CurrentCompanyProperty); }
            set { SetValue(CurrentCompanyProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                titleLabel.Text = Title;
                typeLabel.Text = Type;
                //DateTime dt = Convert.ToDateTime(Address);
                //addressLabel.Text = DateTime.ParseExact(Address, "yyyy-M-dd", null).ToString("D");
                //addressLabel.Text = dt.ToLongDateString();
                addressLabel.Text = Address;
                dateLabel.Text = Date;
                cityLabel.Text = City;
                countryLabel.Text = Country;
                currentCompany = CurrentCompany;

            }
        }

    }
}
