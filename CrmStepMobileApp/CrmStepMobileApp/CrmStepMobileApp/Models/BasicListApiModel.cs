using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{

    public class BasicListApiModel : BaseModel
    {

        public BasicListModel data { get; set; }
    }

    public class BasicListModel
    {
        public List<BasicList> list { get; set; }
        public int count { get; set; }
    }

    public class BasicList : ViewModelBase
    {
        public int id { get; set; }

        private string _name;
        public string name
        {
            get => _name; set
            {

                _name = value;
                RaisePropertyChanged();

            }
        }
    }

}
