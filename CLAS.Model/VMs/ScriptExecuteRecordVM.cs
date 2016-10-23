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
    public class ScriptExecuteRecordVM
    {
         /// <summary>
         /// 脚本编号
         /// </summary>
        public int ScriptId { get; set; }
        /// <summary>
        /// 脚本执行开始时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
         /// <summary>
         /// 脚本执行开始时间
         /// </summary>
         public DateTime ActualExecutionStartTime { get; set; }
         /// <summary>
         /// 脚本执行结束时间
         /// </summary>
         public DateTime ActualExecutionEndTime { get; set; }
         /// <summary>
         /// 脚本执行时间，毫秒
         /// </summary>
         public int ExecuteMSec { get; set; }

         /// <summary>
         /// 脚本执行信息
         /// </summary>
         public string Message { get; set; }

         /// <summary>
         /// 脚本是否执行成功，目前脚本后面叫加一个验证函数
         /// </summary>
         public bool IsSucceed { get; set; }
    }
}
