using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public  class KeyDownRecordDTO
    {
        public int id { get; set; }
        public int BidderId { get; set; }
        public int Key { get; set; }
        public System.DateTime KeyDownTime { get; set; }
        public bool? IsEffictive { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
