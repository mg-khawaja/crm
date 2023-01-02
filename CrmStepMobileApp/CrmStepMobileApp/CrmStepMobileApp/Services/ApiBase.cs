using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Services
{
  public  class ApiBase
    {
        protected static HttpClientBase HttpClientBase = new HttpClientBase();

        public const string GetSingleFormDetailsKey = "Form/Details?id={0}&form_type={1}";

        public const string FormDetailsKey = "Form/Details?form_type={0}";

        public const string FormListKey = "Form/List?form_type={0}&page={1}&count={2}&search={3}&user_ids={4}";

        public const string LoginKey = "User/Login";

        public const string PostDetailKey = "Form/Post";

        public const string SearchKey = "Form/Search?search={0}";

        public const string SearchWithTypeKey = "Form/Search?form_type={0}&search={1}";

        public const string GetScheduleListKey = "Schedule/List?from={0}&to={1}&search={2}&responsible={3}";

        public const string GetBasicInfoKey = "Schedule/BasicInfo";

        public const string GetBasicInfoWithTypeKey = "Schedule/BasicInfo?type={0}";

        public const string SchedulePostKey = "Schedule/Post";

        public const string UpdateStatusKey = "Schedule/UpdateStatus?id={0}&isCompleted={1}";

        public const string GetSingleScheduleKey = "Schedule/Details?id={0}";

        public const string GetTodayEventsKey = "Home/GetTodayEvents?date={0}&responsible={1}";
      
        public const string GetHotSaleProductsKey = "Home/GetHotSaleProducts";

        public const string GetAllIncompletedEventsKey = "Home/GetAllIncompletedEvents?responsible={0}";

        public const string GetClientBirthdayKey = "/Home/GetClientBirthday?date={0}&days={1}&take={2}";

        public const string GetFormDetailsKey = "Home/GetFormDetails?type={0}&take={1}";

        public const string GetEventTimeLineListKey = "Schedule/GetEventTimeLineList?parent_id={0}&parent_type={1}";

        public const string GetListByParentKey = "Document/GetListByParent?parent_id={0}&parent_type={1}&page={2}&count={3}";

        public const string PostDocumnetKey = "Document/Post";

        public const string DownloadDocumnetKey = "Document/Download?document_id={0}";

        public const string DeleteDocumnetKey = "Document/Delete?document_id={0}";

        public const string GetBasicListKey = "Company/GetBasicList";
        public const string GetByCompanyKey = "Project/GetByCompany?company_id={0}";
        public const string SwitchKey = "Project/Switch?switch_project={0}";

        public const string GetNotificationsKey = "Home/GetNotifications";

    }
}
