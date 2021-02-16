using AdonisUI;
using AdonisUI.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PIGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
        }

        private void User_Access_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBox.Show("This item is canceled. Press admin access instead.", "Work in progress");
        }

        private void Admin_Access_Click(object sender, RoutedEventArgs e)
        {
            Admin_Window add_window = new Admin_Window(this);
            this.Close();
            add_window.Show();
        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonColor.Content.ToString() == "Click here to switch to light theme")
            {
                AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
                ButtonColor.Content = "Click here to switch to dark theme";
            }
            else
            {
                AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
                ButtonColor.Content = "Click here to switch to light theme";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
