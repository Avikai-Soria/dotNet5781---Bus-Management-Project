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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03B_6877_2459
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<Bus> list_of_busses = new ObservableCollection<Bus>();
        public MainWindow()
        {
            InitializeComponent();
            Bus bus1 = new Bus("123-45-678", DateTime.Now, 0, 1200, 0);                     // Brand new bus
            Bus bus2 = new Bus("12-345-67", DateTime.Now.AddYears(-3), 40000, 1200, 0);     // Old bus, needs maintenance
            Bus bus3 = new Bus("321-54-876", DateTime.Now.AddYears(-1), 5000, 100, 3000);   // Low on fuel bus
            Bus bus4 = new Bus("213-45-687", DateTime.Now.AddYears(-2), 30000, 1200, 19500);// 500 kms away from need maintienance
            Bus bus5 = new Bus("123-54-678", DateTime.Now.AddMonths(-7), 3000, 800, 3000);  // Fairly new
            Bus bus6 = new Bus("32-167-43", DateTime.Now.AddYears(-15), 100000, 500, 7000);
            Bus bus7 = new Bus("92-431-57", DateTime.Now.AddYears(-7), 50000, 300, 15000);
            Bus bus8 = new Bus("532-64-139", DateTime.Now.AddMonths(-3), 3000, 700, 3000);
            Bus bus9 = new Bus("73-834-12", DateTime.Now.AddYears(-4), 30000, 400, 7000);
            Bus bus10 = new Bus("921-43-213", DateTime.Now.AddDays(-123), 3000, 1000, 1000);
            list_of_busses.Add(bus10); list_of_busses.Add(bus9); list_of_busses.Add(bus8); list_of_busses.Add(bus7);
            list_of_busses.Add(bus6); list_of_busses.Add(bus5); list_of_busses.Add(bus4); list_of_busses.Add(bus3);
            list_of_busses.Add(bus2); list_of_busses.Add(bus1);
            Busses_View.ItemsSource = list_of_busses;
        }
        private void Busses_View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus item = (sender as ListView).SelectedItem as Bus;
            Bus_Information_Window info_window = new Bus_Information_Window(this, item);
            info_window.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Bus_Window add_window = new Add_Bus_Window(this);
            add_window.ShowDialog();
        }
        internal void Add_Bus(Bus bus)
        {
            list_of_busses.Add(bus);
        }
        internal bool Id_Exists(string id)
        {
            return list_of_busses.Where(T => { return T.Id == id; }).Any();
        }
        internal void Update_List()
        {
            
        }
        private void MainWindow_Closed(object sender, EventArgs e) 
        { 
            Environment.Exit(Environment.ExitCode); 
        }
        
    }
}
