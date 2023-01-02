using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class StringFormatConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var str = System.Convert.ToString(value);
                if (!String.IsNullOrEmpty(str) && str!="0")
                {
                    return System.Convert.ToDouble(string.Format("{0:F2}", str)).ToString("N");
                }
            }
            catch(Exception ex)
            {

            }
          
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
