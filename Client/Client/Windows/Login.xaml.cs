using Client.Model;
using Newtonsoft.Json;
using PluginExecutor;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Client.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWIndow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool show = false;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Admin mw = LogIn(txtLogin.Text, txbPassword.Password);

            if (mw != null)
            {
                mw.Show();
                this.Close();
            }
            else
            {
                ShowError();
            }
        }

        public void ShowError()
        {
            MessageBox.Show("Wprowadzone dane są niepoprawne");
        }

        private Admin LogIn(string login, string password)
        {
            Admin window;
            DirectoryInfo di = new DirectoryInfo(".");
            foreach (FileInfo fi in di.GetFiles("Plugin*.dll"))
            {
                Assembly pluginAssembly = Assembly.LoadFrom(fi.FullName);
                foreach (Type pluginType in pluginAssembly.GetExportedTypes())
                {
                    if (pluginType.GetInterface(typeof(IPluginLogin).Name) != null)
                    {
                        IPluginLogin TypeLoadedFromPlugin = (IPluginLogin)Activator.CreateInstance(pluginType);
                        string pracownikJSON = TypeLoadedFromPlugin.Login(login, password);
                        if (!pracownikJSON.Equals("null"))
                        {
                            Pracownik pracownik = JsonConvert.DeserializeObject<Pracownik>(pracownikJSON);
                            bool sudo;
                            switch (pracownik.Sudo)
                            {
                                case 0: sudo = false; break;
                                default: sudo = true; break;
                            }
                            window = Admin.GetInstance(pracownik.idPracownika, sudo);
                            //window = new Admin() { ID = 1 };
                            return window;
                        }
                    }
                }
            }
            return null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            show = !show;
            switch (show)
            {
                case false:
                    ShowPasswordChars();
                    break;

                case true:

                    HidePasswordChars();
                    break;
            }
        }

        private void HidePasswordChars()
        {
            txtPassword.Visibility = System.Windows.Visibility.Visible;
            txbPassword.Visibility = System.Windows.Visibility.Collapsed;
            BtnShow.Content = "Ukryj";
            txtPassword.Focus();
        }

        private void ShowPasswordChars()
        {
            txbPassword.Visibility = System.Windows.Visibility.Visible;
            txtPassword.Visibility = System.Windows.Visibility.Collapsed;
            BtnShow.Content = "Pokaż";
            txbPassword.Focus();
        }

        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = txbPassword.Password;
        }

        private void txtPassword_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            txbPassword.Password = txtPassword.Text;
        }
    }
}