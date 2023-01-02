using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
     
       
        private string _home;
        public string Home
        {
            get => _home;
            set
            {
                _home = value;
                RaisePropertyChanged();
            }
        }

        private string _deals;
        public string Deals
        {
            get => _deals;
            set
            {
                _deals = value;
                RaisePropertyChanged();
            }
        }

        private string _leads;
        public string Leads
        {
            get => _leads;
            set
            {
                _leads = value;
                RaisePropertyChanged();
            }
        }

        private string _companies;
        public string Companies
        {
            get => _companies;
            set
            {
                _companies = value;
                RaisePropertyChanged();
            }
        }

        private string _contacts;
        public string Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                RaisePropertyChanged();
            }
        }

        private string _tickets;
        public string Tickets
        {
            get => _tickets;
            set
            {
                _tickets = value;
                RaisePropertyChanged();
            }
        }

        private string _scheduler;
        public string Scheduler
        {
            get => _scheduler;
            set
            {
                _scheduler = value;
                RaisePropertyChanged();
            }
        }




        public bool SwitchingAllowed { get; set; }


        public RelayCommand<string> ItemTapCommand => new RelayCommand<string>(ItemTapClick);

        private async void ItemTapClick(string param)
        {
            var id = Convert.ToInt32(param);
            switch (id)
            {
                case 0:
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                    break;
                case 1:
                    StaticData.CurrentFormType = (int)FormType.Leads;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 2:
                    StaticData.CurrentFormType = (int)FormType.Contacts;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 3:
                    StaticData.CurrentFormType = (int)FormType.Companies;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 4:
                    StaticData.CurrentFormType = (int)FormType.Deals;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 5:
                    StaticData.CurrentFormType = (int)FormType.Tickets;
                    await App.Current.MainPage.Navigation.PushAsync(new ListPage());
                    break;
                case 6:
                    await App.Current.MainPage.Navigation.PushAsync(new SchedulerPage());
                    break;
            }
        }


        internal async void Initilize()
        {
            try
            {
                InitilizeCulture();

                PageTitle = crmLng.Dashboard;
                Home = crmLng.Home;
                Leads = crmLng.Leads;
                Deals = crmLng.Deals;
                Contacts = crmLng.Contacts;
                Companies = crmLng.Companies;
                Scheduler = crmLng.Scheduler;
                Tickets = crmLng.Tickets;


                var basicListResult = await SwitchProjectServices.GetBasicList();
                if (basicListResult != null && basicListResult.data != null)
                {
                    SwitchingAllowed = true;
                }
                else
                {
                    SwitchingAllowed = false;
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
