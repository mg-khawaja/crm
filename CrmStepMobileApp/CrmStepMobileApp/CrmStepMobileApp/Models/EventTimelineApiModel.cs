using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class EventTimelineApiModel:BaseModel
    {
        public EventTimelineModel data { get; set; }
    }

    public class EventTimelineModel
    {
        public EventTimeline[] list { get; set; }
        public int count { get; set; }
    }

    public class EventTimeline
    {
        public string id { get; set; }
        public string action_text { get; set; }
        public int action { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string scheduled_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string created_on { get; set; }
        public string updated_on { get; set; }
        public string[] responsible { get; set; }
        public bool is_completed { get; set; }
        public bool is_repeat { get; set; }
    }

}
