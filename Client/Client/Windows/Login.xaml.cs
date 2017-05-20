﻿using System.Windows;
using System;
using System.IO;
using System.Reflection;
using PluginExecutor;
using Newtonsoft.Json;
using Client.Model;



namespace Client.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWIndow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Admin mw = LogIn(txtLogin.Text, txtPassword.Text);

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
                            return window;
                        }
                    }
                }
            }
            return null;
        }



    } }