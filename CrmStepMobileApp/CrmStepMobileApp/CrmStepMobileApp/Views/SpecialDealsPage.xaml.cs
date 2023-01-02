using CrmStepMobileApp.CustomControls;
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
    public partial class SpecialDealsPage : ContentPage
    {
        SpecialDealsPageViewModel viewModel = App.Locator.SpecialDealsPageViewModel;

        public SpecialDealsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            App.SetPagePadding((Grid)Content);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Initilize();
            InitializeComponent();

            App.SetPagePadding((Grid)Content);
        }

        private async void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is CustomEntry customEntry)
            {
                await Task.Delay(1000);

                if (e.NewTextValue == customEntry.Text)
                {
                    viewModel.Search(e.NewTextValue);
                }
            }

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (e.SelectedItem is List list)
            {
                viewModel.EditImplementation(list.rowId);
            }
            listView.SelectedItem = null;
        }



        private void Take_Tapped(object sender, EventArgs e)
        {
            if (sender is CustomImage entry)
            {
                if (entry.BindingContext is List model)
                {
                    viewModel.TakeTapped(model);
                }
            }
        }
        private void LeadsPhone_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is List model)
                {
                    viewModel.PhoneTapped(model);
                }
            }
        }
        private void LeadsMMobile_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is List model)
                {
                    viewModel.MobileTapped(model);
                }
            }
        }
        private void LeadsEmail_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack.BindingContext is List model)
                {
                    viewModel.EmailTapped(model);
                }
            }
        }

        private void Expand_Tapped(object sender, EventArgs e)
        {
            if (sender is CustomImage img)
            {
                if (img.BindingContext is List model)
                {
                    if (model.ExtendedViewVisibility)
                    {
                        img.RotateTo(270);
                        model.ExtendedViewVisibility = false;
                    }
                    else
                    {
                        img.RotateTo(90);

                        model.ExtendedViewVisibility = true;
                    }
                    //viewModel.ExpandTapped(model);
                }
            }
        }
    }
}