using AdonisUI.Controls;
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
    public partial class Admin_Busses_View : AdonisWindow
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

            if(m_buses.Any())
                Bus_Info.DataContext = m_buses[0];
        }
        private void Busses_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bus item = (sender as ListView).SelectedItem as Bus;
            Bus_Info.DataContext = item;
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBox.Show("Work on Bus is canceled for now." , "Work in progress");
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBox.Show("Work on Bus is canceled for now.", "Work in progress");
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBox.Show("Work on Bus is canceled for now.", "Work in progress");
        }
    }
}
