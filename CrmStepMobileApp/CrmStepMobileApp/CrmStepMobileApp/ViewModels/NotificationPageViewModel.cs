using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
    public class NotificationPageViewModel : BaseViewModel
    {

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

        private ObservableCollection<EventNotifications> _notificationsEventsList;
        public ObservableCollection<EventNotifications> NotificationsEventsList
        {
            get => _notificationsEventsList;
            set
            {
                _notificationsEventsList = value;
                RaisePropertyChanged();
            }
        }
        internal async void EventChanged(string id, bool isCompleted)
        {
            try
            {
                //  var loader = await ShowLoading();
                await DetailsService.UpdateStatus(Convert.ToString(id), isCompleted);
                //  await loader.DismissAsync();
            }
            catch (Exception ex)
            {

            }
        }

        internal async void Initilize()
        {
            try
            {
                InitilizeCulture();
                PageTitle = crmLng.Name;
                NotificationsList =new ObservableCollection<Notifications>( App.Locator.BottomLayoutViewViewModel.NotificationsModel.others);
               NotificationsEventsList = new ObservableCollection<EventNotifications>(App.Locator.BottomLayoutViewViewModel.NotificationsModel.today);

                //var loader = await ShowLoading();
                //var result = await NotificationService.GetNotifications();
                //if (result.status == 200 && result.data != null && result.data.count > 0)
                //{
                //    NotificationsList = new ObservableCollection<Notifications>(result.data.list);
                //}
                //await loader.DismissAsync();
            }
            catch(Exception ex)
            {

            }
           

        }

        internal async void NavigateToLeads(Notifications notifications)
        {
            StaticData.CurrentFormType = (int)FormType.Leads;
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(notifications.parent_id));
        }

        internal async void NavigateToEvents(EventNotifications eventTimeline)
        {
            await App.Current.MainPage.Navigation.PushAsync(new EventsPage(eventTimeline._Id));
        }
    }
}
