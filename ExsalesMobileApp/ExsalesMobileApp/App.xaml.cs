using ExsalesMobileApp.interfaces;
using ExsalesMobileApp.lang;
using ExsalesMobileApp.pages;
using ExsalesMobileApp.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExsalesMobileApp
{
	public partial class App : Application
	{
        //создаем объект класса с парметрами
        internal static AppParameters APP;

        public App ()
		{
			InitializeComponent();

            //создаем объект класса с парметрами
            APP = AppParameters.getInstance();

            //определение культуры устройства
            if (Device.OS != TargetPlatform.WinPhone)
            {
                LangResources.Culture = DependencyService.Get<ILocalize>()
                                    .GetCurrentCultureInfo();
            }

            //задаем стартовую страницу приложения
            //MainPage = new ExsalesMobileApp.MainPage();
            //MainPage = new NavigationPage(new StartPage());
            MainPage = new StartPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
