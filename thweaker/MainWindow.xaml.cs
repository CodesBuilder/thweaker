using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace thweaker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public void SettingsComponentInit()
        {
            selectLanguageBox.Items.Clear();
            foreach (var i in Lang.GetLanguageList())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = Lang.GetLocalizedString(i, "lang.this");
                item.Tag = i;

                selectLanguageBox.Items.Add(item);

                if (Lang.ActiveLang == i)
                    selectLanguageBox.SelectedItem = item;
            }
        }

        public void ThdatComponentInit()
        {
            switch(App.appDataContext.ThdatMode)
            {
                case "C":
                case "c":
                    break;
                default:
                    MessageBox.Show(App.appDataContext.ConfigErrorText, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.appDataContext;

            SettingsComponentInit();
        }

        private void selectLanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lang.ActiveLang = (string)((ComboBoxItem)e.AddedItems[0]).Tag;
            App.appDataContext.UpdateLocalizedTexts();
        }

        private void thdatModeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
