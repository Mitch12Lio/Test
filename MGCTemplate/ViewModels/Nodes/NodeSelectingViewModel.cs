using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MGCTemplate.ViewModels.Nodes
{
    public class NodeSelectingViewModel : mvvmHandlers
    {
        #region "Properties"


        public Data.Database DB { get; set; }
        public Data.Database db = new Data.Database();


        private string currentFolderPath = Properties.Settings.Default.CurrentFolderPath;
        public string CurrentFolderPath
        {
            get
            {
                return currentFolderPath;
            }
            set
            {
                currentFolderPath = value;
                Properties.Settings.Default.CurrentFolderPath = value;
                SaveProperties();
                NotifyPropertyChanged("CurrentFolderPath");
            }
        }

        private string currentFilePath = Properties.Settings.Default.CurrentFilePath;
        public string CurrentFilePath
        {
            get
            {
                return currentFilePath;
            }
            set
            {
                currentFilePath = value;
                Properties.Settings.Default.CurrentFilePath = value;
                SaveProperties();
                NotifyPropertyChanged("CurrentFilePath");
            }
        }

        private string currentDatabase = Properties.Settings.Default.CurrentDatabase;
        public string CurrentDatabase
        {
            get
            {
                return currentDatabase;
            }
            set
            {
                currentDatabase = value;
                Properties.Settings.Default.CurrentDatabase = value;
                SaveProperties();
                NotifyPropertyChanged("CurrentDatabase");
            }
        }

        private string databasePath = Properties.Settings.Default.DatabasePath;
        public string DatabasePath
        {
            get
            {
                return databasePath;
            }
            set
            {
                databasePath = value;
                Properties.Settings.Default.DatabasePath = value;
                SaveProperties();
                NotifyPropertyChanged("DatabasePath");
            }
        }

        private string mindGameDatabaseName = String.Empty;
        public string MindGameDatabaseName
        {
            get
            {
                return mindGameDatabaseName;
            }
            set
            {
                mindGameDatabaseName = value;
                NotifyPropertyChanged("MindGameDatabaseName");
            }
        }


        #endregion

        public NodeSelectingViewModel()
        {
            DB = db;
            SetFileCommand = new RelayCommand(SetFile, param => this._canExecute);
            SetFolderCommand = new RelayCommand(SetFolder, param => this._canExecute);
        }

        #region "Function"
        //private bool IsSQLite(string pathName)
        //{
        //    byte[] bytes = new byte[17];
        //    using (System.IO.FileStream fs = new System.IO.FileStream(pathName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
        //    {
        //        fs.Read(bytes, 0, 16);
        //    }
        //    string chkStr = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
        //    return chkStr.Contains("SQLite format");
        //}

        //private void CreateDB()
        //{
        //    try
        //    {
        //        int success = -1;
        //        string cs = @"Data Source=" + System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);

        //        using (var con = new Microsoft.Data.Sqlite.SqliteConnection())
        //        {
        //            con.ConnectionString = cs;
        //            con.Open();



        //            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand())
        //            {
        //                cmd.Connection = con;

        //                cmd.CommandText = "DROP TABLE IF EXISTS MindGames";
        //                cmd.ExecuteNonQuery();

        //                cmd.CommandText = @"CREATE TABLE MindGames(Id INTEGER PRIMARY KEY,Year TEXT,Team TEXT,Event TEXT,Hint TEXT,Answer TEXT)";
        //                success = cmd.ExecuteNonQuery();
        //            }
        //        }

        //        if (success > -1)
        //        {
        //            CurrentDatabase = System.IO.Path.Combine(DatabasePath, MindGameDatabaseName);
        //            //StatusMessage = "Database Created.";
        //        }
        //        else
        //        {

        //            //StatusMessage = "Error adding table to database.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //StatusMessage = ex.Message;
        //    }
        //}
        
        
        #endregion

        #region "Commands"

        //private ICommand createDBCommandInit;
        //public ICommand CreateDBInitCommand
        //{
        //    get
        //    {
        //        return createDBCommandInit ?? (createDBCommandInit = new CommandHandler(() => CreateDBInit(), _canExecute));
        //    }
        //}
        //private void CreateDBInit()
        //{
        //    if (MindGameDatabaseName == String.Empty)
        //    {
        //        //Master.StatusMessage = "A database name is required.";
        //        //StatusMessage = "A database name is required.";
        //    }
        //    else if (DatabasePath == String.Empty)
        //    {
        //        //StatusMessage = "A database path is required.";
        //    }
        //    else if (!System.IO.Directory.Exists(DatabasePath))
        //    {
        //        //Master.StatusMessage = "A valid database path is required.";
        //        //StatusMessage = "A valid database path is required.";
        //    }
        //    else
        //    {
        //        //bool dropDB = true;
        //        if (System.IO.File.Exists(System.IO.Path.Combine(DatabasePath, MindGameDatabaseName)))
        //        {
        //            if (System.Windows.MessageBox.Show("Do you want to replace " + MindGameDatabaseName + "?", "Confirm", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
        //            { CreateDB(); }  //Yes                
        //            else  //No
        //            {

        //                //StatusMessage = "A database with that name already exists.";
        //            }
        //        }
        //        else
        //        {
        //            CreateDB();
        //        }
        //    }
        //}



        private ICommand inventDatabaseNameCommand;
        public ICommand InventDatabaseNameCommand
        {
            get
            {
                return inventDatabaseNameCommand ?? (inventDatabaseNameCommand = new CommandHandler(() => InventDatabaseName(), _canExecute));
            }
        }
        private void InventDatabaseName()
        {

            MindGameDatabaseName = "MindGameDB_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".db";

        }

        private void SaveProperties()
        { Properties.Settings.Default.Save(); }

        private ICommand setFolderCommand;
        public ICommand SetFolderCommand
        {
            get
            {
                return setFolderCommand;
            }
            set
            {
                setFolderCommand = value;
            }
        }
        public void SetFolder(object obj)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            switch (obj.ToString())
            {
                case "CreateDatabasePath":
                    if (DatabasePath != string.Empty)
                    {
                        folderBrowserDialog.SelectedPath = DatabasePath;
                    }
                    break;
                default:
                    break;
            }

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string pathName = openFD.FileName;
                string pathName = folderBrowserDialog.SelectedPath;
                switch (obj.ToString())
                {
                    case "CreateDatabasePath":
                        DatabasePath = pathName;
                        break;
                    default:
                        //StatusMessage = "Save Incomplete.";
                        break;
                }
            }

        }


        private ICommand setFileCommand;
        public ICommand SetFileCommand
        {
            get
            {
                return setFileCommand;
            }
            set
            {
                setFileCommand = value;
            }
        }
        public void SetFile(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFD = new Microsoft.Win32.OpenFileDialog();

            if (openFD.ShowDialog() == true)
            {
                string pathName = openFD.FileName;
                switch (obj.ToString())
                {
                    case "SelectDatabasePath":

                        //if (DB.IsSQLite(pathName).code>-1)
                        //{
                        CurrentFilePath = pathName;
                            //StatusMessage = "Database Selected.";
                        //}
                        //else { StatusMessage = "Database Not Selected.  Improper format."; }
                        break;
                    default:
                        //StatusMessage = "Save Incomplete.";
                        break;
                }
            }
        }

        #endregion
    }
}
