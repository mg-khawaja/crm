using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
   public class HomeServices:ApiBase
    {
        public async Task<EventsHomeApiModel> GetTodayEvents(string dateTime, int responsible)
        {
            try
            {
                var endPoint = String.Format(GetTodayEventsKey, dateTime, responsible);
                var result = await HttpClientBase.Get<EventsHomeApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new EventsHomeApiModel() { message = ex.Message };
            }
        }

        public async Task<HotSaleApiHomeModel> GetHotSaleProducts()
        {
            try
            {
                var endPoint = String.Format(GetHotSaleProductsKey);
                var result = await HttpClientBase.Get<HotSaleApiHomeModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new HotSaleApiHomeModel() { message = ex.Message };
            }

        }

        public async Task<EventsHomeApiModel> GetAllIncompletedEvents(int responsibleId)
        {
            try
            {
                var endPoint = String.Format(GetAllIncompletedEventsKey, responsibleId);
                var result = await HttpClientBase.Get<EventsHomeApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new EventsHomeApiModel() { message = ex.Message };
            }

        }


        public async Task<BirthdayHomeApiModel> GetClientBirthday(string date, int days, int take)
        {
            try
            {
                var endPoint = String.Format(GetClientBirthdayKey, date, days, take);
                var result = await HttpClientBase.Get<BirthdayHomeApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new BirthdayHomeApiModel() { message = ex.Message };
            }

        }
        public async Task<FormDetailsHomeApiModel> GetFormDetails(int type, int take)
        {
            try
            {
                var endPoint = String.Format(GetFormDetailsKey, type, take);
                var result = await HttpClientBase.Get<FormDetailsHomeApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new FormDetailsHomeApiModel() { message = ex.Message };
            }

        }

        public async Task<DealHomeApiModel> GetDealHomes(int type, int take)
        {
            try
            {
                var endPoint = String.Format(GetFormDetailsKey, type, take);
                var result = await HttpClientBase.Get<DealHomeApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new DealHomeApiModel() { message = ex.Message };
            }

        }
    }
}
