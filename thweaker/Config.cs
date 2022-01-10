using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace thweaker
{
    public class Config
    {
        public static string getItem(string key, string fallback)
        {
            string value = ConfigurationManager.AppSettings[key] ?? fallback;
            return value;
        }

        public static void writeItem(string key, string value)
        {
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (configFile.AppSettings.Settings[key] == null)
                configFile.AppSettings.Settings.Add(key, value);
            else
                configFile.AppSettings.Settings[key].Value = value;

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
