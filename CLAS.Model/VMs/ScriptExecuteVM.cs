using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;
using CLAS.Model.VMs;

namespace CLAS.Model.TMs
{
    /// <summary>
    ///包含脚本和执行的条件时间等信息
    /// </summary>
    public class ScriptExecuteVM
    {


        public int Id { get; set; }

        public int ScriptId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 执行的脚本
        /// </summary>
        public ScriptVM Script { get; set; }


        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime? ExecuteTime { get; set; }

        public string ExecuteTimeName
        {
            get { return ExecuteTime.ToTime(); }
        }

        /// <summary>
        /// 触发条件
        /// </summary>
        public string ExecuteCondition { get; set; }


        /// <summary>
        /// 触发条件
        /// </summary>
        public string ExecuteConditionName { get; set; }
    }
}
