using CrmStepMobileApp.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrmStepMobileApp.CustomControls
{
    public class CustomLabel : Label
    {
        public CustomLabel()
        {
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    HorizontalTextAlignment = TextAlignment.End;
            //}


            if (StaticData.SelectedLanguageModel.Id == 2)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}
