using CrmStepMobileApp.CustomControls;
using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        ListPageViewModel viewModel = App.Locator.ListPageViewModel;
        public ListPage()
        {

            switch (StaticData.CurrentFormType)
            {
                case 1:
                    viewModel.ItemSize = 400;
                    break;
                case 2:
                    viewModel.ItemSize = 332;
                    break;
                case 3:
                    viewModel.ItemSize = 332;
                    break;
                case 4:
                    viewModel.ItemSize = 400;
                    break;
                case 5:
                    viewModel.ItemSize = 280;
                    break;
                default:
                    viewModel.ItemSize = 400;
                    break;
            }
            InitializeComponent();
            
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            App.SetPagePadding((Grid)Content);


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SearchText = "";
            viewModel.LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.None;
            viewModel.Page = 1;
            viewModel.TotalFetchedReults = 0;
            StaticData.TotalExpectedReults = -1;
            viewModel.Initilize();
            viewModel.LoadMore = Syncfusion.ListView.XForms.LoadMoreOption.Auto;

            App.SetPagePadding((Grid)Content);
        }

        private async void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Page = 1;
            viewModel.TotalFetchedReults = 0;
            StaticData.TotalExpectedReults = -1;

            if (viewModel.FieldsList != null)
            {
                viewModel.FieldsList.Clear();
            }

            viewModel.FieldsList = new ObservableCollection<List>();
            viewModel.FieldsListComplete = new ObservableCollection<List>();

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

        private void LeadsPhone_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if(stack.BindingContext is List model)
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

        private void ListView_ItemSelected(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            if (stack.BindingContext is List list)
            {
                viewModel.EditImplementation(list.rowId);
            }
        }

        private void resultsList_Scrolled(object sender, ScrolledEventArgs e)
        {
            //if (e.ScrollY + ScrollViewEvent.Height >= ScrollViewEvent.ContentSize.Height)
            //    AtEnd = true;
            //else
            //    AtEnd = false;
        }

        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var listView = (Syncfusion.ListView.XForms.SfListView)sender;
            if (e.ItemData is List list)
            {
                viewModel.EditImplementation(list.rowId);
            }
            listView.SelectedItem = null;
        }

        private void Search_Tapped(object sender, EventArgs e)
        {
            viewModel.Page = 1;
            viewModel.TotalFetchedReults = 0;
            StaticData.TotalExpectedReults = -1;

            if (viewModel.FieldsList != null)
            {
                viewModel.FieldsList.Clear();
            }

            viewModel.FieldsList = new ObservableCollection<List>();
            viewModel.FieldsListComplete = new ObservableCollection<List>();
        }
    }
}