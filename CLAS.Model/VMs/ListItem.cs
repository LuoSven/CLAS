using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
     public  class ListItem
    {

        public string id { get; set; }
        public string name { get; set; }

        public List<ListItem> items { get; set; }
    }
}
