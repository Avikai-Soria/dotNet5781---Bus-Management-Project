using AdonisUI.Controls;
using BLApi;
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
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = new SolidColorBrush(Color.FromRgb(130, 0, 30));
            SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0, 128, 0));

            
            if (startStopButton.Content.ToString() == "Start simulation!")  // In this case, we start
            {
                if (!Int32.TryParse(speedTextBox.Text, out int rate))
                {
                    AdonisUI.Controls.MessageBox.Show("Speed must be an integer number", "Invalid value");
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
        }
    }
}
