using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrmStepMobileApp.Interfaces;
using CrmStepMobileApp.iOS.Native;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SetLocaleImplementation))]
namespace CrmStepMobileApp.iOS.Native
{
    public class SetLocaleImplementation : ISetLocale
    {
        public void SetNativeLocale()
        {
            //var date = (UIDatePicker)Control.InputView;
            //date.Locale = new Foundation.NSLocale("en");
        }
    }
}