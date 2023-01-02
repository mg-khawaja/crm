using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using CrmStepMobileApp.Views.PopupView;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Multilingual;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrmStepMobileApp.ViewModels
{
    public class SchedulerPageViewModel : BaseViewModel
    {


        #region Properties


        public List<DateTime> RememberDates { get; set; }
        public bool RefreshNeeded { get; set; }

        private List<SchedulerViewTypeModel> _schedulerTypeList;
        public List<SchedulerViewTypeModel> SchedulerTypeList
        {
            get => _schedulerTypeList;
            set
            {
                _schedulerTypeList = value;
                RaisePropertyChanged();
            }
        }
        private SchedulerViewTypeModel _schedulerViewTypeModel;
        public SchedulerViewTypeModel SchedulerViewTypeModel
        {
            get => _schedulerViewTypeModel;
            set
            {
                _schedulerViewTypeModel = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ScheduleAppointment> _events;
        public ObservableCollection<ScheduleAppointment> Events
        {
            get => _events;
            set
            {
                _events = value;
                RaisePropertyChanged();
            }
        }

        private string _calendarDatesStr;
        public string CalendarDatesStr
        {
            get => _calendarDatesStr;
            set
            {
                _calendarDatesStr = value;
                RaisePropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                RaisePropertyChanged();
            }
        }


        private List<DateTime> _selectedSchedulerDate;
        public List<DateTime> SelectedSchedulerDate
        {
            get => _selectedSchedulerDate;
            set
            {
                _selectedSchedulerDate = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<DicModel> _multiSelectedchoices;
        public ObservableCollection<DicModel> MultiSelectedchoices
        {
            get => _multiSelectedchoices;
            set
            {
                _multiSelectedchoices = value;
                RaisePropertyChanged();
            }
        }

        

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                RaisePropertyChanged();
            }
        }

        private bool _filterVisibility;
        public bool FilterVisibility
        {
            get => _filterVisibility;
            set
            {
                _filterVisibility = value;
                RaisePropertyChanged();
            }
        }
        private bool _schedulerVisibility;
        public bool SchedulerVisibility
        {
            get => _schedulerVisibility;
            set
            {
                _schedulerVisibility = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region Commands

        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EventsPage());
        }

        public RelayCommand SelectScheduleViewCommand => new RelayCommand(SelectScheduleViewClick);

        private async void SelectScheduleViewClick()
        {


            var result = await ShowActionSheet(crmLng.SelectView, SchedulerTypeList.Select(s => s.Name).ToList());
            if (result >= 0)
            {
                SchedulerViewTypeModel = SchedulerTypeList[result];
                SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
                await InitilizeEvents(SelectedSchedulerDate);
            }
        }

        public RelayCommand FilterCommand => new RelayCommand(FilterClick);

        private async void FilterClick()
        {
            var list = new List<DateTime>();
            if (StartDate.HasValue)
            {
                list.Add(StartDate.Value);
            }
            else
            {
                list.Add(DateTime.Now.FirstDayOfMonth());
            }

            if (EndDate.HasValue)
            {
                list.Add(EndDate.Value);
            }
            else
            {
                list.Add(DateTime.Now.LastDayOfMonth());
            }


            SchedulerViewTypeModel = SchedulerTypeList[3];
            SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
            await InitilizeEvents(list, SearchText);
        }

        public RelayCommand ClearCommand => new RelayCommand(ClearClick);

        private async void ClearClick()
        {
            SearchText = String.Empty;
            StartDate = null;
            EndDate = null;
        }

        #endregion

        #region Methods


        internal async void UpdateEventOnDrag(string appointmentId,DateTime startDate)
        {
            var singleModel = await DetailsService.GetSingleSchedule(appointmentId);
            if (singleModel != null && singleModel.status == 200)
            {
              var model=  singleModel.data;

                var diff = model.end_time.Subtract(model.start_time);
                var endDate = startDate.Add(diff);

                var obj = new
                {
                    project_id = StaticData.UserModel.last_project_id,
                    user_id = StaticData.UserModel.user_id,
                    id = appointmentId,
                    action = model.action,
                    parent_type = model.parent_type,
                    parent_id = model.parent_id,
                    location = model.location,
                    subject = model.subject,
                    description = model.description,
                    start_time = startDate.ToString("yyyy-MM-ddTHH:mm:ss") + "Z",//StartOn.GetDateTimeFormat(),// "2019-12-23T19:40:58.205Z",
                    end_time = endDate.ToString("yyyy-MM-ddTHH:mm:ss") + "Z",// EndsOn.GetDateTimeFormat(),
                    schedule_id = model.schedule_id,
                    is_all_day = model.is_all_day,
                    is_read_only = false,
                    is_completed = model.is_completed,
                    recurrence_rule = model.recurrence_rule,// SelectedRepeat,
                    reponsible_persons = model.responsible,
                    assign_to = model.assign_to,
                    priority = model.priority
                };

                var json = JsonConvert.SerializeObject(obj);
                var result = await DetailsService.PostSchedule(json);
                if (result != null && result.status == 200)
                {
                    await ShowSnackbar(crmLng.EventUpdated);

                    //App.Locator.SchedulerPageViewModel.RefreshNeeded = true;

                    //await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await ShowAlert(crmLng.EventCannotBeAdded);

                }
            }
        }

        public async void VisibleDatesChangedClick(List<DateTime> dateTimes)
        {
            SelectedSchedulerDate = dateTimes;
            SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
            await InitilizeEvents(SelectedSchedulerDate);
        }


        public void SetCalendarDatesStr(List<DateTime> dateTimes, SchedulerViewTypeModel schedulerViewTypeModel)
        {

            switch ((int)schedulerViewTypeModel.ScheduleView)
            {
                case (int)ScheduleView.DayView:
                    CalendarDatesStr = dateTimes[0].ToString("dd/MM/yyyy");
                    break;
                case (int)ScheduleView.MonthView:
                    var middleDate = dateTimes[Convert.ToInt32(dateTimes.Count / 2)];
                    var workStartMonth = middleDate.FirstDayOfMonth();
                    var workEndMonth = middleDate.LastDayOfMonth();
                    CalendarDatesStr = $"{workStartMonth.ToString("dd/MM/yyyy")}"; break;
                default:
                    var workStart = dateTimes.FirstOrDefault();//dateTime.AddDays(-(int)dateTime.DayOfWeek);
                    var workEnd = dateTimes.LastOrDefault();// workStart.AddDays(5).AddSeconds(-1);
                    CalendarDatesStr = $"{workStart.ToString("dd/MM/yyyy")} - {workEnd.ToString("dd/MM/yyyy")}";
                    break;

            }


        }

        public async Task InitilizeEvents(List<DateTime> dateTimes, string search = "")
        {
            try
            {
                RememberDates = dateTimes;

              var tempList   = new ObservableCollection<ScheduleAppointment>();
                EventsApiModel eventsApi = new EventsApiModel();

                var items = MultiSelectedchoices.Where(s => s.IsChecked).Select(s=>Convert.ToString(s.Value));
                string ids = String.Join(", ", items);


                eventsApi = await DetailsService.GetScheduleList( dateTimes.FirstOrDefault().Date.GetDateTimeFormat(),
                                                                  dateTimes.LastOrDefault().Date.GetDateTimeFormat(), search, ids);
                if (eventsApi.status == 200)
                {
                    foreach (var item in eventsApi.data.list)
                    {
                        ScheduleAppointment clientMeeting = new ScheduleAppointment();
                        clientMeeting.StartTime = Convert.ToDateTime(item.start_time);
                        clientMeeting.EndTime = Convert.ToDateTime(item.end_time);
                        
                        if (item.action == 1)
                        {
                            clientMeeting.Color = (Color)App.Current.Resources["TaskColor"];

                        }
                        else
                        if (item.action == 2)
                        {
                            clientMeeting.Color = (Color)App.Current.Resources["MeetingColor"];

                        }
                        else
                        if (item.action == 3)
                        {
                            clientMeeting.Color = (Color)App.Current.Resources["CallColor"];

                        }
                        else
                        {
                            clientMeeting.Color = (Color)App.Current.Resources["ThemeColor"];
                        }

                        clientMeeting.Subject = $" {item.subject}  {item.assign_to}";
                      //  clientMeeting.Subject = $"{Convert.ToDateTime(item.start_time).ToString("HH:mm")}-{Convert.ToDateTime(item.end_time).ToString("HH:mm")}  {item.subject}  {item.assign_to}";


                        if (!String.IsNullOrEmpty(item.recurrence_rule))
                        {
                           clientMeeting.RecurrenceRule = item.recurrence_rule;
                        }
                        clientMeeting.Notes = item.id;
                        clientMeeting.Location = Convert.ToString(item.schedule_id);
                        tempList.Add(clientMeeting);
                    }
                }

               var list= tempList.Distinct(new ItemEqualityComparer());
                Events = new ObservableCollection<ScheduleAppointment>(list);

                //    var ss = Events.Where(s => s.Notes == "5e0581ee84e14723fca499aa");
            }
            catch (Exception ex)
            {

            }

        }

        public async Task Initilize()
        {
            InitilizeCulture();



            PageTitle = crmLng.Scheduler;


            SchedulerTypeList = new List<SchedulerViewTypeModel>()
            {
               new SchedulerViewTypeModel(){Id=1,Name=crmLng.Day,ScheduleView= ScheduleView.DayView},
               new SchedulerViewTypeModel(){Id=2,Name=crmLng.Week,ScheduleView= ScheduleView.WeekView},
               new SchedulerViewTypeModel(){Id=3,Name=crmLng.WorkWeek,ScheduleView= ScheduleView.WorkWeekView},
               new SchedulerViewTypeModel(){Id=4,Name=crmLng.Month,ScheduleView= ScheduleView.MonthView}
            };

            SchedulerViewTypeModel = SchedulerTypeList[1];
            SelectedSchedulerDate = new List<DateTime>() { DateTime.Now };

            SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);




            SearchText = String.Empty;
            StartDate = null;
            EndDate = null;
            FilterVisibility = false;
            SchedulerVisibility = true;
            RefreshNeeded = false;

            var loader = await ShowLoading();
            var result = await DetailsService.GetBasicInfo( "");
            foreach (var item in result.data.users)
            {
                if (item.Value == StaticData.UserModel.user_id)
                {
                    item.IsChecked = true;
                }
            }

            if (result.status == 200)
            {
                MultiSelectedchoices = new ObservableCollection<DicModel>(result.data.users);
            }


            await InitilizeEvents(SelectedSchedulerDate);
            await loader.DismissAsync();
        }
        #endregion
    }
}



//public RelayCommand PreviousCommand => new RelayCommand(PreviousClick);

//private async void PreviousClick()
//{
//    var updatedDate = SelectedSchedulerDate.AddDays(-1);
//    SelectedSchedulerDate = updatedDate;
//    SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
//    await InitilizeEvents(SchedulerViewTypeModel, SelectedSchedulerDate);
//}
//public RelayCommand NextCommand => new RelayCommand(NextClick);

//private async void NextClick()
//{
//    var updatedDate = SelectedSchedulerDate.AddDays(1);
//    SelectedSchedulerDate = updatedDate;
//    SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
//    await InitilizeEvents(SchedulerViewTypeModel, SelectedSchedulerDate);
//}
//public RelayCommand CalendarCommand => new RelayCommand(CalendarClick);

//private async void CalendarClick()
//{
//    var calendarView = new CalendarPopupView(SelectedSchedulerDate);
//    calendarView.Disappearing += async (s, e) =>
//    {
//        SelectedSchedulerDate = App.Locator.CalendarPopupViewViewModel.SelectedDate;
//        SetCalendarDatesStr(SelectedSchedulerDate, SchedulerViewTypeModel);
//        await InitilizeEvents(SchedulerViewTypeModel, SelectedSchedulerDate);

//    };

//    await PopupNavigation.Instance.PushAsync(calendarView);
//}



//case ScheduleView.WeekView:
//    var start = dateTimes.FirstOrDefault();// dateTime.AddDays(-(int)dateTime.DayOfWeek);
//    var end = dateTimes.LastOrDefault();// start.AddDays(6).AddSeconds(-1);
//    CalendarDatesStr = $"{start.ToString("dd-MM-yyyy")}-{end.ToString("dd-MM-yyyy")}";
//    break;


//case ScheduleView.WorkWeekView:
//    var workStart = dateTimes.FirstOrDefault();//dateTime.AddDays(-(int)dateTime.DayOfWeek);
//    var workEnd = dateTimes.LastOrDefault();// workStart.AddDays(5).AddSeconds(-1);
//    CalendarDatesStr = $"{workStart.ToString("dd-MM-yyyy")} - {workEnd.ToString("dd-MM-yyyy")}";
//    break;


//case ScheduleView.MonthView:
//    CalendarDatesStr =  dateTime.ToString("dd-MM-yyyy");
//    break;


//case ScheduleView.TimelineView:
//    CalendarDatesStr = dateTime.ToString("dd-MM-yyyy");
//    break;



//switch (schedulerViewTypeModel.Id)
//{
//    case 1:
//        var todayDate = dateTimes[0].GetDateTimeFormat();
//        eventsApi = await DetailsService.GetScheduleList(StaticData.Culture, StaticData.UTCMins,
//                                                         StaticData.UserModel.last_project_id,
//                                                         StaticData.UserModel.user_id,
//                                                         todayDate, todayDate, search);
//        break;

//    case 2:
//        var singleDate = dateTimes[0].GetDateTimeFormat();
//        eventsApi = await DetailsService.GetScheduleList(StaticData.Culture, StaticData.UTCMins,
//                                                         StaticData.UserModel.last_project_id,
//                                                         StaticData.UserModel.user_id,
//                                                         singleDate, singleDate, search); break;

//    case 3:
//        //var start = dateTime.AddDays(-(int)dateTime.DayOfWeek);
//        //var end = start.AddDays(6).AddSeconds(-1);
//        eventsApi = await DetailsService.GetScheduleList(StaticData.Culture, StaticData.UTCMins,
//                                                         StaticData.UserModel.last_project_id,
//                                                         StaticData.UserModel.user_id,
//                                                         dateTimes.FirstOrDefault().GetDateTimeFormat(), dateTimes.LastOrDefault().GetDateTimeFormat(),
//                                                         search); break;

//    case 4:
//        //var workStart = dateTime.AddDays(-(int)dateTime.DayOfWeek);
//        //var workEnd = workStart.AddDays(5).AddSeconds(-1);
//        eventsApi = await DetailsService.GetScheduleList(StaticData.Culture, StaticData.UTCMins,
//                                                      StaticData.UserModel.last_project_id,
//                                                      StaticData.UserModel.user_id,
//                                                         dateTimes.FirstOrDefault().GetDateTimeFormat(), dateTimes.LastOrDefault().GetDateTimeFormat(), search); break;
//    case 5:
//        eventsApi = await DetailsService.GetScheduleList(StaticData.Culture, StaticData.UTCMins,
//                                                       StaticData.UserModel.last_project_id,
//                                                       StaticData.UserModel.user_id,
//                                                       dateTimes.FirstOrDefault().GetDateTimeFormat(), dateTimes.LastOrDefault().GetDateTimeFormat(), search); break;

//    case 6:
//        break;

//    case 7:
//        break;
//}
