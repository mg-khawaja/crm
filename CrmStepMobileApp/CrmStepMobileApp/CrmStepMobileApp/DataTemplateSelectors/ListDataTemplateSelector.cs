using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp
{
    public class ListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeadDataTemplate { get; set; }
        public DataTemplate ContactsDataTemplate { get; set; }

        public DataTemplate CompanyDataTemplate { get; set; }

        public DataTemplate DealsDataTemplate { get; set; }
        public DataTemplate TicketsDataTemplate { get; set; }
        public DataTemplate HeLeadDataTemplate { get; set; }
        public DataTemplate HeContactsDataTemplate { get; set; }

        public DataTemplate HeCompanyDataTemplate { get; set; }

        public DataTemplate HeDealsDataTemplate { get; set; }
        public DataTemplate HeTicketsDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (StaticData.CurrentFormType)
            {
                case 1:
                    if (StaticData.SelectedLanguageModel.Id == 2)
                    {
                        return HeLeadDataTemplate;
                    }
                    else
                    {
                        return LeadDataTemplate;

                    }

                case 2:
                    if (StaticData.SelectedLanguageModel.Id == 2)
                    {
                        return HeContactsDataTemplate;
                    }
                    else
                    {
                        return ContactsDataTemplate;

                    }

                case 3:
                    if (StaticData.SelectedLanguageModel.Id == 2)
                    {
                        return HeCompanyDataTemplate;
                    }
                    else
                    {
                        return CompanyDataTemplate;

                    }

                case 4:
                    if (StaticData.SelectedLanguageModel.Id == 2)
                    {
                        return HeDealsDataTemplate;
                    }
                    else
                    {
                        return DealsDataTemplate;

                    }
                case 5:
                    if (StaticData.SelectedLanguageModel.Id == 2)
                    {
                        return HeTicketsDataTemplate;
                    }
                    else
                    {
                        return TicketsDataTemplate;

                    }
                default:
                    return LeadDataTemplate;
            }
        }
    }
}
