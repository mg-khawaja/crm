using CrmStepMobileApp.Helper;
using GalaSoft.MvvmLight;
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
    public class WeeklyPopupViewViewModel : BaseViewModel
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

        //private string _untileDateStr;
        //public string UntileDateStr
        //{
        //    get => _untileDateStr;
        //    set
        //    {
        //        _untileDateStr = value;
        //        RaisePropertyChanged();
        //    }
        //}
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

        private ObservableCollection<WeekModel> _daysList;

        public ObservableCollection<WeekModel> DaysList
        {
            get => _daysList;
            set
            {
                _daysList = value;
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



        public void CreateReccurenceProperties()
        {
            //RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
            //recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
            //recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
            //recurrenceProperties.Interval = 1;
            //recurrenceProperties.RecurrenceCount = 10;



            var recurrenceProperties = new RecurrenceProperties();
            recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
            recurrenceProperties.Interval = RepeatEveryCount;
            //recurrenceProperties.WeekDays = WeekDays.Monday | WeekDays.Wednesday | WeekDays.Friday;
         //   recurrenceProperties.WeekDays = recurrenceProperties.WeekDays| WeekDays.Sunday ;

            var selectedDays = DaysList.Where(s => s.IsChecked).ToList();
            if (selectedDays.Any())
            {
                foreach (var item in selectedDays)
                {



                    if(item.Day==WeekDays.Monday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays|  WeekDays.Monday;
                    }
                    if (item.Day == WeekDays.Tuesday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Tuesday;

                    }
                    if (item.Day == WeekDays.Wednesday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Wednesday;

                    }
                    if (item.Day == WeekDays.Thursday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Thursday;

                    }
                    if (item.Day == WeekDays.Friday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Friday;

                    }
                    if (item.Day == WeekDays.Saturday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Saturday;

                    }
                    if (item.Day == WeekDays.Sunday.ToString())
                    {
                        recurrenceProperties.WeekDays = recurrenceProperties.WeekDays | WeekDays.Sunday;

                    }
                }
            }
            else
            {
                recurrenceProperties.WeekDays = WeekDays.None;

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

        public void Initilize(RecurrenceProperties recurrenceProperties)
        {
            InitilizeCulture();

            var list = Enum.GetNames(typeof(WeekDays));
            DaysList = new ObservableCollection<WeekModel>();
            foreach (var item in list)
            {
                DaysList.Add(new WeekModel() { Day = item  });
            }
            DaysList.RemoveAt(7);
            DaysList.RemoveAt(0);

            EndIndex = 0;
            EndText = EndList[EndIndex];
            RepeatEveryCount = 1;
            EndCountVisibility = false;
            UntilDatePickerVisibility = false;
            UntileDate = DateTime.Now;

            EndCount = 1;

            CreateReccurenceProperties();

            if (recurrenceProperties != null)
            {

            }

        }
    }

    public class WeekModel : ViewModelBase
    {
        public string Day { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }

        public WeekDays WeekDays { get; set; }
    }
}
