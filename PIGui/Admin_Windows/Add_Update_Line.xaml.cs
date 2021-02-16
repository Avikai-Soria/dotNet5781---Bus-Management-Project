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

namespace PIGui
{
    /// <summary>
    /// Interaction logic for Add_Update_Line.xaml
    /// </summary>
    public partial class Add_Update_Line : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        readonly Line m_line;
        ObservableCollection<Station> m_sourceStations;
        ObservableCollection<Station> m_destinationStations;

        /// <summary>
        /// Add line constructor
        /// </summary>
        public Add_Update_Line()
        {
            InitializeComponent();
            ButtonUpdate.Visibility = Visibility.Hidden;

            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas)).Cast<BO.Areas>();
            cbArea.SelectedIndex = 0;
            m_sourceStations = new ObservableCollection<Station>();
            m_destinationStations = new ObservableCollection<Station>();
            foreach (Station station in bL.GetStations())
            {
                m_sourceStations.Add(station);
            }
            RefreshLists();
        }

        /// <summary>
        /// Update line constructor
        /// </summary>
        /// <param name="line">The line to update</param>
        public Add_Update_Line(Line line)
        {
            InitializeComponent();
            ButtonAdd.Visibility = Visibility.Hidden;
            m_line = line;

            string code = m_line.LineNumber.ToString();
            lineNumberTextBox.Text = code;
            cbArea.ItemsSource = Enum.GetValues(typeof(BO.Areas)).Cast<BO.Areas>();
            cbArea.SelectedIndex = (int) m_line.Area;
            m_sourceStations = new ObservableCollection<Station>();
            m_destinationStations = new ObservableCollection<Station>();
            foreach (Station station in bL.GetStations())
            {
                m_sourceStations.Add(station);
            }
            foreach (LineStation lineStation in m_line.Stations)
            {
                Station station = m_sourceStations.FirstOrDefault(id => id.StationID == lineStation.Station); // This station belong to dest list
                m_sourceStations.Remove(station);
                m_destinationStations.Add(station);
            }
            RefreshLists();
        }

        // Functions
        private void ButtonAddStation_Click(object sender, RoutedEventArgs e)
        {
            Station station = SourceListView.SelectedItem as Station;
            if (station!=null)
            {
                m_sourceStations.Remove(station);
                m_destinationStations.Add(station);
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("You must select a station to add from the list to your left.", "ERROR");
            }    
        }

        private void SourceListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Station station = SourceListView.SelectedItem as Station;
            if (station != null)
            {
                m_sourceStations.Remove(station);
                m_destinationStations.Add(station);
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("You must select a station to add from the list to your left.", "ERROR");
            }
        }

        private void ButtonRemoveStation_Click(object sender, RoutedEventArgs e)
        {
            Station station = DestinationListView.SelectedItem as Station;
            if (station != null)
            {
                m_destinationStations.Remove(station);
                m_sourceStations.Add(station);
                RefreshLists();
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("You must select a station to remove from the list to your right.", "ERROR");
            }
        }

        private void DestinationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Station station = DestinationListView.SelectedItem as Station;
            if (station != null)
            {
                m_destinationStations.Remove(station);
                m_sourceStations.Add(station);
                RefreshLists();
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("You must select a station to remove from the list to your right.", "ERROR");
            }
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
             
            if (!Int32.TryParse(lineNumberTextBox.Text, out int num)) 
            {
                AdonisUI.Controls.MessageBox.Show("Line number must be an integer number", "Invalid value");
                return;
            }
            if (m_destinationStations.Count < 2) 
            {
                AdonisUI.Controls.MessageBox.Show("A line must contain at least 2 stations", "Invalid value");
                return;
            }
            List<Station> stations = m_destinationStations.ToList();
            Line line = new Line()
            {
                LineNumber = num,
                Area = (Areas)Enum.Parse(typeof(Areas), cbArea.Text),
            };
            try
            {
                bL.AddLine(line, stations);
            }
            catch (BadStationIdException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            catch (BO.BadLineStationIdException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            catch (BO.BadAdjStationsException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            catch (BO.BadLineTripIdException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            AdonisUI.Controls.MessageBox.Show("The line was added successfully!", "Success");
            this.Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(lineNumberTextBox.Text, out int num))
            {
                AdonisUI.Controls.MessageBox.Show("Line number must be a number", "Invalid value");
                return;
            }
            if (m_destinationStations.Count < 2)
            {
                AdonisUI.Controls.MessageBox.Show("A line must contain at least 2 stations", "Invalid value");
                return;
            }
            List<Station> stations = m_destinationStations.ToList();
            Areas area = (Areas)Enum.Parse(typeof(Areas), cbArea.Text);
            m_line.LineNumber = num;
            m_line.Area = area;
            try
            {
                bL.UpdateLine(m_line, stations);
            }
            catch (BadStationIdException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            catch (BO.BadLineStationIdException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            catch (BO.BadAdjStationsException ex)
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            AdonisUI.Controls.MessageBox.Show("The line was edited successfully!", "Success");
            this.Close();
        }

        private void RefreshLists()
        {
            m_sourceStations = new ObservableCollection<Station>(m_sourceStations.OrderBy(o => o.Name));
            SourceListView.ItemsSource = m_sourceStations;
            DestinationListView.ItemsSource = m_destinationStations;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineViewSource.Source = [generic data source]
        }
    }
}
