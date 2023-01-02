using GalaSoft.MvvmLight;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class SchedulerViewTypeModel : ViewModelBase
    {

        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name; set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        private ScheduleView _scheduleView;
        public ScheduleView ScheduleView
        {
            get => _scheduleView; set
            {
                _scheduleView = value;
                RaisePropertyChanged();
            }
        }
    }
}
