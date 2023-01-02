using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Interfaces;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using CrmStepMobileApp.Views.PopupView;
using GalaSoft.MvvmLight.Command;
using Plugin.Multilingual;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.ViewModels
{
    public class BottomLayoutViewViewModel : BaseViewModel
    {
        private NotificationsModel _notificationsList;
        public NotificationsModel NotificationsModel
        {
            get => _notificationsList;
            set
            {
                _notificationsList = value;
                RaisePropertyChanged();
            }
        }

        private int _notificationsCount;
        public int NotificationsCount
        {
            get => _notificationsCount;
            set
            {
                _notificationsCount = value;
                RaisePropertyChanged();
            }
        }

        private bool _notificationsVisibility;
        public bool NotificationsVisibility
        {
            get => _notificationsVisibility;
            set
            {
                _notificationsVisibility = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand<string> ItemTapCommand => new RelayCommand<string>(ItemTapClick);

        private async void ItemTapClick(string param)
        {
            var id = Convert.ToInt32(param);
            switch (id)
            {
                case 0:

                    PopUntilDestination(typeof(DashboardPage));
                    break;
                case 1:
                    SwitchProject();
                    break;
                case 2:
                    NavigateToAdd();
                    break;
                case 3:
                    if (NotificationsVisibility)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new NotificationPage());
                    }
                    else
                    {
                        await ShowAlert(crmLng.NoNewNotifications);
                    }
                    break;

                case 4:
                    ChangeLanguage();
                    break;
                case 5:
                    Logout();
                    break;

            }
        }


        public async void Logout()
        {
            var database = await SQLiteDB.Instance;
            await database.DeleteAllUsersAsync();

            Settings.Username = Settings.Password = String.Empty;
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async void SwitchProject()
        {
            InitilizeCulture();
            if (App.Locator.DashboardPageViewModel.SwitchingAllowed)
            {
                await PopupNavigation.Instance.PushAsync(new SwitchProjectView());
            }
            else
            {
                await ShowAlert(crmLng.NoPermissions);
            }
        }

        private async void ChangeLanguage()
        {
            InitilizeCulture();

            var result = await ShowActionSheet(crmLng.Select, StaticData.LangList.Select(s => s.Name).ToList());
            if (result >= 0)
            {
                StaticData.SelectedLanguageModel = StaticData.LangList[result];
                Settings.SelectedLang = StaticData.LangList[result].Id;
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(StaticData.SelectedLanguageModel.PluginName);
                DependencyService.Get<ISetLocale>().SetNativeLocale();
                await App.Locator.LoginPageViewModel.LoginInternal();
                App.Locator.DashboardPageViewModel.Initilize();
                PopUntilDestination(typeof(DashboardPage));
            }
        }

        private async void NavigateToAdd()
        {
            InitilizeCulture();

            var list = new List<string>();
            list.Add(crmLng.Leads);
            list.Add(crmLng.Contacts);
            list.Add(crmLng.Companies);
            list.Add(crmLng.Deals);
            list.Add(crmLng.Tickets);
            if (StaticData.UserModel.last_project_id == 38)
            {
                list.Add(crmLng.SiteOrders);
            }


            var index = await ShowActionSheet(crmLng.Add, list);
            if (index >= 0)
            {
                switch (index)
                {
                    case 0:
                        StaticData.CurrentFormType = 1;
                        break;


                    case 1:
                        StaticData.CurrentFormType = 2;

                        break;


                    case 2:
                        StaticData.CurrentFormType = 3;
                        break;


                    case 3:
                        StaticData.CurrentFormType = 4;
                        break;

                    //case 4:
                    //    StaticData.CurrentFormType = (int)FormType.Deals;
                    //    await App.Current.MainPage.Navigation.PushAsync(new SpecialDealsPage());
                    //    return;
                    case 4:
                        StaticData.CurrentFormType = 5;
                        break;
                }



                await App.Current.MainPage.Navigation.PushAsync(new AddPage());
            }


        }

        public void PopUntilDestination(Type DestinationPage)
        {

            try
            {

                if (App.IsRootPage)
                    return;

                int LeastFoundIndex = 0;
                int PagesToRemove = 0;

                for (int index = App.Current.MainPage.Navigation.NavigationStack.Count - 2; index > 0; index--)
                {
                    if (App.Current.MainPage.Navigation.NavigationStack[index].GetType().Equals(DestinationPage))
                    {
                        break;
                    }
                    else
                    {
                        LeastFoundIndex = index;
                        PagesToRemove++;
                    }
                }

                for (int index = 0; index < PagesToRemove; index++)
                {
                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[LeastFoundIndex]);
                }

                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }


        }
        internal async void Initlize()
        {

            var result = await NotificationService.GetNotifications();
            if (result.status == 200 && result.data != null)
            {
                NotificationsModel = result.data;
                NotificationsCount = result.data.others.Count();
                if (result.data.today.Any())
                {
                    var list = result.data.today.Where(s => Convert.ToDateTime(s.ConvertedStartTime) > DateTime.Now).ToList();
                    NotificationsCount = NotificationsCount + result.data.today.Count();
                }

                var todaysCount = result.data.today.Count();

                NotificationsVisibility =
                NotificationsCount != 0;
            }
            else
            {
                NotificationsModel = result.data;
            }
        }
    }
}
