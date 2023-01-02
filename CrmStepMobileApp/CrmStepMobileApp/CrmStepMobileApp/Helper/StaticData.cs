using CrmStepMobileApp.Models;
using CrmStepMobileApp.Services;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CrmStepMobileApp.Helper
{
    public class StaticData
    {

        public static List<LanguageModel> LangList = new List<LanguageModel>() 
        {
        new LanguageModel()
        {
            Id=1,
            ApiName= "en-US", 
            Name="English",
            PluginName="en"
        },  new LanguageModel()
        {
            Id=2,
            ApiName= "he-IL",
            Name="Hebrew",
            PluginName="he"
        },  new LanguageModel()
        {
            Id=3,
            ApiName= "ru-RU",
            Name="Russian",
            PluginName="ru"
        }
        };


        public static LanguageModel SelectedLanguageModel { get; set; }


        public static UserModel UserModel { get; set; }
        public static SettingsModel SettingsModel { get; set; }

        public static DateTime FetchedTime { get; set; }
        public static string AccessToken { get; set; }
        public static int UTCMins { get; set; }
        public static int DeviceId { get; set; }

        public static int CurrentFormType { get; set; }

        public static string Name { get; set; }
        public static string Amount { get; set; }
        public static int TotalExpectedReults = -1;
        public ImageSource LogoImage { get; set; }

        public static bool IsSessionExpired()
        {
            TimeSpan diff;
            try
            {
                diff = DateTime.Now.Subtract(FetchedTime);
                if (diff.TotalHours < 2)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>diff: \n" + JsonConvert.SerializeObject(diff) + "\n";
                logFile += "==>FetchedTime: \n" + JsonConvert.SerializeObject(FetchedTime) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: IsSessionExpired"), dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: Static Data\n" +
                                            $"Method: IsSessionExpired\n" +
                                            $"Exception: IsSessionExpired\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
            }
            return true;
        }
        public static async Task<string> RefreshToken()
        {
            LocalUserModel user = new LocalUserModel();
            LoginApiModel result = new LoginApiModel();
            try
            {
                LoginServices LoginServices = new LoginServices();
                var database = await SQLiteDB.Instance;
                user = await database.GetUserAsync();
                result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                        StaticData.UTCMins, StaticData.DeviceId, user.Username, user.Password, DeviceInfo.Platform.ToString());
                if (result.status == 200)
                {
                    Settings.Username = user.Username;
                    Settings.Password = user.Password;
                    StaticData.UserModel = result.data.user;
                    StaticData.SettingsModel = result.data.settings;
                    StaticData.AccessToken = result.data.token;
                    StaticData.FetchedTime = DateTime.Now;

                    user.AccessToken = result.data.token;
                    user.UserModel = JsonConvert.SerializeObject(result.data.user);
                    user.SettingsModel = JsonConvert.SerializeObject(result.data.settings);
                    user.FetchedTime = DateTime.Now;
                    await database.SaveUserAsync(user);
                    return result.data.token;
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("", "");
                var logFile = "Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt") + "\n";
                logFile += "==>db user: \n" + JsonConvert.SerializeObject(user) + "\n";
                logFile += "==>API response: \n" + JsonConvert.SerializeObject(result) + "\n";
                logFile += "==>Exception: \n" + JsonConvert.SerializeObject(ex) + "\n";
                Crashes.TrackError(new Exception("Exception: Refresh token"),dict,
                    new ErrorAttachmentLog[]
                                    {
                                        ErrorAttachmentLog.AttachmentWithText(
                                            $"Date: {DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")}\n" +
                                            $"Message: {ex.Message}\n" +
                                            $"Page: Static Data\n" +
                                            $"Method: RefreshToken\n" +
                                            $"Exception: Refresh token\n" +
                                            $"StackTrace: {ex.StackTrace}\n", "event log.txt"+
                                            $"{logFile} \n" )
                                        ,
                                    });
            }
            return "";
        }
    }

    

    public enum FormType
    {
        Leads = 1,
        Contacts = 2,
        Companies = 3,
        Deals = 4,
        Tickets = 5
    }
}
