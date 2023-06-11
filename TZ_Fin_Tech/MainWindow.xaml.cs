﻿using System;
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
           
        }
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
            if (Three_lvl_data_base.SelectedIndex == 0) { lvl_data_base = 1; }
            if (Three_lvl_data_base.SelectedIndex == 1) { lvl_data_base = 2; }
            if (Three_lvl_data_base.SelectedIndex == 2) { lvl_data_base = 3; }
        }

        private void Button_Click_Add_data(object sender, RoutedEventArgs e)
        {
            string text_name = text_box_name.Text; int text_kol = Convert.ToInt32(text_box_kol.Text);
            int text_price = Convert.ToInt32(text_box_price.Text);
            int text_izdelUP = Convert.ToInt32(text_box_izdelUP.Text);int text_izdel = Convert.ToInt32(text_box_izdel.Text);
            int text_parent = Convert.ToInt32(text_box_parent.Text);
            DataBase data = new DataBase();

            data.Inset_data_base_two_table(text_name,text_kol, text_price, text_izdelUP, text_izdel, text_parent);
            
            
        }
    }
}
    

