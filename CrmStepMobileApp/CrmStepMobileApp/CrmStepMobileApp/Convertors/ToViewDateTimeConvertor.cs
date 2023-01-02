using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class ToViewDateTimeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return String.Empty;
                }

                if (System.Convert.ToString(parameter) == "EventsPage")
                {

                    var dateTime1 = System.Convert.ToDateTime(value);
                    return dateTime1.ToString("dd/MM/yyyy HH:mm");
                }


                var dateTime = System.Convert.ToDateTime(value);
                return dateTime.ToString("dd/MM/yyyy");
            }
            catch(Exception ex)
            {
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
