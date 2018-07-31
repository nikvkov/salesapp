using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.view
{
    class OwnSaleCell : ViewCell
    {

        Label productLabel, quantityLabel, bonusLabel;
        // Image image;

        public OwnSaleCell()
        {
            productLabel = new Label { FontSize = 20 };
            quantityLabel = new Label();
            bonusLabel = new Label();
            //   image = new Image();

            StackLayout cell = new StackLayout();
            cell.Orientation = StackOrientation.Horizontal;

            StackLayout titleDetailLayout = new StackLayout();
            titleDetailLayout.Orientation = StackOrientation.Vertical;
            titleDetailLayout.Children.Add(quantityLabel);
            titleDetailLayout.Children.Add(bonusLabel);

            cell.Children.Add(productLabel);
            cell.Children.Add(titleDetailLayout);
            View = cell;
        }

        public static readonly BindableProperty ProductProperty =
            BindableProperty.Create("Product", typeof(string), typeof(OwnSaleCell), "");

        //public static readonly BindableProperty ImagePathProperty =
        //    BindableProperty.Create("ImagePath", typeof(ImageSource), typeof(BranchListViewCell), null);

        //public static readonly BindableProperty ImageWidthProperty =
        //    BindableProperty.Create("ImageWidth", typeof(int), typeof(CustomCell), 100);

        //public static readonly BindableProperty ImageHeightProperty =
        //    BindableProperty.Create("ImageHeight", typeof(int), typeof(CustomCell), 100);

        public static readonly BindableProperty QuantityProperty =
            BindableProperty.Create("Quantity", typeof(int), typeof(OwnSaleCell), 0);

        public static readonly BindableProperty BonusProperty =
            BindableProperty.Create("Bonus", typeof(int), typeof(OwnSaleCell), 0);

        public string Product
        {
            get { return (string)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
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

        public int Quantity
        {
            get { return (int)GetValue(QuantityProperty); }
            set { SetValue(QuantityProperty, value); }
        }

        public int Bonus
        {
            get { return (int)GetValue(BonusProperty); }
            set { SetValue(BonusProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                productLabel.Text = Product;
                quantityLabel.Text = Quantity.ToString();
                bonusLabel.Text = Bonus.ToString();
                //image.Source = ImagePath;
                //image.WidthRequest = ImageWidth;
                //image.HeightRequest = ImageHeight;
            }
        }

    }
}
