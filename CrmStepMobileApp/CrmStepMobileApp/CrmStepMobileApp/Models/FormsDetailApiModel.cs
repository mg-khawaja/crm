using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CrmStepMobileApp.Models
{




    public class FormsDetailApiModel : BaseModel
    {

        public FormsDetailModel data { get; set; }
    }

    public class FormsDetailModel
    {
        public int type { get; set; }
        public string id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string formId { get; set; }
        public Section staticSections { get; set; }
        public List<Section> sections { get; set; }
        public int fieldCount { get; set; }
        public Dictionary dictionary { get; set; }
    }

    public class Section
    {
        public string id { get; set; }
        public string sectionTitle { get; set; }
        public string translatedTitle { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Field
    {
        public string id { get; set; }
        public string type { get; set; }
        public string translatedTitle { get; set; }
        public string title { get; set; }
        public bool isrequired { get; set; }
        public string instructions { get; set; }
        public string url { get; set; }
        public string[] choices { get; set; }
        public int choiseLoadFrom { get; set; }
        public string controlId { get; set; }
        public string[] value { get; set; }
        public string dataType { get; set; }
        public int inputType { get; set; }
        public object[] preLoadFiles { get; set; }
        public List<FileDataObj> fileData { get; set; }
    }

    public class Dictionary
    {
        public string Static_Section_Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ID { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Responsibleperson { get; set; }
        public string CreatedOn { get; set; }
        public string Comment { get; set; }
        public string Select { get; set; }
        public string Stages { get; set; }
        public string Nothandled { get; set; }
        public string Atwork { get; set; }
        public string Handled { get; set; }
        public string Irrelevan { get; set; }


        public string Dateofbirth { get; set; }
        public string CompanyName { get; set; }

        public string Logo { get; set; }

        public string Website { get; set; }
        public string Contact { get; set; }

        public string Name { get; set; }
        public string Amount { get; set; }

        public string Dealstage { get; set; }
        public string Dealdate { get; set; }
        public string RealDealdate { get; set; }

        public string New { get; set; }
        public string Negotiation { get; set; }
        public string Decision { get; set; }
        public string Harmonizationofcontract { get; set; }
        public string Preparationofdocuments { get; set; }
        public string Inwork { get; set; }
        public string Completed { get; set; }
        public string Incomplete { get; set; }


    }







    public class DetailSectionModel : ViewModelBase
    {
        private ObservableCollection<DetailModel> _fields;
        public ObservableCollection<DetailModel> fields
        {
            get => _fields;
            set
            {
                _fields = value;
                RaisePropertyChanged();
            }
        }

        public string id { get; set; }
        public string sectionTitle { get; set; }
        public string translatedTitle { get; set; }

        public bool IsStatic { get; set; }
    }



    public class DetailModel : ViewModelBase
    {
        public string id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string translatedTitle { get; set; }

        public bool isrequired { get; set; }
        public string instructions { get; set; }
        public string url { get; set; }
        public int choiseLoadFrom { get; set; }
        public string controlId { get; set; }
        public string[] value { get; set; }
        public string dataType { get; set; }
        public int inputType { get; set; }
        public object[] preLoadFiles { get; set; }
        public List<FileDataObj> fileData { get; set; }



        private string[] _choices;
        public string[] choices
        {
            get => _choices;
            set
            {
                _choices = value;
                RaisePropertyChanged();
            }
        }


        private string _textValue;
        public string TextValue
        {
            get => _textValue;
            set
            {
                _textValue = value;
                RaisePropertyChanged("TextValue");
            }
        }
        private DateTime _textValueDateTime;
        public DateTime TextValueDateTime
        {
            get => _textValueDateTime;
            set
            {
                _textValueDateTime = value;
                RaisePropertyChanged();
            }
        }


        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                RaisePropertyChanged();
            }
        }


        private string _fileBytesStr;
        public string FileBytesStr
        {
            get => _fileBytesStr;
            set
            {
                _fileBytesStr = value;
                RaisePropertyChanged();
            }
        }


        private SearchModel _selectedSearchModel;
        public SearchModel SelectedSearchModel
        {
            get => _selectedSearchModel;
            set
            {
                _selectedSearchModel = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<SearchModel> _searchList;
        public ObservableCollection<SearchModel> SearchList
        {
            get => _searchList;
            set
            {
                _searchList = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<MultiSelectModel> _multiselectedchoices;
        public ObservableCollection<MultiSelectModel> MultiSelectedchoices
        {
            get => _multiselectedchoices;
            set
            {
                _multiselectedchoices = value;
                RaisePropertyChanged();
            }
        }



    }
    public class MultiSelectModel : ViewModelBase
    {
        public string Name { get; set; }

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

    public class FileDataObj
    {
        public string name { get; set; }
        public string data { get; set; }
    }

}
