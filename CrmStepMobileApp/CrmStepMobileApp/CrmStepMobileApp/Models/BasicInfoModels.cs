using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{
    public class BasicInfoModels : BaseModel
    {
        public BasicInfo data { get; set; }
    }


    public class BasicInfo
    {
        public DicModel[] users { get; set; }
        public DicModel[] assigned_for { get; set; }
        public DicModel[] actions { get; set; }
        public DicModel[] priorities { get; set; }
        public SettingsBasicInfoModel settings { get; set; }
    }

    public class SettingsBasicInfoModel
    {
        public int responsible_person { get; set; }
        public int default_priority { get; set; }
    }

    public class DicModel : ViewModelBase
    {
        public int Value { get; set; }
        public string Text { get; set; }

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
    }


}
