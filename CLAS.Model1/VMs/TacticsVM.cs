using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
    /// <summary>
    /// 策略，调用脚本
    /// </summary>
    public  class TacticsVM
    {
        /// <summary>
        /// 策略编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 策略名称
        /// </summary>
        public string TacticsName { get; set; }
        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        public Dictionary<DateTime, ScriptVM> Scripts { get; set; }
    }
}
