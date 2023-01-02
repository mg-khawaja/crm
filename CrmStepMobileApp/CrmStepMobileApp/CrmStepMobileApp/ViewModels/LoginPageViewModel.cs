using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Interfaces;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CrmStepMobileApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged();
            }

        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }

        }



        public RelayCommand LoginCommand => new RelayCommand(async () => await LoginClick());

        public async Task LoginClick()
        {
            try
            {
                if (await LoginInternal())
                {
                    var notificationManager = DependencyService.Get<INotificationManager>();
                    notificationManager.Initialize();
                    notificationManager.SendNotification("CrmStep", "You have been logged in to the application");
                    await App.Current.MainPage.Navigation.PushAsync(new DashboardPage());
                }
                //  await App.Current.MainPage.Navigation.PushAsync(new MenuPage());

                //if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
                //{
                //    await ShowAlert(crmLng.EnterBothFields);
                //    return;
                //}

                //var loader = await ShowLoading();
                //var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                //    StaticData.UTCMins, StaticData.DeviceId, Username, Password, DeviceInfo.Platform.ToString());
                //await loader.DismissAsync();
                //if (result.status == 200)
                //{
                //    Settings.Username = Username;
                //    Settings.Password = Password;
                //    StaticData.UserModel = result.data.user;
                //    StaticData.SettingsModel = result.data.settings;
                //    StaticData.AccessToken = result.data.token;
                //    await App.Current.MainPage.Navigation.PushAsync(new MenuPage());
                //}
                //else
                //{
                //    await ShowAlert(result.message);
                //}
            }
            catch (Exception ex)
            {

            }
        }


        public async Task<bool> LoginInternal()
        {
            try
            {
                if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
                {
                    await ShowAlert(crmLng.EnterBothFields);
                    return false;
                }

                var loader = await ShowLoading();
                var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                    StaticData.UTCMins, StaticData.DeviceId, Username, Password, DeviceInfo.Platform.ToString());
                await loader.DismissAsync();
                if (result.status == 200)
                {
                    Settings.Username = Username;
                    Settings.Password = Password;
                    StaticData.UserModel = result.data.user;
                    StaticData.SettingsModel = result.data.settings;
                    StaticData.AccessToken = result.data.token;

                    LocalUserModel user = new LocalUserModel();
                    user.Username = Username;
                    user.Password = Password;
                    user.AccessToken = result.data.token;
                    user.UserModel = JsonConvert.SerializeObject(result.data.user);
                    user.SettingsModel = JsonConvert.SerializeObject(result.data.settings);
                    user.FetchedTime = DateTime.Now;
                    var database = await SQLiteDB.Instance;
                    await database.DeleteAllUsersAsync();
                    await database.SaveUserAsync(user);

                    return true;

                }
                else
                {
                    await ShowAlert(result.message);
                }
            }
            catch (Exception ex)
            {

            }
            return false;

        }

        internal void Initilize()
        {
            InitilizeCulture();

            Username = Settings.Username;
            Password = Settings.Password;
            loginCheck();
        }
        private async void loginCheck()
        {
            var loader = await ShowLoading();
            var database = await SQLiteDB.Instance;
            var user = await database.GetUserAsync();
            if (user == null)
            {
                await loader.DismissAsync();
                return;
            }
            else
            {
                DateTime expiry = user.FetchedTime.AddHours(23).AddMinutes(59).AddSeconds(59);
                var diff = DateTime.Now.Subtract(user.FetchedTime);
                //var n = DateTime.Compare(expiry, DateTime.Now);
                if (diff.TotalHours < 2)
                {
                    // within 2 hour
                    Username = Settings.Username = user.Username;
                    Password = Settings.Password = user.Password;
                    StaticData.UserModel = JsonConvert.DeserializeObject<UserModel>(user.UserModel);
                    StaticData.SettingsModel = JsonConvert.DeserializeObject<SettingsModel>(user.SettingsModel);
                    StaticData.AccessToken = user.AccessToken;
                    StaticData.FetchedTime = user.FetchedTime;
                    await loader.DismissAsync();
                }
                else
                {
                    // greater than 2 hour
                    var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName,
                    StaticData.UTCMins, StaticData.DeviceId, user.Username, user.Password, DeviceInfo.Platform.ToString());
                    if (result.status == 200)
                    {
                        Settings.Username = Username;
                        Settings.Password = Password;
                        StaticData.UserModel = result.data.user;
                        StaticData.SettingsModel = result.data.settings;
                        StaticData.AccessToken = result.data.token;
                        StaticData.FetchedTime = DateTime.Now;

                        user.AccessToken = result.data.token;
                        user.UserModel = JsonConvert.SerializeObject(result.data.user);
                        user.SettingsModel = JsonConvert.SerializeObject(result.data.settings);
                        user.FetchedTime = DateTime.Now;
                        await database.SaveUserAsync(user);
                    }
                    await loader.DismissAsync();
                }
                await App.Current.MainPage.Navigation.PushAsync(new DashboardPage());
            }
        }
    }
}
