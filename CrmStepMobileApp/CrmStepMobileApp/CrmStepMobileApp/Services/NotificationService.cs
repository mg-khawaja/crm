using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
  public  class NotificationService:ApiBase
    {
        public async Task<NotificationsApiModel> GetNotifications()
        {
            try
            {
                var endPoint = String.Format(GetNotificationsKey);
                var result = await HttpClientBase.Get<NotificationsApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new NotificationsApiModel() { message = ex.Message };
            }

        }
    }
}
