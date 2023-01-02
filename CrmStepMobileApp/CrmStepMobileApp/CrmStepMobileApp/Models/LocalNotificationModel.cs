using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class LocalNotificationModel
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string EventId { get; set; }
        public int EventAction { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string AssignedTo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Subject { get; set; }
        public string NotifiedTime { get; set; }
    }
}
