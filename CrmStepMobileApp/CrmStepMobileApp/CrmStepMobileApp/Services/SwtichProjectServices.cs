using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
  public  class SwtichProjectServices:ApiBase
    {
        public async Task<BasicListApiModel> GetBasicList()
        {
            try
            {
                var endPoint = String.Format(GetBasicListKey);
                var result = await HttpClientBase.Get<BasicListApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new BasicListApiModel() { message = ex.Message };
            }

        }

        public async Task<BasicListApiModel> GetByCompany(int id)
        {
            try
            {
                var endPoint = String.Format(GetByCompanyKey,id);
                var result = await HttpClientBase.Get<BasicListApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new BasicListApiModel() { message = ex.Message };
            }

        }

        public async Task<LoginApiModel> SwitchProject(int id)
        {
            try
            {
                var endPoint = String.Format(SwitchKey, id);
                var result = await HttpClientBase.Post<LoginApiModel>(endPoint,"", StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new LoginApiModel() { message = ex.Message };
            }

        }
    }
}
