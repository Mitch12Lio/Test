using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCTemplate.Pocos
{
    public class SDEntity
    {
        public int ID { get; set; }
        public bool CopyAll { get; set; }
        public string SourceFile { get; set; }
        public string SourcePath { get; set; }
        public string DestinationGCDocs { get; set; }

    }
}
