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
    public class MenuPageVIewModel : BaseViewModel
    {

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
        private ObservableCollection<Notifications> _notificationsList;
        public ObservableCollection<Notifications> NotificationsList
        {
            get => _notificationsList;
            set
            {
                _notificationsList = value;
                RaisePropertyChanged();
            }
        }

        public bool SwitchingAllowed { get; set; }

        private ObservableCollection<MenuModel> _menusList;
        public ObservableCollection<MenuModel> MenusList
        {

            get => _menusList;
            set
            {
                _menusList = value;
                RaisePropertyChanged();
            }
        }




        public RelayCommand BellCommand => new RelayCommand(BellClick);

        private async void BellClick()
        {
            App.MenuPage.IsPresented = false;

            if (NotificationsVisibility)
                await App.Current.MainPage.Navigation.PushAsync(new NotificationPage());
        }
        public RelayCommand SwitchCommand => new RelayCommand(SwitchClick);

        private async void SwitchClick()
        {
            App.MenuPage.IsPresented = false;
            InitilizeCulture();
            if (SwitchingAllowed)
            {
                await PopupNavigation.Instance.PushAsync(new SwitchProjectView());
            }
            else
            {
                await ShowAlert(crmLng.NoPermissions);
            }
        }
        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            App.MenuPage.IsPresented = false;
            Initilize();
            InitilizeCulture();

            var list = new List<string>();
            list.Add(crmLng.Leads);
            list.Add(crmLng.Contacts);
            list.Add(crmLng.Companies);
            list.Add(crmLng.Deals);


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
                }



                await App.Current.MainPage.Navigation.PushAsync(new AddPage());
            }

        }


        internal async void Navigate(MenuModel menuModel)
        {
            switch (menuModel.Id)
            {

                case 2:
                    StaticData.CurrentFormType = (int)FormType.Leads;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 3:
                    StaticData.CurrentFormType = (int)FormType.Contacts;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 4:
                    StaticData.CurrentFormType = (int)FormType.Companies;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 5:
                    StaticData.CurrentFormType = (int)FormType.Deals;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 6:
                    await App.Current.MainPage.Navigation.PushAsync(new SchedulerPage());
                    break;
                case 7:
                    Settings.Username = Settings.Password = String.Empty;
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case 8:

                    var result = await ShowActionSheet(crmLng.Select, StaticData.LangList.Select(s => s.Name).ToList());
                    if (result >= 0)
                    {
                        StaticData.SelectedLanguageModel = StaticData.LangList[result];
                        Settings.SelectedLang = StaticData.LangList[result].Id;
                        CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(StaticData.SelectedLanguageModel.PluginName);
                        DependencyService.Get<ISetLocale>().SetNativeLocale();


                        await App.Locator.LoginPageViewModel.LoginInternal();

                        Initilize();
                        // App.MenuPage.Detail = new NavigationPage(new HomePage());

                        App.Locator.HomePageViewModel.Initilize();
                    }



                    break;
            }

        }
        public async void Initilize()
        {
            InitilizeCulture();
            var basicListResult = await SwitchProjectServices.GetBasicList();
            if (basicListResult != null && basicListResult.data != null)
            {
                SwitchingAllowed = true;
            }
            else
            {
                SwitchingAllowed = false;
            }


            //NotificationsList = new ObservableCollection<Notifications>();

            //var result = await NotificationService.GetNotifications();
            //if (result.status == 200 && result.data != null && result.data.count > 0)
            //{
            //    NotificationsList = new ObservableCollection<Notifications>(result.data.list);
            //}

            //NotificationsCount = NotificationsList.Count();
            //NotificationsVisibility = NotificationsCount != 0;


            MenusList = new ObservableCollection<MenuModel>();
            MenusList.Add(new MenuModel(1, "home.png", crmLng.Home));
            MenusList.Add(new MenuModel(2, "leads.png", crmLng.Leads));
            MenusList.Add(new MenuModel(3, "contact.png", crmLng.Contacts));
            MenusList.Add(new MenuModel(4, "company.png", crmLng.Companies));
            MenusList.Add(new MenuModel(5, "deals.png", crmLng.Deals));
            MenusList.Add(new MenuModel(6, "calendars.png", crmLng.Scheduler));
            MenusList.Add(new MenuModel(8, "language.png", crmLng.SwitchLanguage));
            MenusList.Add(new MenuModel(7, "logout.png", crmLng.Logout));

        }

    }
}
