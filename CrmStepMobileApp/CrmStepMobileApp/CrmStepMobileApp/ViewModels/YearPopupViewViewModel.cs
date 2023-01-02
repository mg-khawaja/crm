using CrmStepMobileApp.Helper;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
  public  class YearPopupViewViewModel:BaseViewModel
    {
        public List<string> EndList = new List<string>() { crmLng.Never, crmLng.Until, crmLng.Count };

        private int _repeatEveryCount;
        public int RepeatEveryCount
        {
            get => _repeatEveryCount;
            set
            {
                _repeatEveryCount = value;
                RaisePropertyChanged();
            }
        }

        private int _endCount;
        public int EndCount
        {
            get => _endCount;
            set
            {
                _endCount = value;
                RaisePropertyChanged();
            }
        }


        private string _endText;
        public string EndText
        {
            get => _endText;
            set
            {
                _endText = value;
                RaisePropertyChanged();
            }
        }

        private int _dayOfMonth;
        public int DayOfMonth
        {
            get => _dayOfMonth;
            set
            {
                _dayOfMonth = value;
                RaisePropertyChanged();
            }
        }

        private int _endIndex;
        public int EndIndex
        {
            get => _endIndex;
            set
            {
                _endIndex = value;
                RaisePropertyChanged();
            }
        }
        private string _selectedWeek;
        public string SelectedWeek
        {
            get => _selectedWeek;
            set
            {
                _selectedWeek = value;
                RaisePropertyChanged();
            }
        }
        private string _selectedDay;
        public string SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedMonth;
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                RaisePropertyChanged();
            }
        }
        

        private DateTime _untileDate;
        public DateTime UntileDate
        {
            get => _untileDate;
            set
            {
                _untileDate = value;
                RaisePropertyChanged();
            }
        }
        private bool _untilDatePickerVisibility;

        public bool UntilDatePickerVisibility
        {
            get => _untilDatePickerVisibility;
            set
            {
                _untilDatePickerVisibility = value;
                RaisePropertyChanged();
            }
        }

        private bool _endCountVisibility;

        public bool EndCountVisibility
        {
            get => _endCountVisibility;
            set
            {
                _endCountVisibility = value;
                RaisePropertyChanged();
            }
        }

        private bool _firstIsChecked;

        public bool FirstIsChecked
        {
            get => _firstIsChecked;
            set
            {
                _firstIsChecked = value;
                RaisePropertyChanged();
            }
        }

        private bool _secondIsChecked;

        public bool SecondIsChecked
        {
            get => _secondIsChecked;
            set
            {
                _secondIsChecked = value;
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

        private ObservableCollection<WeekModel2> _daysList;

        public ObservableCollection<WeekModel2> DaysList
        {
            get => _daysList;
            set
            {
                _daysList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<WeekModel2> _weeksList;

        public ObservableCollection<WeekModel2> WeeksList
        {
            get => _weeksList;
            set
            {
                _weeksList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<WeekModel2> _monthsList;

        public ObservableCollection<WeekModel2> MonthsList
        {
            get => _monthsList;
            set
            {
                _monthsList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);

        private async void SaveClick()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public RelayCommand EndCommand => new RelayCommand(EndClick);

        private async void EndClick()
        {
            var index = await ShowActionSheet(crmLng.Select, EndList);
            if (index >= 0)
            {
                EndIndex = index;
                EndText = EndList[EndIndex];
                if (EndIndex == 0)
                {
                    EndCountVisibility = UntilDatePickerVisibility = false;
                }
                else if (EndIndex == 1)
                {
                    UntilDatePickerVisibility = true;
                    EndCountVisibility = false;

                }
                else if (EndIndex == 2)
                {
                    UntilDatePickerVisibility = false;
                    EndCountVisibility = true;
                }
            }
        }


        public RelayCommand DaySelectCommand => new RelayCommand(DaySelectClick);

        private async void DaySelectClick()
        {
            var result = await ShowActionSheet(crmLng.Select, DaysList.Select(s => s.Name).ToList());
            if (result >= 0)
            {
                SelectedDay = DaysList[result].Name;
            }
        }

        public RelayCommand WeekSelectCommand => new RelayCommand(WeekSelectClick);

        private async void WeekSelectClick()
        {
            var result = await ShowActionSheet(crmLng.Select, WeeksList.Select(s => s.Name).ToList());
            if (result >= 0)
            {
                SelectedWeek = WeeksList[result].Name;
            }
        }

        public RelayCommand MonthSelectCommand => new RelayCommand(MonthSelectClick);

        private async void MonthSelectClick()
        {
            var result = await ShowActionSheet(crmLng.Select, MonthsList.Select(s => s.Name).ToList());
            if (result >= 0)
            {
                SelectedMonth = MonthsList[result].Name;
            }
        }
        


        public void CreateReccurenceProperties()
        {
            try
            {
                var recurrenceProperties = new RecurrenceProperties();
                recurrenceProperties.RecurrenceType = RecurrenceType.Yearly;
                recurrenceProperties.Interval = RepeatEveryCount;
                recurrenceProperties.Month= WeeksList.FirstOrDefault(s => s.Name == SelectedMonth).Id;
                if (FirstIsChecked)
                {
                    recurrenceProperties.DayOfMonth = DayOfMonth;

                }
                else
                {
                    recurrenceProperties.Week = WeeksList.FirstOrDefault(s => s.Name == SelectedWeek).Id;
                    recurrenceProperties.DayOfWeek = DaysList.FirstOrDefault(s => s.Name == SelectedDay).Id;
                }

                if (EndIndex == 0)
                {
                    recurrenceProperties.RecurrenceRange = RecurrenceRange.NoEndDate;

                }
                else if (EndIndex == 1)
                {
                    recurrenceProperties.RecurrenceRange = RecurrenceRange.EndDate;
                    recurrenceProperties.EndDate = UntileDate;

                }
                else if (EndIndex == 2)
                {
                    recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
                    recurrenceProperties.RecurrenceCount = EndCount;

                }

                RecurrenceProperties = recurrenceProperties;
            }
            catch (Exception ex)
            {

            }

        }

        public void Initilize(RecurrenceProperties recurrenceProperties)
        {
            InitilizeCulture();

            DaysList = new ObservableCollection<WeekModel2>();
            DaysList.Add(new WeekModel2() { Name = "Sunday", Id = 1 });
            DaysList.Add(new WeekModel2() { Name = "Monday", Id = 2 });
            DaysList.Add(new WeekModel2() { Name = "Tuesday", Id = 3 });
            DaysList.Add(new WeekModel2() { Name = "Wednesday", Id = 4 });
            DaysList.Add(new WeekModel2() { Name = "Thursday", Id = 5 });
            DaysList.Add(new WeekModel2() { Name = "Friday", Id = 6 });
            DaysList.Add(new WeekModel2() { Name = "Saturday", Id = 7 });


            WeeksList = new ObservableCollection<WeekModel2>();
            WeeksList.Add(new WeekModel2() { Name = "First", Id = 1 });
            WeeksList.Add(new WeekModel2() { Name = "Second", Id = 2 });
            WeeksList.Add(new WeekModel2() { Name = "Third", Id = 3 });
            WeeksList.Add(new WeekModel2() { Name = "Fourth", Id = 4 });
            WeeksList.Add(new WeekModel2() { Name = "Last", Id = -1 });

            MonthsList = new ObservableCollection<WeekModel2>();
            MonthsList.Add(new WeekModel2() { Name = "January", Id = 1 });
            MonthsList.Add(new WeekModel2() { Name = "February", Id = 2 });
            MonthsList.Add(new WeekModel2() { Name = "March", Id = 3 });
            MonthsList.Add(new WeekModel2() { Name = "April", Id = 4 });
            MonthsList.Add(new WeekModel2() { Name = "May", Id = 5 });
            MonthsList.Add(new WeekModel2() { Name = "June", Id = 6 });
            MonthsList.Add(new WeekModel2() { Name = "July", Id = 7 });
            MonthsList.Add(new WeekModel2() { Name = "August", Id = 8 });
            MonthsList.Add(new WeekModel2() { Name = "September", Id = 9 });
            MonthsList.Add(new WeekModel2() { Name = "October", Id = 10 });
            MonthsList.Add(new WeekModel2() { Name = "November", Id = 11 });
            MonthsList.Add(new WeekModel2() { Name = "December", Id = 12 });

            EndIndex = 0;
            EndText = EndList[EndIndex];
            RepeatEveryCount = 1;
            EndCountVisibility = false;
            UntilDatePickerVisibility = false;
            UntileDate = DateTime.Now;
            FirstIsChecked = true;
            SecondIsChecked = false;
            DayOfMonth = 1;
            EndCount = 1;

            SelectedDay = DaysList[0].Name;
            SelectedWeek = WeeksList[0].Name;
            SelectedMonth = MonthsList[0].Name;

            CreateReccurenceProperties();

            if (recurrenceProperties != null)
            {

            }

        }
    }
}
