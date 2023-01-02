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
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
  public  class SpecialDealsPageViewModel:BaseViewModel
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



        internal async void EditImplementation(string id)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(id,false));
        }


        internal void Search(string newTextValue)
        {
            try
            {
                FieldsList.Clear();
                if (String.IsNullOrEmpty(newTextValue))
                {
                    FieldsListComplete.ForEach(s => FieldsList.Add(s));
                }
                else
                {
                    var searchText = newTextValue.ToLower();
                    IEnumerable<List> list = new List<List>();

                    switch (StaticData.CurrentFormType)
                    {
                        case 1:
                            list = FieldsListComplete.Where(s => s.FirstName.CompareString(searchText) ||
                                                                  s.LastName.CompareString(searchText) || s.ID.CompareString(searchText) ||
                                                                  s.Phone.CompareString(searchText) || s.Mobile.CompareString(searchText) ||
                                                                  s.Email.CompareString(searchText) || s.Address.CompareString(searchText) ||
                                                                  s.Source.CompareString(searchText) || s.Status.CompareString(searchText) ||
                                                                  s.ResponsiblePerson.CompareString(searchText));
                            break;

                        case 2:
                            list = FieldsListComplete.Where(s => s.FirstName.CompareString(searchText) ||
                                                                  s.LastName.CompareString(searchText) || s.ID.CompareString(searchText) ||
                                                                  s.Phone.CompareString(searchText) || s.Mobile.CompareString(searchText) ||
                                                                  s.Email.CompareString(searchText) || s.Address.CompareString(searchText) ||
                                                                  s.CompanyName.CompareString(searchText) || s.Source.CompareString(searchText) ||
                                                                  s.ResponsiblePerson.CompareString(searchText));
                            break;


                        case 3:
                            list = FieldsListComplete.Where(s => s.ID.CompareString(searchText) ||
                                                                 s.Phone.CompareString(searchText) || s.Mobile.CompareString(searchText) ||
                                                                 s.Email.CompareString(searchText) || s.Address.CompareString(searchText) ||
                                                                 s.CompanyName.CompareString(searchText) || s.Contact.CompareString(searchText) ||
                                                                 s.ResponsiblePerson.CompareString(searchText)); break;


                        case 4:
                            list = FieldsListComplete.Where(s => s.Name.CompareString(searchText) ||
                                                                 s.Amount.CompareString(searchText) || s.Contact.CompareString(searchText) ||
                                                                 s.CompanyName.CompareString(searchText) || s.DealStage.CompareString(searchText) ||
                                                                 s.ResponsiblePerson.CompareString(searchText)); break;
                    }


                    FieldsList = new ObservableCollection<List>(list);// list.ForEach(s => FieldsList.Add(s));
                }
            }
            catch (Exception ex)
            {

            }



        }

        internal async void ExpandTapped(List model)
        {
        }

        internal async void TakeTapped(List model)
        {
            try
            {
                var loader = await ShowLoading();
                var result = await DetailsService.GetSingleFormDetails(model.rowId, StaticData.CurrentFormType);
               var fullName= StaticData.UserModel.first_name + " " + StaticData.UserModel.last_name;
               
                await loader.DismissAsync();

                if (result.status == 200)
                {
                 
                    foreach(var item in result.data.staticSections.fields)
                    {
                        if(item.title== "Responsible person")
                        {
                            item.value = new string[] { fullName };
                            break;
                        }
                    }

                     loader = await ShowLoading();
                    var postResult = await DetailsService.PostDetail(result.data);
                    await loader.DismissAsync();

                    if (postResult.status == 200 && !String.IsNullOrEmpty(result.data.id))
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new AddPage(model.rowId))
                        ;                    }
                    else
                    {
                        await ShowSnackbar(result.message);
                    }

                }
                else
                {
                    await ShowAlert(result.message);
                }

            }
            catch (Exception ex)
            {

            }
        }

        internal async void PhoneTapped(List model)
        {
            try
            {

                if (!String.IsNullOrEmpty(model.Phone))
                {
                    Xamarin.Essentials.PhoneDialer.Open(model.Phone);
                }
            }
            catch (Exception ex)
            {

            }

        }
        internal async void MobileTapped(List model)
        {
            try
            {

                if (!String.IsNullOrEmpty(model.Phone))
                {
                    Xamarin.Essentials.PhoneDialer.Open(model.Mobile);
                }
            }
            catch (Exception ex)
            {

            }

        }
        internal async void EmailTapped(List model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.Email))
                {
                    await Xamarin.Essentials.Email.ComposeAsync(new EmailMessage() { To = new List<string>() { model.Email } });

                }
            }
            catch (Exception ex)
            {

            }

        }

        internal async Task Initilize()
        {
            try
            {
                InitilizeCulture();

                if (FieldsList != null)
                {
                    FieldsList.Clear();
                }

                PageTitle = crmLng.SiteOrders;


                var result = await DetailsService.GetFormList(StaticData.CurrentFormType,1,1000,"133");
                if (result.status == 200)
                {
                    var list = result.data.list.OrderByDescending(s => Convert.ToDateTime(s.CreatedOn));
                    FieldsList = new ObservableCollection<List>(list);
                    FieldsListComplete = new ObservableCollection<List>(list);
                }
                else
                {
                    await ShowAlert(result.message);
                }
                SearchText = String.Empty;

            }
            catch (Exception ex)
            {
                FieldsList = new ObservableCollection<List>();
            }


        }
    }
}
