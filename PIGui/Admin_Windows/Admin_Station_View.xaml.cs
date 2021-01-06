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
    /// Interaction logic for Admin_Station_View.xaml
    /// </summary>
    public partial class Admin_Station_View : Window
    {
        readonly IBL bL = BLFactory.GetBI();
        Admin_Window m_main;
        ObservableCollection<Station> m_stations;
        public Admin_Station_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;

            m_stations = new ObservableCollection<Station>();
            foreach (BO.Station station in bL.GetStations())
                m_stations.Add(station);
            Stations_View.ItemsSource = m_stations;
        }

        private void Stations_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station item = (sender as ListView).SelectedItem as Station;
            MainGrid.DataContext = item;
        }

        private void Add_Buttom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
