using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public class BidderScreenCutDTO
    {
        public int id { get; set; }
        public int BidderId { get; set; } 
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public System.DateTime UploadTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
