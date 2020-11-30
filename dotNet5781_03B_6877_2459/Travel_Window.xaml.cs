using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace dotNet5781_03B_6877_2459
{
    /// <summary>
    /// Interaction logic for Travel_Window.xaml
    /// </summary>
    public partial class Travel_Window : Window
    {
        Bus m_bus;
        MainWindow main;
        BackgroundWorker travel_worker;
        int m_length;
        int m_duration;
        internal Travel_Window(MainWindow main_g, Bus bus)
        {
            InitializeComponent();
            main = main_g;
            m_bus = bus;
        }
        private void Travel_Bus(Bus bus, int length)
        {
            if (bus.Status == State.Ready)
            {
                if (!bus.IsQualified())
                {
                    MessageBox.Show("This bus is not qualified for a travel. Please maintain it.");
                    return;
                }
                if (!bus.IsCapable(length))
                {
                    MessageBox.Show("This bus doesn't have enough fuel for this travel. Please refuel it.");
                    return;
                }
                bus.Status = State.Busy;
                Random r = new Random();
                int speed = r.Next(30, 51);
                m_duration = ((length / speed) * 6);  // length - km, speed - km/h duration - hours 

                travel_worker = new BackgroundWorker();
                travel_worker.DoWork += Travel_DoWork;
                travel_worker.ProgressChanged += Travel_ProgressChanged;
                travel_worker.RunWorkerCompleted += Travel_RunWorkerCompleted;
                travel_worker.WorkerReportsProgress = true;
                travel_worker.RunWorkerAsync();
            }
            else
                main.Unavailable_Bus(bus);
        }
        private void Travel_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < m_duration * 1000) // *1000 because we want to compare miliseconds
            {
                travel_worker.ReportProgress((int)(m_duration * 1000 - stopwatch.ElapsedMilliseconds) / 1000);
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void Travel_ProgressChanged(object sender, ProgressChangedEventArgs s)
        {
            TimeSpan t = TimeSpan.FromSeconds(s.ProgressPercentage * 10 * 60);
            m_bus.Timer = string.Format("{0:D2}h:{1:D2}m", t.Hours + t.Days * 24, t.Minutes);
            m_bus.Print = m_bus.ToString();
        }
        private void Travel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_bus.Timer = "";
            m_bus.PerformTravel(m_length);
            m_bus.Status = State.Ready;
            m_bus.Print = m_bus.ToString();
        }

        private void Lenght_insert_KeyDown(object sender, KeyEventArgs e)
        {
            int lenght;
            if (e.Key == Key.Enter)
            {
                if (!Int32.TryParse(Lenght_insert.Text, out lenght) || lenght < 0) 
                    MessageBox.Show("You must insert positive integer number only!");
                else
                {
                    m_length = lenght;
                    Close();
                    Travel_Bus(m_bus, m_length);
                }

            }
        }
    }
}
