using Microsoft.Win32;
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
using Wpf.Ui.Controls;
using Ookii.Dialogs.Wpf;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Wpf.Ui.Common;
using FifaLibrary;
using System.Media;

namespace universalfifaregentool
{
    /// <summary>
    /// Interaktionslogik für DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        private FifaFat m_FatFile;

        public DashboardPage()
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
                        Wpf.Ui.TaskBar.TaskBarProgress.SetValue(
                            parentWindow,
                            Wpf.Ui.TaskBar.TaskBarProgressState.Normal,
                            80);
                        break;

                    case 2:
                        Wpf.Ui.TaskBar.TaskBarProgress.SetValue(
                            parentWindow,
                            Wpf.Ui.TaskBar.TaskBarProgressState.Error,
                            80);
                        break;

                    case 3:
                        Wpf.Ui.TaskBar.TaskBarProgress.SetValue(
                            parentWindow,
                            Wpf.Ui.TaskBar.TaskBarProgressState.Paused,
                            80);
                        break;

                    case 4:
                        Wpf.Ui.TaskBar.TaskBarProgress.SetValue(
                            parentWindow,
                            Wpf.Ui.TaskBar.TaskBarProgressState.Indeterminate,
                            80);
                        break;

                    default:
                        Wpf.Ui.TaskBar.TaskBarProgress.SetState(parentWindow, Wpf.Ui.TaskBar.TaskBarProgressState.None);
                        break;
                }
            }
        }

        private void browseGameFolder(object sender, RoutedEventArgs e)
        {
            string gameDir;

            gameDir = ShowFolderBrowserDialog();

            gameDirectory.Text = gameDir;
        }

        private void editConfig(string gameDir)
        {

        }

        private void execRegen()
        {
            if (gameDirectory.Text.Length == 0)
            {
                statusText.Text = "Status : Enter a valid path before continuing!";
                SystemSounds.Exclamation.Play();
            }
            else
            {
                CreateFat(gameDirectory.Text);
                RegenerateFatBh();
                SystemSounds.Hand.Play();
            }
        }
        private string ShowFolderBrowserDialog()
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.


            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                //Wpf.Ui.Controls.MessageBox.Show();
                System.Windows.MessageBox.Show("File browsing is not supported, how are you even running it on XP?");
            }

            dialog.ShowDialog();

            return dialog.SelectedPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            execRegen();
        }

        private void CreateFat(string rootPath)
        {
            statusText.Text = "Status : Creating FatFile...";
            this.m_FatFile = FifaFat.Create(rootPath);
            FifaFat fatFile = this.m_FatFile;
            statusText.Text = "Status : Created FatFile successfully!";
        }
        private void RegenerateFatBh()
        {
            if (this.m_FatFile != null)
            {
                statusText.Text = "Status : Regeneratring BH...";
                this.m_FatFile.RegenerateAllBh(true);
                statusText.Text = "Status : Done!";
            }
        }
    }
}
