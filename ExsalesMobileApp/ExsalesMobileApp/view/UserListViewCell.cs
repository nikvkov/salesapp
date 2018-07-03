using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.view
{
    public class UserListViewCell : ViewCell
    {

        Label firstnameLabel, lastnameLabel, emailLabel, phoneLabel;
 

        public UserListViewCell()
        {
            firstnameLabel = new Label { FontSize = 26 };
            lastnameLabel = new Label { FontSize = 26 };
            emailLabel = new Label();
            phoneLabel = new Label();

            StackLayout cell = new StackLayout();
          

            StackLayout titleDetailLayout = new StackLayout();
            titleDetailLayout.Orientation = StackOrientation.Horizontal;
            titleDetailLayout.Children.Add(firstnameLabel);
            titleDetailLayout.Children.Add(lastnameLabel);

            cell.Children.Add(titleDetailLayout);
            cell.Children.Add(emailLabel);
            cell.Children.Add(phoneLabel);

            View = cell;
        }

        public static readonly BindableProperty FirstNameProperty =
            BindableProperty.Create("FirstName", typeof(string), typeof(UserListViewCell), "User");

        public static readonly BindableProperty LastNameProperty =
            BindableProperty.Create("LastName", typeof(string), typeof(UserListViewCell), "-");

        public static readonly BindableProperty EmailProperty =
            BindableProperty.Create("Email", typeof(string), typeof(UserListViewCell), "-");

        public static readonly BindableProperty PhoneProperty =
            BindableProperty.Create("Phone", typeof(string), typeof(UserListViewCell), "-");

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {

                firstnameLabel.Text = FirstName;
                lastnameLabel.Text = LastName;
                emailLabel.Text = Email;
                phoneLabel.Text = Phone;

            }
        }

    }//class
}//namespace
