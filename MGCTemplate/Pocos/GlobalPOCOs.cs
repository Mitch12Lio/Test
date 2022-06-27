using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCTemplate.Pocos
{



    public class GlobalPOCOs
    {

    }


    public class Annual 
    {
        public int Id { get; set; }
        public int Year { get; set; }

    }

    public class Event
    {
        public int Id { get; set; }
        public string Holiday { get; set; }

    }
}
