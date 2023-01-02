using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using CrmStepMobileApp.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Id { get; set; }
        //  public Dictionary<string,string> Dictionaries { get; set; }

        private FormsDetailApiModel _leadsApiModel;
        public FormsDetailApiModel LeadsApiModel
        {
            get => _leadsApiModel;
            set
            {
                _leadsApiModel = value;
                RaisePropertyChanged();
            }
        }

        private double _moreWidth;
        public double MoreWidth
        {
            get => _moreWidth;
            set
            {
                _moreWidth = value;
                RaisePropertyChanged();
            }
        }

        private bool _showSubmit;
        public bool ShowSubmit
        {
            get => _showSubmit;
            set
            {
                _showSubmit = value;
                RaisePropertyChanged();
            }
        }
        private bool _idEnabled;
        public bool IdEnabled
        {
            get => _idEnabled;
            set
            {
                _idEnabled = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<DetailSectionModel> _sectionsList;
        public ObservableCollection<DetailSectionModel> SectionsList
        {
            get => _sectionsList;
            set
            {
                _sectionsList = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand => new RelayCommand(SaveClick);

        private async void SaveClick()
        {
            try
            {
                LeadsApiModel.data.sections = new List<Section>();
                foreach (var sectionItem in SectionsList)
                {

                    try
                    {
                        var section = new Section();
                        section.fields = new List<Field>();

                        foreach (var item in sectionItem.fields)
                        {

                            try
                            {
                                var model = new Field();


                                if (item.inputType == 8)
                                {
                                    var multiList = item.MultiSelectedchoices.Where(s => s.IsChecked).Select(s => s.Name);
                                    model.value = multiList.ToArray();
                                }
                                else if (item.inputType == 6)
                                {
                                    if (String.IsNullOrEmpty(item.TextValue))
                                    {
                                        model.value = new string[0] { };
                                    }
                                    else
                                    {
                                        model.value = new string[1] { item.TextValue };
                                    }
                                }
                                else if (item.inputType == 5 || item.inputType == 3)
                                {
                                    if (!String.IsNullOrEmpty(item.TextValue))
                                    {
                                        model.value = new string[1] { Convert.ToDateTime(item.TextValueDateTime).GetDateTimeFormat() };
                                    }
                                }
                                else if (item.inputType == 12)
                                {

                                    if (item.type == "fileupload")
                                    {
                                        //model.ImageUrl = item.value.Any() ? item.value[0] : "";
                                        //model.TextValue = item.value.Any() ? item.value[0] : "";
                                        model.value = item.value;

                                        if (!String.IsNullOrEmpty(item.FileBytesStr))
                                        {
                                            var obj = new FileDataObj()
                                            {
                                                name = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png",
                                                data = item.FileBytesStr
                                            };
                                            // var jsonObj = JsonConvert.SerializeObject(obj);

                                            var list = new List<FileDataObj>();
                                            list.Add(obj);
                                            model.fileData = list;
                                        }
                                    }
                                    else
                                    {
                                        if (String.IsNullOrEmpty(item.TextValue))
                                        {
                                            model.value = new string[0] { };
                                        }
                                        else
                                        {
                                            model.value = new string[1] { item.TextValue };
                                        }
                                    }

                                }
                                else if (item.inputType == 0)
                                {
                                    if (item.SelectedSearchModel == null)
                                    {
                                        model.value = new string[0] { };
                                    }
                                    else
                                    {
                                        model.value = new string[2] { item.SelectedSearchModel.Name, item.SelectedSearchModel.RowId };
                                    }
                                }
                                else if (item.inputType == 9)
                                {
                                    if (String.IsNullOrEmpty(item.TextValue))
                                    {
                                        model.value = new string[0] { };
                                    }
                                    else
                                    {
                                        model.value = new string[1] { item.TextValue };

                                        if (item.type == "number" && item.title == "Amount")
                                        {
                                            //   Convert.ToDouble(string.Format("{0:F2}", model.value)).ToString("N");

                                            double.Parse(item.TextValue, NumberStyles.Currency);

                                            model.value = new string[1] { Convert.ToString(double.Parse(item.TextValue, NumberStyles.Currency)) };
                                        }

                                    }

                                }

                                if (model.fileData == null)
                                {
                                    model.fileData = item.fileData;
                                }

                                model.choices = item.choices;
                                model.choiseLoadFrom = item.choiseLoadFrom;
                                model.controlId = item.controlId;
                                model.dataType = item.dataType;
                                model.id = item.id;
                                model.inputType = item.inputType;
                                model.instructions = item.instructions;
                                model.isrequired = item.isrequired;
                                model.preLoadFiles = item.preLoadFiles;
                                model.title = item.title;
                                model.type = item.type;
                                model.url = item.url;






                                section.fields.Add(model);
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        section.id = sectionItem.id;
                        section.sectionTitle = sectionItem.sectionTitle;
                        if (sectionItem.IsStatic)
                        {
                            LeadsApiModel.data.staticSections = section;
                            //LeadsApiModel.data.staticSections.fields = list.ToArray();
                        }
                        else
                        {
                            LeadsApiModel.data.sections.Add(section);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }


                var json = JsonConvert.SerializeObject(LeadsApiModel.data);
                var loader = await ShowLoading();
                var result = await DetailsService.PostDetail(LeadsApiModel.data);
                await loader.DismissAsync();

                if (result.status == 200)
                {
                    await ShowSnackbar(crmLng.RecordAddedSuccess);
                    if (!String.IsNullOrEmpty(result.data.id))
                    {
                        await Initilize(result.data.id, ShowSubmit);
                    }
                }
                else
                {
                    await ShowSnackbar(result.message);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public RelayCommand MoreCommand => new RelayCommand(MoreClick);

        private async void MoreClick()
        {

            try
            {
                InitilizeCulture();
                var list = new List<string>() { crmLng.Events, crmLng.Documents };

                var result = await ShowActionSheet(crmLng.Select, list);
                if (result >= 0)
                {
                    if (result == 0)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new EventTimeLinePage(Id, Name));
                    }
                    else if (result == 1)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new DocumentsPage(Id));

                    }
                }
            }
            catch (Exception ex)
            {

            }


        }

        internal async Task FillSections(Section section, bool isStatic)
        {
            var detailSectionModel = new DetailSectionModel();
            detailSectionModel.sectionTitle = section.sectionTitle;
            detailSectionModel.translatedTitle = section.sectionTitle;
            detailSectionModel.id = section.id;
            detailSectionModel.fields = new ObservableCollection<DetailModel>();
            detailSectionModel.IsStatic = isStatic;
            foreach (var item in section.fields)
            {
                var model = new DetailModel();
                model.MultiSelectedchoices = new ObservableCollection<MultiSelectModel>();


                if (item.inputType == 8)
                {
                    foreach (var item2 in item.choices)
                    {
                        var multiSelectModel = new MultiSelectModel();
                        multiSelectModel.Name = item2;
                        model.MultiSelectedchoices.Add(multiSelectModel);
                    }

                }
                model.value = item.value;
                model.choices = item.choices;
                model.choiseLoadFrom = item.choiseLoadFrom;
                model.controlId = item.controlId;
                model.dataType = item.dataType;
                model.fileData = item.fileData;
                model.id = item.id;
                model.inputType = item.inputType;
                model.instructions = item.instructions;
                model.isrequired = item.isrequired;
                model.preLoadFiles = item.preLoadFiles;
                model.title = item.title;//.GetValFromDic(Dictionaries);
                model.translatedTitle = item.translatedTitle;//.GetValFromDic(Dictionaries);
                model.type = item.type;
                model.url = item.url;



                if (item.inputType == 8)
                {
                    if (item.value.Any())
                    {
                        foreach (var multiSelectItem in item.value)
                        {
                            var xx = model.MultiSelectedchoices.FirstOrDefault(s => s.Name == multiSelectItem);
                            if (xx != null)
                            {
                                xx.IsChecked = true;// .Where(s => s.IsChecked).Select(s => s.Name);
                            }
                        }
                    }

                }
                else if (item.inputType == 6)
                {
                    if (isStatic)
                    {
                        model.TextValue = item.value.Any() ? item.value[0] : item.choices[0];

                    }
                    else
                    {
                        model.TextValue = item.value.Any() ? item.value[0] : "";

                    }
                }
                else if (item.inputType == 5 || item.inputType == 3)
                {
                    if (item.title == "Date of birth" && StaticData.CurrentFormType == 2)
                    {
                        if (!String.IsNullOrEmpty(Id))
                        {
                            model.TextValue = item.value.Any() ? Convert.ToDateTime(item.value[0]).ToString("yyyy-MM-dd") : "";
                            if (item.value.Any())
                            {
                                model.TextValue = Convert.ToDateTime(item.value[0]).ToString("yyyy-MM-dd");
                                model.TextValueDateTime = Convert.ToDateTime(item.value[0]);
                            }
                        }
                    }
                    else
                    {
                        model.TextValue = item.value.Any() ? Convert.ToDateTime(item.value[0]).ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                        model.TextValueDateTime = item.value.Any() ? Convert.ToDateTime(item.value[0]) : DateTime.Now;
                    }
                    //    model.TextValueDateTime= item.value.Any() ? Convert.ToDateTime(item.value[0]) : DateTime.Now;
                }
                else if (item.inputType == 12)
                {



                    if (item.type == "fileupload")
                    {
                        model.ImageUrl = item.value.Any() ? item.value[0] : "";
                        model.TextValue = item.value.Any() ? item.value[0] : "";
                    }
                    else
                    {
                        model.TextValue = item.value.Any() ? item.value[0] : "";
                    }

                }
                else if (item.inputType == 0)
                {
                    model.SelectedSearchModel = new SearchModel();
                    if (item.value.Any())
                    {
                        model.SelectedSearchModel.Name = item.value[0];
                        model.TextValue = item.value[0];
                        if (item.value.Length == 2)
                        {
                            model.SelectedSearchModel.RowId = item.value[1];
                        }
                    }

                }
                else if (item.inputType == 9)
                {
                    model.TextValue = item.value.Any() ? System.Convert.ToDouble(string.Format("{0:F2}", item.value[0])).ToString("N") : "";
                    // model.TextValue = String.Format("{0:n0}", string.Format("{0:F2}", number)) ;
                }


                if (StaticData.CurrentFormType == (int)FormType.Deals)
                {
                    if (model.title == "Name")
                    {
                        if (!String.IsNullOrEmpty(StaticData.Name))
                        {
                            model.TextValue = StaticData.Name;
                            StaticData.Name = String.Empty;
                        }
                    }
                    if (model.title == "Amount")
                    {
                        if (!String.IsNullOrEmpty(StaticData.Amount))
                        {
                            model.TextValue = StaticData.Amount;
                            StaticData.Amount = String.Empty;

                        }
                    }

                }

                detailSectionModel.fields.Add(model);
            }
            SectionsList.Add(detailSectionModel);

        }

        internal async Task Initilize(string id = null, bool showSubmit = true)
        {
            ShowSubmit = showSubmit;
            Id = id;
            InitilizeCulture();
            if (SectionsList != null)
            {
                SectionsList.Clear();
            }


            var page = Enum.GetName(typeof(FormType), StaticData.CurrentFormType);
            PageTitle = crmLng.ResourceManager.GetString(page);

            if(StaticData.CurrentFormType == 5)
                IdEnabled = false;
            else
                IdEnabled = true;

            var loader = await ShowLoading();

            try
            {
                FormsDetailApiModel result;
                if (!String.IsNullOrEmpty(id))
                {
                    MoreWidth = 30d;
                    result = await DetailsService.GetSingleFormDetails(id, StaticData.CurrentFormType);
                    //  Dictionaries = result.data.dictionary.GetPropNames();
                }
                else
                {
                    MoreWidth = 0.1d;
                    result = await DetailsService.GetFormDetails(StaticData.CurrentFormType);
                    // Dictionaries = result.data.dictionary.GetPropNames();
                }

                if (result.status == 200)
                {
                    LeadsApiModel = result;
                    SectionsList = new ObservableCollection<DetailSectionModel>();
                    await FillSections(LeadsApiModel.data.staticSections, true);
                    foreach (var sectionModel in LeadsApiModel.data.sections)
                    {
                        await FillSections(sectionModel, false);
                    }


                    // for editing documents/events page
                    var section = SectionsList.FirstOrDefault(s => s.IsStatic);
                    switch (StaticData.CurrentFormType)
                    {
                        case 1:
                            var first = section.fields.FirstOrDefault(s => s.title == "First name");
                            var last = section.fields.FirstOrDefault(s => s.title == "Last name");
                            Name = $"{first.TextValue} {last.TextValue}";
                            break;

                        case 2:
                            var first1 = section.fields.FirstOrDefault(s => s.title == "First name");
                            var last1 = section.fields.FirstOrDefault(s => s.title == "Last name");
                            Name = $"{first1.TextValue} {last1.TextValue}";
                            break;

                        case 3:
                            var companyName = section.fields.FirstOrDefault(s => s.title == "Company Name");
                            Name = $"{companyName.TextValue}";
                            break;

                        case 4:
                            var name = section.fields.FirstOrDefault(s => s.title == "Name");
                            Name = $"{name.TextValue}";
                            break;
                        case 5:
                            var name1 = section.fields.FirstOrDefault(s => s.title == "Name");
                            Name = $"{name1.TextValue}";
                            break;
                    }
                }
                else
                {
                    await ShowAlert(result.message);
                }
            }
            catch (Exception ex)
            {

            }
            await loader.DismissAsync();
        }
    }
}
