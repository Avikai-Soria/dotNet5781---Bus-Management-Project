using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        readonly IBL bL = BLFactory.GetBI();
        Admin_Window m_main;
        ObservableCollection<Bus> m_buses;
        public Admin_Busses_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;

            m_buses = new ObservableCollection<Bus>();
            foreach (BO.Bus bus in bL.GetBuses()) 
                m_buses.Add(bus);
            Busses_View.ItemsSource = m_buses;
        }
        private void Busses_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bus item = (sender as ListView).SelectedItem as Bus;
            MainGrid.DataContext = item;
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
