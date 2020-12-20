using AdonisUI;
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
        BackgroundWorker maintain_worker;
        
        internal Bus_Information_Window(MainWindow main_g , Bus bus)
        {
            InitializeComponent();
            AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
            main = main_g;
            m_bus = bus;
            MainGrid.DataContext = bus;
            bus.ValueChange += Bus_ValueChange;
        }

        private void Bus_ValueChange(object sender, EventArgs e)
        {
            Timer_label.GetBindingExpression(Label.ContentProperty).UpdateTarget();
            Information_Label.GetBindingExpression(Label.ContentProperty).UpdateTarget();
        }

        internal void Refuel_Buttom_Click(object sender, RoutedEventArgs e)
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
            else
                main.Unavailable_Bus(m_bus);
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
            m_bus.Timer = string.Format("{0:D2}h:{1:D2}m", t.Hours + t.Days * 24, t.Minutes);
            m_bus.Print = m_bus.ToString();
        }
        private void Refuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_bus.Timer = "";
            m_bus.PerformRefueling();
            m_bus.Status = State.Ready;
            m_bus.Print = m_bus.ToString();
        }

        private void Maintain_Buttom_Click(object sender, RoutedEventArgs e)
        {
            if (m_bus.Status == State.Ready)
            {
                m_bus.Status = State.Maintained;
                maintain_worker = new BackgroundWorker();
                maintain_worker.DoWork += Maintain_DoWork;
                maintain_worker.ProgressChanged += Maintain_ProgressChanged;
                maintain_worker.RunWorkerCompleted += Maintain_RunWorkerCompleted;
                maintain_worker.WorkerReportsProgress = true;
                maintain_worker.RunWorkerAsync();
            }
            else
                main.Unavailable_Bus(m_bus);
        }
        private void Maintain_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < 144000)
            {
                maintain_worker.ReportProgress((int)(144000 - stopwatch.ElapsedMilliseconds) / 1000);
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void Maintain_ProgressChanged(object sender, ProgressChangedEventArgs s)
        {
            TimeSpan t = TimeSpan.FromSeconds(s.ProgressPercentage * 10 * 60);
            m_bus.Timer = string.Format("{0:D2}h:{1:D2}m", t.Hours + t.Days * 24, t.Minutes);
            m_bus.Print = m_bus.ToString();
        }
        private void Maintain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_bus.Timer = "";
            m_bus.PerformMaintenance();
            m_bus.Status = State.Ready;
            m_bus.Print = m_bus.ToString();
        }
    }
}
