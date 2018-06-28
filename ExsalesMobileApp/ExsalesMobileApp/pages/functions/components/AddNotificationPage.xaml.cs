using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNotificationPage : Xamarin.Forms.ContentPage
    {
        Branch currentBranch;
        CustomNotification currentNotice;
        List<string> imgSource;
        List<string> tempImgSource;
        int count = 0;
        ObservableCollection<ServerImage> images;

        internal AddNotificationPage (Branch _branch, CustomNotification _notice)
		{
			InitializeComponent ();
            currentBranch = _branch;
            currentNotice = _notice;

            if(_notice == null)
            {
                imgSource = new List<string>();
                bt_edit.IsVisible = false;
                bt_delete.IsVisible = false;
            }
            else
            {
                imgSource = currentNotice.Pictures;
                tempImgSource = new List<string>();
                bt_add.IsVisible = false;
            }

            bt_add.Text = "Save";
            bt_edit.Text = "Edit";
            bt_delete.Text = "X";
            bt_back.Text = "Back";

            bt_add_photo.Text = "Add photo";
            bt_make_photo.Text = "Make photo";

            en_item_header.Placeholder = "Put header";

            bt_add.Clicked += Bt_add_Clicked;
            bt_edit.Clicked += Bt_edit_Clicked;
            bt_delete.Clicked += Bt_delete_Clicked;
            bt_make_photo.Clicked += Bt_make_photo_Clicked;
            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_add_photo.Clicked += Bt_add_photo_Clicked;
            lv_images.Unfocus();

		}

        //сделать фото
        private async void Bt_make_photo_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                        Directory = "Exsales",
                        Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                        //Name = string.Format("{0}", DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss"))
                    });

                    if (file == null)
                    {
                       
                        return;
                    }


                    if (currentNotice == null)
                    {
                        imgSource.Add(file.Path);
                    }
                    else
                    {
                        if (file.Path != null && file.Path != "")
                        {
                            WebClient myWebClient = new WebClient();
                            myWebClient.QueryString = new System.Collections.Specialized.NameValueCollection
                                {
                                    { "notice_id",currentNotice.Id.ToString() }
                                };
                            byte[] responseArray = myWebClient.UploadFile(ApiService.URL_ADD_MEDIA, file.Path);
                            var ans = System.Text.Encoding.ASCII.GetString(responseArray);

                            JObject o = JObject.Parse(ans);

                            if ((bool)o["status"])
                            {
                                await DisplayAlert("Success", "Image was added", "OK");
                                OnAppearing();
                            }
                            else
                            {
                                await DisplayAlert("Warning", "Image was not added", "Done");
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //удаление записи 
        private async void Bt_delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_REMOVE_NOTIFICATION };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"id", currentNotice.Id.ToString() }
                };
                api.AddParams(data);
                var res = await api.GetRequest();
                if ((bool)res["status"])
                {
                    await DisplayAlert("Success", "Notice was deleted", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Notice was deleted", "Done");
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //редактирование записи
        private async void Bt_edit_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (currentBranch == null) throw new Exception();
                if (en_item_header.Text.Length == 0) throw new Exception("Header must be fill");
                if (ed_item_content.Text.Length == 0) throw new Exception("Content must be fill");
                ApiService api = new ApiService { Url = ApiService.URL_EDIT_NOTIFICATION };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey},
                    {"id", currentNotice.Id.ToString()},
                    {"header", en_item_header.Text},
                    {"content", ed_item_content.Text},
                   // {"images", JsonConvert.SerializeObject(imgSource)},
                };

                var res = await api.Post(data);

                if (res == System.Net.HttpStatusCode.OK)
                {
                    foreach (var item in tempImgSource)
                    {
                        WebClient myWebClient = new WebClient();
                        myWebClient.QueryString = new System.Collections.Specialized.NameValueCollection
                        {
                            { "notice_id",currentNotice.Id.ToString() }
                        };
                        byte[] responseArray = myWebClient.UploadFile(ApiService.URL_ADD_MEDIA, item);
                        var ans = System.Text.Encoding.ASCII.GetString(responseArray);

                    }
                    await DisplayAlert("Success", "Notification was updated", "OK");
                    await Navigation.PopModalAsync(true);

                }
                else
                {
                    await DisplayAlert("Warning", "Notification was not updated", "Done");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //показать картинку
        private async void bt_viewClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new DetailImage((ServerImage)lv_images.Item, currentNotice), true);
        }

        //удаление картинки
        private async void bt_deleteClick(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url = ApiService.URL_REMOVE_MEDIA };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"id", currentNotice.Id.ToString()},
                    {"image", ((ServerImage)lv_images.Item).CustomImage }
                };

                api.AddParams(data);

                var res = await api.GetRequest();

                if ((bool)res["status"])
                {
                    await DisplayAlert("Success", "Image was deleted", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Image was not deleted", "Done");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }

        //заполнение данных
        void FillData()
        {
            if (currentNotice != null)
            {
                en_item_header.Text = currentNotice.Header;
                ed_item_content.Text = currentNotice.Content;
                images = new ObservableCollection<ServerImage>();

                if (currentNotice.Media != null && currentNotice.Media!="")
                {
                    string[] urls = currentNotice.Media.Trim().Split(' ');

                    foreach (var item in urls)
                    {
                        images.Add(new ServerImage(item));

                    }

                }

                lv_images.ItemsSource = images;
            }

        }//FillData

        protected async override void OnAppearing()
        {


            if (currentNotice != null)
            {
         
                try
                {
                    //await DisplayAlert("Warning", currentNotice.Id.ToString(), "Done");
                    ApiService api = new ApiService { Url = ApiService.URL_GET_CURRENT_NOTIFICATION };
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "id", currentNotice.Id.ToString() }
                    };
                    api.AddParams(data);
                    JObject res = await api.GetRequest();
                   // await DisplayAlert("Warning", res["data"].ToString(), "Done");
                    currentNotice = CustomNotification.GetNotice(res);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Warning", ex.Message, "Done");
                }

            }
            FillData();

            base.OnAppearing();
        }

        //добавить новое фото
        private async void Bt_add_photo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    Xamarin.Forms.ImageSource img = Xamarin.Forms.ImageSource.FromFile(photo.Path);
                    if (currentNotice == null)
                    {
                        imgSource.Add(photo.Path);
                    }
                    else
                    {
                        if (photo.Path != null && photo.Path != "")
                        {
                            //tempImgSource.Add(photo.Path);
                            //images.Add(new ServerImage(photo.Path));
                            WebClient myWebClient = new WebClient();
                            myWebClient.QueryString = new System.Collections.Specialized.NameValueCollection
                            {
                                { "notice_id",currentNotice.Id.ToString() }
                            };
                            byte[] responseArray = myWebClient.UploadFile(ApiService.URL_ADD_MEDIA, photo.Path);
                            var ans = System.Text.Encoding.ASCII.GetString(responseArray);

                            JObject o = JObject.Parse(ans);

                            if ((bool)o["status"])
                            {
                                await DisplayAlert("Success", "Image was added", "OK");
                                OnAppearing();
                            }
                            else
                            {
                                await DisplayAlert("Warning", "Image was not added", "Done");
                            }
                        }
                    }
         
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }//Bt_add_photo_Clicked

        //добавление
        private async void Bt_add_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (currentBranch==null) throw new Exception();
                if (en_item_header.Text.Length == 0) throw new Exception("Header must be fill");
                if(ed_item_content.Text.Length == 0) throw new Exception("Content must be fill");
                ApiService api = new ApiService {Url = ApiService.URL_ADD_NOTIFICATION };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"auth_key", App.APP.CurrentUser.AuthKey},
                    {"link_id", currentBranch.Id.ToString()},
                    {"header", en_item_header.Text},
                    {"content", ed_item_content.Text},
                   // {"images", JsonConvert.SerializeObject(imgSource)},
                };

                var res = await api.Post(data);

                if (res == System.Net.HttpStatusCode.OK)
                {
                    foreach (var item in imgSource)
                    {
                        WebClient myWebClient = new WebClient();
                        myWebClient.QueryString = new System.Collections.Specialized.NameValueCollection
                        {
                            { "link_id",currentBranch.Id.ToString() }
                        };
                        byte[] responseArray = myWebClient.UploadFile(ApiService.URL_ADD_MEDIA, item);
                        var ans = System.Text.Encoding.ASCII.GetString(responseArray);

                    }
                    await DisplayAlert("Success", "Notification was added", "OK");
                    await Navigation.PopModalAsync(true);

                }
                else
                {
                    await DisplayAlert("Warning", "Notification was not added", "Done");
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error",ex.Message, "Done");
            }
        }

    }//class
}//namespace