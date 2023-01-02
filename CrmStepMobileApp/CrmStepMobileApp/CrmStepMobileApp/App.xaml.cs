using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Interfaces;
using CrmStepMobileApp.Locator;
using CrmStepMobileApp.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Multilingual;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp
{
    public partial class App : Application
    {

        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public static MenuPage MenuPage;

        public static SfSchedule SfSchedule;

        public static bool IsRootPage { get; set; }

        public App()
        {
            InitializeComponent();


            XF.Material.Forms.Material.Init(this);


            TimeZone localZone = TimeZone.CurrentTimeZone;
            DateTime currentDate = DateTime.Now;
            TimeSpan currentOffset =
                localZone.GetUtcOffset(currentDate);


            if (Settings.SelectedLang == 0)
            {
                Settings.SelectedLang = 2;
            }

            StaticData.SelectedLanguageModel = StaticData.LangList.FirstOrDefault(s => s.Id == Settings.SelectedLang);
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(StaticData.SelectedLanguageModel.PluginName);
            StaticData.UTCMins = Convert.ToInt32(currentOffset.TotalMinutes);
            StaticData.DeviceId = 111;
            DependencyService.Get<ISetLocale>().SetNativeLocale();



            MainPage = new NavigationPage(new LoginPage());
            
        }

        public static void SetPagePadding(Grid grid)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                grid.Padding = new Thickness(0, 30, 0, 0);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Microsoft.AppCenter.AppCenter.Start(
                "android=5462c7dc-8e7d-4b29-939e-7470027e084e;" +
                "ios=3b86c934-16f3-4c08-a332-b65b0ea1d1e4;",
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
