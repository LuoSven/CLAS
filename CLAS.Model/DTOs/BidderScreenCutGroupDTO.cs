using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public class BidderScreenCutGroupDTO
    {
        public int id { get; set; }
        public int BidderId { get; set; }
        public int BidderName { get; set; }
        public List<BidderScreenCutDTO> List { get; set; }
    }
}
