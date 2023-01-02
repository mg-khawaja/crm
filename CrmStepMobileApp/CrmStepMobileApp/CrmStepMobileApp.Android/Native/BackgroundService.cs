using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using CrmStepMobileApp.Droid.Native;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Services;
using Java.Sql;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xamarin.Essentials;
using NetworkAccess = Xamarin.Essentials.NetworkAccess;

namespace CrmStepMobileApp.Droid
{
    [Service(Label = "BackgroundService")]
    class BackgroundService : Service
    {
        Handler handlerLocation;
        Action notificationsCheck;
        private bool isStarted;
        string CHANNEL_ID = "#chennelId";
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        long notificationsInterval = 1 * 30000;
        AndroidNotificationManager notify;
        DetailsService DetailsService = new DetailsService();
        LoginServices LoginServices = new LoginServices();
        SQLiteDB database;
        int count = 4;
        public override async void OnCreate()
        {
            base.OnCreate();
            handlerLocation = new Handler();
            notify = new AndroidNotificationManager();
            Log.Debug("Create", "Create");

            database = await SQLiteDB.Instance;

        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (isStarted)
            {
                Log.Debug("Stat", "Stat");
            }
            else
            {
                Log.Debug("Register", "Register");
                RegisterForegroundService();
                if (notificationsCheck == null)
                {
                    notificationsCheck = new Action(async () =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;

                            if (count % 8 == 0 && current == NetworkAccess.Internet)
                            {
                                var user = await database.GetUserAsync();
                                if (user != null)
                                {
                                    StaticData.AccessToken = user.AccessToken;
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
                                                    notification.ScheduleTime = schTime;
                                                    notification.Subject = item.subject;
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
                            }
                        }
                        catch (Exception ex)
                        {
                            var dict = new Dictionary<string, string>();
                            dict.Add("", "");
                            var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                            logFile += "==>Stacktrace: \n" + JsonConvert.SerializeObject(ex.StackTrace) + "\n";
                            logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                            Crashes.TrackError(new Exception("Exception: Background Service"), dict,
                                new ErrorAttachmentLog[]
                                                {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                                    $"Page: HttpClientBase\n" +
                                            $"Exception JSON: {JsonConvert.SerializeObject(ex)}\n" +
                                            $"Exception: Background Service Exception\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                                });
                        }
                        try
                        {
                            var notificationsResult = await database.GetNotificationsAsync();

                            var now = DateTime.Now;
                            //datatime = new DateTime(datatime.Year, datatime.Month, datatime.Day, datatime.Hour, datatime.Minute, 0);
                            foreach (var item in notificationsResult)
                            {
                                if (item.ScheduleTime.Year == now.Year && item.ScheduleTime.Month == now.Month &&
                                item.ScheduleTime.Day == now.Day && item.ScheduleTime.Hour == now.Hour && item.ScheduleTime.Minute == now.Minute)
                                {
                                    if (item.NotifiedTime == now.ToString("HH:mm"))
                                    {
                                        continue;
                                    }
                                    notify.SendNotification(item.AssignedTo + " - " + item.Subject, item.StartTime + " - " + item.EndTime);
                                    item.NotifiedTime = now.ToString("HH:mm");
                                    await database.SaveNotificationAsync(item);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var dict = new Dictionary<string, string>();
                            dict.Add("", "");
                            var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                            logFile += "==>Stacktrace: \n" + JsonConvert.SerializeObject(ex.StackTrace) + "\n";
                            logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                            Crashes.TrackError(new Exception("Exception: Background Service"), dict,
                                new ErrorAttachmentLog[]
                                                {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                                    $"Page: HttpClientBase\n" +
                                            $"Exception JSON: {JsonConvert.SerializeObject(ex)}\n" +
                                            $"Exception: Background Service Exception\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                                });
                        }
                        count++;
                        handlerLocation.PostDelayed(notificationsCheck, notificationsInterval);
                    });
                }
                //if (fetchEventsAction == null)
                //{
                //    fetchEventsAction = new Action(async () =>
                //    {

                //        handlerLocation.PostDelayed(fetchEventsAction, eventsInterval);
                //    });
                //}
                handlerLocation.PostDelayed(notificationsCheck, notificationsInterval);
                //handlerLocation.PostDelayed(fetchEventsAction, eventsInterval);
                isStarted = true;
            }
            return StartCommandResult.Sticky;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnDestroy()
        {
            StopSelf();
            base.OnDestroy();
        }

        void RegisterForegroundService()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(CHANNEL_ID, "Channel", NotificationImportance.Default)
                {
                    Description = "Foreground Service Channel"
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);

                var notification = new Notification.Builder(this, CHANNEL_ID)
                .SetOngoing(true)
                .Build();

                // Enlist this instance of the service as a foreground service
                StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            }
            else
            {
                var notification = new Notification.Builder(this)
                    .SetOngoing(true)
                    .Build();

                // Enlist this instance of the service as a foreground service
                StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            }
        }
        //async void GetTodaysEvents()
        //{
        //    if (string.IsNullOrEmpty(StaticData.AccessToken))
        //    {
        //        return;
        //    }
        //    var todaysEventsResult = await DetailsService.GetScheduleList(DateTime.Now.GetDateTimeFormat(), DateTime.Now.GetDateTimeFormat());
        //    if (todaysEventsResult.status == 200 && todaysEventsResult.data != null && todaysEventsResult.data.list != null)
        //    {
        //        try
        //        {
        //            var EventsList = new List<Events>(todaysEventsResult.data.list);
        //            await database.DeleteAllEventsAsync();
        //            await database.InsertAllEventsAsync(EventsList);

        //            var notifications = await database.GetNotificationsAsync();
        //            foreach (var item in EventsList)
        //            {
        //                var notification = notifications.Where(n => n.EventId == item.id).FirstOrDefault();
        //                if (notification == null)
        //                {
        //                    var schTime = item.start_time.Add(new TimeSpan(1, 0, 0));
        //                    //var schTime = item.start_time;
        //                    var schNotification = notify.ScheduleNotification("Title", "You have a pending event", schTime);
        //                    var EventContentJson = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings()
        //                    {
        //                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                    });
        //                    var newNotification = new LocalNotificationModel()
        //                    {
        //                        NotifyId = schNotification.NotifyId,
        //                        Title = schNotification.Title,
        //                        Message = schNotification.Message,
        //                        CreatedOn = DateTime.Now,
        //                        EventContent = EventContentJson,
        //                        EventId = item.id,
        //                        UpdatedOn = DateTime.Now,
        //                        ScheduleTime = schTime,
        //                    };
        //                    await database.SaveNotificationAsync(newNotification);
        //                }
        //                else
        //                {
        //                    if (notification.ScheduleTime != item.start_time.Add(new TimeSpan(1, 0, 0)))
        //                    {
        //                        notify.CancelScheduledNotification(notification.Title, notification.Message, notification.NotifyId);
        //                        var schTime = item.start_time.Add(new TimeSpan(1, 0, 0));
        //                        //var schTime = item.start_time;
        //                        var schNotification = notify.ScheduleNotification("Title", "You have a pending event", schTime);
        //                        notification.NotifyId = schNotification.NotifyId;
        //                        notification.Title = schNotification.Title;
        //                        notification.Message = schNotification.Message;
        //                        var EventContentJson = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings()
        //                        {
        //                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                        });
        //                        notification.EventContent = EventContentJson;
        //                        notification.UpdatedOn = DateTime.Now;
        //                        await database.SaveNotificationAsync(notification);
        //                    }
        //                }
        //            }
        //            foreach (var item in notifications)
        //            {
        //                if (!EventsList.Any(e => e.id == item.EventId))
        //                {
        //                    await database.DeleteNotificationAsync(item);
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        //}
    }
}