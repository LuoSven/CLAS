using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
    /// <summary>
    /// 脚本，由指令组成，由策略调用
    /// </summary>
     public  class ScriptVM
    {
        /// <summary>
        /// 脚本Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 脚本名称
        /// </summary>
        public int ScritpName { get; set; }

        /// <summary>
        /// 执行指令
        /// </summary>
        public List<string> ExecuteInstructions { get; set; }


        /// <summary>
        /// 校验指令
        /// </summary>
        public string CheckInstruction { get; set; }
    }
}
