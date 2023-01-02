using CrmStepMobileApp.CustomControls;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.ViewModels;
using CrmStepMobileApp.Views.PopupView;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        EventsPageViewModel viewModel = App.Locator.EventsPageViewModel;

        CalendarPopupView StartsOnPopup;
        CalendarPopupView EndsOnPopup;

        public EventsPage(string id="",DateTime? dateTime=null, SearchModel searchModel = null)
        {
            viewModel.LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize(id,dateTime, searchModel);
            App.SetPagePadding((Grid)Content);

            StartsOnPopup = new CalendarPopupView(new List<DateTime>() { viewModel.StartOn });
            EndsOnPopup = new CalendarPopupView(new List<DateTime>() { viewModel.EndsOn });


            StartsOnPopup.Disappearing+=(s,e) =>
            {
                StartOnTP.Time = new TimeSpan(viewModel.StartOn.Hour, viewModel.StartOn.Minute, viewModel.StartOn.Second);

                StartOnTP.Focus();
                var vmDate = StartsOnPopup.viewModel.SelectedDate;

                var date = new DateTime(vmDate.Year, vmDate.Month, vmDate.Day, StartOnTP.Time.Hours, StartOnTP.Time.Minutes, StartOnTP.Time.Seconds);
                viewModel.StartOn = date;
            };
           
            StartOnTP.Unfocused += (s, e) =>
            {

                var date = new DateTime(viewModel.StartOn.Year, viewModel.StartOn.Month, viewModel.StartOn.Day, StartOnTP.Time.Hours,StartOnTP.Time.Minutes,StartOnTP.Time.Seconds);
                viewModel.StartOn = date;
                viewModel.EndsOn = date.AddMinutes(15);
            };



            EndsOnPopup.Disappearing += (s, e) =>
            {
                EndsOnTP.Time = new TimeSpan(viewModel.EndsOn.Hour, viewModel.EndsOn.Minute, viewModel.EndsOn.Second);

                EndsOnTP.Focus();

                var vmDate = StartsOnPopup.viewModel.SelectedDate;

                var date = new DateTime(vmDate.Year, vmDate.Month, vmDate.Day, EndsOnTP.Time.Hours, EndsOnTP.Time.Minutes, EndsOnTP.Time.Seconds);
                viewModel.EndsOn = date;
            };
          
            EndsOnTP.Unfocused += (s, e) =>
            {
                var date = new DateTime(viewModel.EndsOn.Year, viewModel.EndsOn.Month, viewModel.EndsOn.Day, EndsOnTP.Time.Hours, EndsOnTP.Time.Minutes, EndsOnTP.Time.Seconds);
                viewModel.EndsOn = date;
            };


            //hebreew

   

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.clearList();
        }

        private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //if (viewModel.IsItemSelected) return;

                //if (sender is CustomEntry customEntry)
                //{
                //    await Task.Delay(500);
                //    if (e.NewTextValue == customEntry.Text)
                //    {
                //        await viewModel.SearchImplementation(customEntry.Text);
                //    }

                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private async void SearchItem_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsItemSelected) return;

                if (sender is CustomCardView cardView)
                {
                    if (cardView.BindingContext is SearchModel field)
                    {
                        viewModel.IsItemSelected = true;
                        viewModel.ItemSelected(field);
                        viewModel.IsItemSelected = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void StartOn_Tapped(object sender, EventArgs e)
        {
            //StartOnDP.Date = viewModel.StartOn ;
            // StartOnDP.Focus();

            try
            {
                StartsOnPopup.viewModel.SelectedDate = viewModel.StartOn;
                await PopupNavigation.Instance.PushAsync(StartsOnPopup);
            }
            catch(Exception ex)
            {

            }

         
        }

        private async void EndsOn_Tapped(object sender, EventArgs e)
        {
            try
            {
                EndsOnPopup.viewModel.SelectedDate = viewModel.EndsOn;
                await PopupNavigation.Instance.PushAsync(EndsOnPopup);
            }
            catch (Exception ex)
            {

            }
           
        }

        private void CustomEditor_Focused(object sender, FocusEventArgs e)
        {
            MainScroll.Padding = new Thickness(0, 0, 0, 300);
            MainScroll1.Padding = new Thickness(0, 0, 0, 300);
        }

        private void CustomEditor_Unfocused(object sender, FocusEventArgs e)
        {
            MainScroll.Padding = new Thickness(0);
            MainScroll1.Padding = new Thickness(0);
        }
    }
}