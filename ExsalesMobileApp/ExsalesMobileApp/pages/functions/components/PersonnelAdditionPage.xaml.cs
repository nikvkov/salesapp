using ExsalesMobileApp.library;
using ExsalesMobileApp.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonnelAdditionPage : ContentPage
	{
        Person currentUser;
		public PersonnelAdditionPage (Object obj)
		{
			InitializeComponent ();

            currentUser = (Person)obj;

            lb_email.Text = "User`s email";
            en_email.Placeholder = "Put user`s email";
            lb_functions.Text = "Select user`s functions";
            bt_add.Text = "Add";
            bt_edit.Text = "Edit";
            bt_del.Text = "X";
            bt_back.Text = "Back";

            chb_companyManagement.DefaultText = "Company management";
            chb_networkManagement.DefaultText = "Management of a distribution network";
            chb_retailManagement.DefaultText = "Management of a retail location";
            chb_personnelManagement.DefaultText = "Personnel management";
            chb_productManagement.DefaultText = "Product/service management";
            chb_bonusManagement.DefaultText = "Bonus point management";
            chb_salesManagement.DefaultText = "Sales";
            chb_salesMonitoring.DefaultText = "Sales monitoring";
            chb_rack_jobberManagement.DefaultText = "Retail location organization (rack jobber)";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_add.Clicked += async (x, y) => { await AddNewUser(); };

        }//ctor

        private async Task AddNewUser()
        {
            try
            {

                List<FunctionData> functions = new List<FunctionData>();
                if (chb_companyManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 1, Functions = "Company management" });
                }
                if (chb_networkManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 2, Functions = "Management of a distribution network" });
                }
                if (chb_retailManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 3, Functions = "Management of a retail location" });
                }
                if (chb_personnelManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 4, Functions = "Personnel management" });
                }
                if (chb_productManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 5, Functions = "Product/service management" });
                }
                if (chb_bonusManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 6, Functions = "Bonus point management" });
                }
                if (chb_salesManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 7, Functions = "Sales" });
                }
                if (chb_salesMonitoring.Checked)
                {
                    functions.Add(new FunctionData { Id = 8, Functions = "Sales monitoring" });
                }
                if (chb_rack_jobberManagement.Checked)
                {
                    functions.Add(new FunctionData { Id = 9, Functions = "Retail location organization (rack jobber)" });
                }


                if (en_email.Text != "" && en_email.Text.Length > 0 )
                {
                    ApiService api = new ApiService { Url = ApiService.URL_ADD_USER };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"email", ApiService.StringUrlEncode(en_email.Text) }
                    };

                    if (functions.Count > 0)
                    {
                        data.Add("functions", JsonConvert.SerializeObject(functions.ToArray()));
                    }
                    else
                    {
                        throw new Exception("At least one function must be selected");
                    }

                    var res = await api.Post(data);
                    if(res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "User was added", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "User was not added", "Done");
                    }
                }
                else
                {
                    throw new Exception("Email must to be fill");
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//

    }//class
}//namespace