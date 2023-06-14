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

namespace TZ_Fin_Tech
{
    /// <summary>
    /// Логика взаимодействия для Created_new_position.xaml
    /// </summary>
    public partial class Created_new_position : Window
    {
        public Created_new_position()
        {
            InitializeComponent();
        }

        private void Button_Click_Add_Data_Base(object sender, RoutedEventArgs e)
        {
            DataBase data = new DataBase();
            int add_mas_parent = data.Seatch_max_lvl_parent();
            add_mas_parent ++;
            try
            {
                var max_izdel_id = data.Seatch_Izel_Unique();
                string text_name = text_box_name.Text;
                int text_kol = Convert.ToInt32(text_box_kol.Text) as int? ?? default(int);
                int text_price = Convert.ToInt32(text_box_price.Text) as int? ?? default(int);
                int text_izdel = (max_izdel_id + 1);
                data.Inset_data_base_two_table(text_name, text_kol, text_price, 1, text_izdel, add_mas_parent);
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                text_box_name.Background = Brushes.IndianRed;
                text_box_kol.Background = Brushes.IndianRed;
                text_box_price.Background = Brushes.IndianRed;
                MessageBox.Show(ex.Message);
            }
        }

        private void text_box_price_GotFocus(object sender, RoutedEventArgs e)        { text_box_price.Clear(); }

        private void text_box_kol_GotFocus(object sender, RoutedEventArgs e)       { text_box_kol.Clear(); }

        private void text_box_name_GotFocus(object sender, RoutedEventArgs e)        { text_box_name.Clear(); }

        private void Button_Click_Close_back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow   = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
