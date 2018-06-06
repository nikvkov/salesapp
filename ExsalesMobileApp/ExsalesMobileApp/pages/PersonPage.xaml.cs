#define DEBUG
#undef RELEASE

using ExsalesMobileApp.lang;
using ExsalesMobileApp.library;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
#if(DEBUG)
            titleLabel.Text = "Your personal information";
            firstNameLabelText.Text = "First name";
            en_firstName.Placeholder = "Put your first name";
            lastNameLabelText.Text = "Last Name";
            en_lastName.Placeholder = "Put your last name";
            emailLabelText.Text = "Your email";
            en_email.Placeholder = "Put your email";
            phoneLabelText.Text = "Your phone number";
            en_phone.Placeholder = "Put your phone";
            passwordLabelText.Text = "Need new password?";
            en_password.Placeholder = "Put new password";
            bt_edit.Text = "Edit";
            bt_save.Text = "Save";
            bt_back.Text = "Back";
#else

            titleLabel.Text = LangResources.PersonPageTitleLabelText;
               firstNameLabelText.Text = LangResources.PersonPageFirstNameLabelText;
               en_firstName.Placeholder = LangResources.PersonPageFirstNamePlaceholder;
               lastNameLabelText.Text = LangResources.PersonPageLastNameLabelText;
               en_lastName.Placeholder = LangResources.PersonPageLastNamePlaceholder;
               emailLabelText.Text = LangResources.PersonPageEmailLabelText;
               en_email.Placeholder = LangResources.PersonPageEmailPlaceholder;
               phoneLabelText.Text = LangResources.PersonPagePhoneLabelText;
               en_phone.Placeholder = LangResources.PersonPagePhonePlaceholder;
               passwordLabelText.Text = LangResources.PersonPagePasswordLabelText;
               en_password.Placeholder = LangResources.PersonPagePasswordPlaceholder;
               bt_edit.Text = LangResources.PersonPageButtonEditText;
               bt_save.Text = LangResources.PersonPageButtonSaveText;
               bt_back.Text = LangResources.PageBack;                       
#endif               

            //добавляем обработчики событий
            bt_back.Clicked += Bt_back_Clicked;
            bt_edit.Clicked += Bt_edit_Clicked;
            bt_save.Clicked += Bt_save_Clicked;
            en_firstName.TextChanged += En_field_TextChanged;
            en_lastName.TextChanged += En_field_TextChanged;
            en_email.TextChanged += En_field_TextChanged;
            en_email.Completed += En_email_Completed;
            en_phone.TextChanged += En_field_TextChanged;
            en_password.TextChanged+= En_field_TextChanged;

           
        }//PersonPage

        //окончание редактирования email
        private async void En_email_Completed(object sender, EventArgs e)
        {
            await DisplayAlert("Email", "Вы изменили email. Проверьте правильность указанного email для избежания проблем с авторизацией", "OK");
            //await DisplayAlert("Email", LangResources.PersonPageMessageChangeEmail, "OK");

        }

        //сохранение данных
        private async void Bt_save_Clicked(object sender, EventArgs e)
        {
            try
            {
#if (DEBUG)
                if (String.IsNullOrEmpty(en_firstName.Text) || en_firstName.Text.Length == 0) throw new Exception("Проверьте указанное имя!");
                if (String.IsNullOrEmpty(en_lastName.Text) || en_lastName.Text.Length == 0) throw new Exception("Проверьте указанную фамилию!");
                if (String.IsNullOrEmpty(en_email.Text) || en_email.Text.Length == 0) throw new Exception("Проверьте email!");
                if (String.IsNullOrEmpty(en_phone.Text) || en_phone.Text.Length == 0) throw new Exception("Проверьте телефон!");
                if (sw_needPassword.IsToggled && (String.IsNullOrEmpty(en_password.Text) || en_password.Text.Length <8)) throw new Exception("Пароль должен содержать не менее 8 символов!");
#else
                 if (String.IsNullOrEmpty(en_firstName.Text) || en_firstName.Text.Length == 0) throw new Exception(LangResources.PersonPageMessageBadFirstName);
                   if (String.IsNullOrEmpty(en_lastName.Text) || en_lastName.Text.Length == 0) throw new Exception(LangResources.PersonPageMessageBadLastName);
                   if (String.IsNullOrEmpty(en_email.Text) || en_email.Text.Length == 0) throw new Exception(LangResources.PersonPageMessageBadEmail);
                   if (String.IsNullOrEmpty(en_phone.Text) || en_phone.Text.Length == 0) throw new Exception(LangResources.PersonPageMessageBadPhone);
                   if (sw_needPassword.IsToggled && (String.IsNullOrEmpty(en_password.Text) || en_password.Text.Length < 8)) throw new Exception(LangResources.PersonPageMessageBadPassword);
#endif                

                //собираем данные для запроса
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("auth_key", user.AuthKey);
                data.Add("firstName", en_firstName.Text);
                data.Add("lastName", en_lastName.Text);
                data.Add("email", en_email.Text);
                data.Add("phone", en_phone.Text);
                if (sw_needPassword.IsToggled)
                {
                    data.Add("password", en_password.Text);
                }

                //отправляем запрос
                //задаем url отправки
                ApiService api = new ApiService
                {
                    Url = "https://www.exsales.net/api/v1/user/save"
                };

                //отправляем запрос и ждем результат
                var res = await api.Post(data);

                if (res == HttpStatusCode.OK)
                {
#if(DEBUG)
                    await DisplayAlert("Success", "Данные пользователя обновлены", "OK");
#else
                    await DisplayAlert(LangResources.Success, LangResources.PersonPageMessageUpdateData, "OK");
#endif
                }
                else
                {
#if(DEBUG)
                    await DisplayAlert("Warning", "Ошибка обновления данных!Проверьте правильность указанных данных и попробуйте еще раз", "OK");
#else
                    await DisplayAlert(LangResources.Warning, LangResources.PersonPageMessageNotUpdateData, "OK");
#endif
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", ex.Message, "OK");
            }
        }

        private void En_field_TextChanged(object sender, TextChangedEventArgs e)
        {
            bt_save.IsEnabled = true;
        }

        //нажати на кнопку редактировать
        private void Bt_edit_Clicked(object sender, EventArgs e)
        {
            en_firstName.IsEnabled = true;
            en_lastName.IsEnabled = true;
            en_email.IsEnabled = true;
            en_phone.IsEnabled = true;
        }

        //нажатие на кнопку назад
        private async void Bt_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }//class PersonPage
}//namespace 