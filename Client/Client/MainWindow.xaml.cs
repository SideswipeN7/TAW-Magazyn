using System.Windows;
using System;
using System.IO;
using System.Reflection;
using PluginExecutor;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(".");
            foreach (FileInfo fi in di.GetFiles("Plugin*.dll"))
            {
                Assembly pluginAssembly = Assembly.LoadFrom(fi.FullName);
                foreach (Type pluginType in pluginAssembly.GetExportedTypes())
                {
                    if (pluginType.GetInterface(typeof(IPluginLogin).Name) != null)
                    {
                        IPluginLogin TypeLoadedFromPlugin = (IPluginLogin)Activator.CreateInstance(pluginType);
                        string idPracownika = TypeLoadedFromPlugin.Login(txtLogin.Text, txtPassword.Text);
                        lblId.Content = idPracownika;
                    }
                }
            }
        }
    }
}