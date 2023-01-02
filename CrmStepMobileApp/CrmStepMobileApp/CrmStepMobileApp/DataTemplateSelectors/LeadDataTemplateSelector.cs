using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.DataTemplateSelectors
{
    public class LeadDataTemplateSelector : DataTemplateSelector
    {

        public bool IsHebrew { get; set; }

        public DataTemplate IDEntryDataTemplate { get; set; }
        public DataTemplate EntryDataTemplate { get; set; }
        public DataTemplate DateDataTemplate { get; set; }

        public DataTemplate SelectDataTemplate { get; set; }

        public DataTemplate MultiSelectDataTemplate { get; set; }

        public DataTemplate TextAreaDataTemplate { get; set; }

        public DataTemplate PhotoDataTemplate { get; set; }
        public DataTemplate AutoCompleteDataTemplate { get; set; }
        public DataTemplate NumberDataTemplate { get; set; }


        public DataTemplate IDEntryDataTemplateHe { get; set; }
        public DataTemplate EntryDataTemplateHe { get; set; }
        public DataTemplate DateDataTemplateHe { get; set; }

        public DataTemplate SelectDataTemplateHe { get; set; }

        public DataTemplate MultiSelectDataTemplateHe { get; set; }

        public DataTemplate TextAreaDataTemplateHe { get; set; }

        public DataTemplate PhotoDataTemplateHe { get; set; }
        public DataTemplate AutoCompleteDataTemplateHe { get; set; }
        public DataTemplate NumberDataTemplateHe { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            IsHebrew = StaticData.SelectedLanguageModel.Id == 2;

            if (item is DetailModel field)
            {
                switch (field.inputType)
                {
                    case 12:
                        if (field.type == "textarea")
                        {
                            return IsHebrew ? TextAreaDataTemplateHe : TextAreaDataTemplate;
                        }
                        else if (field.type == "fileupload")
                        {
                            return IsHebrew ? PhotoDataTemplateHe : PhotoDataTemplate;
                        }
                        else
                        {
                            if(field.title == "ID")
                            {
                                return IsHebrew ? IDEntryDataTemplateHe : IDEntryDataTemplate;
                            }
                            else
                            {
                                return IsHebrew ? EntryDataTemplateHe : EntryDataTemplate;
                            }
                        }
                    case 5:
                        return IsHebrew ? DateDataTemplateHe : DateDataTemplate;
                    case 3:
                        return IsHebrew ? DateDataTemplateHe : DateDataTemplate;
                    case 6:
                        return IsHebrew ? SelectDataTemplateHe : SelectDataTemplate;
                    case 8:
                        return IsHebrew ? MultiSelectDataTemplateHe : MultiSelectDataTemplate;
                    case 0:
                        return IsHebrew ? AutoCompleteDataTemplateHe : AutoCompleteDataTemplate;
                    case 9:
                        return IsHebrew?NumberDataTemplateHe:  NumberDataTemplate;


                }
            }
            return EntryDataTemplate;
        }
    }
}
