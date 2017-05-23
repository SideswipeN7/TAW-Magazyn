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
using System.Windows.Shapes;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private static About _instance;

        public static About GetInstance()
        {
            if (_instance == null)
                _instance = new About();
            return _instance;
        }


        public About()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            SetLabel();
        }
        private void SetLabel()
        {
            LblAboutOpis.Content = $"Aplikacja do zarządzania sklepem:{Environment.NewLine} Wykonywanie transakcji{Environment.NewLine} Zarządzanie transakcjami{Environment.NewLine} Zarządzanie stanem magazynu{Environment.NewLine} Zarządzanie pracownikami";
        }

        private void BtnAboutOk_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
