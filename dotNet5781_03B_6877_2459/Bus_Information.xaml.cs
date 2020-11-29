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

namespace dotNet5781_03B_6877_2459
{
    /// <summary>
    /// Interaction logic for Bus_Information.xaml
    /// </summary>
    public partial class Bus_Information_Window : Window
    {
        Bus m_bus;

        internal Bus_Information_Window(Bus bus)
        {
            InitializeComponent();
            m_bus = bus;
            Information_Label.Content = m_bus.ToString();
        }

        private void Refuel_Buttom_Click(object sender, RoutedEventArgs e)
        {
            if (m_bus.Status == State.Ready)
            {
                m_bus.PerformRefueling();   // TODO change status of bus
            }
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
