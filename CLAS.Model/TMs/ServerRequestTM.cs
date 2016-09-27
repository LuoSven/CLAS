using CLAS.Common;
using CLAS.Model.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    public class ServerRequestTM
    {
        /// <summary>
        /// 服务器命令的类型,更新指令或者更新脚本
        /// </summary>
        public ServerCommandType CommandType { get; set; }
        /// <summary>
        /// 要更新的策略
        /// </summary>
        public TacticsVM Tactics { get; set; }
        /// <summary>
        /// 要更新的脚本
        /// </summary>
        public List<ScriptVM> Scripts { get; set; }

        /// <summary>
        /// 用来表示发送的时间的
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}
