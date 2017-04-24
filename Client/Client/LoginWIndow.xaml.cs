using System.Windows;
using System;
using System.IO;
using System.Reflection;
using PluginExecutor;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWIndow.xaml
    /// </summary>
    public partial class LoginWIndow : Window
    {
        public LoginWIndow()
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
                        int id = 0;
                        if (Int32.TryParse(TypeLoadedFromPlugin.Login(txtLogin.Text, txtPassword.Text), out id))
                        {
                            if (id > 0)
                            {
                                MainWindow mw = new MainWindow();
                                mw.Show();
                                this.Close();
                            }
                            else
                            {
                                ShowError();
                            }
                        }
                        else
                        {
                            ShowError();
                        }
                    }
                }
            }
        }

        public void ShowError()
        {
            MessageBox.Show("Wprowadzone dane są niepoprawne");
        }
    }
}
