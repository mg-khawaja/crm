using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Models
{



    public class AddResponseApiModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public AddResponseModel data { get; set; }
    }

    public class AddResponseModel
    {
        public string id { get; set; }
        public bool is_success { get; set; }
        public string[] img_path { get; set; }
    }


    public class FormsListApiModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public FormsModel data { get; set; }
    }

    public class FormsModel
    {
        public Column[] columns { get; set; }
        public List<List> list { get; set; }
        public int count { get; set; }
    }

    public class Column
    {
        public string field { get; set; }
        public string header_text { get; set; }
        public string data_type { get; set; }
        public bool visible { get; set; }
        public string control_type { get; set; }
    }

    public class List : ViewModelBase
    {
        public string rowId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ID { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string ResponsiblePerson { get; set; }
        public string CreatedOn { get; set; }
        public string Comment { get; set; }
        public string Photo { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }

        public string Contact { get; set; }
        public string Amount { get; set; }
        public string DealStage { get; set; }
        public string Name { get; set; }



        public string AddressTo { get; set; }
        public string AddressFrom { get; set; }
        public string AmountWithVat { get; set; }

        private bool _extendedViewVisibility;
        public bool ExtendedViewVisibility
        {
            get => _extendedViewVisibility;
            set
            {
                _extendedViewVisibility = value;
                RaisePropertyChanged();
            }
        }


    }














}
