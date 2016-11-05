using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    /// <summary>
    /// 脚本，由指令组成，由策略调用
    /// </summary>
     public  class ScriptTM
    {
        /// <summary>
        /// 脚本Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 执行指令
        /// </summary>
        public string ExecuteExpressions { get; set; }


        /// <summary>
        /// 校验指令
        /// </summary>
        public string CheckInstruction { get; set; }
    }
}
