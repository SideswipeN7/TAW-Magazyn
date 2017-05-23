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
    /// Interaction logic for InProgress.xaml
    /// </summary>
    public partial class InProgress : Window

    {
        private static InProgress _instance;

        public static InProgress GetInstance()
        {
            if (_instance == null)
                _instance = new InProgress();
            return _instance;          
        }

        public InProgress()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
    }
}
