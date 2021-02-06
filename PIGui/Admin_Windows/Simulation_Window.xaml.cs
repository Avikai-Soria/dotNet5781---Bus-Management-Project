using AdonisUI.Controls;
using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Simulation_Window.xaml
    /// </summary>
    public partial class Simulation_Window : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        private TimeSpan m_currentTime;
        private BackgroundWorker m_worker;
        private bool m_update;
        public Simulation_Window()
        {
            InitializeComponent();
            InitializeIntro();
            m_worker = new BackgroundWorker();
            m_worker.DoWork += Worker_DoWork;
            m_worker.ProgressChanged += Worker_ProgressChanged;
            m_worker.WorkerReportsProgress = true;
            m_worker.RunWorkerAsync();
        }

        

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (m_update)
                {
                    m_worker.ReportProgress(0);
                    m_update = false;
                }
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            cbHours.SelectedIndex = m_currentTime.Hours;
            cbMinutes.SelectedIndex = m_currentTime.Minutes;
            cbSeconds.SelectedIndex = m_currentTime.Seconds;

            Refresh_List();
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = new SolidColorBrush(Color.FromRgb(130, 0, 30));
            SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0, 128, 0));

            
            if (startStopButton.Content.ToString() == "Start simulation!")  // In this case, we start
            {
                if (!Int32.TryParse(speedTextBox.Text, out int rate) || rate < 0)
                {
                    AdonisUI.Controls.MessageBox.Show("Speed must be an integer positive number", "Invalid value");
                    return;
                }
                string timeString = cbHours.SelectedValue.ToString() + ":" + cbMinutes.SelectedValue.ToString() + ":" + cbSeconds.SelectedValue.ToString();
                TimeSpan startTime = TimeSpan.Parse(timeString);
                startStopButton.Background = red;
                startStopButton.Content = "Stop simulation!";
                speedTextBox.IsEnabled = false;
                cbHours.IsEnabled = cbMinutes.IsEnabled = cbSeconds.IsEnabled = false;
                bL.StartSimulator(startTime, rate, UpdateTime);
            }
            else
            {
                startStopButton.Background = green;
                startStopButton.Content = "Start simulation!";
                speedTextBox.IsEnabled = true;
                cbHours.IsEnabled = cbMinutes.IsEnabled = cbSeconds.IsEnabled = true;
                bL.StopSimulator();
            }
        }
        private void UpdateTime(TimeSpan span)
        {
            m_currentTime = span;
            m_update = true;
        }
        private void InitializeIntro()
        {
            for(int i=0; i<10; i++)
            {
                cbHours.Items.Add("0" + i.ToString());
                cbMinutes.Items.Add("0" + i.ToString());
                cbSeconds.Items.Add("0" + i.ToString());
            }
            for (int i = 10; i < 24; i++)
            {
                cbHours.Items.Add(i.ToString());
                cbMinutes.Items.Add(i.ToString());
                cbSeconds.Items.Add(i.ToString());
            }
            for (int i = 24; i < 60; i++)
            {
                cbMinutes.Items.Add(i.ToString());
                cbSeconds.Items.Add(i.ToString());
            }
            cbHours.SelectedIndex = 0;
            cbMinutes.SelectedIndex = 0;
            cbSeconds.SelectedIndex = 0;
            speedTextBox.Text = "20";
            cbStation.ItemsSource=bL.GetStations().OrderBy(o => o.Name);
            cbStation.SelectedIndex = 0;
        }
        private void Refresh_List()
        {
            ListViewItem item;
            Station station = cbStation.SelectedItem as Station;
            SolidColorBrush red = new SolidColorBrush(Color.FromRgb(130, 0, 30));
            SolidColorBrush yellow = new SolidColorBrush(Color.FromRgb(184, 134, 11));
            SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0, 128, 0));



            if (station == null)
            {
                IncomingLinesListView.Items.Clear();
                return;
            }

            IncomingLinesListView.Items.Clear();
            foreach (KeyValuePair<TimeSpan, int> pair in bL.GetIncomingLines(station, m_currentTime))
            {
                item = new ListViewItem();
                if (pair.Key > new TimeSpan(0, 5, 0)) // Positive time
                {
                    item.Content = "Line " + pair.Value + " will arrive in " + (pair.Key).ToString(@"hh\:mm\:ss");
                    item.FontSize = 20;
                    item.Background = green;
                }
                else
                {
                    if(pair.Key > new TimeSpan(0, 0, 0))
                    {
                        item.Content = "Line " + pair.Value + " will arrive in " + (pair.Key).ToString(@"hh\:mm\:ss");
                        item.FontSize = 20;
                        item.Background = yellow;
                    }
                    else
                    {
                        item.Content = "Line " + pair.Value + " passed " + (new TimeSpan(0, 0, 0) - pair.Key).ToString(@"hh\:mm\:ss") + " ago";
                        item.FontSize = 20;
                        item.Background = red;
                    }
                }

                IncomingLinesListView.Items.Add(item);
            }

        }

        private void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh_List();
        }
    }
}
