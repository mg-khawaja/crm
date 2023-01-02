using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class LocalUserModel
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SettingsModel { get; set; }
        public string UserModel { get; set; }
        public string AccessToken { get; set; }
        public DateTime FetchedTime { get; set; }
    }
}
