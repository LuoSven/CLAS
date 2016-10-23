using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public  class ScriptExecuteDTO
    {

        public int ScriptId { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
        /// <summary>
        /// 脚本指令集
        /// </summary>
        public string ExecuteInstructions { get; set; }
        /// <summary>
        /// 脚本指令集
        /// </summary>
        public string CheckInstruction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SyncStopTimeBegin { get; set; }
        /// <summary>
        /// 脚本指令集
        /// </summary>
        public DateTime? SyncStopTimeStop { get; set; }
    }
}
