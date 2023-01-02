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
    public partial class EventTimeLinePage : ContentPage
    {
        string _id;
        string _name;
        EventTimeLinePageViewModel viewModel = App.Locator.EventTimeLinePageViewModel;
        public EventTimeLinePage(string id,string name)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _id = id;
            _name = name;
            NavigationPage.SetHasNavigationBar(this, false);
            App.SetPagePadding((Grid)Content);

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initilize(_id, _name);

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        { var listview = (ListView)sender;


            if (listview.SelectedItem == null) return;
            viewModel.ItemSelected(e.SelectedItemIndex);
            listview.SelectedItem = null;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(sender is Switch switchContol)
            {
                if(switchContol.BindingContext is EventTimeline eventTimeline)
                {
                    //eventTimeline.is_completed = !eventTimeline.is_completed;
                    viewModel.EventChanged(eventTimeline.id,eventTimeline.is_completed);
                }
            }
        }
    }
}