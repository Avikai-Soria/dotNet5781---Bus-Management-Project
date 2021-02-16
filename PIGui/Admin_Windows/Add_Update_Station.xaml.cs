using AdonisUI.Controls;
using BLApi;
using BO;
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
    /// Interaction logic for Add_Update_Station.xaml
    /// </summary>
    public partial class Add_Update_Station : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        readonly Station m_station;
        /// <summary>
        /// Add station constructor
        /// </summary>
        public Add_Update_Station()
        {
            InitializeComponent();
            ButtonUpdate.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Update station constructor
        /// </summary>
        /// <param name="item">The item to update</param>
        public Add_Update_Station(Station item)
        {
            InitializeComponent();
            ButtonAdd.Visibility = Visibility.Hidden;
            m_station = item;
            nameTextBox.Text = m_station.Name;
            codeTextBox.Text = m_station.Code.ToString();
            longitudeTextBox.Text = m_station.Longitude.ToString();
            lattitudeTextBox.Text = m_station.Lattitude.ToString();
        }

        // Functions
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                AdonisUI.Controls.MessageBox.Show("Name must be inserted", "Invalid value");
                return;
            }
            if (!Int32.TryParse(codeTextBox.Text, out int code))
            {
                AdonisUI.Controls.MessageBox.Show("Code must be an integer number", "Invalid value");
                return;
            }
            if (!Double.TryParse(lattitudeTextBox.Text, out double lattitude))
            {
                AdonisUI.Controls.MessageBox.Show("Lattitude must be a number", "Invalid value");
                return;
            }
            if (!Double.TryParse(longitudeTextBox.Text, out double longitude))
            {
                AdonisUI.Controls.MessageBox.Show("Longitude must be a number", "Invalid value");
                return;
            }
            BO.Station station = new Station()
            {
                Code = code,
                Name = nameTextBox.Text,
                Lattitude = lattitude,
                Longitude = longitude
            };
            try
            {
                bL.AddStation(station);
            }
            catch (BO.BadStationIdException ex)// Need to catch exception from BO Station
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg , "ERROR");
                return;
            }
            AdonisUI.Controls.MessageBox.Show("The station was added successfully!", "Success");
            this.Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                AdonisUI.Controls.MessageBox.Show("Name must be inserted", "Invalid value");
                return;
            }
            if (!Int32.TryParse(codeTextBox.Text, out int code))
            {
                AdonisUI.Controls.MessageBox.Show("Code must be a number", "Invalid value");
                return;
            }
            if (!Double.TryParse(lattitudeTextBox.Text, out double lattitude))
            {
                AdonisUI.Controls.MessageBox.Show("Lattitude must be a number", "Invalid value");
                return;
            }
            if (!Double.TryParse(longitudeTextBox.Text, out double longitude))
            {
                AdonisUI.Controls.MessageBox.Show("Longitude must be a number", "Invalid value");
                return;
            }
            BO.Station station = new Station()
            {
                StationID = m_station.StationID,
                Code = code,
                Name = nameTextBox.Text,
                Lattitude = lattitude,
                Longitude = longitude
            };
            try
            {
                bL.UpdateStation(station);
            }
            catch (BO.BadStationIdException ex)// Need to catch exception from BO Station
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            AdonisUI.Controls.MessageBox.Show("The station was edited successfully!", "Success");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }
    }
}
