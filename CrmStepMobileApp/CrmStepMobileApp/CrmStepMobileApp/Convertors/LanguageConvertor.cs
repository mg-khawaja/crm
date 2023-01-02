using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class LanguageConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var val = System.Convert.ToString(value);
                if (!String.IsNullOrEmpty(val))
                {

                }
            }
            catch(Exception ex)
            {

            }

           
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
