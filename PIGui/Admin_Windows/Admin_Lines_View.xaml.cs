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
    /// Interaction logic for Admin_Lines_View.xaml
    /// </summary>
    public partial class Admin_Lines_View : AdonisWindow
    {
        readonly IBL bL = BLFactory.GetBI();
        Admin_Window m_main;
        readonly ObservableCollection<Line> m_lines;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="main">The main window (kinda useless)</param>
        public Admin_Lines_View(Admin_Window main)
        {
            InitializeComponent();
            m_main = main;
            m_lines = new ObservableCollection<Line>();
            Refresh_lines();

        }

        // Functions

        private void cbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(m_lines.Count()!=0)
            {
                Line item = cbLines.SelectedValue as Line;
                Lines_Info.DataContext = item;
                LineStation_View.ItemsSource = item.Stations;
            }   
        }

        private void LineStation_Changed(object sender, SelectionChangedEventArgs e)
        {
            LineStation item = (sender as ListView).SelectedItem as LineStation;
            LineStation_Info.DataContext = item;
        }

        private void Add_Buttom_Click(object sender, RoutedEventArgs e)
        {
            Add_Update_Line add_Line = new Add_Update_Line();
            add_Line.ShowDialog();
            Refresh_lines();
        }

        private void Edit_Buttom_Click(object sender, RoutedEventArgs e)
        {
            Line item = cbLines.SelectedValue as Line;
            Add_Update_Line update_Line = new Add_Update_Line(item);
            update_Line.ShowDialog();
            Refresh_lines();
        }

        private void Remove_Buttom_Click(object sender, RoutedEventArgs e)
        {
            AdonisUI.Controls.MessageBoxResult result = AdonisUI.Controls.MessageBox.Show("Are you sure you want to delete this station?", "Confirmation",
                                          AdonisUI.Controls.MessageBoxButton.YesNo,
                                          AdonisUI.Controls.MessageBoxImage.Question);
            if (result == AdonisUI.Controls.MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Line item = cbLines.SelectedValue as Line;
                try
                {
                    bL.DeleteLine(item);
                }
                catch (BadLineIdException ex)
                {
                    AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                    return;
                }
                catch (BadLineStationIdException ex)
                {
                    AdonisUI.Controls.MessageBox.Show(ex.error_msg, "ERROR");
                    return;
                }
                AdonisUI.Controls.MessageBox.Show("The line was removed successfully!", "Success");
                Refresh_lines();
            }
        }

        private void Button_Linestation_Edit_Click(object sender, RoutedEventArgs e)
        {
            var elem = sender as FrameworkElement;
            LineStation item = elem.DataContext as LineStation;
            Line line = cbLines.SelectedValue as Line;
            //Lines_Info.DataContext = item;
            //LineStation_View.ItemsSource = item.Stations;

            if (item!= line.Stations.Last())
            {
                Add_Update_LineStation add_Update_LineStation = new Add_Update_LineStation(item);
                add_Update_LineStation.ShowDialog();
                Refresh_lines();
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("You can't insert distance nor duration in the last linestation.", "Invalid button");
                return;
            }
            
        }

        private void Refresh_lines()
        {
            m_lines.Clear();
            foreach (BO.Line line in bL.GetLines())
                m_lines.Add(line);
            cbLines.ItemsSource = m_lines;
            cbLines.DisplayMemberPath = "LineNumber";
            cbLines.SelectedIndex = 0;

            if(m_lines.Any())
            {
                LineStation_View.ItemsSource = m_lines[0].Stations;
                Lines_Info.DataContext = m_lines[0];
                LineStation_Info.DataContext = m_lines[0].Stations[0];
            }

        }

    }
}
