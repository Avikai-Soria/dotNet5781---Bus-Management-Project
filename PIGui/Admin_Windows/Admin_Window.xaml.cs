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
using System.Windows.Shapes;

namespace PIGui
{
    /// <summary>
    /// Interaction logic for Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        MainWindow m_main;
        public Admin_Window(MainWindow main)
        {
            InitializeComponent();
            m_main = main;

        }
        private void Busses_view_Click(object sender, RoutedEventArgs e)
        {
            Admin_Busses_View admin_Busses_View = new Admin_Busses_View(this);
            admin_Busses_View.ShowDialog();
        }

        private void Lines_View_Click(object sender, RoutedEventArgs e)
        {
            Admin_Lines_View admin_Lines_View = new Admin_Lines_View(this);
            admin_Lines_View.ShowDialog();
        }

        private void Stations_View_Click(object sender, RoutedEventArgs e)
        {
            Admin_Station_View admin_Station_View = new Admin_Station_View(this);
            admin_Station_View.ShowDialog();
        }
    }
}
