using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;
using System.Windows.Documents;

namespace TZ_Fin_Tech
{
    internal class DataBase
    {

        public void CreatTable_Izdel()
        {
            //добавление таблицы в случае ее отсутсвия ;
            ApplicationConnect applicationConnect = new ApplicationConnect();
            string comandCreat_Izdel = "CREATE TABLE IF NOT EXISTS Izdel " +
                "(id INTEGER PRIMARY KEY, Name VARCHAR(100), Price DECIMAL(20,2))";
            string comandCreat_Links = "CREATE TABLE IF NOT EXISTS Links " +
                "(IzdelUp bigint, Izdel bigint, kol int)";
            SQLiteCommand command_izdel = new SQLiteCommand(comandCreat_Izdel, applicationConnect.myConnection);
            SQLiteCommand command_links = new SQLiteCommand(comandCreat_Links, applicationConnect.myConnection);

            applicationConnect.myConnection.Open();
            command_izdel.ExecuteNonQuery();
            command_links.ExecuteNonQuery();
        }
        public IList<Izdel> table_Info_Izdel()
        {
            //Извлечение информации о товаре из базы данных  


            SQLiteDataReader reader = null;
            ApplicationConnect applicationConnect = new ApplicationConnect();
            string comandInfo_Izdel = "SELECT * FROM Izdel";
            SQLiteCommand qLiteCommand = new SQLiteCommand(comandInfo_Izdel, applicationConnect.myConnection);
            applicationConnect.OpenConnection();
            reader = qLiteCommand.ExecuteReader();
            List<Izdel> izdel = new List<Izdel>();

            while (reader.Read())
            {
                izdel.Add(new Izdel()
                {
                    Id = Convert.ToInt32(reader.GetInt32(0)),
                    Name = Convert.ToString(reader.GetString(1)),
                    Price = Convert.ToInt32(reader.GetDecimal(2)),
                    Parent_id = Convert.ToInt32(reader.GetInt32(3))
                });
            }
            return izdel;

        }
        public IList<Links> table_Info_Links()
        {
            //Извлечение информации о товаре из базы данных  


            SQLiteDataReader reader = null;
            ApplicationConnect applicationConnect = new ApplicationConnect();
            string comandInfo_link = "SELECT * FROM Links";
            SQLiteCommand qLiteCommand = new SQLiteCommand(comandInfo_link, applicationConnect.myConnection);
            applicationConnect.OpenConnection();
            reader = qLiteCommand.ExecuteReader();
            List<Links> link = new List<Links>();

            while (reader.Read())
            {
                link.Add(new Links(0, 0, 0)
                {
                    IzdelUp = reader.GetInt32(0),
                    Izdel = reader.GetInt32(1),
                    Kol = reader.GetInt32(2)
                });
            }
            return link;

        }
        public IList<Parent> Data_Base_Out_User(int parent)
        {
            ApplicationConnect whereAccount = new ApplicationConnect();

            string whereAcc = $"SELECT  Izdel.Name, IZDEL.Price , links.kol " +
                $"FROM Links inner JOIN Izdel ON Izdel = izdel.izdel_id " +
                $"AND IzdelUp_id = IzdelUp AND parent_id = '{parent}'";
            SQLiteCommand command = new SQLiteCommand(whereAcc, whereAccount.myConnection);
            whereAccount.OpenConnection();
            var reader = command.ExecuteReader();
            List<Parent> par = new List<Parent>();
            while (reader.Read())
            {
                par.Add(new Parent()
                {
                    Name = reader.GetString(0),
                    Price = reader.GetInt32(1) as int? ?? default(int),
                    Kol = reader.GetInt32(2) as int? ?? default(int)
                });
            }
            return par;
        }
    }
}
