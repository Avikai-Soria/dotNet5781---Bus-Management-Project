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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03A_6877_2459
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private BusLine_Collection collection;

        public MainWindow()
        {
            InitializeComponent();
            BusLine line_1 = new BusLine(0);                            // This BusLine will have all 40 BusLine Station
            BusLine line_2 = new BusLine(1);                            // This will recive only first and last station
            BusLine line_3 = new BusLine(2);                            // This line will recive all even numbers of stations
            BusLine line_4 = new BusLine(3);                            // This line will recive all odd numbers of stations
            BusLine line_5 = new BusLine(4);                            // This line will recive all stations that divide by 5
            BusLine line_6 = new BusLine(5);                            // This line will recive all stations that divide by 6
            BusLine line_7 = new BusLine(6);                            // This line will recive all stations that divide by 7
            BusLine line_8 = new BusLine(7);                            // This line will recive all stations that divide by 8
            BusLine line_9 = new BusLine(8);                            // This line will recive all stations that divide by 9
            BusLine line_10 = new BusLine(9);                           // This line will recive all stations that divide by 10
            BusLine_Station[] a = new BusLine_Station[40];
            List<BusLine_Station> stations = new List<BusLine_Station>();

            for (int i = 0; i < 40; i++)
            {
                a[i] = new BusLine_Station(i, "a" + i);                 // 40 random stations
                line_1.Add(a[i], i);
                if (i == 0 || i == 39)
                    line_2.Add(a[i], i / 39);
                if (i % 2 == 0)
                    line_3.Add(a[i], i / 2);
                if (i % 2 == 1)
                    line_4.Add(a[i], i / 2);
                if (i % 5 == 0)
                    line_5.Add(a[i], i / 5);
                if (i % 6 == 0)
                    line_6.Add(a[i], i / 6);
                if (i % 7 == 0)
                    line_7.Add(a[i], i / 7);
                if (i % 8 == 0)
                    line_8.Add(a[i], i / 8);
                if (i % 9 == 0)
                    line_9.Add(a[i], i / 9);
                if (i % 10 == 0)
                    line_10.Add(a[i], i / 10);
            }
            stations = a.ToList<BusLine_Station>();
            collection = new BusLine_Collection();
            collection.Add(line_1); collection.Add(line_2); collection.Add(line_3); collection.Add(line_4);
            collection.Add(line_5); collection.Add(line_6); collection.Add(line_7); collection.Add(line_8); collection.Add(line_9); collection.Add(line_10);
            cbBusLines.ItemsSource = collection;
            cbBusLines.DisplayMemberPath = "BusLine_Id";
            cbBusLines.SelectedIndex = 0;
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = collection[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
            tbArea.DataContext = currentDisplayBusLine.Area;
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLine_Id);
           tbArea.Text=((cbBusLines.SelectedValue as BusLine).Area).ToString();
        }
    }
}
