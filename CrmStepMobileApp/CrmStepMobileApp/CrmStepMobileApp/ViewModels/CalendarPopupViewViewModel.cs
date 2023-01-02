using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.ViewModels
{
    public class CalendarPopupViewViewModel : BaseViewModel
    {
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate; set
            {

                _selectedDate = value;
                RaisePropertyChanged();
            }
        }
        private DateTime _moveToDate;
        public DateTime MoveTODate
        {
            get => _moveToDate; set
            {

                _moveToDate = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand DateSelectedCommand => new RelayCommand(DateSelectedClick);

        public async void DateSelectedClick()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        internal void Initilize(List<DateTime> selectedDate=null)
        {
            try
            {
                InitilizeCulture();


                if (selectedDate == null)
                {
                    SelectedDate = DateTime.MinValue;
                    MoveTODate = DateTime.Now;
                    return;

                }

                //if (SelectedDate == DateTime.MinValue)
                //{
                //    SelectedDate = selectedDate[0];
                //}
                SelectedDate = selectedDate[0];

                MoveTODate = SelectedDate;

                //if (SelectedDate.Date != DateTime.Now.Date)
                //{
                //    MoveTODate = SelectedDate;
                //}

            }
            catch (Exception ex)
            {

            }

           

        }
    }
}
