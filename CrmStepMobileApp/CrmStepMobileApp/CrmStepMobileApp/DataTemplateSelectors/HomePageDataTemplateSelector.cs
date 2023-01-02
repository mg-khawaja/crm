using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.DataTemplateSelectors
{
  public  class HomePageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EnglishTemplate { get; set; }
        public DataTemplate HebrewTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null)
            {

                if (StaticData.SelectedLanguageModel.Id == 2)
                {
                    return HebrewTemplate;
                }
            }
            return EnglishTemplate;
        }
    }

    public class EventDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EnglishTemplate { get; set; }
        public DataTemplate HebrewTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null)
            {

                if (StaticData.SelectedLanguageModel.Id == 2)
                {
                    return HebrewTemplate;
                }
            }
            return EnglishTemplate;
        }
    }

    public class IncompleteEventsDataTemplateSelector
    {
    }

    public class BirthdaysDataTemplateSelector
    {
    }

    public class HotSalesDataTemplateSelector
    {
    }

    public class LeadsDataTemplateSelector
    {
    }

    public class DealsDataTemplateSelector
    {
    }


}
