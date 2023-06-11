﻿using System;
using System.Collections.Generic;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace TZ_Fin_Tech
{
    internal class ExcelApp
    {
        public void ExportExcel()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            DataBase db = new DataBase();
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app;
            Microsoft.Office.Interop.Excel.Workbook wb;
            Microsoft.Office.Interop.Excel.Worksheet ws;
            
            app = new Excel.Application();
            
            wb = app.Workbooks.Add();
            ws = wb.Worksheets[1];

            //ws.Range["A1"].Value = "Id";
            //ws.Range["B1"].Value = "Наименование";
            //ws.Range["C1"].Value = "Стоимость";
            ws.Range["D1"].Value = "Изделие";
            ws.Range["D1"].Interior.Color = Excel.XlRgbColor.rgbGray;
            ws.Range["E1"].Value = "Кол-во";
            ws.Range["E1"].Interior.Color = Excel.XlRgbColor.rgbGray;
            ws.Range["F1"].Value = "Стоимость" ;
            ws.Range["F1"].Interior.Color = Excel.XlRgbColor.rgbGray;
            ws.Range["G1"].Value = "Цена" ;
            ws.Range["G1"].Interior.Color = Excel.XlRgbColor.rgbGray;


            DataBase izd= new DataBase();

            var izdel = izd.table_Info_Izdel();
            var link = izd.table_Info_Links();
            var parent = izd.Data_Base_Out_User(1);
            
            try
            {
                string cellName;
                int counter = 2;
                int full_price_for_all = 0;
                int summ_price = 0;
                int summ_kol = 0;
                int full_price_for_price_and_kol = 0;
                int number_control_id = 0;

                foreach (Parent zdel in parent)
                {
                    number_control_id = zdel.Izdel_id;
                    summ_price = zdel.Price;
                    summ_kol = zdel.Kol;
                    string counter_spaces = new string(' ', number_control_id);
                    full_price_for_price_and_kol = summ_price * summ_kol;
                    cellName = "D" + counter.ToString();
                    var range2 = ws.get_Range(cellName, cellName);
                    if (zdel.Izdel_id == number_control_id)
                    {  
                        range2.Value2 = ws.Range["D" + counter].Value = 
                        counter_spaces + $"{number_control_id}. " + zdel.Name.ToString();
                    }
                    else
                    {   
                        range2.Value2 = zdel.Name.ToString();
                    }
                    cellName = "E" + counter.ToString();
                    var range = ws.get_Range(cellName, cellName);
                    range.Value2 = zdel.Kol.ToString();
                    cellName = "F" + counter.ToString();
                    var range3 = ws.get_Range(cellName, cellName);
                    range3.Value2 = full_price_for_price_and_kol;
                    cellName= "G" + counter.ToString();
                    var range4 = ws.get_Range(cellName, cellName);
                    range4.Value2 =zdel.Price.ToString();

                    full_price_for_all += full_price_for_price_and_kol;
                    ++counter;
                }
                
                ws.Range["F2"].Value = full_price_for_all;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            wb.SaveAs(path + "Техническое задание(Куницин).xlsx", 
                Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                    misValue,
                misValue, misValue, misValue,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                misValue, misValue, misValue, misValue, misValue);

                wb.Close(true, misValue, misValue);
                app.Quit();
                MessageBox.Show("Файл создан");
        }
    }
}
