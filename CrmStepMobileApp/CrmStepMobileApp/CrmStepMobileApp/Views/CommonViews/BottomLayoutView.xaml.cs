using CrmStepMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Views.CommonViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomLayoutView : ContentView
    {
        BottomLayoutViewViewModel viewModel = App.Locator.BottomLayoutViewViewModel;
        public BottomLayoutView()
        {
            viewModel.InitilizeCulture();
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initlize();
          
        }
    }
}