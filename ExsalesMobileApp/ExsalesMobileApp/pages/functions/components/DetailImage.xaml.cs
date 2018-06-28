using ExsalesMobileApp.model;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions.components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailImage : ContentPage
	{
        ServerImage currentImage;
        CustomNotification notice;
        internal DetailImage (ServerImage _image, CustomNotification _notice)
		{
			InitializeComponent ();
            currentImage = _image;
            notice = _notice;

            if(currentImage != null)
            {
                im_current.Source = currentImage.UriImage;
            }

            bt_back.Text = "Back";
            bt_del.Text = "X";

            bt_back.Clicked += async (x, y) => { await Navigation.PopModalAsync(true); };
            bt_del.Clicked += Bt_del_Clicked;
		}

        //удаление
        private async void Bt_del_Clicked(object sender, EventArgs e)
        {
            try
            {
                ApiService api = new ApiService { Url=ApiService.URL_REMOVE_MEDIA };
                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    {"id", notice.Id.ToString()},
                    {"image", currentImage.CustomImage }
                };

                api.AddParams(data);

                var res = await api.GetRequest();

                if((bool)res["status"])
                {
                    await DisplayAlert("Success", "Image was deleted", "OK");
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await DisplayAlert("Warning", "Image was not deleted", "Done");
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Done");
            }
        }
    }//class
}//namespace