using System.Windows;
using System;
using System.IO;
using System.Reflection;
using PluginExecutor;
using Newtonsoft.Json;
using Client.Model;
using LoginDataLib;

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
            LoginData userLoginData = Login(txtLogin.Text, txtPassword.Text);

            if (userLoginData.Id == 0)
            {
                ShowError();
            }
            else
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        public void ShowError()
        {
            MessageBox.Show("Wprowadzone dane są niepoprawne");
        }

        public LoginData Login(String login, String password)
        {
            LoginData loginData = new LoginData(0, 0);
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
                            loginData = new LoginData(pracownik.idPracownika, pracownik.Sudo);
                            return loginData;

                        }
                    }
                }
            }

            return loginData;
        }
    }
}
