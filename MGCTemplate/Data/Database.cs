using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Input;

namespace MGCTemplate.Data
{
    public class Database
    {
        public class DatabaseCode
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        private static String databaseLocation = Properties.Settings.Default.DatabasePath;
        public static String DatabaseLocation
        {
            get
            {
                return databaseLocation;
            }
            set
            {
                databaseLocation = value;
            }
        }



        public DatabaseCode IsSQLite(string pathName)
        {
            DatabaseCode dbCode = new DatabaseCode();

            byte[] bytes = new byte[17];
            using (System.IO.FileStream fs = new System.IO.FileStream(pathName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                fs.Read(bytes, 0, 16);
            }
            string chkStr = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
            //isSqlite = chkStr.Contains("SQLite format");

            if (chkStr.Contains("SQLite format"))
            {
                dbCode.code = 1;
                dbCode.message = "Database Selected.";
            }
            else 
            {
                dbCode.code = -1;
                dbCode.message = "Database Not Selected.  Improper format.";
            }

            return dbCode;
        }

        public DatabaseCode CreateDB(string DatabasePath, string MindGameDatabaseName)
        {
            DatabaseCode dbCode = new DatabaseCode();
            try
            {
                int success = -1;
                string cs = @"Data Source=" + System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);

                using (var con = new Microsoft.Data.Sqlite.SqliteConnection())
                {
                    con.ConnectionString = cs;
                    con.Open();

                    using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandText = "DROP TABLE IF EXISTS MindGames";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"CREATE TABLE MindGames(Id INTEGER PRIMARY KEY,Year TEXT,Team TEXT,Event TEXT,Hint TEXT,Answer TEXT)";
                        success = cmd.ExecuteNonQuery();
                    }
                }

                if (success > -1)
                {
                    dbCode.code = 0;
                    dbCode.message = "Database successfully created.";
                    //CurrentDatabase = System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);
                    //return System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);

                }
                else
                {
                    dbCode.code = -1;
                    dbCode.message = "Error adding table to database.";
                    //return System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);
                    //StatusMessage = "Error adding table to database.";
                }
            }
            catch (Exception ex)
            {
                dbCode.code = -1;
                dbCode.message = ex.Message;
                //StatusMessage = ex.Message;
            }

            return dbCode;
        }

    }
}
