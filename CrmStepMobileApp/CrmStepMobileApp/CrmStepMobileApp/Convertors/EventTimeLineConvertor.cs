using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class EventTimeLineConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = System.Convert.ToInt32(value);
            switch (id)
            {
                case 1:
                    return "task";


                case 2:
                    return "meeting";


                case 3:
                    return "call";


                case 4:
                    return "comment";


                case 5:
                    return "sms";
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
