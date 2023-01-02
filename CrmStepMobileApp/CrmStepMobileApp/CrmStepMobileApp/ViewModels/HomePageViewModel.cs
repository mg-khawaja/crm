using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public bool PageLoaded { get; set; }


      

        private ObservableCollection<EventsHomeModel> _eventsList;
        public ObservableCollection<EventsHomeModel> EventsList
        {
            get => _eventsList;
            set
            {
                _eventsList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<EventsHomeModel> _incompleteEventsList;
        public ObservableCollection<EventsHomeModel> IncompleteEventsList
        {
            get => _incompleteEventsList;
            set
            {
                _incompleteEventsList = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<BirthdayHomeModel> _birthdaysList;
        public ObservableCollection<BirthdayHomeModel> BirthdaysList
        {
            get => _birthdaysList;
            set
            {
                _birthdaysList = value;
                RaisePropertyChanged();
            }
        }

      
        private ObservableCollection<HotSaleHomeModel> _hotSalesList;
        public ObservableCollection<HotSaleHomeModel> HotSalesList
        {
            get => _hotSalesList;
            set
            {
                _hotSalesList = value;
                RaisePropertyChanged();
            }
        }

       
        private ObservableCollection<FormDetailsHomeModel> _leadsList;
        public ObservableCollection<FormDetailsHomeModel> LeadsList
        {
            get => _leadsList;
            set
            {
                _leadsList = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<DealHomeModel> _dealsList;

       

        public ObservableCollection<DealHomeModel> DealsList
        {
            get => _dealsList;
            set
            {
                _dealsList = value;
                RaisePropertyChanged();
            }
        }


        private bool _englishTitle;

        

        public bool EnglishTitle
        {
            get => _englishTitle;
            set
            {
                _englishTitle = value;
                RaisePropertyChanged();
            }
        }

       

        private bool _hebrewTitlt;
        public bool HebrewTitle
        {
            get => _hebrewTitlt;
            set
            {
                _hebrewTitlt = value;
                RaisePropertyChanged();
            }
        }

      

        private string _todayEvents;
        public string TodayEvents
        {
            get => _todayEvents;
            set
            {
                _todayEvents = value;
                RaisePropertyChanged();
            }
        }

        private string _incompleteEvents;
        public string IncompleteEvents
        {
            get => _incompleteEvents;
            set
            {
                _incompleteEvents = value;
                RaisePropertyChanged();
            }
        }

       

        private string _hotSaleProducts;
        public string HotSaleProducts
        {
            get => _hotSaleProducts;
            set
            {
                _hotSaleProducts = value;
                RaisePropertyChanged();
            }
        }
        private string _customerBirthday;
        public string CustomerBirthday
        {
            get => _customerBirthday;
            set
            {
                _customerBirthday = value;
                RaisePropertyChanged();
            }
        }
        private string _recentDeals;
        public string RecentDeals

        {
            get => _recentDeals;
            set
            {
                _recentDeals = value;
                RaisePropertyChanged();
            }
        }
        private string _recentLeads;
        public string RecentLeads

        {
            get => _recentLeads;
            set
            {
                _recentLeads = value;
                RaisePropertyChanged();
            }
        }
        private string _showAll;
        public string ShowAll

        {
            get => _showAll;
            set
            {
                _showAll = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ShowAllEventsCommand => new RelayCommand(ShowAllEventsClick);

        private async void ShowAllEventsClick()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SchedulerPage());
        }

        public RelayCommand ShowAllLeadsCommand => new RelayCommand(ShowAllLeadsClick);

        private async void ShowAllLeadsClick()
        {
            StaticData.CurrentFormType = Convert.ToInt32(FormType.Leads);
            await App.Current.MainPage.Navigation.PushAsync(new ListPage());
        }

        public RelayCommand ShowAllDealsCommand => new RelayCommand(ShowAllDealsClick);

        private async void ShowAllDealsClick()
        {
            StaticData.CurrentFormType = Convert.ToInt32(FormType.Deals);
            await App.Current.MainPage.Navigation.PushAsync(new ListPage());
        }


        internal async void ToggleClick(EventsHomeModel eventsHomeModel)
        {
        

            try
            {
                var result = await DetailsService.UpdateStatus(eventsHomeModel.id, eventsHomeModel.is_completed);
                if (result.status == 200 && result.data != null && result.data.success)
                {
                //    await ShowSnackbar(crmLng.EventUpdated);
                }
            }
            catch (Exception ex)
            {

            }
        }

        internal async void ToggleClickIncomplete(EventsHomeModel eventsHomeModel)
        {
            try
            {
                var result = await DetailsService.UpdateStatus(eventsHomeModel.id, eventsHomeModel.is_completed);
                 Initilize();
            }
            catch(Exception ex)
            {

            }
           
        }

        internal async Task NavigateToEvents(EventsHomeModel model)
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(new EventsPage(model.id));

            }
            catch (Exception ex)
            {

            }
        }
        internal async Task NavigateToDeals(DealHomeModel model)
        {
         
            try
            {
                StaticData.CurrentFormType = (int)FormType.Deals;
                await App.Current.MainPage.Navigation.PushAsync(new AddPage(model.id));
            }
            catch (Exception ex)
            {

            }
        }

        internal async Task NavigateToHotSales(HotSaleHomeModel model)
        {
            try
            {
                StaticData.Name = model.name;
                StaticData.Amount = model.consumer_price;
                StaticData.CurrentFormType = (int)FormType.Deals;
                await App.Current.MainPage.Navigation.PushAsync(new AddPage());
            }
            catch (Exception ex)
            {

            }
            
        }
        internal async Task NavigateToLeads(FormsDetailModel model)
        {
            try
            {
                StaticData.CurrentFormType = (int)FormType.Leads;

                await App.Current.MainPage.Navigation.PushAsync(new AddPage(model.id));
            }
            catch (Exception ex)
            {

            }
           

        }

        internal async Task NavigateToContacts(BirthdayHomeModel model)
        {
            try
            {
                StaticData.CurrentFormType = (int)FormType.Contacts;
                await App.Current.MainPage.Navigation.PushAsync(new AddPage(model.id));
            }
            catch (Exception ex)
            {

            }

            
        }
        internal async void Initilize()
        {
            //var loader = await ShowLoading();

            try
            {
                PageLoaded = false;

                InitilizeCulture();

                if (StaticData.SelectedLanguageModel.Id == 2)
                {
                    HebrewTitle = true ;
                    EnglishTitle = false;
                }
                else
                {
                    HebrewTitle = false;
                    EnglishTitle = true;
                }

                TodayEvents = crmLng.TodayEvents;
                IncompleteEvents = crmLng.IncompleteEvents;
                HotSaleProducts = crmLng.HotSaleProducts;
                CustomerBirthday = crmLng.CustomerBirthday;
                RecentDeals = crmLng.RecentDeals;
                RecentLeads = crmLng.RecentLeads;
                ShowAll = crmLng.ShowAll;

                EventsList = new ObservableCollection<EventsHomeModel>();
                IncompleteEventsList = new ObservableCollection<EventsHomeModel>();
                HotSalesList = new ObservableCollection<HotSaleHomeModel>();
                BirthdaysList = new ObservableCollection<BirthdayHomeModel>();
                LeadsList = new ObservableCollection<FormDetailsHomeModel>();
                DealsList = new ObservableCollection<DealHomeModel>();

                PageTitle = crmLng.Home;


           

                var todaysEventsResult = await HomeServices.GetTodayEvents(DateTime.Now.GetDateTimeFormat(), StaticData.UserModel.user_id);
                if (todaysEventsResult.status == 200 && todaysEventsResult.data!=null)
                {
                    EventsList = new ObservableCollection<EventsHomeModel>(todaysEventsResult.data);
                }


                var todaysIncompleteEventsResult = await HomeServices.GetAllIncompletedEvents(StaticData.UserModel.user_id);
                if (todaysIncompleteEventsResult.status == 200 && todaysIncompleteEventsResult.data != null)
                {
                    IncompleteEventsList = new ObservableCollection<EventsHomeModel>(todaysIncompleteEventsResult.data.Take(3));
                }


                var hotSaleResult = await HomeServices.GetHotSaleProducts();
                if (hotSaleResult.status == 200 && hotSaleResult.data != null)
                {
                    HotSalesList = new ObservableCollection<HotSaleHomeModel>(hotSaleResult.data.Take(3));
                }

                var clientBirthDayesult = await HomeServices.GetClientBirthday(DateTime.Now.GetDateTimeFormat(), 100, 5);
                if (clientBirthDayesult.status == 200 && clientBirthDayesult.data != null)
                {
                    if (clientBirthDayesult.data != null)
                    {
                        foreach (var item in clientBirthDayesult.data)
                        {
                            item.days_to_go = item.days_to_go.Split('.').FirstOrDefault();
                            item.photo = String.IsNullOrEmpty(item.photo) ? "placeholder.png" : item.photo;
                        }

                    }
                    BirthdaysList = new ObservableCollection<BirthdayHomeModel>(clientBirthDayesult.data.Take(3));
                }

                var leadsResult = await HomeServices.GetFormDetails(Convert.ToInt32(FormType.Leads), 3);
                if (leadsResult.status == 200 && leadsResult.data != null)
                {
                    LeadsList = new ObservableCollection<FormDetailsHomeModel>(leadsResult.data.Take(3));
                }

                var dealsesult = await HomeServices.GetDealHomes(Convert.ToInt32(FormType.Deals), 3);
                if (dealsesult.status == 200 && dealsesult.data != null)
                {
                    DealsList = new ObservableCollection<DealHomeModel>(dealsesult.data.Take(3));
                }
            }
            catch(Exception ex)
            {

            }
            await Task.Delay(3000);

            PageLoaded = true;

          //await  loader.DismissAsync();
        }
    }
}
