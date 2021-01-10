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
    /// Interaction logic for Admin_Lines_View.xaml
    /// </summary>
    public partial class Admin_Lines_View : Window
    {
        readonly IBL bL = BLFactory.GetBI();
        Admin_Window m_main;
        ObservableCollection<Line> m_lines;
        public Admin_Lines_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;

            m_lines = new ObservableCollection<Line>();
            foreach (BO.Line line in bL.GetLines())
                m_lines.Add(line);
            Lines_View.ItemsSource = m_lines;

            LineStation_View.ItemsSource = m_lines[0].Stations;
            Lines_Info.DataContext = m_lines[0];
            LineStation_Info.DataContext = m_lines[0].Stations[0];
        }

        private void Lines_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Line item = (sender as ListView).SelectedItem as Line;
            Lines_Info.DataContext = item;
            LineStation_View.ItemsSource = item.Stations;
        }

        private void LineStation_Changed(object sender, SelectionChangedEventArgs e)
        {
            LineStation item = (sender as ListView).SelectedItem as LineStation;
            LineStation_Info.DataContext = item;
        }
        private void Add_Buttom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
