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
    /// Interaction logic for Bus_Information.xaml
    /// </summary>
    public partial class Bus_Information_Window : Window
    {
        Bus m_bus;
        MainWindow main;
        BackgroundWorker refuel_worker;
        
        internal Bus_Information_Window(MainWindow main_g , Bus bus)
        {
            InitializeComponent();
            main = main_g;
            m_bus = bus;
            Information_Label.Content = m_bus.ToString();
        }

        private void Refuel_Buttom_Click(object sender, RoutedEventArgs e)
        {
            if (m_bus.Status == State.Ready)
            {
                m_bus.Status = State.Refueling;
                refuel_worker = new BackgroundWorker();
                refuel_worker.DoWork += Refuel_DoWork;
                refuel_worker.ProgressChanged += Refuel_ProgressChanged;
                refuel_worker.RunWorkerCompleted += Refuel_RunWorkerCompleted;
                refuel_worker.WorkerReportsProgress = true;
                refuel_worker.RunWorkerAsync();
            }
        }
        private void Refuel_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(stopwatch.ElapsedMilliseconds<12000)
            {
                refuel_worker.ReportProgress((int) (12000 - stopwatch.ElapsedMilliseconds) / 1000);
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void Refuel_ProgressChanged(object sender, ProgressChangedEventArgs s)
        {
            TimeSpan t = TimeSpan.FromSeconds(s.ProgressPercentage * 10 * 60);
            Information_Label.Content = m_bus.ToString();
            Timer_label.Content = string.Format("{0:D2}h:{1:D2}m", t.Hours, t.Minutes);
        }
        private void Refuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Timer_label.Content = "Finish!";
            m_bus.PerformRefueling();
            m_bus.Status = State.Ready;
            Information_Label.Content = m_bus.ToString();

        }

        private void Maintain_Buttom_Click(object sender, RoutedEventArgs e)
        {
            if (m_bus.Status == State.Ready)
            {
                m_bus.PerformMaintenance(); // TODO same as above
            }
        }
    }
}
