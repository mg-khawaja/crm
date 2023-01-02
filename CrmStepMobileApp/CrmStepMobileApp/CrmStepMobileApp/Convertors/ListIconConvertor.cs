using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class ListIconConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var str = System.Convert.ToString(value);
                if (String.IsNullOrEmpty(str))
                {
                    return true;
                }
                else
                {
                    return false;
                }
             

            }
            catch(Exception ex)
            {

            }
            return true;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
