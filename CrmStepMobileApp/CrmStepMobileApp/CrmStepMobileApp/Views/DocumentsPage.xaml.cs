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
    public partial class DocumentsPage : ContentPage
    {
        public string Id { get; set; }
        DocumentsPageViewModel viewModel = App.Locator.DocumentsPageViewModel;
        public DocumentsPage(string id)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Id = id;
            NavigationPage.SetHasNavigationBar(this, false);
            App.SetPagePadding((Grid)Content);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initilize(Id);

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listview =(ListView) sender;
            if (listview.SelectedItem == null) return;

            listview.SelectedItem = null;
        }

        private void View_Tapped(object sender, EventArgs e)
        {
            if(sender is CustomImage customImage)
            {
                if(customImage.BindingContext is Document document)
                {
                    viewModel.ViewTapped(document);
                }
            }
        }
        private void Delete_Tapped(object sender, EventArgs e)
        {
            if (sender is CustomImage customImage)
            {
                if (customImage.BindingContext is Document document)
                {
                    viewModel.DeleteTapped(document);
                }
            }
        }
        private void Edit_Tapped(object sender, EventArgs e)
        {
            if (sender is CustomImage customImage)
            {
                if (customImage.BindingContext is Document document)
                {
                    viewModel.EditTapped(document);
                }
            }
        }
    }
}