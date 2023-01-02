using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
  public  class EventsServices:ApiBase
    {
        
    public async Task<EventTimelineApiModel> GetEventTimeLineList(string parentId, int parentType)
        {
            try
            {
                var endPoint = String.Format(GetEventTimeLineListKey, parentId, parentType);
                var result = await HttpClientBase.Post<EventTimelineApiModel>(endPoint,"", StaticData.AccessToken);
                
                return result;
            }
            catch (Exception ex)
            {
                return new EventTimelineApiModel() { message = ex.Message };
            }
        }
    }
}
