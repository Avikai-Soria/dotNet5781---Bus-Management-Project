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
    /// Interaction logic for Admin_Busses_View.xaml
    /// </summary>
    public partial class Admin_Busses_View : Window
    {
        Admin_Window m_main;
        public Admin_Busses_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;
        }
        private void Busses_View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Bus item = (sender as ListView).SelectedItem as Bus;
            //Bus_Information_Window info_window = new Bus_Information_Window(this, item);
            //info_window.Show();
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
