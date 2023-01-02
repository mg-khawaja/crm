using CoreLocation;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Services;
using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;

namespace CrmStepMobileApp.iOS.Native
{
    public class LocationManager
    {
        public CLLocationManager locMgr;
        int notificationsInterval = 1 * 30000;
        iOSNotificationManager notify = new iOSNotificationManager();
        DetailsService DetailsService = new DetailsService();
        LoginServices LoginServices = new LoginServices();
        SQLiteDB database;
        int count = 4;
        bool IsFlagOn = true;
        public LocationManager()
        {

            this.locMgr = new CLLocationManager();
            this.locMgr.RequestAlwaysAuthorization();
            this.locMgr.PausesLocationUpdatesAutomatically = false;

            // iOS 8 has additional permissions requirements
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                locMgr.RequestAlwaysAuthorization(); // works in background
                                                     //locMgr.RequestWhenInUseAuthorization (); // only in foreground
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                locMgr.AllowsBackgroundLocationUpdates = true;
            }
        }
        public void StartLocationUpdates()
        {
            try
            {
                if (CLLocationManager.LocationServicesEnabled)
                {
                    //set the desired accuracy, in meters
                    locMgr.DesiredAccuracy = 1;
                    locMgr.LocationsUpdated += LocMgr_LocationsUpdated;
                    locMgr.StartUpdatingLocation();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LocMgr_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            if (IsFlagOn)
            {
                callBackgroundService();
            }
            //var notify = new iOSNotificationManager();
            //notify.Initialize();
            //notify.SendNotification("Test", "Test");

        }
        private async void callBackgroundService()
        {
            IsFlagOn = false;
            if (true)
            {
                if (database == null)
                {
                    database = await SQLiteDB.Instance;
                }
                if (count % 8 == 0)
                {
                    var user = await database.GetUserAsync();
                    if (user != null)
                    {
                        StaticData.AccessToken = user.AccessToken;
                        try
                        {
                            var diff = DateTime.Now.Subtract(user.FetchedTime);

                            //var n = DateTime.Compare(expiry, DateTime.Now);
                            if (diff.TotalHours >= 24)
                            {
                                // greater than 24 hour
                                var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                                StaticData.UTCMins, StaticData.DeviceId, user.Username, user.Password, DeviceInfo.Platform.ToString());
                                if (result.status == 200)
                                {
                                    Settings.Username = user.Username;
                                    Settings.Password = user.Password;
                                    StaticData.UserModel = result.data.user;
                                    StaticData.SettingsModel = result.data.settings;
                                    StaticData.AccessToken = result.data.token;

                                    user.AccessToken = result.data.token;
                                    user.UserModel = JsonConvert.SerializeObject(result.data.user);
                                    user.SettingsModel = JsonConvert.SerializeObject(result.data.settings);
                                    user.FetchedTime = DateTime.Now;
                                    await database.SaveUserAsync(user);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Microsoft.AppCenter.Crashes.Crashes.TrackError(e);
                        }
                        try
                        {
                            var todaysEventsResult = await DetailsService.GetScheduleList(DateTime.Now.GetDateTimeFormat(), DateTime.Now.GetDateTimeFormat());
                            if (todaysEventsResult.status == 200 && todaysEventsResult.data != null && todaysEventsResult.data.list != null)
                            {
                                var EventsList = new List<Events>(todaysEventsResult.data.list);
                                await database.DeleteAllEventsAsync();
                                await database.InsertAllEventsAsync(EventsList);

                                var notifications = await database.GetNotificationsAsync();

                                foreach (var item in EventsList)
                                {
                                    var notification = notifications.Where(n => n.EventId == item.id).FirstOrDefault();
                                    if (notification == null)
                                    {
                                        var schTime = new DateTime(item.start_time.Year, item.start_time.Month, item.start_time.Day, item.start_time.Hour, item.start_time.Minute, 0);
                                        if (item.action == 2)
                                        {
                                            schTime = schTime.Subtract(new TimeSpan(0, 15, 0));
                                        }
                                        var newNotification = new LocalNotificationModel()
                                        {
                                            AssignedTo = item.assign_to,
                                            StartTime = item.start_time.ToString("HH:mm"),
                                            EndTime = item.end_time.ToString("HH:mm"),
                                            EventId = item.id,
                                            ScheduleTime = schTime,
                                            EventAction = item.action,
                                            Subject = item.subject,
                                        };
                                        await database.SaveNotificationAsync(newNotification);
                                    }
                                    else
                                    {
                                        //if (notification.Notified)
                                        //{
                                        //    continue;
                                        //}
                                        var schTime = new DateTime(item.start_time.Year, item.start_time.Month, item.start_time.Day, item.start_time.Hour, item.start_time.Minute, 0);
                                        if (item.action == 2)
                                        {
                                            schTime = schTime.Subtract(new TimeSpan(0, 15, 0));
                                        }
                                        if (notification.ScheduleTime != schTime || notification.StartTime != item.start_time.ToString("HH:mm") ||
                                        notification.EndTime != item.end_time.ToString("HH:mm") || notification.EventAction != item.action ||
                                        notification.Subject != item.subject)
                                        {
                                            //notify.CancelScheduledNotification(notification.Title, notification.Message, notification.NotifyId);
                                            //var schTime = item.start_time.Add(new TimeSpan(1, 0, 0));
                                            //var schTime = item.start_time;
                                            //var schNotification = notify.ScheduleNotification("Title", "You have a pending event", schTime);
                                            //notification.NotifyId = schNotification.NotifyId;
                                            notification.AssignedTo = item.assign_to;
                                            notification.StartTime = item.start_time.ToString("HH:mm");
                                            notification.EndTime = item.end_time.ToString("HH:mm");
                                            notification.EventAction = item.action;
                                            notification.Subject = item.subject;
                                            if (notification.ScheduleTime != schTime)
                                            {
                                                notification.ScheduleTime = schTime;
                                                notification.NotifiedTime = "";
                                            }
                                            await database.SaveNotificationAsync(notification);
                                        }
                                    }
                                }
                                foreach (var item in notifications)
                                {
                                    if (!EventsList.Any(e => e.id == item.EventId))
                                    {
                                        await database.DeleteNotificationAsync(item);
                                    }
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            Microsoft.AppCenter.Crashes.Crashes.TrackError(exc);
                        }
                    }
                }
                try
                {
                    var notificationsResult = await database.GetNotificationsAsync();

                    var now = DateTime.Now;
                    //datatime = new DateTime(datatime.Year, datatime.Month, datatime.Day, datatime.Hour, datatime.Minute, 0);
                    foreach (var item in notificationsResult)
                    {
                        var diff = DateTime.Now.Subtract(item.ScheduleTime);
                        if (diff.TotalMinutes >= 0 && string.IsNullOrEmpty(item.NotifiedTime))
                        //    if (item.ScheduleTime.Year == now.Year && item.ScheduleTime.Month == now.Month &&
                        //item.ScheduleTime.Day == now.Day && item.ScheduleTime.Hour == now.Hour && item.ScheduleTime.Minute <= now.Minute)
                        {
                            notify.Initialize();
                            notify.SendNotification(item.AssignedTo + " - " + item.Subject, item.StartTime + " - " + item.EndTime, DateTime.Now.AddSeconds(5));
                            item.NotifiedTime = now.ToString("HH:mm");
                            await database.SaveNotificationAsync(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);
                }
            }
            
            count++;
            await Task.Delay(notificationsInterval);
            IsFlagOn = true;
        }
    }
}