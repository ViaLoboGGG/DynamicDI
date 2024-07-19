using System.Collections.Specialized;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
namespace DIPrototype
{
    public class ConfigSettings
    {
        public NameValueCollection? appSettings;
        public static string ContentRoot;
        public static string FileUploadLocation;
        public ConfigSettings()
        {
            ReadAllSettings();
            if (appSettings["ContentRoot"] == null) AddUpdateAppSettings("ContentRoot", "D:\\TPB\\");
            if (appSettings["DBFile"] == null) AddUpdateAppSettings("DBFile", "D:\\TPB\\ParaChat.db");
            if (appSettings["UseURL"] == null) AddUpdateAppSettings("UseURL", "http://127.0.0.1:5005/");
            ReadAllSettings();
            ContentRoot = appSettings["ContentRoot"];
        }

        void ReadAllSettings()
        {
            try
            {
                appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        void ReadSetting(string key)
        {
            try
            {
                appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
