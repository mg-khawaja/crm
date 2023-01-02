using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace CrmStepMobileApp.ViewModels
{
    public class SwitchProjectViewViewModel : BaseViewModel
    {
        private BasicList _selectedCompany;
        public BasicList SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                RaisePropertyChanged();
            }
        }
        private BasicList _selectedProject;
        public BasicList SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                RaisePropertyChanged();
            }
        }



        public List<BasicList> ProjectsList{ get; set; }
        public List<BasicList> CompaniesList { get; set; }
        public List<string> CompaniesListStr { get; set; }





        public RelayCommand ProjectCommand => new RelayCommand(ProjectClick);

        private async void ProjectClick()
        {
            try
            {
                if (!ProjectsList.Any()) return;

                var index = await ShowActionSheet(crmLng.Select, ProjectsList.Select(s=>s.name).ToList());
                if (index >= 0)
                {
                    SelectedProject = ProjectsList[index];


                }
            }
            catch(Exception ex)
            {

            }

          
        }
        public RelayCommand SwitchCommand => new RelayCommand(SwitchClick);

        private async void SwitchClick()
        {
            try
            {

                if (SelectedProject!=null && !String.IsNullOrEmpty(SelectedProject.name))
                {
                    var loader = await ShowLoading();
                    var model = await SwitchProjectServices.SwitchProject(SelectedProject.id);
                    await loader.DismissAsync();

                    if (model.status == 200)
                    {
                         loader = await ShowLoading();
                        var result = await LoginServices.Login(StaticData.SelectedLanguageModel.ApiName, StaticData.UTCMins, StaticData.DeviceId, Settings.Username, Settings.Password, DeviceInfo.Platform.ToString());
                        await loader.DismissAsync();
                        if (result.status == 200)
                        {
                         
                            StaticData.UserModel = result.data.user;
                            StaticData.SettingsModel = result.data.settings;
                            StaticData.AccessToken = result.data.token;
                          
                            
                            App.Locator.HomePageViewModel.Initilize();
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            await ShowAlert(result.message);
                        }
                     

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public RelayCommand CancelCommand => new RelayCommand(CancelClick);

        private async void CancelClick()
        {
            try
            {

                await PopupNavigation.Instance.PopAsync();

            }
            catch (Exception ex)
            {

            }
        }


        public RelayCommand CompanyCommand => new RelayCommand(CompanyClick);

        private async void CompanyClick()
        {
            try
            {
                var strList = CompaniesList.Select(s => s.name).ToList();
                var index = await ShowActionSheet(crmLng.Select, strList);
                if (index >= 0)
                {
                    SelectedCompany = CompaniesList[index];
                    SelectedProject = new BasicList();
                    ProjectsList = new List<BasicList>();

                    var result = await SwitchProjectServices.GetByCompany(SelectedCompany.id);
                    if (result.data != null && result.data.list != null)
                    {
                        ProjectsList = result.data.list;
                        SelectedProject = ProjectsList[0];
                    }
                }
            }
            catch (Exception ex)
            {

            }
          
        }

        internal async void Initilize()
        {
            try
            {
                var loader = await ShowLoading();

                if (ProjectsList == null)
                {
                    ProjectsList = new List<BasicList>();
                    CompaniesList = new List<BasicList>();
                    var basicListResult = await SwitchProjectServices.GetBasicList();
                    if (basicListResult.data != null && basicListResult.data.list != null)
                    {
                        CompaniesList = basicListResult.data.list;
                        CompaniesListStr = CompaniesList.Select(s => s.name).ToList();


                        SelectedCompany = CompaniesList[0];
                        var result = await SwitchProjectServices.GetByCompany(SelectedCompany.id);
                        if (result.data != null && result.data.list != null)
                        {
                            ProjectsList = result.data.list;

                          var  tempProject = ProjectsList.FirstOrDefault(s => s.id == StaticData.UserModel.last_project_id);
                            SelectedProject = tempProject!=null?tempProject: ProjectsList[0];
                        }

                    }
                }

                await loader.DismissAsync();
            }
            catch (Exception ex)
            {
            }

        }
    }
}
