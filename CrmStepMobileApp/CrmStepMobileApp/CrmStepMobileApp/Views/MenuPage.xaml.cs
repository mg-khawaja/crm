using CrmStepMobileApp.Models;
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
    public partial class MenuPage : MasterDetailPage
    {
        MenuPageVIewModel viewModel = App.Locator.MenuPageVIewModel;
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            Detail = new NavigationPage(new HomePage());
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize();
            App.MenuPage = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            var model = (StackLayout)sender;
            if (model.BindingContext is MenuModel menuModel)
            {
                viewModel.Navigate(menuModel);

             

            }
        }
    }
}