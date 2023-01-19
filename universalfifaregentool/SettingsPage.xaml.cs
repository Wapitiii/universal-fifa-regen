using System;
using System.Collections.Generic;
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

namespace universalfifaregentool
{
    /// <summary>
    /// Interaktionslogik für SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void TaskbarStateComboBox_OnSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox comboBox)
            {
                var parentWindow = System.Windows.Window.GetWindow(this);

                if (parentWindow == null)
                    return;

                var selectedIndex = comboBox.SelectedIndex;

                switch (selectedIndex)
                {
                    case 1:
                        Wpf.Ui.Appearance.Theme.Apply(
                          Wpf.Ui.Appearance.ThemeType.Dark,     // Theme type
                          Wpf.Ui.Appearance.BackgroundType.Mica, // Background type
                          true                                   // Whether to change accents automatically
                        );
                        break;

                    case 2:
                        Wpf.Ui.Appearance.Theme.Apply(
                          Wpf.Ui.Appearance.ThemeType.Light,     // Theme type
                          Wpf.Ui.Appearance.BackgroundType.Mica, // Background type
                          true                                   // Whether to change accents automatically
                        );
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
