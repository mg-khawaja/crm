using CommonServiceLocator;
using CrmStepMobileApp.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<MenuPageVIewModel>();
            SimpleIoc.Default.Register<AddPageViewModel>();
            SimpleIoc.Default.Register<ListPageViewModel>();
            SimpleIoc.Default.Register<SchedulerPageViewModel>();
            SimpleIoc.Default.Register<CalendarPopupViewViewModel>();
            SimpleIoc.Default.Register<EventsPageViewModel>();
            SimpleIoc.Default.Register<DailyPopupViewViewModel>();
            SimpleIoc.Default.Register<WeeklyPopupViewViewModel>();
            SimpleIoc.Default.Register<MonthlyPopupViewViewModel>();
            SimpleIoc.Default.Register<YearPopupViewViewModel>();
            SimpleIoc.Default.Register<SwitchProjectViewViewModel>();
            SimpleIoc.Default.Register<NotificationPageViewModel>();
            SimpleIoc.Default.Register<BottomLayoutViewViewModel>();
            SimpleIoc.Default.Register<DashboardPageViewModel>();
            SimpleIoc.Default.Register<EventTimeLinePageViewModel>();
            SimpleIoc.Default.Register<DocumentsPageViewModel>();
            SimpleIoc.Default.Register<AddDocumentPageViewModel>();
            SimpleIoc.Default.Register<SpecialDealsPageViewModel>();
            
        }
        
        public LoginPageViewModel LoginPageViewModel => ServiceLocator.Current.GetInstance<LoginPageViewModel>();
        public MenuPageVIewModel MenuPageVIewModel => ServiceLocator.Current.GetInstance<MenuPageVIewModel>();
        public HomePageViewModel HomePageViewModel => ServiceLocator.Current.GetInstance<HomePageViewModel>();
        public AddPageViewModel AddPageViewModel => ServiceLocator.Current.GetInstance<AddPageViewModel>();
        public ListPageViewModel ListPageViewModel => ServiceLocator.Current.GetInstance<ListPageViewModel>();
        public SchedulerPageViewModel SchedulerPageViewModel => ServiceLocator.Current.GetInstance<SchedulerPageViewModel>();
        public CalendarPopupViewViewModel CalendarPopupViewViewModel => ServiceLocator.Current.GetInstance<CalendarPopupViewViewModel>();
        public EventsPageViewModel EventsPageViewModel => ServiceLocator.Current.GetInstance<EventsPageViewModel>();
        public DailyPopupViewViewModel DailyPopupViewViewModel => ServiceLocator.Current.GetInstance<DailyPopupViewViewModel>();
        public WeeklyPopupViewViewModel WeeklyPopupViewViewModel => ServiceLocator.Current.GetInstance<WeeklyPopupViewViewModel>();
        public MonthlyPopupViewViewModel MonthlyPopupViewViewModel => ServiceLocator.Current.GetInstance<MonthlyPopupViewViewModel>();
        public YearPopupViewViewModel YearPopupViewViewModel => ServiceLocator.Current.GetInstance<YearPopupViewViewModel>();
        public SwitchProjectViewViewModel SwitchProjectViewViewModel => ServiceLocator.Current.GetInstance<SwitchProjectViewViewModel>();
        public NotificationPageViewModel NotificationPageViewModel => ServiceLocator.Current.GetInstance<NotificationPageViewModel>();
        public BottomLayoutViewViewModel BottomLayoutViewViewModel => ServiceLocator.Current.GetInstance<BottomLayoutViewViewModel>();
        public DashboardPageViewModel DashboardPageViewModel => ServiceLocator.Current.GetInstance<DashboardPageViewModel>();
        public EventTimeLinePageViewModel EventTimeLinePageViewModel => ServiceLocator.Current.GetInstance<EventTimeLinePageViewModel>();
        public DocumentsPageViewModel DocumentsPageViewModel => ServiceLocator.Current.GetInstance<DocumentsPageViewModel>();
        public AddDocumentPageViewModel AddDocumentPageViewModel => ServiceLocator.Current.GetInstance<AddDocumentPageViewModel>();
        public SpecialDealsPageViewModel SpecialDealsPageViewModel => ServiceLocator.Current.GetInstance<SpecialDealsPageViewModel>();
        
    }
    
}
