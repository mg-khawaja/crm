using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.ViewModels
{
    public class AddDocumentPageViewModel : BaseViewModel
    {
        public bool IsUpdating { get; set; }

        private bool _IsNewDocumentAdded;
        public bool IsNewDocumentAdded
        {
            get => _IsNewDocumentAdded;
            set
            {
                _IsNewDocumentAdded = value;
                RaisePropertyChanged();
            }
        }
        public int DocumentId { get; set; }
        public string Id { get; set; }

        public List<DicModel> UsersList { get; set; }



        private ImageSource _Source;
        public ImageSource Source
        {
            get => _Source;
            set
            {
                _Source = value;
                RaisePropertyChanged();
            }
        }

        private DicModel _selectedUser;
        public DicModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                RaisePropertyChanged();
            }
        }
        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                _createdOn = value;
                RaisePropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private byte[] _byteArr;
        public byte[] ByteArr
        {
            get => _byteArr;
            set
            {
                _byteArr = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand SelectUserCommand => new RelayCommand(SelectUserClick);

        private async void SelectUserClick()
        {
            try
            {

                var list = UsersList.Select(s => s.Text).ToList();

                var result = await ShowActionSheet(crmLng.Select, list);
                if (result >= 0)
                {
                    SelectedUser = UsersList[result];
                }
            }
            catch (Exception ex)
            {

            }
        }

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);

        private async void SaveClick()
        {
            try
            {

                InitilizeCulture();

                string json ;

                if (IsUpdating)
                {

                    if (IsNewDocumentAdded)
                    {
                        var obj = new
                        {
                            document_id = DocumentId,
                            title = Title,
                            file = ByteArr,
                            file_name = FileName,
                            parent_id = Id,
                            parent_type = StaticData.CurrentFormType,
                            user_id = SelectedUser.Value
                        };

                        json = JsonConvert.SerializeObject(obj);
                    }
                    else
                    {
                        var obj = new
                        {
                            document_id = DocumentId,
                            title = Title,
                            file_name = FileName,
                            parent_id = Id,
                            parent_type = StaticData.CurrentFormType,
                            user_id = SelectedUser.Value
                        };

                        json = JsonConvert.SerializeObject(obj);
                    }

               
                   
                }
                else
                {
                    var obj = new
                    {
                        document_id = DocumentId,
                        title = Title,
                        file = ByteArr,
                        file_name = FileName,
                        parent_id = Id,
                        parent_type = StaticData.CurrentFormType,
                        user_id = SelectedUser.Value
                    };

                    json = JsonConvert.SerializeObject(obj);
                }

               
                var loader = await ShowLoading();
                var result = await DocumentsService.PostDocument(json);
                await loader.DismissAsync();

                if (result.is_success)
                {
                    await ShowSnackbar(crmLng.DocumentAddedSuccessully);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await ShowAlert(result.msg);
                }
            }
            catch (Exception ex)
            {

            }


        }
        public RelayCommand UploadCommand => new RelayCommand(UploadClick);

        private async void UploadClick()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                IsNewDocumentAdded = true;

                ByteArr = fileData.DataArray;
                FileName = fileData.FileName;
            }
            catch (Exception ex)
            {
            }


        }
        public RelayCommand TakeCommand => new RelayCommand(TakeClick);

        private async void TakeClick()
        {
            try
            {
                var result = await MediaService.TakePhoto();
                if (result != null)
                {
                    ByteArr = result.Item3;
                    FileName = result.Item2;
                }
                IsNewDocumentAdded = true;

            }
            catch (Exception ex)
            {

            }
        }
        internal async void Initilize(string id, Document document = null)
        {
            Id = id;
            DocumentId = 0;
            PageTitle = crmLng.Documents;// String.IsNullOrEmpty(id) ? crmLng.Documents : crmLng.edit;
           IsNewDocumentAdded = false;
            IsUpdating = false;

            Title = String.Empty;
            ByteArr = null;
            FileName = String.Empty;
            CreatedOn = DateTime.Now;
            SelectedUser = new DicModel();
            UsersList = new List<DicModel>();

            EngVisibility = HeVisibility = false;

            if (StaticData.SelectedLanguageModel.Id == 2)
            {
                EngVisibility = false;
                HeVisibility = true;
            }
            else
            {
                EngVisibility = true;
                HeVisibility = false;
            }

            var result = await DetailsService.GetBasicInfo("");
            if (result.status == 200)
            {
                UsersList = result.data.users.ToList();
                SelectedUser = UsersList.FirstOrDefault(s => s.Value == StaticData.UserModel.user_id);
            }


            if (document != null)
            {
                IsUpdating = true;
                DocumentId = document.document_id;
                Title = document.title;
                FileName = Path.GetFileName(document.file_name);
                SelectedUser = new DicModel() { Value = document.user_id, Text = document.user };
                //var bytes = await DocumentsService.DownloadDocument(document.document_id);
                //if (bytes != null)
                //{
                //    ByteArr = bytes;

                //}
            }

        }
    }
}
