using System.Windows;

namespace Client.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy Creators.xaml
    /// </summary>
    public partial class Creators : Window
    {
        private static Creators _instance;

        public static Creators GetInstance()
        {
            if (_instance == null)
                _instance = new Creators();
            return _instance;
        }

        public Creators()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void bt_creators_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}