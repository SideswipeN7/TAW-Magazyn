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
    /// Logika interakcji dla klasy Creators.xaml
    /// </summary>
    public partial class Creators : Window
    {
        public Creators()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void bt_creators_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
