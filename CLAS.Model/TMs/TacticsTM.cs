using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Model.VMs;

namespace CLAS.Model.TMs
{
    /// <summary>
    /// 策略，调用脚本
    /// </summary>
    public  class TacticsTM
    {
        /// <summary>
        /// 策略编号
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        private Dictionary<DateTime, ScriptTM> scripts;
        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        public Dictionary<DateTime, ScriptTM> Scripts
        {
            get
            {
                if (scripts == null)
                {
                    scripts = new Dictionary<DateTime, ScriptTM>();
                }
                return scripts;
            }
            set
            {
                scripts = value;
            }
        }
        /// <summary>
        /// 停止同步时间-开始
        /// </summary>
        public DateTime? SyncStopTimeBegin { get; set; }
        /// <summary>
        /// 停止同步时间-结束
        /// </summary>
        public DateTime? SyncStopTimeStop { get; set; }
    }
}
