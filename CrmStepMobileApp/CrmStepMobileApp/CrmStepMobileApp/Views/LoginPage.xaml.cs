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
    public partial class LoginPage : ContentPage
    {

        LoginPageViewModel viewModel = App.Locator.LoginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel.Initilize();

#if DEBUG
            viewModel.Username = "info@crmstep.com";
            viewModel.Password = "1wsaq2";

            //viewModel.Username = "bakay.igor@gmail.com";
            //viewModel.Password = "1q2w3e";

            //viewModel.Username = "sales@crmstep.com";
            //viewModel.Password = "1q2w3e";

            //viewModel.Username = "oleg.litvinenko75@gmail.com";
            //viewModel.Password = "ביגמן";
#endif
        }
    }
}