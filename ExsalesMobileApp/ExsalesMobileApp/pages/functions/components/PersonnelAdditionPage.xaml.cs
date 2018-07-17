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
using XLabs.Forms.Controls;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonnelAdditionPage : ContentPage
	{
        User currentUser;
		internal PersonnelAdditionPage (User obj)
		{
			InitializeComponent ();

            currentUser = obj;

            lb_email.Text = "User`s email";
            en_email.Placeholder = "Put user`s email";
           
            bt_add.Text = "Save";
           // bt_edit.Text = "Edit";
            bt_del.Text = "X";
            bt_back.Text = "Back";

            // lb_functions.Text = "Select user`s functions";
            //chb_companyManagement.DefaultText = "Company management";
            //chb_networkManagement.DefaultText = "Management of a distribution network";
            //chb_retailManagement.DefaultText = "Management of a retail location";
            //chb_personnelManagement.DefaultText = "Personnel management";
            //chb_productManagement.DefaultText = "Product/service management";
            //chb_bonusManagement.DefaultText = "Bonus point management";
            //chb_salesManagement.DefaultText = "Sales";
            //chb_salesMonitoring.DefaultText = "Sales monitoring";
            //chb_rack_jobberManagement.DefaultText = "Retail location organization (rack jobber)";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_add.Clicked += async (x, y) => { await AddNewUser(); };
           // bt_edit.Clicked += async (x, y) => { await UpdateUser(); };
            bt_del.Clicked += async (x, y) => { await RemoveUser(); };
            lv_functions.ItemSelected += async (x, y) => { await Navigation.PushModalAsync(new SetFunctionsPage((CompanyData)y.SelectedItem, currentUser),true); };

            if (currentUser != null)
            {
                bt_add.IsVisible = false;
                en_email.Text = currentUser.Email;
                en_email.IsEnabled = false;
                lv_functions.IsVisible = true;
            }
            else
            {
                //bt_edit.IsVisible = false;
                bt_del.IsVisible = false;
                lv_functions.IsVisible = false;
            }

        }//ctor

        //удаление пользователя
        private async Task RemoveUser()
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_REMOVE_USER };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey },
                    {"id", currentUser.Id.ToString() },
                };
                api.AddParams(data);
                var res = await api.GetRequest();

                if ((bool)res["status"])
                {
                    await DisplayAlert("Success", "User was deleted", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "User was not deleted", "Done");
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //обновление данных пользователя
        private async Task UpdateUser()
        {
            if (currentUser == null) return;

            try
            {

                //List<FunctionData> functions = new List<FunctionData>();
                //if (chb_companyManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 1, Functions = "Company management" });
                //}
                //if (chb_networkManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 2, Functions = "Management of a distribution network" });
                //}
                //if (chb_retailManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 3, Functions = "Management of a retail location" });
                //}
                //if (chb_personnelManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 4, Functions = "Personnel management" });
                //}
                //if (chb_productManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 5, Functions = "Product/service management" });
                //}
                //if (chb_bonusManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 6, Functions = "Bonus point management" });
                //}
                //if (chb_salesManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 7, Functions = "Sales" });
                //}
                //if (chb_salesMonitoring.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 8, Functions = "Sales monitoring" });
                //}
                //if (chb_rack_jobberManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 9, Functions = "Retail location organization (rack jobber)" });
                //}


                if (en_email.Text != "" && en_email.Text.Length > 0)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_UPDATE_USER };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"id", currentUser.Id.ToString() },
                       // {"email", ApiService.StringUrlEncode(en_email.Text) }
                    };

                    //if (functions.Count > 0)
                    //{
                    //    data.Add("functions", JsonConvert.SerializeObject(functions.ToArray()));
                    //}
                    //else
                    //{
                    //    throw new Exception("At least one function must be selected");
                    //}

                    var res = await api.Post(data);
                    if (res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "User was updated", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Warning", "User was not updated", "Done");
                    }
                }
                else
                {
                    throw new Exception("Email must to be fill");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//UpdateUser()

        protected async override void OnAppearing()
        {
            try
            {
                if (App.APP.CompanyCollection == null)
                {
                    ApiService api = new ApiService { Url = ApiService.URL_GET_COMPANIES };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey }
                    };

                    api.AddParams(data);
                    App.APP.CompanyCollection = (await api.GetCompany()).ToList();
                }

                lv_functions.ItemsSource = App.APP.CompanyCollection;

            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }

            if (currentUser != null)
            {
                try
                {
                    //ApiService api = new ApiService { Url = ApiService.URL_USER_FUNCTIONS };
                    //Dictionary<string, string> data = new Dictionary<string, string>
                    //{
                    //    {"auth_key", App.APP.CurrentUser.AuthKey},
                    //    {"user_id", currentUser.Id.ToString()}
                    //};

                    //api.AddParams(data);

                    //var res = await api.Function();


                    //                  FunctionData temp;

                    //                  //отмечаем включенные функции
                    //                  //компании
                    //                  temp = res.Where(x => x.Id == 1).FirstOrDefault();
                    ////                  await DisplayAlert("temp", temp.ToString(), "ddeded");
                    //                  chb_companyManagement.Checked = temp != null ? true : false;

                    //                  //торговые сети
                    //                  temp = res.Where(x => x.Id == 2).FirstOrDefault();
                    ////                  await DisplayAlert("temp", temp.ToString(), "ddeded");
                    //                  chb_networkManagement.Checked = temp != null ? true : false;

                    //                  //точки реализации
                    //                  temp = res.Where(x => x.Id == 3).FirstOrDefault();
                    //                  chb_retailManagement.Checked = temp != null ? true : false;

                    //                  //personnel management
                    //                  temp = res.Where(x => x.Id == 4).FirstOrDefault();
                    //                  chb_personnelManagement.Checked = temp != null ? true : false;

                    //                  //product management
                    //                  temp = res.Where(x => x.Id == 5).FirstOrDefault();
                    //                  chb_productManagement.Checked = temp != null ? true : false;

                    //                  //bonus management
                    //                  temp = res.Where(x => x.Id == 6).FirstOrDefault();
                    //                  chb_bonusManagement.Checked = temp != null ? true : false;

                    //                  //sales
                    //                  temp = res.Where(x => x.Id == 7).FirstOrDefault();
                    //                  chb_salesManagement.Checked = temp != null ? true : false;

                    //                  //sales monitoring
                    //                  temp = res.Where(x => x.Id == 8).FirstOrDefault();
                    //                  chb_salesMonitoring.Checked = temp != null ? true : false;

                    //                  //rack-jobber
                    //                  temp = res.Where(x => x.Id == 9).FirstOrDefault();
                    //                  chb_rack_jobberManagement.Checked = temp != null ? true : false;
           

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Done");
                }
            }
            base.OnAppearing();
        }

        //добавление нового пользователя
        private async Task AddNewUser()
        {
            try
            {

                //List<FunctionData> functions = new List<FunctionData>();
                //if (chb_companyManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 1, Functions = "Company management" });
                //}
                //if (chb_networkManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 2, Functions = "Management of a distribution network" });
                //}
                //if (chb_retailManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 3, Functions = "Management of a retail location" });
                //}
                //if (chb_personnelManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 4, Functions = "Personnel management" });
                //}
                //if (chb_productManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 5, Functions = "Product/service management" });
                //}
                //if (chb_bonusManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 6, Functions = "Bonus point management" });
                //}
                //if (chb_salesManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 7, Functions = "Sales" });
                //}
                //if (chb_salesMonitoring.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 8, Functions = "Sales monitoring" });
                //}
                //if (chb_rack_jobberManagement.Checked)
                //{
                //    functions.Add(new FunctionData { Id = 9, Functions = "Retail location organization (rack jobber)" });
                //}


                if (en_email.Text != "" && en_email.Text.Length > 0 )
                {
                    ApiService api = new ApiService { Url = ApiService.URL_ADD_USER };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        {"auth_key", App.APP.CurrentUser.AuthKey },
                        {"email", ApiService.StringUrlEncode(en_email.Text) }
                    };

                    //if (functions.Count > 0)
                    //{
                    //    data.Add("functions", JsonConvert.SerializeObject(functions.ToArray()));
                    //}
                    //else
                    //{
                    //    throw new Exception("At least one function must be selected");
                    //}

                    var res = await api.Post(data);
                    if(res == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Success", "User was added", "OK");
                        lv_functions.IsVisible = true;
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
        }//AddNewUser

    }//class
}//namespace