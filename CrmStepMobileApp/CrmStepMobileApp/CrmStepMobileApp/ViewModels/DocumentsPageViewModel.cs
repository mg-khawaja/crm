using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace CrmStepMobileApp.ViewModels
{
    public class DocumentsPageViewModel : BaseViewModel
    {

        public string Id { get; set; }
        private ObservableCollection<Document> _list;
        public ObservableCollection<Document> List
        {
            get => _list;
            set
            {
                _list = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand AddCommand => new RelayCommand(AddClick);

        private async void AddClick()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddDocumentPage(Id));
        }


        internal async void DeleteTapped(Document document)
        {
            var loader = await ShowLoading();

            var result = await DocumentsService.DeleteDocumnet(document.document_id);
            if (result.is_success)
            {
                List.Remove(document);
            }

            await loader.DismissAsync();
        }

        internal async void EditTapped(Document document)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddDocumentPage(Id, document));
        }

        internal async void ViewTapped(Document document)
        {

            try
            {
                var supportsUri = await Launcher.CanOpenAsync(document.file_name);
                if (supportsUri)
                    await Launcher.OpenAsync(document.file_name);
            }
            catch (Exception ex)
            {
                await ShowAlert(document.file_name + "          " + ex.Message);
            }

        }

        internal async void Initilize(string id)
        {
            try
            {
                Id = id;

                InitilizeCulture();
                PageTitle = crmLng.Documents;
                List = new ObservableCollection<Document>();


                var loader = await ShowLoading();
                var result = await DocumentsService.GetListByParent(id, StaticData.CurrentFormType, 1, 1000);
                await loader.DismissAsync();

                if (result.status == 200 && result.data != null && result.data.result != null)
                {

                    result.data.result.ToList().ForEach(s => s.FileName = Path.GetFileName(s.file_name));

                    List = new ObservableCollection<Document>(result.data.result);
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
