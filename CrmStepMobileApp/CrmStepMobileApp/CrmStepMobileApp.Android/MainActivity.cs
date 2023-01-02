using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Java.Util;
using Android.Icu.Util;
using Rg.Plugins.Popup.Services;
using Plugin.CrossPlatformTintedImage.Android;
using Java.Text;
using Plugin.CurrentActivity;
using Plugin.Multilingual;
using Android.Content;
using Xamarin.Forms;
using CrmStepMobileApp.Interfaces;
using CrmStepMobileApp.Droid.Native;

namespace CrmStepMobileApp.Droid
{
    [Activity(Label = "CrmStep", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        Exported = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Instance = this;

            TintedImageRenderer.Init();
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQyNTQ1QDMxMzgyZTMxMmUzMEk3aDVZbUhmZGkzZk5aT1J4bW5mQXYra2gwU3Fad0d3L05XbHh1elNWZWs9");

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            LoadApplication(new App());
            CreateNotificationFromIntent(Intent);
            StartService(new Intent(this, typeof(BackgroundService)));
        }

        public override async void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
        }
    }
}