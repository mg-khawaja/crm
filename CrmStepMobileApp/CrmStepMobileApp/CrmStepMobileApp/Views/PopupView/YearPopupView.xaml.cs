using CrmStepMobileApp.CustomControls;
using CrmStepMobileApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Views.PopupView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YearPopupView : PopupPage
    {
        YearPopupViewViewModel viewModel = App.Locator.YearPopupViewViewModel;
        public YearPopupView(RecurrenceProperties recurrenceProperties = null)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.Initilize(recurrenceProperties);
            UntileDatePicker.Unfocused += (s, e) =>
            {
                viewModel.UntileDate = UntileDatePicker.Date;
            };
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.CreateReccurenceProperties();
        }


        private async void UntilDatePicker_Tapped(object sender, EventArgs e)
        {
            UntileDatePicker.Focus();
        }

        private void first_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.Value)
            {
                viewModel.SecondIsChecked = false;
            }
        }

        private void second_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.Value)
            {
                viewModel.FirstIsChecked = false;
            }
        }

        private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var entry = (CustomEntry)sender;
                var day = Convert.ToInt32(entry.Text);
                if (day > 31)
                {
                    viewModel.DayOfMonth = 31;
                }
            }
            catch (Exception ex)
            {
                viewModel.DayOfMonth = 31;

            }

        }
    }
}