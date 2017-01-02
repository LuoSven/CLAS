using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.SMs
{
    public class ScreenCutRecordSM
    {
        /// <summary>
        /// 拍手编号
        /// </summary>
        public string BidderId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }


    }
}
