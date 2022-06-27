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

namespace MGCTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModels.Master Master { get; set; }
        public ViewModels.Master master = new ViewModels.Master();
        public MainWindow()
        {

            InitializeComponent();

            Master = master;
            this.DataContext = this;

           

            Master.Initialize();
            Loaded += delegate { Master.OnLoaded(); };
            Unloaded += delegate { Master.OnUnloaded(); };
        }
    }
}
