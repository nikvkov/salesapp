using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.model
{
    public class ServerImage
    {
        public string CustomImage { get; set; }

        public UriImageSource UriImage { get; private set; }

        public ServerImage(string uri)
        {
            UriImage = new UriImageSource
            {
                CachingEnabled = true,
                CacheValidity = new System.TimeSpan(2, 0, 0, 0),
                Uri = new System.Uri(uri)
            };

            CustomImage = uri;
        }
        
    }

    
}
