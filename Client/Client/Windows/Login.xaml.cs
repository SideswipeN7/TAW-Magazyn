using Client.Model;
using Newtonsoft.Json;
using PluginExecutor;
using System;
using System.Collections.Generic;
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

#pragma warning disable IDE1006 // Naming Styles
        private void btnLogin_Click(object sender, RoutedEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            string password = "";
            if (txbPassword.IsVisible) password = txbPassword.Password;
            if (txtPassword.IsVisible) password = txtPassword.Text;
            Admin mw = LogIn(txtLogin.Text, password);

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
                        Dictionary<int, string> dictionary = new Dictionary<int, string>();
                        dictionary.Add(0, login);
                        dictionary.Add(1, password);
                        string pracownikJSON = TypeLoadedFromPlugin.Execute(dictionary);
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
            txtPassword.Text = txbPassword.Password;
            BtnShow.Content = "Ukryj";
            // txtPassword.Focus();
        }

        private void ShowPasswordChars()
        {
            txbPassword.Visibility = System.Windows.Visibility.Visible;
            txtPassword.Visibility = System.Windows.Visibility.Collapsed;
            txbPassword.Password = txtPassword.Text;
            BtnShow.Content = "Pokaż";
            //txbPassword.Focus();
        }


    }
}