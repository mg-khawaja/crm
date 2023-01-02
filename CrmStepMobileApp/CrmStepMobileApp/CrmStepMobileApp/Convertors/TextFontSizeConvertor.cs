using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class TextFontSizeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = System.Convert.ToString(value);
            if (String.IsNullOrEmpty(str))
            {
                return 12;
            }
            if (str.Length > 15)
            {
                return 14;
            }
            return 20;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
