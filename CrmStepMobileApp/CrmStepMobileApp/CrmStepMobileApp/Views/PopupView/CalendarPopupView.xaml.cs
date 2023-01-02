using CrmStepMobileApp.Helper;
using CrmStepMobileApp.ViewModels;
using Rg.Plugins.Popup.Pages;
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
    public partial class CalendarPopupView : PopupPage
    {
      public  CalendarPopupViewViewModel viewModel = App.Locator.CalendarPopupViewViewModel;
        public CalendarPopupView(List<DateTime> dateTime=null)
        {
            InitializeComponent();

            calendar.Locale = new System.Globalization.CultureInfo(StaticData.SelectedLanguageModel.ApiName);
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize(dateTime);


         //   calendar.SelectedDate = dateTime;
        }

        protected override bool OnBackgroundClicked()
        {
            viewModel.DateSelectedClick();
            return base.OnBackgroundClicked();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel.DateSelectedClick();
        }
    }
}