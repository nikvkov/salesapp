using ExsalesMobileApp.library;
using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdditionFunctionCompanyPage : ContentPage
	{
        CompanyData currentCompany;
        User currentUser;
		internal AdditionFunctionCompanyPage (User user, CompanyData company)
		{

			InitializeComponent ();

            currentCompany = company;
            currentUser = user;

            lb_company.Text = "Current company";
            lb_functions.Text = "Functions";

            chb_companyManagement.DefaultText = "Company management";
            chb_networkManagement.DefaultText = "Management of a distribution network";
            chb_retailManagement.DefaultText = "Management of a retail location";
            chb_personnelManagement.DefaultText = "Personnel management";
            chb_productManagement.DefaultText = "Product/service management";
            chb_bonusManagement.DefaultText = "Bonus point management";
            chb_salesManagement.DefaultText = "Sales";
            chb_salesMonitoring.DefaultText = "Sales monitoring";
            chb_rack_jobberManagement.DefaultText = "Retail location organization (rack jobber)";

            bt_save.Text = "Save";
            bt_back.Text = "Back";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_save.Clicked += async (x, y) => { await SaveFunctions(); };

            pc_company.ItemsSource = App.APP.CompanyCollection;
            pc_company.SelectedItem = company;
            pc_company.IsEnabled = false;

		}//c_tor

        /// <summary>
        /// Сохранение функций для компании
        /// </summary>
        /// <returns></returns>
        private async Task SaveFunctions()
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


                if (currentUser!=null && currentCompany!=null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_SET_USER_FUNCTIONS };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", currentUser.Id.ToString()},
                        {"company_id", (currentCompany).Id.ToString() },
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
                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "Functions was added", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Functions was not added", "Done");
                    }
                }
                else
                {
                    throw new Exception("Not found current user");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }

        }//SaveFunctions()


        protected async override void OnAppearing()
        {

            //заполнение данных пользователя
            if (currentUser != null && currentCompany!=null )
            {
                try
                {
                    ApiService api = new ApiService { Url = ApiService.URL_USER_FUNCTIONS };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey},
                        {"user_id", currentUser.Id.ToString()},
                        {"company_id", currentCompany.Id.ToString()}
                    };

                    api.AddParams(data);

                    var res = await api.Function();

                    FunctionData temp;

                    //отмечаем включенные функции
                    //компании
                    temp = res.Where(x => x.Id == 1).FirstOrDefault();
                    chb_companyManagement.Checked = temp != null ? true : false;

                    //торговые сети
                    temp = res.Where(x => x.Id == 2).FirstOrDefault();
                    chb_networkManagement.Checked = temp != null ? true : false;

                    //точки реализации
                    temp = res.Where(x => x.Id == 3).FirstOrDefault();
                    chb_retailManagement.Checked = temp != null ? true : false;

                    //personnel management
                    temp = res.Where(x => x.Id == 4).FirstOrDefault();
                    chb_personnelManagement.Checked = temp != null ? true : false;

                    //product management
                    temp = res.Where(x => x.Id == 5).FirstOrDefault();
                    chb_productManagement.Checked = temp != null ? true : false;

                    //bonus management
                    temp = res.Where(x => x.Id == 6).FirstOrDefault();
                    chb_bonusManagement.Checked = temp != null ? true : false;

                    //sales
                    temp = res.Where(x => x.Id == 7).FirstOrDefault();
                    chb_salesManagement.Checked = temp != null ? true : false;

                    //sales monitoring
                    temp = res.Where(x => x.Id == 8).FirstOrDefault();
                    chb_salesMonitoring.Checked = temp != null ? true : false;

                    //rack-jobber
                    temp = res.Where(x => x.Id == 9).FirstOrDefault();
                    chb_rack_jobberManagement.Checked = temp != null ? true : false;

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Done");
                }
            }

            base.OnAppearing();
        }

    }//class
}//namespace