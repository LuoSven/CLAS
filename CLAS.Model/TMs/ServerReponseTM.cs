using CLAS.Common;
using CLAS.Model.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    /// <summary>
    /// 服务器向客户端发送请求的对象
    /// </summary>
    public class ServerReponseTM
    {
        /// <summary>
        /// 服务器命令的类型,更新指令或者更新脚本
        /// </summary>
        public ServerCommandType CommandType { get; set; }
        /// <summary>
        /// 要同步的策略
        /// </summary>
        public TacticsTM Tactics { get; set; }
        /// <summary>
        /// 要同步的脚本
        /// </summary>
        public List<ScriptExecuteTM> Scripts { get; set; } 

        /// <summary>
        /// 用来表示发送的时间的
        /// </summary>
        public DateTime SendTime { get; set; } 
    }
}
