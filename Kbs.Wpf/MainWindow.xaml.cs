﻿using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Kbs.Business.Session;
using Kbs.Wpf.Attributes;

namespace Kbs.Wpf
{
    public partial class MainWindow : Window, INavigationManager
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Navigate<TPage>(Func<TPage> creator) where TPage : Page
        {
            var page = creator();
            var attributes = typeof(TPage).GetCustomAttributes(typeof(HasRoleAttribute))
                .Cast<HasRoleAttribute>().ToArray();

            if (attributes.Length > 0)
            {
                var user = SessionManager.Instance?.Current?.User;
                if (user == null)
                {
                    MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
                    return;
                }

                if (attributes.Any(e => user.Is(e.Role)))
                {
                    NavigationFrame.Navigate(page);
                }

                MessageBox.Show(this, "U heeft geen toegang tot deze functie", "Toegang geweigerd");
            }

            NavigationFrame.Navigate(page);
        }
    }
}