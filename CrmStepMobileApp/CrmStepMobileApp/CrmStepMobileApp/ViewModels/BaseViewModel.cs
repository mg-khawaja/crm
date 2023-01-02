using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace CrmStepMobileApp.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {

        public LoginServices LoginServices = new LoginServices();
        public DetailsService DetailsService = new DetailsService();
        public MediaService MediaService = new MediaService();
        public HomeServices HomeServices = new HomeServices();
        public SwtichProjectServices SwitchProjectServices = new SwtichProjectServices();
        public NotificationService NotificationService = new NotificationService();
        public EventsServices EventsServices = new EventsServices();
        public DocumentsService DocumentsService = new DocumentsService();
        


            private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                RaisePropertyChanged();
            }
        }
        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                RaisePropertyChanged();
            }
        }

        private bool _engVisibility;
        public bool EngVisibility
        {
            get => _engVisibility;
            set
            {
                _engVisibility = value;
                RaisePropertyChanged();
            }
        }
        private bool _heVisibility;
        public bool HeVisibility
        {
            get => _heVisibility;
            set
            {
                _heVisibility = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand BackCommand => new RelayCommand(BackClick);

        private void BackClick()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }



        public async Task<IMaterialModalPage> ShowLoading(string message = "", Color? backgroundColor = null)
        {
            InitilizeCulture();

            var materialAlertDialog = new MaterialLoadingDialogConfiguration()
            {
                BackgroundColor = (backgroundColor == null) ? (Color)App.Current.Resources["DarkThemeColor"] : backgroundColor.Value,
                MessageTextColor = Color.White,
                TintColor = Color.White,
                CornerRadius = 10
            };
            var loader = await MaterialDialog.Instance.LoadingDialogAsync(message, materialAlertDialog);
            return loader;
        }


        public async Task ShowAlert(string message, string title = "", Color? backgroundColor = null)
        {
            InitilizeCulture();

            var materialAlertDialog = new MaterialAlertDialogConfiguration()
            {
                BackgroundColor = (backgroundColor == null) ? (Color)App.Current.Resources["DarkThemeColor"] : backgroundColor.Value,
                MessageTextColor = Color.White,
                TitleTextColor = Color.White,
                TintColor = Color.White,
                CornerRadius = 10
            };


            await MaterialDialog.Instance.AlertAsync(message, title, materialAlertDialog);

        }
        public async Task ShowSnackbar(string message, Color? backgroundColor = null)
        {
            InitilizeCulture();

            var materialAlertDialog = new MaterialSnackbarConfiguration()
            {
                BackgroundColor = (backgroundColor == null) ? (Color)App.Current.Resources["DarkThemeColor"] : backgroundColor.Value,
                MessageTextColor = Color.White,
           //     TitleTextColor = Color.White,
                TintColor = Color.White,
                CornerRadius = 10,

            };


            await MaterialDialog.Instance.SnackbarAsync(message,2750, materialAlertDialog);

        }

        public async Task<int> ShowActionSheet(string title, List<string> actions, Color? backgroundColor = null)
        {
            InitilizeCulture();

            var materialAlertDialog = new MaterialSimpleDialogConfiguration()
            {
                BackgroundColor = (backgroundColor == null) ? (Color)App.Current.Resources["DarkThemeColor"] : backgroundColor.Value,
                TextColor = Color.White,
                TitleTextColor = Color.White,
                TintColor = Color.White,
                CornerRadius=10
            };


            var result = await MaterialDialog.Instance.SelectActionAsync(title, actions, materialAlertDialog);



            return result;
        }

        public void InitilizeCulture()
        {
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(StaticData.SelectedLanguageModel.PluginName);

        }

        public async Task HideLoading(IMaterialModalPage loader)
        {

            await loader.DismissAsync();
        }
    }
}
