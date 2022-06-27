using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MGCTemplate.ViewModels.Setup
{
    public class SetupViewModel:mvvmHandlers
    {
        public List<Pocos.Annual> Years { get; set; }

        private Pocos.Annual selectedYear;
        public Pocos.Annual SelectedYear
        {
            get
            {
                return selectedYear;
            }
            set
            {
                selectedYear = value;
                NotifyPropertyChanged("SelectedYear");
            }
        }

        public SetupViewModel()
        {
            Years = new List<Pocos.Annual>();

            int counter = 0;
            for (int i = 2000; i < 2050; i++)
            {
                Years.Add(new Pocos.Annual { Id = counter, Year = i });
                counter++;
            }

            SelectedYear = Years.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault();
        }
    }
}
