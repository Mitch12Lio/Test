using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MGCTemplate.ViewModels
{
    public class Master : mvvmHandlers
    {
        #region "Properties"

        public ViewModels.Clues.ReverseAlphabetViewModel RacViewModel { get; set; }
        public ViewModels.Clues.ReverseAlphabetViewModel racViewModel = new ViewModels.Clues.ReverseAlphabetViewModel();

        public ViewModels.Setup.SetupViewModel SetupViewModel { get; set; }
        public ViewModels.Setup.SetupViewModel setupViewModel = new ViewModels.Setup.SetupViewModel();

        public ViewModels.Nodes.NodeSelectingViewModel NodeSelectingViewModel { get; set; }
        public ViewModels.Nodes.NodeSelectingViewModel nodeSelectingViewModel = new ViewModels.Nodes.NodeSelectingViewModel();

        public Data.Database DB { get; set; }
        public Data.Database db = new Data.Database();



        private string statusMessage = "Ready";
        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                NotifyPropertyChanged("StatusMessage");
            }
        }

        private string gridAlphabetWord = string.Empty;
        public string GridAlphabetWord
        {
            get
            {
                return gridAlphabetWord;
            }
            set
            {
                gridAlphabetWord = value;
                CreateGridAlphabetClue();
                NotifyPropertyChanged("GridAlphabetWord");
            }
        }

        private string gridAlphabetWordClue = string.Empty;
        public string GridAlphabetWordClue
        {
            get
            {
                return gridAlphabetWordClue;
            }
            set
            {
                gridAlphabetWordClue = value;
                NotifyPropertyChanged("GridAlphabetWordClue");
            }
        }


        private System.Data.DataTable gridAlphabeteDT = new System.Data.DataTable();
        public System.Data.DataTable GridAlphabeteDT
        {
            get
            {
                return gridAlphabeteDT;
            }
            set
            {
                gridAlphabeteDT = value;
                NotifyPropertyChanged("GridAlphabeteDT");
            }
        }



        //private string currentFolderPath = Properties.Settings.Default.CurrentFolderPath;
        //public string CurrentFolderPath
        //{
        //    get
        //    {
        //        return currentFolderPath;
        //    }
        //    set
        //    {
        //        currentFolderPath = value;
        //        Properties.Settings.Default.CurrentFolderPath = value;
        //        SaveProperties();
        //        NotifyPropertyChanged("CurrentFolderPath");
        //    }
        //}

        //private string currentFilePath = Properties.Settings.Default.CurrentFilePath;
        //public string CurrentFilePath
        //{
        //    get
        //    {
        //        return currentFilePath;
        //    }
        //    set
        //    {
        //        currentFilePath = value;
        //        Properties.Settings.Default.CurrentFilePath = value;
        //        SaveProperties();
        //        NotifyPropertyChanged("CurrentFilePath");
        //    }
        //}

        //private string currentDatabase = Properties.Settings.Default.CurrentDatabase;
        //public string CurrentDatabase
        //{
        //    get
        //    {
        //        return currentDatabase;
        //    }
        //    set
        //    {
        //        currentDatabase = value;
        //        Properties.Settings.Default.CurrentDatabase = value;
        //        SaveProperties();
        //        NotifyPropertyChanged("CurrentDatabase");
        //    }
        //}

        //private string databasePath = Properties.Settings.Default.DatabasePath;
        //public string DatabasePath
        //{
        //    get
        //    {
        //        return databasePath;
        //    }
        //    set
        //    {
        //        databasePath = value;
        //        Properties.Settings.Default.DatabasePath = value;
        //        SaveProperties();
        //        NotifyPropertyChanged("DatabasePath");
        //    }
        //}


        //private string mindGameDatabaseName = String.Empty;
        //public string MindGameDatabaseName
        //{
        //    get
        //    {
        //        return mindGameDatabaseName;
        //    }
        //    set
        //    {
        //        mindGameDatabaseName = value;
        //        NotifyPropertyChanged("MindGameDatabaseName");
        //    }
        //}



        #endregion //Properties

        #region "Constructor"
        public Master()
        {
            RacViewModel = racViewModel;
            SetupViewModel = setupViewModel;
            NodeSelectingViewModel = nodeSelectingViewModel;
            DB = db;

            GridAlphabeteDT = CreateGrid();
        }

        #endregion //Contructor

        #region "Functions"

        #region "************************************************************************************************* View Life Cycle"

        private bool _isLoaded = false;

        public void Initialize()
        {
            // TODO: Add your initialization code here 
            // This method is only called when the application is running
        }

        public void OnLoaded()
        {
            if (!_isLoaded)
            {
                // TODO: Add your loaded code here 
                _isLoaded = true;
            }
        }

        public void OnUnloaded()
        {
            if (_isLoaded)
            {
                // TODO: Add your cleanup/unloaded code here 
                _isLoaded = false;
            }
        }

        #endregion

       

        private ICommand createGridAlphabetClueCommand;
        public ICommand CreateGridAlphabetClueCommand
        {
            get
            {
                return createGridAlphabetClueCommand ?? (createGridAlphabetClueCommand = new CommandHandler(() => CreateGridAlphabetClue(), _canExecute));
            }
        }
        private void CreateGridAlphabetClue()
        {
            if (GridAlphabetWord != string.Empty)
            {
                GridAlphabetWordClue = string.Empty;
                string kdkdk = GridAlphabetWord;
                string currentCharacter = string.Empty;
                foreach (char chareacter in kdkdk)
                {
                    currentCharacter = chareacter.ToString().ToUpper();
                    //if (Char.ToUpper(chareacter) == 'Z')
                    if ((chareacter >= 'A' && chareacter < 'Z') || (chareacter >= 'a' && chareacter < 'z'))
                    {
                        foreach (System.Data.DataRow dr in GridAlphabeteDT.Rows)
                        {
                            foreach (System.Data.DataColumn dc in GridAlphabeteDT.Columns)
                            {
                                if (Convert.ToChar(dr[dc]) == Char.ToUpper(chareacter))
                                {
                                    GridAlphabetWordClue += dc.ColumnName + ":" + (GridAlphabeteDT.Rows.IndexOf(dr) + 1).ToString() + " ";
                                    break;
                                }
                            }
                        }
                    }
                    else 
                    {
                        GridAlphabetWordClue += chareacter.ToString() + " ";

                    }
                }
            }
        }
        private System.Data.DataTable CreateGrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Columns.Add("D");
            dt.Columns.Add("E");
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();

            char c = 'A';
            while (c <= 'Z')
            {
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        dr[dc] = c.ToString();
                        c++;
                    }
                }
                c++;
            }
            return dt;
        }

        private ICommand openMenuItemCommand;
        public ICommand OpenMenuItemCommand
        {
            get
            {
                return openMenuItemCommand ?? (openMenuItemCommand = new CommandHandler(() => OpenMenuItem(), _canExecute));
            }
        }
        private void OpenMenuItem()
        {

            if (System.Windows.MessageBox.Show("Open Menu Item?", "Confirm", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
            {
                System.Windows.MessageBox.Show("Yes");
            }
            else
            {
                System.Windows.MessageBox.Show("No");
            }

        }

       

        private ICommand selectDBInitCommand;
        public ICommand SelectDBInitCommand
        {
            get
            {
                return selectDBInitCommand ?? (selectDBInitCommand = new CommandHandler(() => SelectDBInit(), _canExecute));
            }
        }
        public void SelectDBInit()
        {
            string selectDatabasePath = "SelectDatabasePath";
            NodeSelectingViewModel.SetFile(selectDatabasePath);
            if (DB.IsSQLite(NodeSelectingViewModel.CurrentFilePath).code > -1)
            {
                NodeSelectingViewModel.CurrentDatabase = NodeSelectingViewModel.CurrentFilePath;
            }
            else
            {
                StatusMessage = "Database Not Selected.  Improper format.";
            }
        }
        private ICommand createDBInitCommand;
        public ICommand CreateDBInitCommand
        {
            get
            {
                return createDBInitCommand ?? (createDBInitCommand = new CommandHandler(() => CreateDBInit(), _canExecute));
            }
        }
        public void CreateDBInit()
        {
            if (NodeSelectingViewModel.MindGameDatabaseName == String.Empty)
            {
                //Master.StatusMessage = "A database name is required.";
                StatusMessage = "A database name is required.";
            }
            else if (NodeSelectingViewModel.DatabasePath == String.Empty)
            {
                StatusMessage = "A database path is required.";
            }
            else if (!System.IO.Directory.Exists(NodeSelectingViewModel.DatabasePath))
            {
                StatusMessage = "A valid database path is required.";
            }
            else
            {
                //bool dropDB = true;
                if (System.IO.File.Exists(System.IO.Path.Combine(NodeSelectingViewModel.DatabasePath, NodeSelectingViewModel.MindGameDatabaseName)))
                {
                    if (System.Windows.MessageBox.Show("Do you want to replace " + NodeSelectingViewModel.MindGameDatabaseName + "?", "Confirm", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
                    {
                        CreateDB();
                    }  //Yes                
                    else  //No
                    {

                        StatusMessage = "A database with that name already exists.";
                    }
                }
                else
                {
                    CreateDB();
                }
            }
        }

        private void CreateDB()
        {
            StatusMessage = DB.CreateDB(NodeSelectingViewModel.DatabasePath, NodeSelectingViewModel.MindGameDatabaseName).message;
        }

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
        //        StatusMessage = "A database name is required.";
        //    }
        //    else if (DatabasePath == String.Empty)
        //    {
        //        StatusMessage = "A database path is required.";
        //    }
        //    else if (!System.IO.Directory.Exists(DatabasePath))
        //    {
        //        StatusMessage = "A valid database path is required.";
        //    }
        //    else
        //    {
        //        //bool dropDB = true;
        //        if (System.IO.File.Exists(System.IO.Path.Combine(DatabasePath, MindGameDatabaseName)))
        //        {
        //            if (System.Windows.MessageBox.Show("Do you want to replace " + MindGameDatabaseName + "?", "Confirm", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
        //            { CreateDB(); }  //Yes                
        //            else  //No
        //            { StatusMessage = "A database with that name already exists."; }
        //        }
        //        else
        //        { CreateDB(); }
        //    }
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
        //            StatusMessage = "Database Created.";
        //        }
        //        else
        //        {
        //            StatusMessage = "Error adding table to database.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = ex.Message;
        //    }
        //}

        //private ICommand inventDatabaseNameCommand;
        //public ICommand InventDatabaseNameCommand
        //{
        //    get
        //    {
        //        return inventDatabaseNameCommand ?? (inventDatabaseNameCommand = new CommandHandler(() => InventDatabaseName(), _canExecute));
        //    }
        //}
        //private void InventDatabaseName()
        //{

        //    MindGameDatabaseName = "MindGameDB_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".db";

        //}

        #endregion //Commands

    }
}
