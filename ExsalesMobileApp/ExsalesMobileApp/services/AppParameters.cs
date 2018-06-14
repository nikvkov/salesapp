using ExsalesMobileApp.library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static ExsalesMobileApp.services.ApiService;

namespace ExsalesMobileApp.services
{
    class AppParameters
    {
        private static AppParameters instance;

        public Person CurrentUser
        {
            get; set;
        }

        public List<CompanyData> CompanyCollection
        {
            get;set;
        }

        private AppParameters() { }

        public static AppParameters getInstance()
        {
            if (instance == null)
                instance = new AppParameters();
            return instance;
        }

    }//class

}//namespace
