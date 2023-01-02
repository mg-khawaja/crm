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
    public partial class NotificationPage : ContentPage
    {
     

        NotificationPageViewModel viewModel = App.Locator.NotificationPageViewModel;
        public NotificationPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
                NavigationPage.SetHasNavigationBar(this, false);
                viewModel.Initilize();
                App.SetPagePadding((Grid)Content);
            }
            catch(Exception ex)
            {

            }

           

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (sender is Switch switchContol)
            {
                if (switchContol.BindingContext is EventNotifications eventTimeline)
                {
                    viewModel.EventChanged(eventTimeline._Id, eventTimeline.IsCompleted);
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is CustomCardView customCardView)
            {
                if (customCardView.BindingContext is EventNotifications eventTimeline)
                {
                    viewModel.NavigateToEvents(eventTimeline);
                }
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (sender is CustomCardView customCardView)
            {
                if (customCardView.BindingContext is Notifications notifications)
                {
                    viewModel.NavigateToLeads(notifications);
                }
            }
        }
    }
}