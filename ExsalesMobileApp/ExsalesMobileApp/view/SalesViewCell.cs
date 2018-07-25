using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.view
{
    class SalesViewCell : ViewCell
    {

        Label titleLabel, eanLabel, bonusLabel;
     
        public SalesViewCell()
        {
            titleLabel = new Label { FontSize = 18 };
            eanLabel = new Label();
            bonusLabel = new Label();

            StackLayout cell = new StackLayout();
            cell.Orientation = StackOrientation.Horizontal;

            StackLayout titleDetailLayout = new StackLayout();
            titleDetailLayout.Children.Add(titleLabel);
            titleDetailLayout.Children.Add(eanLabel);

            cell.Children.Add(titleDetailLayout);
            cell.Children.Add(bonusLabel);
            View = cell;
        }

        public static readonly BindableProperty TitleProperty =
             BindableProperty.Create("Title", typeof(string), typeof(SalesViewCell), "");

        public static readonly BindableProperty EANProperty =
             BindableProperty.Create("EAN", typeof(string), typeof(SalesViewCell), "");

        public static readonly BindableProperty BonusProperty =
             BindableProperty.Create("Bonus", typeof(int), typeof(SalesViewCell), 0);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string EAN
        {
            get { return (string)GetValue(EANProperty); }
            set { SetValue(EANProperty, value); }
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
                titleLabel.Text = Title;
                eanLabel.Text = EAN;
                bonusLabel.Text = Bonus.ToString();
            }
        }


    }//class
}
