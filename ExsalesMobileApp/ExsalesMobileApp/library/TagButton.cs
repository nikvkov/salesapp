using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExsalesMobileApp.library
{
    public class TagButton : Button
    {
        public static readonly BindableProperty TagProperty =
            BindableProperty.Create("Tag",                      // название обычного свойства
                                     typeof(int),               // возвращаемый тип 
                                     typeof(TagButton),         // тип,  котором объявляется свойство
                                     0                          // значение по умолчанию
            );
        public int Tag
        {
            set
            {
                SetValue(TagProperty, value);
            }
            get
            {
                return (int)GetValue(TagProperty);
            }
        }
    }
}
