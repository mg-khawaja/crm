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
    public partial class HomePage : ContentPage
    {
        HomePageViewModel viewModel = App.Locator.HomePageViewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize();
            App.SetPagePadding((Grid)Content);
            //MyCheckBox.Choices = new List<string>()
            //{
            //    "asdsad",
            //    "asdsad",
            //    "sssss",
            //    "aaaaa"
            //};

            //MyCheckBox.SelectedColor = Color.Blue;
            //MyCheckBox.UnselectedColor = Color.Red;

        }

        private void Menu_Click(object sender, EventArgs e)
        {
            App.MenuPage.IsPresented = !App.MenuPage.IsPresented;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (viewModel.PageLoaded)
            {
                var switchControl = (Switch)sender;
                if (switchControl.BindingContext is EventsHomeModel eventsHomeModel)
                {
                    viewModel.ToggleClick(eventsHomeModel);
                }
            }
           
        }
        private void Switch_Toggled2(object sender, ToggledEventArgs e)
        {
            if (viewModel.PageLoaded)
            {
                var switchControl = (Switch)sender;
                if (switchControl.BindingContext is EventsHomeModel eventsHomeModel)
                {
                    viewModel.ToggleClickIncomplete(eventsHomeModel);
                }
            }
          
        }

        private async void Events_Tapped(object sender, EventArgs e)
        {
            if(sender is StackLayout stack)
            {
                if(stack.BindingContext is EventsHomeModel model)
                {
                    await viewModel.NavigateToEvents(model);
                }
            }
        }
        private async void HotSales_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is HotSaleHomeModel model)
                {
                    await viewModel.NavigateToHotSales(model);
                }
            }
        }
        private async void Deal_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is DealHomeModel model)
                {
                    await viewModel.NavigateToDeals(model);
                }
            }
        }
        private async void Leads_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is FormsDetailModel model)
                {
                    await viewModel.NavigateToLeads(model);
                }
            }
        }
        private async void Birthdays_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is BirthdayHomeModel model)
                {
                    await viewModel.NavigateToContacts(model);
                }
            }
        }
        
    }
}