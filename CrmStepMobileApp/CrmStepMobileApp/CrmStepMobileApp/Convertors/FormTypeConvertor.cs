using CrmStepMobileApp.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.Convertors
{
    public class FormTypeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter is string lang)
            {
                var typeID = System.Convert.ToInt32(value);
                var val = Enum.GetName(typeof(FormType), typeID);//  field.FormType
                if(lang == "1")
                {
                    switch (val)
                    {
                        case "Leads":
                            return "לידים";
                        case "Contacts":
                            return "אנשי קשר";
                        case "Companies":
                            return "חברות";
                        case "Deals":
                            return "עסקאות";
                        case "Tickets":
                            return "קריאות";
                    }
                }
                return val;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
