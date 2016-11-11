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
        public int ScritpId { get; set; }

        /// <summary>
        /// 脚本名称
        /// </summary>
        public int ScritpName { get; set; }

        /// <summary>
        /// 指令列表
        /// </summary>
        public List<string> Instructions { get; set; }
    }
}
