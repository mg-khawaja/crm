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
    public partial class WeeklyPopupView : PopupPage
    {
        WeeklyPopupViewViewModel viewModel = App.Locator.WeeklyPopupViewViewModel;
        public WeeklyPopupView(RecurrenceProperties recurrenceProperties = null)
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
    }
}