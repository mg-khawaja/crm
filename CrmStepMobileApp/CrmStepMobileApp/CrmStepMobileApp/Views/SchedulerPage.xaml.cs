using CrmStepMobileApp.Helper;
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
    public partial class SchedulerPage : ContentPage
    {
        bool OnGoingDragging = false;
        SchedulerPageViewModel viewModel = App.Locator.SchedulerPageViewModel;
        public SchedulerPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize();
            App.SetPagePadding((Grid)Content);
            DayViewSettings dayViewSettings = new DayViewSettings();
            DayLabelSettings dayLabelSettings = new DayLabelSettings();
            dayLabelSettings.TimeFormat = "HH:mm";
            dayViewSettings.DayLabelSettings = dayLabelSettings;
            schedule.DayViewSettings = dayViewSettings;

            WeekViewSettings weekViewSettings = new WeekViewSettings();
            WeekLabelSettings weekLabelSettings = new WeekLabelSettings();
            weekLabelSettings.TimeFormat = "HH:mm";
            weekViewSettings.WeekLabelSettings = weekLabelSettings;
   

            schedule.WeekViewSettings = weekViewSettings;

            WorkWeekViewSettings workweekViewSettings = new WorkWeekViewSettings();
            WorkWeekLabelSettings workWeekLabelSettings = new WorkWeekLabelSettings();
            workWeekLabelSettings.TimeFormat = "HH:mm";
            workweekViewSettings.WorkWeekLabelSettings = workWeekLabelSettings;
            schedule.WorkWeekViewSettings = workweekViewSettings;

            //schedule.DragDropSettings = new DragDropSettings();
            //schedule.dra

            schedule.VisibleDatesChangedEvent += (s, e) =>
            {
                viewModel.VisibleDatesChangedClick(e.visibleDates);
            };

            App.SfSchedule = schedule;


            StartDatePicker.DateSelected += (s, e) =>
              {
                  viewModel.StartDate = e.NewDate;
              };
            EndDatePicker.DateSelected += (s, e) =>
            {

                viewModel.EndDate = e.NewDate;
            };

            schedule.AppointmentDrop +=async (s, e) =>
            {
                var app = (ScheduleAppointment)e.Appointment;
                if (!String.IsNullOrEmpty(app.RecurrenceRule))
                {
                    if (OnGoingDragging)
                    {
                        return;
                    }
                    OnGoingDragging = true;
                    e.Cancel = true;
                    await App.Current.MainPage.Navigation.PushAsync(new EventsPage(app.Notes));
                    await Task.Delay(1000);
                    OnGoingDragging = false;
                }
                else
                {
                    viewModel.UpdateEventOnDrag(app.Notes,e.DropTime);
                }
            };

           schedule.Locale = StaticData.SelectedLanguageModel.PluginName;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.RefreshNeeded)
            {
                await viewModel.InitilizeEvents(viewModel.RememberDates);
                viewModel.RefreshNeeded = false;
            }

        }


        private void Forward(object sender, EventArgs e)
        {
            schedule.Forward();
        }
        private void Backward(object sender, EventArgs e)
        {
            schedule.Backward();
        }
        private async void SelectCalendar(object sender, EventArgs e)
        {

            try
            {
                var calendarView = new CalendarPopupView(viewModel.SelectedSchedulerDate);
                calendarView.Disappearing += async (s, ee) =>
                {
                    schedule.NavigateTo(App.Locator.CalendarPopupViewViewModel.SelectedDate);
                    viewModel.SetCalendarDatesStr(viewModel.SelectedSchedulerDate, viewModel.SchedulerViewTypeModel);
                    await viewModel.InitilizeEvents(viewModel.SelectedSchedulerDate);

                };

                await PopupNavigation.Instance.PushAsync(calendarView);
            }
            catch(Exception ex)
            {

            }

            
        }

        private async void schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            try
            {
                if ((int)schedule.ScheduleView == (int)ScheduleView.MonthView)
                {
                    if (e.Appointments != null && e.Appointments.Any())
                    {

                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new EventsPage("", e.Datetime));

                    }
                }
                else
                {

                    if (e.Appointment != null)
                    {
                        var app = (ScheduleAppointment)e.Appointment;
                        await App.Current.MainPage.Navigation.PushAsync(new EventsPage(app.Notes));
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new EventsPage("", e.Datetime));
                    }

                }
            }
            catch (Exception ex)
            {

            }
           

        }
        private async void schedule_MonthInlineAppointmentTapped(object sender, MonthInlineAppointmentTappedEventArgs e)
        {
            try
            {
                if (e.Appointment != null)
                {
                    var app = (ScheduleAppointment)e.Appointment;
                    await App.Current.MainPage.Navigation.PushAsync(new EventsPage(app.Notes));
                }
                else
                {
                    //  await App.Current.MainPage.Navigation.PushAsync(new EventsPage("", e.Datetime));
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void StartDateTapped(object sender, EventArgs e)
        {
            try
            {
                StartDatePicker.Focus();

            }
            catch (Exception ex)
            {

            }
        }
        private void EndDateTapped(object sender, EventArgs e)
        {
            try
            {
                EndDatePicker.Focus();

            }
            catch (Exception ex)
            {

            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private async void Filter_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.FilterVisibility)
                {
                    viewModel.FilterVisibility = false;
                    viewModel.SchedulerVisibility = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        FilterImage.Rotation = 270;

                        //  await FilterImage.RotateTo(270);

                    });
                }
                else
                {
                    viewModel.FilterVisibility = true;
                    viewModel.SchedulerVisibility = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        FilterImage.Rotation = 90;

                        //  await FilterImage.RotateTo(90);

                    });

                }
            }
            catch (Exception ex)
            {

            }
           
        }

        private void CustomButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.FilterVisibility = false;
                viewModel.SchedulerVisibility = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await FilterImage.RotateTo(270);

                });
            }
            catch (Exception ex)
            {

            }
           
        }

       
    }
}