using CrmStepMobileApp.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.DataTemplateSelectors
{
    public class EventTimeLineDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate EnglishDataTemplate { get; set; }
        public DataTemplate HebrewDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (StaticData.SelectedLanguageModel.Id == 2)
            {
                return HebrewDataTemplate;
            }
            else
            {
                return EnglishDataTemplate;

            }


            //switch (StaticData.CurrentFormType)
            //{
            //    case 1:
            //        if (StaticData.SelectedLanguageModel.Id == 2)
            //        {
            //            return HebrewDataTemplate;
            //        }
            //        else
            //        {
            //            return EnglishDataTemplate;

            //        }

            //    case 2:
            //        if (StaticData.SelectedLanguageModel.Id == 2)
            //        {
            //            return HebrewDataTemplate;
            //        }
            //        else
            //        {
            //            return ContactsDataTemplate;

            //        }

            //    case 3:
            //        if (StaticData.SelectedLanguageModel.Id == 2)
            //        {
            //            return HeCompanyDataTemplate;
            //        }
            //        else
            //        {
            //            return CompanyDataTemplate;

            //        }

            //    case 4:
            //        if (StaticData.SelectedLanguageModel.Id == 2)
            //        {
            //            return HeDealsDataTemplate;
            //        }
            //        else
            //        {
            //            return DealsDataTemplate;

            //        }

            //    default:
            //        return LeadDataTemplate;
            //}
        }
    }
}
