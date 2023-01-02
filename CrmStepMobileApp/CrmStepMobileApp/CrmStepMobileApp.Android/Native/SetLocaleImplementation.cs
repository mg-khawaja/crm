using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CrmStepMobileApp.Droid.Native;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(SetLocaleImplementation))]
namespace CrmStepMobileApp.Droid.Native
{
    public class SetLocaleImplementation : ISetLocale
    {
        public void SetNativeLocale()
        {
            try
            {
            MainActivity.Instance.Resources.Configuration.SetLocale(new Java.Util.Locale(StaticData.SelectedLanguageModel.PluginName));
            }
            catch (Exception ex)
            {

            }

        }
    }
}