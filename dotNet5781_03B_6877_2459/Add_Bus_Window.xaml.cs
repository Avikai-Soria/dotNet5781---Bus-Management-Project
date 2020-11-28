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
    /// Interaction logic for Add_Bus_Window.xaml
    /// </summary>
    public partial class Add_Bus_Window : Window
    {
        MainWindow main;
        public Add_Bus_Window(MainWindow main_g)
        {
            InitializeComponent();
            main = main_g;
        }
        static string fix_id(string id)
        {
            if (id.Length == 7)
            {
                id = id.Substring(0, 2) + '-' + id.Substring(2, 3) + '-' + id.Substring(5, 2);
            }
            else
            {
                id = id.Substring(0, 3) + '-' + id.Substring(3, 2) + '-' + id.Substring(5, 3);
            }
            return id;
        }
        private bool Ensure_Id(int id, DateTime date)
        {
            if (date.Year < 2018 && (id > 999999) && (id < 10000000))
                return true;
            if (date.Year >= 2018 && ((id > 9999999) && (id < 100000000)))
                return true;
            return false;
        }
        private bool Ensure_Id_Exists(string id)
        {
            return main.Id_Exists(id);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int testid;
            if (!Int32.TryParse(Bus_ID_insert.Text, out testid))
            {
                ERROR_label.Content = "ERROR! ID must contain only numbers!";
                return;
            }
            DateTime date = new DateTime();
            if (!DateTime.TryParse(Bus_CD_insert.Text, out date))
            {
                ERROR_label.Content = "ERROR! Please insert a valid date.";
                return;
            }
            if (!Ensure_Id(testid, date))
            {
                ERROR_label.Content = "ERROR! ID and Creation Date doesn't match. \n Insert 7 ID digits for busses created before 2018. \n Insert 8 ID digits otherwise.";
                return;
            }
            if (Ensure_Id_Exists(fix_id(Bus_ID_insert.Text)))
            {
                ERROR_label.Content = "ERROR! This bus's ID already exists in the system;";
                return;
            }
            Bus bus = new Bus(fix_id(Bus_ID_insert.Text), date, 0, 1200, 0);
            main.Add_Bus(bus);
            this.Close();
        }
    }
}
