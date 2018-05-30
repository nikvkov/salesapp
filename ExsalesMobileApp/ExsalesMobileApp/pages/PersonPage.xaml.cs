using ExsalesMobileApp.lang;
using ExsalesMobileApp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonPage : ContentPage
	{
        Person user;

		public PersonPage (Person _user)
		{

			InitializeComponent ();

            this.user = _user;

            en_firstName.IsEnabled = false;
            en_lastName.IsEnabled = false;
            en_email.IsEnabled = false;
            en_phone.IsEnabled = false;

            //заполняем данные пользователя
            en_firstName.Text = user.FirstName;
            en_lastName.Text = user.LastName;
            en_email.Text = user.Email;
            en_phone.Text = user.Phone;

            /*Блок перевода*/
            titleLabel.Text = "Your personal information";
            firstNameLabelText.Text = "First name";
            en_firstName.Placeholder = "Put your first name";
            lastNameLabelText.Text = "Last Name";
            en_lastName.Placeholder = "Put your last name";
            emailLabelText.Text = "Your email";
            en_email.Placeholder = "Put your email";
            phoneLabelText.Text = "Your phone number";
            en_phone.Placeholder = "Put your phone";
            bt_edit.Text = "Edit";
            bt_save.Text = "Save";
            bt_back.Text = "Back";

         /*   titleLabel.Text = LangResources.PersonPageTitleLabelText;
            firstNameLabelText.Text = LangResources.PersonPageFirstNameLabelText;
            en_firstName.Placeholder = LangResources.PersonPageFirstNamePlaceholder;
            lastNameLabelText.Text = LangResources.PersonPageLastNameLabelText;
            en_lastName.Placeholder = LangResources.PersonPageLastNamePlaceholder;
            emailLabelText.Text = LangResources.PersonPageEmailLabelText;
            en_email.Placeholder = LangResources.PersonPageEmailPlaceholder;
            phoneLabelText.Text = LangResources.PersonPagePhoneLabelText;
            en_phone.Placeholder = LangResources.PersonPagePhonePlaceholder;
            bt_edit.Text = LangResources.PersonPageButtonEditText;
            bt_save.Text = LangResources.PersonPageButtonSaveText;
            bt_back.Text = LangResources.PageBack;                       */


        }//PersonPage
    }//class PersonPage
}//namespace 