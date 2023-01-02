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

namespace CrmStepMobileApp.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyPopupView : PopupPage
    {
        DailyPopupViewViewModel viewModel = App.Locator.DailyPopupViewViewModel;
        public DailyPopupView(RecurrenceProperties recurrenceProperties = null)
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