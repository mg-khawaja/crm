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
    public partial class SwitchProjectView : PopupPage
    {
        SwitchProjectViewViewModel viewModel = App.Locator.SwitchProjectViewViewModel;
        public SwitchProjectView()
        {
            InitializeComponent();


            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}