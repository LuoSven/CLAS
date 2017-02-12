using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public  class BidderlogTM
    {

        public int Id { get; set; }
        public int BidderId { get; set; }
        public string  Message { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime CreateTime { get; set; } 
    }
}
