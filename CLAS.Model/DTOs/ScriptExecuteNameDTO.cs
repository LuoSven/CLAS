using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.DTOs
{
    public class ScriptExecuteNameDTO
    {

        public int ScriptId { get; set; }

        public string SciptName { get; set; }
        public System.DateTime CreateDate { get; set; } 
        public Nullable<System.DateTime> ModifyDate { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? ExecuteTime { get; set; }
        /// <summary>
        /// 执行条件
        /// </summary>
        public string ExecuteCondition { get; set; } 
        /// <summary>
        /// 脚本指令集
        /// </summary>
        public string ExecuteInstructions { get; set; }
        /// <summary>
        /// 验证脚本
        /// </summary>
        public string CheckInstruction { get; set; }
    }
}
