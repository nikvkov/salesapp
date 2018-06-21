using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.view
{
    class BranchListViewCell:ViewCell
    {

        Label titleLabel, companyLabel, retailerLabel;
       // Image image;

        public BranchListViewCell()
        {
            titleLabel = new Label { FontSize = 20};
            companyLabel = new Label();
            retailerLabel = new Label();
         //   image = new Image();

            StackLayout cell = new StackLayout();
            cell.Orientation = StackOrientation.Horizontal;

            StackLayout titleDetailLayout = new StackLayout();
            titleDetailLayout.Orientation = StackOrientation.Vertical;
            titleDetailLayout.Children.Add(companyLabel);
            titleDetailLayout.Children.Add(retailerLabel);

            cell.Children.Add(titleLabel);
            cell.Children.Add(titleDetailLayout);
            View = cell;
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(BranchListViewCell), "");

        //public static readonly BindableProperty ImagePathProperty =
        //    BindableProperty.Create("ImagePath", typeof(ImageSource), typeof(BranchListViewCell), null);

        //public static readonly BindableProperty ImageWidthProperty =
        //    BindableProperty.Create("ImageWidth", typeof(int), typeof(CustomCell), 100);

        //public static readonly BindableProperty ImageHeightProperty =
        //    BindableProperty.Create("ImageHeight", typeof(int), typeof(CustomCell), 100);

        public static readonly BindableProperty CompanyProperty =
            BindableProperty.Create("Company", typeof(string), typeof(BranchListViewCell), "");

        public static readonly BindableProperty RetailerProperty =
            BindableProperty.Create("Retailer", typeof(string), typeof(BranchListViewCell), "");

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //public int ImageWidth
        //{
        //    get { return (int)GetValue(ImageWidthProperty); }
        //    set { SetValue(ImageWidthProperty, value); }
        //}
        //public int ImageHeight
        //{
        //    get { return (int)GetValue(ImageHeightProperty); }
        //    set { SetValue(ImageHeightProperty, value); }
        //}

        //public ImageSource ImagePath
        //{
        //    get { return (ImageSource)GetValue(ImagePathProperty); }
        //    set { SetValue(ImagePathProperty, value); }
        //}

        public string Company
        {
            get { return (string)GetValue(CompanyProperty); }
            set { SetValue(CompanyProperty, value); }
        }

        public string Retailer
        {
            get { return (string)GetValue(RetailerProperty); }
            set { SetValue(RetailerProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                titleLabel.Text = Title;
                companyLabel.Text = Company;
                retailerLabel.Text = Retailer;
                //image.Source = ImagePath;
                //image.WidthRequest = ImageWidth;
                //image.HeightRequest = ImageHeight;
            }
        }

    }//class
}//namespace
