using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ExsalesMobileApp.interfaces
{
    /**
     *Интерфейс для определения локали устройства - переопределяется для каждого устройства
     */
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
