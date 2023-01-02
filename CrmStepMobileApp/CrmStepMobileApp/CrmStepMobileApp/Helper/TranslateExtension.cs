using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmStepMobileApp.Helper
{
    
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "CrmStepMobileApp.Helper.crmLng";

        static readonly Lazy<ResourceManager> resmgr =
            new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var ci = CrossMultilingual.Current.CurrentCultureInfo;

            var translation = resmgr.Value.GetString(Text, ci);

            if (translation == null)
            {

#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER  
#endif
            }
            return translation;
        }


    }

//    public static class TranslateExtensionText
//    {
//        const string ResourceId = "CrmStepMobileApp.Helper.crmLng";

//        static readonly Lazy<ResourceManager> resmgr =
//            new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));
//        public static string GetTranslation(this string text)
//        {
//            if (text == null)
//                return "";

//            var ci = CrossMultilingual.Current.CurrentCultureInfo;

//            var translation = resmgr.Value.GetString(text, ci);

//            if (translation == null)
//            {

//#if DEBUG
//                throw new ArgumentException(
//                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", text, ResourceId, ci.Name),
//                    "Text");
//#else
//                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER  
//#endif
//            }
//            return translation;
//        }

//    }
}
