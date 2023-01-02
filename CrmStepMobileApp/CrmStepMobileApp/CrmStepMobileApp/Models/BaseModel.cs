using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class BaseModel
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    public class UpdateApiModel : BaseModel
    {
        public UpdateModel data { get; set; }
    }

    public class UpdateModel
    {
        public bool success { get; set; }
    }

    public class DocumentResponseModel
    {
        public bool is_success { get; set; }
        public string msg { get; set; }
    }

}
