using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
  
    public class DocumentApiModel:BaseModel
    {
        public DocumentModel data { get; set; }
    }

    public class DocumentModel
    {
        public Document[] result { get; set; }
        public int count { get; set; }
    }

    public class Document
    {
        public int serial { get; set; }
        public int document_id { get; set; }
        public string title { get; set; }
        public int user_id { get; set; }
        public string user { get; set; }
        public object file { get; set; }
        public string file_name { get; set; }
        public string created_on { get; set; }
        public object updated_on { get; set; }
        public string parent_id { get; set; }
        public int parent_type { get; set; }


        public string FileName { get; set; }

    }

}
