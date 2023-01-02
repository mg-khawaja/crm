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
    public partial class AddDocumentPage : ContentPage
    {
        AddDocumentPageViewModel viewModel = App.Locator.AddDocumentPageViewModel;
        public AddDocumentPage(string id, Document document=null)
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize(id, document);
            App.SetPagePadding((Grid)Content);
        }
    }
}