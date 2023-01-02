using CrmStepMobileApp.ViewModels;
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
    public partial class DashboardPage : ContentPage
    {
        DashboardPageViewModel viewModel = App.Locator.DashboardPageViewModel;
        public DashboardPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch(Exception e)
            {

            }
        }


        protected override bool OnBackButtonPressed()
        {

            return false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            viewModel.Initilize();

            //InitializeComponent();
            App.IsRootPage = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.IsRootPage = false;
        }
    }
}