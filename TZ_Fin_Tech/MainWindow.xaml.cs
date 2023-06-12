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
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Windows.Markup;
using System.Security.Cryptography.X509Certificates;

namespace TZ_Fin_Tech
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataBase data = new DataBase();
            data.CreatTable_Izdel();
            
            var izdelUp = data.Seatch_all_lvl_IzelUp();
            foreach (Parent par in izdelUp)
            {
                izdelUp_all_lvl.Items.Add(par.IzdelUP_id.ToString());
            }
            var izdel_add_new_position = data.Seatch_all_lvl_parent();
            foreach (Izdel zde in izdel_add_new_position)
            {
                parent_all_lvl.Items.Add(zde.Parent_id.ToString());
            }
            var izdel_paren_list_view = data.Seatch_all_lvl_parent();
            foreach (Izdel zde in izdel_paren_list_view)
            {
                Three_lvl_data_base.Items.Add(zde.Parent_id.ToString());
            }
        }
        private static int _lvl_parent_all = 0;
        public static int lvl_parent_all { get { return _lvl_parent_all; } set { _lvl_parent_all = value; } }
        private static int _lvl_data_base = 0;
        public static int lvl_data_base { get { return _lvl_data_base; } set { _lvl_data_base = value; } }

        ExcelApp ex = new ExcelApp();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportExcel();
        }

        private void Button_Click_Out__All_BD(object sender, RoutedEventArgs e)
        {
            ex.Export_full_DataBase_Excel();
        }

        public void Three_lvl_data_base_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBase data = new DataBase();
            int numb_selec = Convert.ToInt32(Three_lvl_data_base.SelectedValue);
            //lvl_data_base = numb_selec;
            if (Three_lvl_data_base.SelectedIndex == numb_selec - 1)
            {
                parentList.Items.Clear(); lvl_data_base = numb_selec;
                var list = data.Out_data_view_list(numb_selec);
                foreach (var item in list)
                {
                    parentList.Items.Add(item);
                }
            };
        }
    

        private void Button_Click_Add_data(object sender, RoutedEventArgs e)
        {   try
            {
                DataBase data = new DataBase();
                var max_izdel_id = data.Seatch_Izel_Unique();
                string text_name = text_box_name.Text;
                int text_kol = Convert.ToInt32(text_box_kol.Text)as int ? ??default(int);
                int text_price = Convert.ToInt32(text_box_price.Text) as int? ?? default(int);
                int text_izdelUP = Convert.ToInt32(izdelUp_all_lvl.SelectedValue) as int? ?? default(int);
                int text_izdel = (max_izdel_id+1);
                int text_parent = Convert.ToInt32(parent_all_lvl.SelectedValue) as int? ?? default(int);
                data.Inset_data_base_two_table(text_name, text_kol, text_price, text_izdelUP, text_izdel, text_parent);
            }
            catch(Exception ex)
            {
                text_box_name.Background = Brushes.IndianRed;
                text_box_kol.Background = Brushes.IndianRed;
                text_box_price.Background = Brushes.IndianRed;
                MessageBox.Show(ex.Message);
            }
        }

        private void text_box_name_GotFocus(object sender, RoutedEventArgs e)  {  text_box_name.Clear();  }
        private void text_box_kol_GotFocus(object sender, RoutedEventArgs e)   {    text_box_kol.Clear();   }
        private void text_box_price_GotFocus(object sender, RoutedEventArgs e)   { text_box_price.Clear();   }

        private void Button_Create_new_position(object sender, RoutedEventArgs e)
        {
            Created_new_position created = new Created_new_position();
            created.Show();
            this.Close();
        }
    }
}
    

