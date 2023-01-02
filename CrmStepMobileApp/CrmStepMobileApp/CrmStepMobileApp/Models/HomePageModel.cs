using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{


    public class EventsHomeApiModel : BaseModel
    {

        public EventsHomeModel[] data { get; set; }
    }

    public class EventsHomeModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public bool is_completed { get; set; }
        public string location { get; set; }
        public string parent_id { get; set; }
        public int parent_type { get; set; }
        public string assign_to { get; set; }
        public int priority { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        [PrimaryKey]
        public string id { get; set; }
        public int type { get; set; }
    }

    public class HotSaleApiHomeModel : BaseModel
    {
        public HotSaleHomeModel[] data { get; set; }
    }

    public class HotSaleHomeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string consumer_price { get; set; }
        public string img { get; set; }
        public string count { get; set; }
        public string category { get; set; }
        public string supplier { get; set; }
    }



    public class BirthdayHomeApiModel : BaseModel
    {
        public List<BirthdayHomeModel> data { get; set; }
    }

    public class BirthdayHomeModel
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string address { get; set; }
        public string dob { get; set; }
        public string days_to_go_str { get; set; }
        public string days_to_go { get; set; }
    }


    public class FormDetailsHomeApiModel : BaseModel
    {

        public FormDetailsHomeModel[] data { get; set; }
    }

    public class FormDetailsHomeModel
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string status { get; set; }
        public string mobile { get; set; }
        public string mail { get; set; }
    }


    public class DealHomeApiModel:BaseModel
    {

        public DealHomeModel[] data { get; set; }
    }

    public class DealHomeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string contact { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
    }

}
