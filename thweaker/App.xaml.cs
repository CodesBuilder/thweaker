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
        private class LocalizedText : Attribute
        {
        }
        public class AppDataContext : INotifyPropertyChanged
        {
            // Localized texts.
            [LocalizedText]
            public string SettingsText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.tab.settings");

            [LocalizedText]
            public string LanguageText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.language");

            [LocalizedText]
            public string ThtkToolchainPathText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.thtkToolchainPath");

            [LocalizedText]
            public string TargetGameVersionText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.settings.targetGameVersion");

            [LocalizedText]
            public string ModeText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.mode");

            [LocalizedText]
            public string ModeCreateText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.modes.create");

            [LocalizedText]
            public string ModeListText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.modes.list");

            [LocalizedText]
            public string ModeExtractText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.modes.extract");

            [LocalizedText]
            public string FilePathText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.filePath");

            [LocalizedText]
            public string ConfigErrorText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.errors.config");

            [LocalizedText]
            public string RunText => Lang.GetLocalizedString(Lang.ActiveLang, "lang.general.run");

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

            private string thdatFilePath;
            public string ThdatFilePath
            {
                get => thtkToolchainPath;
                set
                {
                    thdatFilePath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(thdatFilePath)));
                    Config.writeItem("thdatFilePath", value);
                }
            }

            private string thdatMode;
            public string ThdatMode
            {
                get => thtkToolchainPath;
                set
                {
                    thdatMode = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(thdatMode)));
                    Config.writeItem("thdatMode", value);
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void UpdateLocalizedTexts()
            {
                var members = this.GetType().GetMembers();
                foreach (var i in members)
                    foreach(var j in i.GetCustomAttributes(true))
                        if(j is LocalizedText)
                        {
                            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(i.Name));
                            break;
                        }
            }

            public AppDataContext()
            {
                ThtkToolchainPath = Config.getItem("thtkToolchainPath", ".\\thtk");
                TargetGameVersion = Config.getItem("targetGameVersion", "06");
                ThdatFilePath = Config.getItem("thdatFilePath", ".\\th15.dat");
                ThdatMode = Config.getItem("thdatMode", "c");

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
