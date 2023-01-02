using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using CrmStepMobileApp.Views.ContentViews;
using CrmStepMobileApp.Views.PopupView;
using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
    public class EventsPageViewModel : BaseViewModel
    {
        #region 
        public int Page = 1;
        private int _count = 10;
        public int TotalFetchedReults = 0;
        public int TotalExpectedReults = 0;
        private bool _isFetching;
        public bool IsFetching
        {
            get => _isFetching;
            set
            {
                _isFetching = value;
                RaisePropertyChanged();
            }
        }
        private Syncfusion.ListView.XForms.LoadMoreOption _loadMore;
        public Syncfusion.ListView.XForms.LoadMoreOption LoadMore
        {
            get => _loadMore;
            set
            {
                _loadMore = value;
                RaisePropertyChanged();
            }
        }
        public string searchText;
        private bool _isListVisible;
        public bool IsListVisible
        {
            get => _isListVisible;
            set
            {
                _isListVisible = value;
                RaisePropertyChanged();
            }
        }


        public List<string> ReccurenceList { get; set; } = new List<string>() { crmLng.Never, crmLng.Daily, crmLng.Weekly, crmLng.Monthly, crmLng.Yearly };
        private BasicInfo BasicInfo { get; set; }

        private DateTime _startsOn;
        public DateTime StartOn
        {
            get => _startsOn;
            set
            {
                _startsOn = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _endsOn;
        public DateTime EndsOn
        {
            get => _endsOn;
            set
            {
                _endsOn = value;
                RaisePropertyChanged();
            }
        }


        private SearchModel _selectedMode;
        private string _assignedTo;
        public string AssignedTo
        {
            get => _assignedTo;
            set
            {
                _assignedTo = value;
                AssignedToSelected = false;
                _selectedMode = new SearchModel();
                RaisePropertyChanged();
            }
        }
        private bool _assignedToSelected = false;
        public bool AssignedToSelected
        {
            get => _assignedToSelected;
            set
            {
                _assignedToSelected = value;
                RaisePropertyChanged();
            }
        }
        private bool _isCancelBtnVisible = true;
        public bool IsCancelBtnVisible
        {
            get => _isCancelBtnVisible;
            set
            {
                _isCancelBtnVisible = value;
                RaisePropertyChanged();
            }
        }
        private string _assignedFor;
        public string AssignedFor
        {
            get => _assignedFor;
            set
            {
                _assignedFor = value;
                RaisePropertyChanged();
            }
        }

        private DicModel _selectedPriorty;
        public DicModel SelectedPriorty
        {
            get => _selectedPriorty;
            set
            {
                _selectedPriorty = value;
                RaisePropertyChanged();
            }
        }


        private DicModel _selectedAction;
        public DicModel SelectedAction
        {
            get => _selectedAction;
            set
            {
                _selectedAction = value;
                RaisePropertyChanged();
            }
        }
        private SearchModel _searchModel;
        public SearchModel SearchModel
        {
            get => _searchModel;
            set
            {
                _searchModel = value;
                RaisePropertyChanged();
            }
        }



        private string _selectedRepeat;
        public string SelectedRepeat
        {
            get => _selectedRepeat;
            set
            {
                _selectedRepeat = value;
                RaisePropertyChanged();
            }
        }


        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                RaisePropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged();
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                RaisePropertyChanged();
            }
        }

        private bool _isAllDay;
        public bool IsAllDay
        {
            get => _isAllDay;
            set
            {
                _isAllDay = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> _checkedIds;
        public ObservableCollection<int> CheckedIds
        {
            get => _checkedIds;
            set
            {
                _checkedIds = value;
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

        private ObservableCollection<SearchModel> _assignedToList;
        public ObservableCollection<SearchModel> AssignedToList
        {
            get => _assignedToList;
            set
            {
                _assignedToList = value;
                RaisePropertyChanged();
            }
        }
        private RecurrenceProperties _recurrenceProperties;

        public RecurrenceProperties RecurrenceProperties
        {
            get => _recurrenceProperties;
            set
            {
                _recurrenceProperties = value;
                RaisePropertyChanged();
            }
        }

        private string _reoccurenceRule;

        public string ReoccurenceRule
        {
            get => _reoccurenceRule;
            set
            {
                _reoccurenceRule = value;
                RaisePropertyChanged();
            }
        }



        public string Id { get; set; }
        public int ScheduleId { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsItemSelected { get; set; }
        public bool IsPageLoaded { get; set; }

        #endregion

        #region Commands

        public RelayCommand ActionCommand => new RelayCommand(ActionClick);

        private async void ActionClick()
        {
            var list = BasicInfo.actions.Select(s => s.Text).ToList();
            var index = await ShowActionSheet(crmLng.Select, list);
            if (index >= 0)
            {
                SelectedAction = BasicInfo.actions[index];
            }

        }
        public RelayCommand RepeatCommand => new RelayCommand(RepeatClick);

        private async void RepeatClick()
        {

            var index = await ShowActionSheet(crmLng.Select, ReccurenceList);
            if (index >= 0)
            {
                if (index == 0)
                {
                    SelectedRepeat = ReccurenceList[index];

                    return;
                }
                if (index == 1)
                {
                    SelectedRepeat = ReccurenceList[index];
                    var page = new DailyPopupView();
                    page.Disappearing += (s, e) =>
                    {
                        RecurrenceProperties = App.Locator.DailyPopupViewViewModel.RecurrenceProperties;
                    };
                    await PopupNavigation.Instance.PushAsync(page);
                    return;
                }
                if (index == 2)
                {
                    SelectedRepeat = ReccurenceList[index];
                    var page = new WeeklyPopupView();
                    page.Disappearing += (s, e) =>
                    {
                        RecurrenceProperties = App.Locator.WeeklyPopupViewViewModel.RecurrenceProperties;
                    };
                    await PopupNavigation.Instance.PushAsync(page);
                    return;
                }
                if (index == 3)
                {
                    SelectedRepeat = ReccurenceList[index];
                    var page = new MonthlyPopupView();
                    page.Disappearing += (s, e) =>
                    {
                        RecurrenceProperties = App.Locator.MonthlyPopupViewViewModel.RecurrenceProperties;
                    };
                    await PopupNavigation.Instance.PushAsync(page);
                    return;
                }
                if (index == 4)
                {
                    SelectedRepeat = ReccurenceList[index];
                    var page = new YearPopupView();
                    page.Disappearing += (s, e) =>
                    {
                        RecurrenceProperties = App.Locator.YearPopupViewViewModel.RecurrenceProperties;
                    };
                    await PopupNavigation.Instance.PushAsync(page);
                    return;
                }
            }

        }
        public RelayCommand PriortyCommand => new RelayCommand(PriortyClick);

        private async void PriortyClick()
        {
            var list = BasicInfo.priorities.Select(s => s.Text).ToList();
            var index = await ShowActionSheet(crmLng.Select, list);
            if (index >= 0)
            {
                SelectedPriorty = BasicInfo.priorities[index];
            }
        }

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);
        private async void SaveClick()
        {

            try
            {
                var checkedIds = MultiSelectedchoices.Where(s => s.IsChecked).Select(s => s.Value).ToList();
                CheckedIds = new ObservableCollection<int>(checkedIds);


                if (SelectedRepeat != ReccurenceList[0])
                {

                    ReoccurenceRule = App.SfSchedule.RRuleGenerator(RecurrenceProperties, StartOn, EndsOn);

                    if (string.IsNullOrEmpty(ReoccurenceRule))
                    {
                        await ShowAlert(crmLng.ReccurenceRuleNotValid);
                        return;
                    }
                }
                else
                {
                    ReoccurenceRule = String.Empty;
                }


                if (String.IsNullOrEmpty(SelectedAction.Text))
                {
                    return;
                }


                var obj = new
                {
                    project_id = StaticData.UserModel.last_project_id,
                    user_id = StaticData.UserModel.user_id,
                    id = Id,
                    action = SelectedAction.Value,
                    parent_type = SearchModel.FormType,
                    parent_id = SearchModel.RowId,
                    location = Location,
                    subject = Subject,
                    description = Description,
                    start_time = StartOn.ToString("yyyy-MM-ddTHH:mm:ss") + "Z",//StartOn.GetDateTimeFormat(),// "2019-12-23T19:40:58.205Z",
                    end_time = EndsOn.ToString("yyyy-MM-ddTHH:mm:ss") + "Z",// EndsOn.GetDateTimeFormat(),
                    schedule_id = ScheduleId,
                    is_all_day = IsAllDay,
                    is_read_only = IsReadOnly,
                    is_completed = IsCompleted,
                    recurrence_rule = ReoccurenceRule,// SelectedRepeat,
                    reponsible_persons = CheckedIds,
                    assign_to = AssignedTo,
                    priority = SelectedPriorty.Value
                };

                var json = JsonConvert.SerializeObject(obj);
                var result = await DetailsService.PostSchedule(json);
                if (result != null && result.status == 200)
                {
                    await ShowSnackbar(crmLng.EventUpdated);

                    App.Locator.SchedulerPageViewModel.RefreshNeeded = true;

                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await ShowSnackbar(crmLng.EventCannotBeAdded);

                }
            }
            catch (Exception ex)
            {

            }



        }

        public RelayCommand SearchCommand => new RelayCommand(SearchClick);
        private async void SearchClick()
        {

            try
            {
                IsFetching = true;
                if (IsPageLoaded)
                {
                    if (!String.IsNullOrEmpty(AssignedTo) && AssignedTo.Length > 1)
                    {
                        var result = await DetailsService.Search("", AssignedTo, _count, Page);
                        IsListVisible = true;
                        if (result.status == 200)
                        {
                            clearList();
                            if (result.data.list != null)
                            {
                                searchText = AssignedTo;
                                TotalExpectedReults = result.data.count;
                                TotalFetchedReults = result.data.list.Count();
                                result.data.list.ForEach(s => AssignedToList.Add(s));
                                IsListVisible = true;
                                LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;
                                Page++;
                            }
                        }
                        else
                        {
                            clearList();
                            await ShowAlert(result.message);
                        }
                    }
                    else
                    {
                        clearList();
                    }
                }
                IsFetching = false;
            }
            catch (Exception ex)
            {
                IsFetching = false;
            }
        }

        public RelayCommand CancelCommand => new RelayCommand(CancelClick);
        private void CancelClick()
        {

            try
            {
                AssignedTo = "";
                AssignedFor = "";
                AssignedToSelected = false;
                _selectedMode = new SearchModel();
                clearList();
            }
            catch (Exception ex)
            {

            }
        }

        public RelayCommand LoadMoreItemsCommand => new RelayCommand(fetchData, CanLoadMoreItems);
        private bool CanLoadMoreItems()
        {
            if (TotalExpectedReults == TotalFetchedReults)
                return false;
            return true;
        }
        private async void fetchData()
        {
            if (IsFetching == true)
                return;
            IsFetching = true;
            try
            {
                var result = await DetailsService.Search("", searchText, _count, Page);
                if (result.status == 200 && result.data != null && result.data.list != null)
                {
                    TotalExpectedReults = result.data.count;
                    TotalFetchedReults += result.data.list.Count();
                    result.data.list.ForEach(s => AssignedToList.Add(s));
                    Page++;
                }
                else
                {
                    clearList();
                    await ShowAlert(result.message);
                }
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string> {
                    { "Username", Settings.Username },
                    { "Form Type", "Schedule Event fetch for Assign To" }
                  };
                Crashes.TrackError(e, properties);
                clearList();
                await ShowAlert("An error occured, Please try again later!");
            }
            IsFetching = false;
        }
        public void clearList()
        {
            AssignedToList.Clear();
            IsListVisible = false;
            LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
            TotalFetchedReults = TotalExpectedReults = 0;
            Page = 1;
            searchText = "";
        }

        public RelayCommand EditModeCommand => new RelayCommand(GotoEditMode);
        private async void GotoEditMode()
        {
            try
            {
                if (_selectedMode == null || string.IsNullOrEmpty(_selectedMode.RowId))
                    return;
                var val = Enum.GetName(typeof(FormType), _selectedMode.FormType);//  field.FormType
                switch (val)
                {
                    case "Leads":
                        StaticData.CurrentFormType = 1;
                        break;
                    case "Contacts":
                        StaticData.CurrentFormType = 2;
                        break;
                    case "Companies":
                        StaticData.CurrentFormType = 3;
                        break;
                    case "Deals":
                        StaticData.CurrentFormType = 4;
                        break;
                    case "Tickets":
                        StaticData.CurrentFormType = 5;
                        break;
                }
                await App.Current.MainPage.Navigation.PushAsync(new AddPage(_selectedMode.RowId));
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        #region Events

        public async Task SearchImplementation(string search)
        {
            if (IsPageLoaded)
            {
                if (!String.IsNullOrEmpty(search) && search.Length > 1)
                {
                    var result = await DetailsService.Search("", search, _count, Page);
                    if (result.status == 200)
                    {
                        clearList();
                        if (result.data.list != null)
                        {
                            LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
                            searchText = search;
                            IsListVisible = true;
                            TotalExpectedReults = result.data.count;
                            TotalFetchedReults = result.data.list.Count();
                            result.data.list.ForEach(s => AssignedToList.Add(s));
                            LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;
                            Page++;
                        }
                    }
                    else
                    {
                        clearList();
                        await ShowAlert(result.message);
                    }
                }
                else
                {
                    clearList();
                }
            }
        }

        internal void ItemSelected(SearchModel field)
        {
            AssignedTo = field.Name;
            InitilizeCulture();
            SearchModel = field;
            var val = Enum.GetName(typeof(FormType), field.FormType);//  field.FormType
            switch (val)
            {
                case "Leads":
                    AssignedFor = crmLng.Leads;
                    break;

                case "Contacts":
                    AssignedFor = crmLng.Contacts;
                    break;

                case "Companies":
                    AssignedFor = crmLng.Companies;
                    break;

                case "Deals":
                    AssignedFor = crmLng.Deals;
                    break;
                case "Tickets":
                    AssignedFor = crmLng.Tickets;
                    break;
            }
            AssignedToSelected = true;
            _selectedMode = field;
            clearList();
        }

        internal async void Initilize(string id = "", DateTime? dateTime = null, SearchModel inputSearchModel = null)
        {
            try
            {

                HeVisibility = StaticData.SelectedLanguageModel.Id == 2;

                EngVisibility = StaticData.SelectedLanguageModel.Id != 2;



                InitilizeCulture();

                IsPageLoaded = false;

                PageTitle = crmLng.Events;
                AssignedToList = new ObservableCollection<SearchModel>();

                StartOn = DateTime.Now;
                EndsOn = StartOn.Date.AddMinutes(15);

                var loader = await ShowLoading();
                var result = await DetailsService.GetBasicInfo("");
                await loader.DismissAsync();

                ReccurenceList = new List<string>() { crmLng.Never, crmLng.Daily, crmLng.Weekly, crmLng.Monthly, crmLng.Yearly };

                SelectedRepeat = ReccurenceList[0];

                if (result.status == 200)
                {
                    BasicInfo = result.data;
                    foreach (var item in BasicInfo.users)
                    {
                        if (item.Value == StaticData.UserModel.user_id)
                        {
                            item.IsChecked = true;
                        }
                        else
                        {
                            item.IsChecked = false;
                        }
                    }

                    MultiSelectedchoices = new ObservableCollection<DicModel>(BasicInfo.users);



                }

                SelectedAction = new DicModel();
                SearchModel = new SearchModel();
                SelectedPriorty = BasicInfo.priorities[1];

                Location = Description = Subject = String.Empty;

                if (dateTime == null)
                {
                    dateTime = DateTime.Now;
                }

                StartOn = dateTime.Value;
                EndsOn = dateTime.Value.AddMinutes(15);
                IsCompleted = IsAllDay = false;
                AssignedTo = AssignedFor = String.Empty;
                ScheduleId = 0;
                Id = "";
                IsReadOnly = false;
                ReoccurenceRule = String.Empty;
                CheckedIds = new ObservableCollection<int>();


                if (inputSearchModel != null)
                {
                    IsItemSelected = true;
                    ItemSelected(inputSearchModel);
                    await Task.Delay(2000);
                    IsItemSelected = false;

                }


                if (!String.IsNullOrEmpty(id))
                {
                    var singleModel = await DetailsService.GetSingleSchedule(id);
                    if (singleModel != null && singleModel.status == 200)
                    {
                        var searchModel = new SearchModel();
                        searchModel.RowId = singleModel.data.parent_id;
                        searchModel.FormType = singleModel.data.parent_type;
                        SearchModel = searchModel;


                        Id = singleModel.data.id;
                        SelectedAction = BasicInfo.actions.FirstOrDefault(s => s.Value == singleModel.data.action);

                        Location = singleModel.data.location;
                        Subject = singleModel.data.subject;
                        Description = singleModel.data.description;
                        StartOn = singleModel.data.start_time;
                        EndsOn = singleModel.data.end_time;
                        ScheduleId = singleModel.data.schedule_id;
                        IsAllDay = singleModel.data.is_all_day;
                        IsReadOnly = false;
                        IsCompleted = singleModel.data.is_completed;
                        ReoccurenceRule = singleModel.data.recurrence_rule;
                        CheckedIds = new ObservableCollection<int>(singleModel.data.responsible);
                        AssignedTo = singleModel.data.assign_to;
                        var val = Enum.GetName(typeof(FormType), singleModel.data.parent_type);//  field.FormType
                        switch (val)
                        {
                            case "Leads":
                                AssignedFor = crmLng.Leads;
                                break;

                            case "Contacts":
                                AssignedFor = crmLng.Contacts;
                                break;

                            case "Companies":
                                AssignedFor = crmLng.Companies;
                                break;

                            case "Deals":
                                AssignedFor = crmLng.Deals;
                                break;
                            case "Tickets":
                                AssignedFor = crmLng.Tickets;
                                break;
                        }

                        if (!string.IsNullOrEmpty(searchModel.RowId))
                        {
                            AssignedToSelected = true;
                        }
                        else
                        {
                            AssignedToSelected = false;
                        }
                        _selectedMode = searchModel;

                        SelectedPriorty = BasicInfo.priorities.FirstOrDefault(s => s.Value == singleModel.data.priority);

                        for (var i = 0; i < MultiSelectedchoices.Count; i++)
                        {
                            MultiSelectedchoices[i].IsChecked = false;
                        }
                        
                        foreach (var checkId in CheckedIds)
                        {
                            //foreach (var multiSelectedchoice in MultiSelectedchoices)
                            // {
                            //    if (multiSelectedchoice.Value == checkId)
                            //    {
                            //       multiSelectedchoice.IsChecked = true;
                            //     }
                            // }
                            var choice = MultiSelectedchoices.Where(mc => mc.Value == checkId).FirstOrDefault();
                            if (choice != null)
                            {
                                var index = MultiSelectedchoices.IndexOf(mc => mc.Value == checkId);
                                choice.IsChecked = true;
                                MultiSelectedchoices[index] = choice;
                            }
                        }



                        if (String.IsNullOrEmpty(singleModel.data.recurrence_rule))
                        {
                            SelectedRepeat = ReccurenceList[0];
                        }
                        else
                        {

                            RecurrenceProperties = App.SfSchedule.RRuleParser(singleModel.data.recurrence_rule, singleModel.data.start_time);
                            if (RecurrenceProperties.RecurrenceType == RecurrenceType.Daily)
                            {
                                SelectedRepeat = ReccurenceList[1];
                            }
                            else if (RecurrenceProperties.RecurrenceType == RecurrenceType.Weekly)
                            {

                                SelectedRepeat = ReccurenceList[2];
                            }
                            else if (RecurrenceProperties.RecurrenceType == RecurrenceType.Monthly)
                            {
                                SelectedRepeat = ReccurenceList[3];
                            }
                            else if (RecurrenceProperties.RecurrenceType == RecurrenceType.Yearly)
                            {
                                SelectedRepeat = ReccurenceList[4];

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            await Task.Delay(2000);
            IsPageLoaded = true;

        }
        #endregion
    }
}
