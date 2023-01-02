using System;
using System.Collections.Generic;
using System.Linq;
using CrmStepMobileApp.iOS.Native;
using FFImageLoading.Forms.Platform;
using Foundation;
using Plugin.CrossPlatformTintedImage.iOS;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfSchedule.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using UIKit;
using UserNotifications;

namespace CrmStepMobileApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        //public LocationManager Manager { get; set; }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();
            CachedImageRenderer.Init();
            XF.Material.iOS.Material.Init();
            TintedImageRenderer.Init();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQyNTQ1QDMxMzgyZTMxMmUzMEk3aDVZbUhmZGkzZk5aT1J4bW5mQXYra2gwU3Fad0d3L05XbHh1elNWZWs9");
            SfCalendarRenderer.Init();
            SfScheduleRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfRadioButtonRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();


            var lang = NSBundle.MainBundle.PreferredLocalizations[0];
            Syncfusion.XForms.iOS.Buttons.SfSwitchRenderer.Init();

            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
);
            app.RegisterUserNotificationSettings(notificationSettings);

            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver();

            LoadApplication(new App());


            var notify = new iOSNotificationManager();
            notify.Initialize();
            //notify.SendNotification("Test", "Test", DateTime.Now.AddMinutes(1));

            //BackgroundService background = new BackgroundService();
            //background.Start();

            //Manager = new LocationManager();

            ////Background Location Permissions
            //if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            //{
            //    Manager.locMgr.RequestAlwaysAuthorization();
            //}

            //if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            //{
            //    Manager.locMgr.AllowsBackgroundLocationUpdates = true;
            //}

            //Manager.StartLocationUpdates();

            bool result = base.FinishedLaunching(app, options);

            ObjCRuntime.Selector selector = new ObjCRuntime.Selector("setSemanticContentAttribute:");
            IntPtr_objc_msgSend(UIView.Appearance.Handle, selector.Handle, UISemanticContentAttribute.ForceRightToLeft);

            return result;
        }
        [System.Runtime.InteropServices.DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
        internal extern static IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector, UISemanticContentAttribute arg1);
        //public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        //{
        //    // show an alert
        //    UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
        //    okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

        //    Window.RootViewController.PresentViewController(okayAlertController, true, null);

        //    // reset our badge
        //    UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        //}

    }
}
