using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
    public class EventTimeLinePageViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        private ObservableCollection<EventTimeline> _list;
        public ObservableCollection<EventTimeline> List
        {
            get => _list;
            set
            {
                _list = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            var searchModel = new SearchModel();
            searchModel.RowId = Id;
            searchModel.FormType = StaticData.CurrentFormType;
            searchModel.Name = Name;

            await App.Current.MainPage.Navigation.PushAsync(new EventsPage("", DateTime.Now, searchModel));

        }

        internal async void ItemSelected(int selectedItemIndex)
        {
            var selectedItem = List[selectedItemIndex];
            await App.Current.MainPage.Navigation.PushAsync(new EventsPage(selectedItem.id));

        }

        internal async void EventChanged(string id, bool is_completed)
        {
            try
            {
                //  var loader = await ShowLoading();
                await DetailsService.UpdateStatus(id, is_completed);
                //  await loader.DismissAsync();
            }
            catch (Exception ex)
            {

            }


        }
        internal async void Initilize(string id, string name)
        {
            try
            {
                Id = id;
                Name = name;
                InitilizeCulture();
                PageTitle = crmLng.Events;
                List = new ObservableCollection<EventTimeline>();


                var loader = await ShowLoading();
                var result = await EventsServices.GetEventTimeLineList(id, StaticData.CurrentFormType);
                await loader.DismissAsync();

                if (result.status == 200 && result.data != null && result.data.list != null)
                {
                    var infoResult = await DetailsService.GetBasicInfo("");
                    if (infoResult.status == 200 && infoResult.data != null && infoResult.data.actions != null)
                    {
                        for (int i = 0; i < result.data.list.Length; i++)
                        {
                            result.data.list[i].action_text = infoResult.data.actions.FirstOrDefault(s => s.Value == result.data.list[i].action).Text;
                        }
                    }
                    List = new ObservableCollection<EventTimeline>(result.data.list);
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
