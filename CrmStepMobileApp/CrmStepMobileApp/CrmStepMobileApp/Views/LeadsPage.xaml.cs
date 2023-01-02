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
    public partial class LeadsPage : ContentPage
    {
        LeadsPageViewModel viewModel = App.Locator.LeadsPageViewModel;
        public LeadsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
          await  viewModel.Initilize();
            InitializeComponent();
        }

        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Search(e.NewTextValue);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if(e.SelectedItem is List list)
            {
                viewModel.EditImplementation(list);
            }
            listView.SelectedItem = null;
        }
    }
}