using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
   public class KeyDownRecordGroupVM
    {
        public int BidderId { get; set; }

        public string BidderName { get; set; }

        public List<KeyDownRecordGroupDetailVM> Details { get; set; }
    }
}
