using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
  
    public class LoginApiModel:BaseModel
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string token { get; set; }
        public UserModel user { get; set; }
        public SettingsModel settings { get; set; }
    }

    public class UserModel
    {
        public int user_id { get; set; }
        public int last_project_id { get; set; }
        public int role_id { get; set; }
        public object role_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_path { get; set; }
        public string logo_path { get; set; }
        public bool is_super_admin { get; set; }
    }

    public class SettingsModel
    {
        public string date_format { get; set; }
        public string time_format { get; set; }
        public string currency_format { get; set; }
        public int default_grid_rows { get; set; }
        public bool show_incomplete_events { get; set; }
    }

}
