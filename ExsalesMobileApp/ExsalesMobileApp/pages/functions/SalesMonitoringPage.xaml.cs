using ExsalesMobileApp.pages.functions.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExsalesMobileApp.pages.functions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesMonitoringPage : TabbedPage
    {
        public SalesMonitoringPage ()
        {
            InitializeComponent();

            var ownerPages = new OwnerSalesMonitoring();
            var otherSalesMonitoring = new OtherSalesMonitoring();

            Children.Add(ownerPages);
            Children.Add(otherSalesMonitoring);
        }
    }
}