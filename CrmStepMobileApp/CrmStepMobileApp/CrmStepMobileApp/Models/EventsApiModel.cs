using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{

    public class EventsApiModel : BaseModel
    {

        public EventsModel data { get; set; }
    }

    public class EventsModel
    {
        public Events[] list { get; set; }
        public int count { get; set; }
    }

    public class Events
    {
        public string id { get; set; }
        public int action { get; set; }
        public string assign_to { get; set; }
        public int created_by { get; set; }
        public string description { get; set; }
        public bool is_all_day { get; set; }
        public bool is_completed { get; set; }
        public string location { get; set; }
        public string parent_id { get; set; }
        public int parent_type { get; set; }
        public int project_id { get; set; }
        public int schedule_id { get; set; }
        public string recurrence_rule { get; set; }
        public string subject { get; set; }
        public int user_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public bool is_google { get; set; }
        public string google_event_id { get; set; }
        public string responsible { get; set; }
        public int priority { get; set; }
    }



    public class ScheduleDetailApiModel:BaseModel
    {
  
        public ScheduleDetailModel data { get; set; }
    }

    public class ScheduleDetailModel
    {
        public string id { get; set; }
        public int action { get; set; }
        public string assign_to { get; set; }
        public int created_by { get; set; }
        public string description { get; set; }
        public bool is_all_day { get; set; }
        public bool is_completed { get; set; }
        public string location { get; set; }
        public string parent_id { get; set; }
        public int parent_type { get; set; }
        public int project_id { get; set; }
        public int schedule_id { get; set; }
        public string recurrence_rule { get; set; }
        public string subject { get; set; }
        public int user_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public bool is_google { get; set; }
        public object google_event_id { get; set; }
        public int[] responsible { get; set; }
        public int priority { get; set; }
    }




}
