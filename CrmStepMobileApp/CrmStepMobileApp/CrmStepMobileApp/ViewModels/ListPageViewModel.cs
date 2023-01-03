using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
    public class ListPageViewModel : BaseViewModel
    {
        public int Page = 1;
        private int _count = 10;
        public int TotalFetchedReults = 0;
        private bool _isFetching;
        public bool IsFetching
        {
            get => _isFetching;
            set
            {
                _isFetching = value;
                RaisePropertyChanged();
            }
        }
        private double _itemSize;
        public double ItemSize
        {
            get => _itemSize;
            set
            {
                _itemSize = value;
                RaisePropertyChanged();
            }
        }
        private Syncfusion.ListView.XForms.LoadMoreOption _loadMore;
        public Syncfusion.ListView.XForms.LoadMoreOption LoadMore
        {
            get => _loadMore;
            set
            {
                _loadMore = value;
                RaisePropertyChanged();
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;
                }
                else
                {
                    LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
                }
                _searchText = value;
                RaisePropertyChanged();
            }
        }
        private string _search = "";

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

        private bool _isLoadMoreVisible { get; set; }
        public bool IsLoadMoreVisible
        {
            get => _isLoadMoreVisible;
            set
            {
                _isLoadMoreVisible = value;
                RaisePropertyChanged();
            }
        }

        private List<List> _fieldsList { get; set; }
        public List<List> FieldsList
        {
            get => _fieldsList;
            set
            {
                _fieldsList = value;
                RaisePropertyChanged();
            }
        }

        private List<List> _fieldsListComplete { get; set; }
        public List<List> FieldsListComplete
        {
            get => _fieldsListComplete;
            set
            {
                _fieldsListComplete = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ClearTextCommand => new RelayCommand(ClearTextClick);
        private void ClearTextClick()
        {
            if (_search == "")
            {
                SearchText = String.Empty;
                return;
            }
            SearchText = String.Empty;
            //LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;

            Page = 1;
            TotalFetchedReults = 0;
            StaticData.TotalExpectedReults = -1;
            _search = "";
            if (FieldsList != null)
            {
                FieldsList.Clear();
            }
            FieldsList = new List<List>();
            FieldsListComplete = new List<List>();
            fetchData();
            //LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;
        }
        public RelayCommand SearchCommand => new RelayCommand(SearchFromAPI);
        private void SearchFromAPI()
        {
            if ((_search == "" && SearchText == "") || _search == SearchText)
            {
                return;
            }
            //LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
            Page = 1;
            TotalFetchedReults = 0;
            StaticData.TotalExpectedReults = -1;
            _search = SearchText;
            if (FieldsList != null)
            {
                FieldsList.Clear();
            }
            FieldsList = new List<List>();
            FieldsListComplete = new List<List>();
            fetchData();
            //LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;
        }

        public RelayCommand LoadMoreItemsCommand => new RelayCommand(fetchData, CanLoadMoreItems);
        private bool CanLoadMoreItems()
        {
            if (StaticData.TotalExpectedReults == TotalFetchedReults)
                return false;
            return true;
        }
        private async void fetchData()
        {
            if (IsFetching == true)
                return;
            IsFetching = true;
            try
            {
                var result = await DetailsService.GetFormList(StaticData.CurrentFormType, Page, _count, search: _search);
                if (result.status == 200)
                {
                    FormsListApiModel = result;
                    var list = result.data.list.OrderByDescending(s => Convert.ToDateTime(s.CreatedOn));
                    var fList = new List<List>();
                    foreach (var item in FieldsList)
                    {
                        fList.Add(item);
                    }
                    foreach (var item in list)
                    {
                        fList.Add(item);
                        FieldsListComplete.Add(item);
                    }
                    FieldsList = fList;
                    FieldsListComplete = fList;
                    Page++;
                    TotalFetchedReults += list.Count();
                    if (StaticData.TotalExpectedReults == TotalFetchedReults)
                        IsLoadMoreVisible = false;
                    else
                        IsLoadMoreVisible = true;
                }
                else
                {
                    await ShowAlert(result.message);
                }
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string> {
                    { "Username", Settings.Username },
                    { "Form Type", Enum.GetName(typeof(FormType), StaticData.CurrentFormType) }
                  };
                Crashes.TrackError(e, properties);
                await ShowAlert("An error occured, Please try again later!");
            }
            IsFetching = false;
        }

        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(null));
        }
        internal async void EditImplementation(string id)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(id));
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


                    FieldsList = list.ToList();// list.ForEach(s => FieldsList.Add(s));
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

        internal void Initilize()
        {
            try
            {
                InitilizeCulture();

                if (FieldsList != null)
                {
                    FieldsList.Clear();
                }

                var page = Enum.GetName(typeof(FormType), StaticData.CurrentFormType);
                PageTitle = crmLng.ResourceManager.GetString(page);
                FieldsList = new List<List>();
                FieldsListComplete = new List<List>();

                SearchText = String.Empty;

                LoadMoreItemsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                FieldsList = new List<List>();
            }


        }

    }
}
