using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
    public class LeadsPageViewModel : BaseViewModel
    {

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

        private FormsListApiModel _formsListApiModel;
        public FormsListApiModel FormsListApiModel
        {
            get => _formsListApiModel;
            set
            {
                _formsListApiModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<List> _fieldsList;
        public ObservableCollection<List> FieldsList
        {
            get => _fieldsList;
            set
            {
                _fieldsList = value;
                RaisePropertyChanged();
            }
        }

      
        private ObservableCollection<List> _fieldsListComplete;
        public ObservableCollection<List> FieldsListComplete
        {
            get => _fieldsListComplete;
            set
            {
                _fieldsListComplete = value;
                RaisePropertyChanged();
            }
        }

        
 public RelayCommand ClearTextCommand => new RelayCommand(ClearTextClick);

        private async void ClearTextClick()
        {
            SearchText = String.Empty;
        }
        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(null));
        }
        internal async void EditImplementation(List list)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(list));
        }

        internal void Search(string newTextValue)
        {
            FieldsList.Clear();
            if (String.IsNullOrEmpty(newTextValue))
            {
                FieldsListComplete.ForEach(s => FieldsList.Add(s));
            }
            else
            {
                var searchText = newTextValue.ToLower();
                var list = FieldsListComplete.Where(s => s.FirstName.ToLower().Contains(searchText) || s.LastName.ToLower().Contains(searchText) ||
                                                      s.Address.ToLower().Contains(searchText) || s.Phone.ToLower().Contains(searchText) ||
                                                      s.Mobile.ToLower().Contains(searchText) ||s.Email.ToLower().Contains(searchText) ||
                                                      s.ResponsiblePerson.ToLower().Contains(searchText) || s.Source.ToLower().Contains(searchText));
                list.ForEach(s=>FieldsList.Add(s));
            }

        }


        internal async Task Initilize()
        {
            var result = await DetailsService.GetFormList(StaticData.Culture, StaticData.UTCMins, StaticData.UserModel.last_project_id, StaticData.UserModel.user_id, StaticData.CurrentFormType);
            if (result.status == 200)
            {
                FormsListApiModel = result;
                FieldsList = new ObservableCollection<List>(result.data.list);
                FieldsListComplete = new ObservableCollection<List>(result.data.list);
            }
            else
            {
                await ShowAlert(result.message);
            }
        }
    }
}
