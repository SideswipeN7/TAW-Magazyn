using System.Windows;

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