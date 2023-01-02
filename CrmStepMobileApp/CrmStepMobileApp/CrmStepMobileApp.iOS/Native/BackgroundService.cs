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
    public class BackgroundService
    {
        CancellationTokenSource _cts;
        nint _taskId;
        CancellationToken cancellation;

        private bool isStarted;
        string CHANNEL_ID = "#chennelId";
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        int notificationsInterval = 1 * 10000;
        iOSNotificationManager notify = new iOSNotificationManager();
        DetailsService DetailsService = new DetailsService();
        LoginServices LoginServices = new LoginServices();
        SQLiteDB database;
        int count = 4;

        public async Task Start()
        {
            notify.Initialize();
            database = await SQLiteDB.Instance;

            cancellation = new CancellationToken();
            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("Long running", OnExpiration);
            Console.WriteLine("Start");
            await Task.Run(async () =>
           {
               while (true)
               {
                   if (true)
                   {
                       //if (count % 8 == 0)
                       //{
                       //    var user = await database.GetUserAsync();
                       //    if (user != null)
                       //    {
                       //        StaticData.AccessToken = user.AccessToken;
                       //        try
                       //        {
                       //            var diff = DateTime.Now.Subtract(user.FetchedTime);

                       //            //var n = DateTime.Compare(expiry, DateTime.Now);
                       //            if (diff.TotalHours >= 24)
                       //            {
                       //                // greater than 24 hour
                       //                var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                       //                StaticData.UTCMins, StaticData.DeviceId, user.Username, user.Password, DeviceInfo.Platform.ToString());
                       //                if (result.status == 200)
                       //                {
                       //                    Settings.Username = user.Username;
                       //                    Settings.Password = user.Password;
                       //                    StaticData.UserModel = result.data.user;
                       //                    StaticData.SettingsModel = result.data.settings;
                       //                    StaticData.AccessToken = result.data.token;

                       //                    user.AccessToken = result.data.token;
                       //                    user.UserModel = JsonConvert.SerializeObject(result.data.user);
                       //                    user.SettingsModel = JsonConvert.SerializeObject(result.data.settings);
                       //                    user.FetchedTime = DateTime.Now;
                       //                    await database.SaveUserAsync(user);
                       //                }
                       //            }
                       //        }
                       //        catch (Exception e)
                       //        {
                       //            Microsoft.AppCenter.Crashes.Crashes.TrackError(e);
                       //        }
                       //        try
                       //        {
                       //            var todaysEventsResult = await DetailsService.GetScheduleList(DateTime.Now.GetDateTimeFormat(), DateTime.Now.GetDateTimeFormat());
                       //            if (todaysEventsResult.status == 200 && todaysEventsResult.data != null && todaysEventsResult.data.list != null)
                       //            {
                       //                var EventsList = new List<Events>(todaysEventsResult.data.list);
                       //                await database.DeleteAllEventsAsync();
                       //                await database.InsertAllEventsAsync(EventsList);

                       //                var notifications = await database.GetNotificationsAsync();

                       //                foreach (var item in EventsList)
                       //                {
                       //                    var notification = notifications.Where(n => n.EventId == item.id).FirstOrDefault();
                       //                    if (notification == null)
                       //                    {
                       //                        var schTime = new DateTime(item.start_time.Year, item.start_time.Month, item.start_time.Day, item.start_time.Hour, item.start_time.Minute, 0);
                       //                        if (item.action == 2)
                       //                        {
                       //                            schTime = schTime.Subtract(new TimeSpan(0, 15, 0));
                       //                        }
                       //                        var newNotification = new LocalNotificationModel()
                       //                        {
                       //                            AssignedTo = item.assign_to,
                       //                            StartTime = item.start_time.ToString("HH:mm"),
                       //                            EndTime = item.end_time.ToString("HH:mm"),
                       //                            EventId = item.id,
                       //                            ScheduleTime = schTime,
                       //                            EventAction = item.action,
                       //                            Subject = item.subject,
                       //                        };
                       //                        await database.SaveNotificationAsync(newNotification);
                       //                    }
                       //                    else
                       //                    {
                       //                        //if (notification.Notified)
                       //                        //{
                       //                        //    continue;
                       //                        //}
                       //                        var schTime = new DateTime(item.start_time.Year, item.start_time.Month, item.start_time.Day, item.start_time.Hour, item.start_time.Minute, 0);
                       //                        if (item.action == 2)
                       //                        {
                       //                            schTime = schTime.Subtract(new TimeSpan(0, 15, 0));
                       //                        }
                       //                        if (notification.ScheduleTime != schTime || notification.StartTime != item.start_time.ToString("HH:mm") ||
                       //                        notification.EndTime != item.end_time.ToString("HH:mm") || notification.EventAction != item.action ||
                       //                        notification.Subject != item.subject)
                       //                        {
                       //                            //notify.CancelScheduledNotification(notification.Title, notification.Message, notification.NotifyId);
                       //                            //var schTime = item.start_time.Add(new TimeSpan(1, 0, 0));
                       //                            //var schTime = item.start_time;
                       //                            //var schNotification = notify.ScheduleNotification("Title", "You have a pending event", schTime);
                       //                            //notification.NotifyId = schNotification.NotifyId;
                       //                            notification.AssignedTo = item.assign_to;
                       //                            notification.StartTime = item.start_time.ToString("HH:mm");
                       //                            notification.EndTime = item.end_time.ToString("HH:mm");
                       //                            notification.EventAction = item.action;
                       //                            notification.Subject = item.subject;
                       //                            if (notification.ScheduleTime != schTime)
                       //                            {
                       //                                notification.ScheduleTime = schTime;
                       //                                notification.NotifiedTime = "";
                       //                            }
                       //                            await database.SaveNotificationAsync(notification);
                       //                        }
                       //                    }
                       //                }
                       //                foreach (var item in notifications)
                       //                {
                       //                    if (!EventsList.Any(e => e.id == item.EventId))
                       //                    {
                       //                        await database.DeleteNotificationAsync(item);
                       //                    }
                       //                }
                       //            }
                       //        }
                       //        catch (Exception exc)
                       //        {
                       //            Microsoft.AppCenter.Crashes.Crashes.TrackError(exc);
                       //        }
                       //    }
                       //}
                       //try
                       //{
                       //    var notificationsResult = await database.GetNotificationsAsync();

                       //    var now = DateTime.Now;
                       //    //datatime = new DateTime(datatime.Year, datatime.Month, datatime.Day, datatime.Hour, datatime.Minute, 0);
                       //    foreach (var item in notificationsResult)
                       //    {
                       //        var diff = DateTime.Now.Subtract(item.ScheduleTime);
                       //        if (diff.TotalMinutes >= 0 && string.IsNullOrEmpty(item.NotifiedTime))
                       //        //    if (item.ScheduleTime.Year == now.Year && item.ScheduleTime.Month == now.Month &&
                       //        //item.ScheduleTime.Day == now.Day && item.ScheduleTime.Hour == now.Hour && item.ScheduleTime.Minute <= now.Minute)
                       //        {
                       //            notify.SendNotification(item.AssignedTo + " - " + item.Subject, item.StartTime + " - " + item.EndTime);
                       //            item.NotifiedTime = now.ToString("HH:mm");
                       //            await database.SaveNotificationAsync(item);
                       //        }
                       //    }
                       //}
                       //catch (Exception ex)
                       //{
                       //    Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);
                       //} 
                   }

                   notify.SendNotification("Test", "Test");

                   count++;
                   await Task.Delay(notificationsInterval);
               }
           }, cancellation);
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        private void OnExpiration()
        {
            //if (_cts != null)
            //{
            //    _cts.Token.ThrowIfCancellationRequested();
            //    _cts.Cancel();
            //}
        }
    }
}