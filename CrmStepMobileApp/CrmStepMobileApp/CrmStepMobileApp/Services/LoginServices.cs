using CrmStepMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
    public class LoginServices : ApiBase
    {
        public async Task<LoginApiModel> Login(string cultre, int utcMin, int deviceId, string userName, string password,string deviceType)
        {
            try
            {

                var obj = new
                {
                    username = userName,
                    secret = password,
                    device_type = deviceType,
                    device_id = deviceId,
                    culture = cultre,
                    utc_mins_diff = utcMin
                };
                var json = JsonConvert.SerializeObject(obj);

                var endPoint = String.Format(LoginKey);
                var result = await HttpClientBase.Post<LoginApiModel>(endPoint, json);
                return result;
            }
            catch (Exception ex)
            {
                return new LoginApiModel() { message = ex.Message };
            }

        }
    }
}
