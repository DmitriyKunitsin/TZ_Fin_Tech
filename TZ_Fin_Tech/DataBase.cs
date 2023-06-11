﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.RightsManagement;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Shapes;

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

            string whereAcc = $"SELECT  Izdel.Name, IZDEL.Price , links.kol , IzdelUp_id FROM Links inner JOIN Izdel ON parent_id = links.parent AND izdelUP_id == links.IzdelUp AND  parent_id  = {parent}  AND id = links.name_id ;";
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
                    Kol = reader.GetInt32(2) as int? ?? default(int),
                    IzdelUP_id = reader.GetInt32(3) as int? ?? default(int)
                });
            }
            return par;
        }
        public IList<Parent> Output_all_Data_Base()
        {
            ApplicationConnect whereAccount = new ApplicationConnect();

            string whereAcc = $"SELECT  Izdel.Name, IZDEL.Price , links.kol , IzdelUp_id FROM Links inner JOIN Izdel ON  izdelUP_id == links.IzdelUp AND id = links.name_id ORDER BY parent_id ;";
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
                    Kol = reader.GetInt32(2) as int? ?? default(int),
                    IzdelUP_id = reader.GetInt32(3) as int? ?? default(int)
                });
            }
            return par;
        }

        public void Inset_data_base_two_table(string Name,int kol ,int price, int IzdelUP_id, int izdel_id, int parent_id)
        {
            int id = 0;
            ApplicationConnect connect = new ApplicationConnect();
            SQLiteDataReader reader = null;
            string add_table_one = "INSERT INTO Izdel ('Name', 'Price', 'IzdelUp_id','izdel_id','parent_id') VALUES (@Name, @Price, @IzdelUp_id, @izdel_id, @parent_id)";
            SQLiteCommand myCommand = new SQLiteCommand(add_table_one, connect.myConnection);
            connect.OpenConnection();
            myCommand.Parameters.AddWithValue("@Name", Name);
            myCommand.Parameters.AddWithValue("@Price", price);
            myCommand.Parameters.AddWithValue("@IzdelUp_id", IzdelUP_id);
            myCommand.Parameters.AddWithValue("@izdel_id", izdel_id);
            myCommand.Parameters.AddWithValue("@parent_id", parent_id);
            var resault = myCommand.ExecuteNonQuery();
            string search_id_name = $"SELECT id FROM Izdel WHERE Name='{Name}'";
            SQLiteCommand comand_id_name = new SQLiteCommand(search_id_name, connect.myConnection);
            connect.OpenConnection();
            reader = comand_id_name.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            string add_data_table_two = "INSERT INTO Links ('IzdelUp', 'Izdel', 'kol','parent','name_id') VALUES (@IzdelUp, @Izdel, @kol, @parent, @name_id)";
            SQLiteCommand comand_add_two_table = new SQLiteCommand(add_data_table_two, connect.myConnection);
            connect.OpenConnection();
            comand_add_two_table.Parameters.AddWithValue("@IzdelUp", IzdelUP_id);
            comand_add_two_table.Parameters.AddWithValue("@Izdel", izdel_id);
            comand_add_two_table.Parameters.AddWithValue("@kol", kol);
            comand_add_two_table.Parameters.AddWithValue("@parent", parent_id);
            comand_add_two_table.Parameters.AddWithValue("@name_id", id);
            resault = comand_add_two_table.ExecuteNonQuery();
            connect.CloseConnection();
        }
    }
}
      