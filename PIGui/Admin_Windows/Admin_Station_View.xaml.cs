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
    /// Interaction logic for Admin_Station_View.xaml
    /// </summary>
    public partial class Admin_Station_View : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        readonly Admin_Window m_main;
        ObservableCollection<Station> m_stations;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="main">Again, kinda useless</param>
        public Admin_Station_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;

            m_stations = new ObservableCollection<Station>();
            Refresh_List();
        }

        //Functions
        
        private void Stations_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station item = (sender as ListView).SelectedItem as Station;
            Station_Info.DataContext = item;
        }

        private void Refresh_List()
        {
            m_stations.Clear();
            foreach (BO.Station station in bL.GetStations())
                m_stations.Add(station);
            m_stations = new ObservableCollection<Station>(m_stations.OrderBy(o => o.Name));
            Stations_View.ItemsSource = m_stations;
            if(m_stations.Any())
                Station_Info.DataContext = m_stations[0];
        }

        private void Add_Buttom_Click(object sender, RoutedEventArgs e)
        {
            Add_Update_Station add_Update_Station = new Add_Update_Station();
            add_Update_Station.ShowDialog();
            Refresh_List();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            var elem = sender as FrameworkElement;
            Station item = elem.DataContext as Station;
            Add_Update_Station add_Update_Station = new Add_Update_Station(item);
            add_Update_Station.ShowDialog();
            Refresh_List();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBoxResult result = AdonisUI.Controls.MessageBox.Show("Are you sure you want to delete this station?", "Confirmation",
                                          AdonisUI.Controls.MessageBoxButton.YesNo,
                                          AdonisUI.Controls.MessageBoxImage.Question);
            if (result == AdonisUI.Controls.MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var elem = sender as FrameworkElement;
                Station item = elem.DataContext as Station;
                try
                {
                    bL.DeleteStation(item);
                }
                catch(BadStationIdException ex)
                {
                    AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                    return;
                }
                catch(NotImplementedException ex)
                {
                    AdonisUI.Controls.MessageBox.Show(ex.Message, "ERROR");
                    return;
                }
                AdonisUI.Controls.MessageBox.Show("The station was removed successfully!", "Success");
                Refresh_List();

            }
        }

    }
}
