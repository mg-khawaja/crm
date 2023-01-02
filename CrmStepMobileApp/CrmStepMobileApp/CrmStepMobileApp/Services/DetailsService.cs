using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
    public class DetailsService : ApiBase
    {
        public async Task<FormsDetailApiModel> GetFormDetails(int formType)
        {
            try
            {

                //Form/Details?culture={0}&utc_mins_diff={1}&project_id={2}&user_id={3}&form_type={4}
                var endPoint = String.Format(FormDetailsKey, formType);
                var result = await HttpClientBase.Get<FormsDetailApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new FormsDetailApiModel() { message = ex.Message };
            }

        }

        public async Task<FormsListApiModel> GetFormList(int formType, int page = 1, int count = 0, string search = "", string userIds = "")
        {
            try
            {
                //search = search.Replace(" ", "%20");
                var endPoint = String.Format(FormListKey, formType, page, count, search, userIds);
                var result = await HttpClientBase.GetJson(endPoint, StaticData.AccessToken);

                var formsListApiModel = JsonConvert.DeserializeObject<FormsListApiModel>(result);
                if (formsListApiModel != null && formsListApiModel.status == 200 
                    && formsListApiModel.data != null)
                {
                    JObject rss = JObject.Parse(result);
                    JArray rssList = new JArray();
                    if (rss["data"]["list"] is JArray)
                    {
                        rssList = (JArray)rss["data"]["list"];
                    }
                    else
                    {
                        var data = rss["data"]["list"];
                    }
                    var columnsList = (JArray)rss["data"]["columns"];
                    StaticData.TotalExpectedReults = (int)rss["data"]["count"];

                    formsListApiModel.data.list = new List<List>();



                    foreach (JObject listItem in rssList)
                    {

                        try
                        {
                            var model = new List();
                            var propertiesValues = listItem.Values().ToList();

                            for (int index = 0; index < columnsList.Count; index++)
                            {
                                var coulmnsValues = columnsList[index].Values().ToList();
                                switch (Convert.ToString(coulmnsValues[1]))
                                {
                                    case "ID":
                                        model.ID = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "מזהה":
                                        model.ID = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Пасспорт":
                                        model.ID = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "First name":
                                        model.FirstName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "שם פרטי":
                                        model.FirstName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Имя":
                                        model.FirstName = Convert.ToString(propertiesValues[index + 1]);
                                        model.Name = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Last name":
                                        model.LastName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "שם משפחה":
                                        model.LastName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Фамилия":
                                        model.LastName = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Mobile":
                                        model.Mobile = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "טלפון נייד":
                                        model.Mobile = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Мобильный":
                                        model.Mobile = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Phone":
                                        model.Phone = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "טלפון":
                                        model.Phone = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Телефон":
                                        model.Phone = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Email":
                                        model.Email = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "אימייל":
                                        model.Email = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Эл. почта":
                                        model.Email = Convert.ToString(propertiesValues[index + 1]);
                                        break;


                                    case "Responsible Person":
                                        model.ResponsiblePerson = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Responsible person":
                                        model.ResponsiblePerson = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "משתמש":
                                        model.ResponsiblePerson = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Пользователь":
                                        model.ResponsiblePerson = Convert.ToString(propertiesValues[index + 1]);
                                        break;


                                    case "Status":
                                        model.Status = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "סטטוס":
                                        model.Status = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Статус":
                                        model.Status = Convert.ToString(propertiesValues[index + 1]);
                                        break;


                                    case "Created On":
                                        model.CreatedOn = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "נוצר ב":
                                        model.CreatedOn = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Создался":
                                        model.CreatedOn = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Photo":
                                        model.Photo = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "תמונה":
                                        model.Photo = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Фото":
                                        model.Photo = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Company Name":
                                        model.CompanyName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "שם חברה":
                                        model.CompanyName = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Компания":
                                        model.CompanyName = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Logo":
                                        model.Logo = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "לוגו":
                                        model.Logo = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Логотип":
                                        model.Logo = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Contact":
                                        model.Contact = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "איש קשר":
                                        model.Contact = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Контакт":
                                        model.Contact = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Deal Stage":
                                        model.DealStage = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "סטטוס עיסקה":
                                        model.DealStage = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "Статус сделки":
                                        model.DealStage = Convert.ToString(propertiesValues[index + 1]);
                                        break;

                                    case "Amount":
                                        model.Amount = Convert.ToString(propertiesValues[index + 1]);
                                        model.AmountWithVat = Convert.ToString(Convert.ToDouble(propertiesValues[index + 1]) * 1.17d);
                                        break;
                                    case "סכום":
                                        model.Amount = Convert.ToString(propertiesValues[index + 1]);
                                        model.AmountWithVat = Convert.ToString(Convert.ToDouble(propertiesValues[index + 1]) * 1.17d);
                                        break;
                                    case "Сумма":
                                        model.Amount = Convert.ToString(propertiesValues[index + 1]);
                                        model.AmountWithVat = Convert.ToString(Convert.ToDouble(propertiesValues[index + 1]) * 1.17d);
                                        break;

                                    case "Name":
                                        model.Name = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                    case "שם":
                                        model.Name = Convert.ToString(propertiesValues[index + 1]);
                                        break;


                                    case "כתובת איסוף":
                                        model.AddressFrom = Convert.ToString(propertiesValues[index + 1]);
                                        break;


                                    case "כתובת יעד":
                                        model.AddressTo = Convert.ToString(propertiesValues[index + 1]);
                                        break;
                                };



                            }
                            model.rowId = Convert.ToString(propertiesValues[0]);
                            formsListApiModel.data.list.Add(model);

                            //model.rowId = Convert.ToString(propertiesValues[0]);
                            //model.FirstName = Convert.ToString(propertiesValues[1]);
                            //model.LastName = Convert.ToString(propertiesValues[2]);
                            //model.ID = Convert.ToString(propertiesValues[3]);
                            //model.Phone = Convert.ToString(propertiesValues[4]);
                            //model.Mobile = Convert.ToString(propertiesValues[5]);
                            //model.Email = Convert.ToString(propertiesValues[6]);
                            //model.Address = Convert.ToString(propertiesValues[7]);
                            //model.Source = Convert.ToString(propertiesValues[8]);
                            //model.Status = Convert.ToString(propertiesValues[9]);
                            //model.ResponsiblePerson = Convert.ToString(propertiesValues[10]);
                            //model.CreatedOn = Convert.ToString(propertiesValues[11]);
                            //model.Comment = Convert.ToString(propertiesValues[12]);


                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }

                return formsListApiModel;
            }
            catch (Exception ex)
            {
                return new FormsListApiModel() { message = ex.Message };

            }

        }

        public async Task<AddResponseApiModel> PostDetail(FormsDetailModel formsDetail)
        {
            try
            {
                var json = JsonConvert.SerializeObject(formsDetail);
                var endPoint = String.Format(PostDetailKey);
                var result = await HttpClientBase.Post<AddResponseApiModel>(endPoint, json, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new AddResponseApiModel() { message = ex.Message };
            }
        }
        public async Task<FormsDetailApiModel> GetSingleFormDetails(string id, int formType)
        {
            try
            {
                //Form/Details?culture={0}&utc_mins_diff={1}&project_id={2}&user_id={3}&form_type={4}
                var endPoint = String.Format(GetSingleFormDetailsKey, id, formType);
                var result = await HttpClientBase.Get<FormsDetailApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new FormsDetailApiModel() { message = ex.Message };
            }
        }
        public async Task<SearchApiModel> Search(string formType, string search, int count = 0, int page = 0)
        {
            try
            {
                string endPoint;
                if (String.IsNullOrEmpty(formType))
                {
                    endPoint = String.Format(SearchKey, search);
                }
                else
                {
                    endPoint = String.Format(SearchWithTypeKey, formType, search);
                }
                if (count > 0)
                {
                    endPoint += "&count=" + count;
                }
                if (page > 0)
                {
                    endPoint += "&page=" + page;
                }
                var result = await HttpClientBase.Get<SearchApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new SearchApiModel() { message = ex.Message };
            }

        }

        public async Task<EventsApiModel> GetScheduleList(string start = "", string end = "", string search = "", string ids = "")
        {
            try
            {
                var endPoint = String.Format(GetScheduleListKey, start, end, search, ids);
                var result = await HttpClientBase.Get<EventsApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new EventsApiModel() { message = ex.Message };
            }

        }

        public async Task<BasicInfoModels> GetBasicInfo(string type = "")
        {
            try
            {

                string endPoint;
                if (String.IsNullOrEmpty(type))
                {
                    endPoint = String.Format(GetBasicInfoKey);
                }
                else
                {
                    endPoint = String.Format(GetBasicInfoWithTypeKey, type);
                }

                var result = await HttpClientBase.Get<BasicInfoModels>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new BasicInfoModels() { message = ex.Message };
            }

        }

        public async Task<BaseModel> PostSchedule(string json)
        {
            try
            {
                var endPoint = String.Format(SchedulePostKey);
                var result = await HttpClientBase.Post<BaseModel>(endPoint, json, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseModel() { message = ex.Message };
            }

        }

        public async Task<UpdateApiModel> UpdateStatus(string eventId, bool isCompleted)
        {
            try
            {
                var endPoint = String.Format(UpdateStatusKey, eventId, isCompleted);
                var result = await HttpClientBase.Post<UpdateApiModel>(endPoint, "", StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new UpdateApiModel() { message = ex.Message };
            }

        }


        public async Task<ScheduleDetailApiModel> GetSingleSchedule(string scheduleId)
        {
            try
            {
                var endPoint = String.Format(GetSingleScheduleKey, scheduleId);
                var result = await HttpClientBase.Get<ScheduleDetailApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new ScheduleDetailApiModel() { message = ex.Message };
            }

        }

        //Home Services






    }
}
