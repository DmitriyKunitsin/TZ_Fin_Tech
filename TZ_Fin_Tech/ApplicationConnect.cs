using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ_Fin_Tech
{
    internal class ApplicationConnect
    {
        public SQLiteConnection myConnection;

        public static Izdel izdel = new Izdel();
        public static Links link = new Links();
        public ApplicationConnect()
        {
            myConnection= new SQLiteConnection("Data source=FinTech.sqlite3");

            if (!File.Exists("./FinTech.sqlite3"))
            {
                SQLiteConnection.CreateFile("./FinTech.sqlite3");
                Console.WriteLine("Data Base File Create");
            }
            else
            {
                Console.WriteLine("The file is present");
            }
        }
        public void OpenConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
