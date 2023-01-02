using CrmStepMobileApp.Helper;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
    public class DailyPopupViewViewModel : BaseViewModel
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

        //internal void PageOnDisAppearring()
        //{
        //    RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
        //    recurrenceProperties.RecurrenceType = RecurrenceType.Daily;
        //    recurrenceProperties.Interval = RepeatEveryCount;
        //    if (EndIndex == 0)
        //    {
        //        recurrenceProperties.RecurrenceRange = RecurrenceRange.NoEndDate;

        //    }
        //    else if (EndIndex == 1)
        //    {
        //        recurrenceProperties.RecurrenceRange = RecurrenceRange.EndDate;
        //        recurrenceProperties.EndDate = UntileDate;

        //    }
        //    else if (EndIndex == 2)
        //    {
        //        recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
        //        recurrenceProperties.RecurrenceCount = EndCount;

        //    }

        //    RecurrenceProperties = recurrenceProperties;


        //    //    recurrenceProperties.RecurrenceRule = schedule.RRuleGenerator(recurrenceProperties, meeting.From, Meeting.To);

        //    // Setting recursive rule for an event
        //    //  meeting.RecurrenceRule = recurrenceProperties.RecurrenceRule;       
        //}

        public void CreateReccurenceProperties()
        {
            //RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
            //recurrenceProperties.RecurrenceType = RecurrenceType.Daily;
            //recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
            //recurrenceProperties.Interval = 2;
            //recurrenceProperties.RecurrenceCount = 10;



            var recurrenceProperties = new RecurrenceProperties();
            recurrenceProperties.RecurrenceType = RecurrenceType.Daily;
            recurrenceProperties.Interval = RepeatEveryCount;
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

            EndIndex = 0;
            EndText = EndList[EndIndex];
            RepeatEveryCount = 1;
            EndCountVisibility  = false;
            UntilDatePickerVisibility = false;
            UntileDate = DateTime.Now;

            EndCount = 1;


            CreateReccurenceProperties();

            if (recurrenceProperties != null)
            {

            }

        }
    }
}
