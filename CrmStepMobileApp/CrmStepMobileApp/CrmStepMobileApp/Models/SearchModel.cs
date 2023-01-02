using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
   public class SearchApiModel:BaseModel
    {
        public SearchListModel data { get; set; }
    }

    public class SearchListModel
    {
        public SearchModel[] list { get; set; }
        public int count { get; set; }
    }

    public class SearchModel
    {
        public string RowId { get; set; }
        public int FormType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDoc { get; set; }
    }

}
