using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class EventHomeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is EventsHomeModel eventsHomeModel)
            {
                var list = new List<string>();
                if(!String.IsNullOrEmpty(eventsHomeModel.location)){
                    list.Add(eventsHomeModel.location);

                }
                list.Add(eventsHomeModel.assign_to);

              return  String.Join(" , ", list.ToArray());
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
