using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
    /// <summary>
    /// 用来记录脚本的执行时间
    /// </summary>
    public class ScriptExecuteRecordDetailVM
    {
        public int Id { get; set; }
        public int ScriptId { get; set; }
        public string ScriptName { get; set; }
        public DateTime ExecuteTime { get; set; }
        public DateTime ActualExecutionStartTime { get; set; }
        public DateTime ActualExecutionEndTime { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
        public string BidderName { get; set; }
        public int BidderId { get; set; }
        public int? ExecuteMSec { get; set; }

    }
}
