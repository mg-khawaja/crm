using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrmStepMobileApp.Helper
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string UsernameKey = "UsernameKey";
        private static readonly string UsernameDefault = string.Empty;

        private const string PasswordKey = "PasswordKey";
        private static readonly string PasswordDefault = string.Empty;


        private const string SelectedLangKey = "SelectedLangKey";
        private static readonly int SelectedLangDefault = 0;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsernameKey, value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PasswordKey, value);
            }
        }
        public static int SelectedLang
        {
            get
            {
                return AppSettings.GetValueOrDefault(SelectedLangKey, SelectedLangDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SelectedLangKey, value);
            }
        }
        


    }
}
