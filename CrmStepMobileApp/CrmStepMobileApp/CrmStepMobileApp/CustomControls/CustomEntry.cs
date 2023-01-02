using CrmStepMobileApp.Helper;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace CrmStepMobileApp.CustomControls
{
  public  class CustomEntry : MaterialTextField
    {
        public CustomEntry()
        {

            TextFontSize = 16;
            TintColor = (Color)App.Current.Resources["ThemeColor"];
            FloatingPlaceholderEnabled = false;
            HeightRequest = 40;
            BackgroundColor = Color.FromHex("#F3F6FB");
            AlwaysShowUnderline = true;
            UnderlineColor = (Color)App.Current.Resources["ThemeColor"];
            PlaceholderColor = Color.Transparent;

            Focused += (s, e) =>
            {
                //this.PlaceholderColor = Color.Transparent;
                //FloatingPlaceholderEnabled = false;
               var sender= (CustomEntry)s;
                sender.Text = Text;
            };

            if (StaticData.SelectedLanguageModel.Id == 2)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            PlaceholderColor = Color.Transparent;

        }
    }
}
