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
    /// Interaction logic for Add_Update_LineStation.xaml
    /// </summary>
    public partial class Add_Update_LineStation : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        LineStation m_lineStation;
        public Add_Update_LineStation(LineStation item)
        {
            InitializeComponent();
            m_lineStation = item;

            distanceTextBox.Text = m_lineStation.Distance.ToString();
            durationTextBox.Text = m_lineStation.Duration.ToString();
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(distanceTextBox.Text, out double distance))
            {
                AdonisUI.Controls.MessageBox.Show("Distance must be a number", "Invalid value");
                return;
            }
            if (!TimeSpan.TryParse(durationTextBox.Text, out TimeSpan duration))
            {
                AdonisUI.Controls.MessageBox.Show("Duration must be a time in the format of hh:mm:ss", "Invalid value");
                return;
            }
            m_lineStation.Distance = distance;
            m_lineStation.Duration = duration;
            try
            {
                bL.UpdateLineStation(m_lineStation);
            }
            catch (BO.BadAdjStationsException ex)// Need to catch exception from BO Station
            {
                AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                return;
            }
            AdonisUI.Controls.MessageBox.Show("The linestation was edited successfully!", "Success");
            this.Close();
        }

        private void AdonisWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AdonisWindow_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineStationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineStationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineStationViewSource.Source = [generic data source]
        }

        
    }
}
