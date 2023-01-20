using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            initConfig();
        }

        private void initConfig()
        {
            string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + $"\\Universal FIFA Regenerator";

            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
                File.Create(configPath + $"\\config.ini").Close();
            }

            Console.WriteLine(configPath);
        }
    }
}
