using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    //public class NotificationsApiModel
    //{
    //    public int status { get; set; }
    //    public string message { get; set; }
    //    public NotificationsModel data { get; set; }
    //}

    //public class NotificationsModel
    //{
    //    public Notifications[] list { get; set; }
    //    public int count { get; set; }
    //}

    //public class Notifications
    //{
    //    public string msg { get; set; }
    //    public string title { get; set; }
    //    public int notification_type { get; set; }
    //    public string date { get; set; }
    //    public string parent_id { get; set; }
    //    public int id { get; set; }
    //}

    public class NotificationsApiModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public NotificationsModel data { get; set; }
    }






    public class NotificationsModel
    {
        public Notifications[] others { get; set; }
        public EventNotifications[] today { get; set; }
        public EventNotifications[] tomorrow { get; set; }
        public EventNotifications[] thisWeek { get; set; }
    }

    public class EventNotifications
    {
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public string _Id { get; set; }
        public int Action { get; set; }
        public string ActionText { get; set; }
        public int ParentType { get; set; }
        public string ParentID { get; set; }
        public object Location { get; set; }
        public string Subject { get; set; }
        public object Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ConvertedStartTime { get; set; }
        public string StartTimeText { get; set; }
        public DateTime ConvertedEndTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public string AssignTo { get; set; }
        public int Priority { get; set; }
        public string PriorityText { get; set; }
    }

  



    public class Notifications
    {
        public string msg { get; set; }
        public string title { get; set; }
        public int notification_type { get; set; }
        public string date { get; set; }
        public string parent_id { get; set; }
        public int id { get; set; }
    }


}
