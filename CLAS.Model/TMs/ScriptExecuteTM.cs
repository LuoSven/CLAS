using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    /// <summary>
    ///包含脚本和执行的条件时间等信息
    /// </summary>
     public  class ScriptExecuteTM
    { 

        /// <summary>
        /// 执行的脚本
        /// </summary>
        public ScriptTM Script { get; set; }


        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? ExecuteTime { get; set; }

        /// <summary>
        /// 执行条件
        /// </summary>
        public string ExecuteCondition { get; set; }
    }
}
