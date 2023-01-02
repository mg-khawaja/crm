using Plugin.Multilingual;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CrmStepMobileApp.Helper
{
    public static class Extensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static string GetDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static bool CompareString(this string first, string second)
        {
            if (String.IsNullOrEmpty(first) || String.IsNullOrEmpty(second))
            {
                return false;
            }

            return first.ToLower().Contains(second.ToLower());

        }




        public static Dictionary<string, string> GetPropNames<T>(this T inputModel)
        {
            //var list = new List<string>();

            PropertyInfo[] infos = inputModel.GetType().GetProperties();

            Dictionary<string, string> dix = new Dictionary<string, string>();

            foreach (PropertyInfo info in infos)
            {
                string val = String.Empty;
                try
                {
                    val = info.GetValue(inputModel, null).ToString();
                }
                catch (Exception ex)
                {

                }



                dix.Add(info.Name, val);
            }

            foreach (string key in dix.Keys)
            {
                Console.WriteLine("nameProperty: {0}; value: {1}", key, dix[key]);
            }

            //PropertyInfo[] propertyInfos;
            //propertyInfos = type.GetProperties(BindingFlags.Public |
            //                                              BindingFlags.Static);
            //// sort properties by name
            //Array.Sort(propertyInfos,
            //        delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
            //        { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            //// write property names
            //foreach (PropertyInfo propertyInfo in propertyInfos)
            //{
            //    list.Add(propertyInfo.Name);
            //}

            return dix;
        }

        public static string GetValFromDic(this string input, Dictionary<string, string> dic)
        {
            try
            {
                var newInput = RemoveWhitespace(input);
                if (dic.ContainsKey(newInput))
                {

                    var ouput = dic[newInput];
                    return ouput;

                }
              
            }
            catch(Exception ex)
            {

            }
            return input;
          
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }

    public class ItemEqualityComparer : IEqualityComparer<ScheduleAppointment>
    {
        public bool Equals(ScheduleAppointment x, ScheduleAppointment y)
        {
            // Two items are equal if their keys are equal.
            return (x.Location == y.Location);
        }

        public int GetHashCode(ScheduleAppointment obj)
        {
            return obj.Location.GetHashCode();
        }
    }
}
