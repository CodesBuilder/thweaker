using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace thweaker
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public class AppDataContext : INotifyPropertyChanged
        {
            // Localized texts.
            private string settingsText;
            public string SettingsText
            {
                get => settingsText;
                set
                {
                    settingsText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(settingsText)));
                }
            }
            public string languageText;
            public string LanguageText
            {
                get => languageText;
                set
                {
                    languageText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(languageText)));
                }
            }

            private string thtkToolchainPathText;
            public string ThtkToolchainPathText
            {
                get => thtkToolchainPathText;
                set
                {
                    thtkToolchainPathText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(thtkToolchainPathText)));
                }
            }

            private string targetGameVersionText;
            public string TargetGameVersionText
            {
                get => targetGameVersionText;
                set
                {
                    targetGameVersionText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(targetGameVersionText)));
                }
            }

            // Configurations.
            private string thtkToolchainPath;

            public string ThtkToolchainPath
            {
                get => thtkToolchainPath;
                set
                {
                    thtkToolchainPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(thtkToolchainPath)));
                    Config.writeItem("thtkToolchainPath", value);
                }
            }

            private string targetGameVersion;
            public string TargetGameVersion
            {
                get => targetGameVersion;
                set
                {
                    targetGameVersion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(targetGameVersion)));
                    Config.writeItem("targetGameVersion", value);
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void UpdateLocalizedTexts()
            {
                SettingsText = Lang.GetLocalizedString(Lang.ActiveLang, "lang.tab.settings");
                LanguageText = Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.language");
                ThtkToolchainPathText = Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.thtkToolchainPath");
                TargetGameVersionText = Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.targetGameVersion");
            }

            public AppDataContext()
            {
                Lang.ActiveLang = Config.getItem("language", "en-us");

                ThtkToolchainPath = Config.getItem("thtkToolchainPath", ".\\thtk");
                TargetGameVersion = Config.getItem("targetGameVersion", "06");

                UpdateLocalizedTexts();
            }
        }

        public static AppDataContext appDataContext;

        public App()
        {
            Lang.Init();

            appDataContext = new AppDataContext();
        }
    }
}
