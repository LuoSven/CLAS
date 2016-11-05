using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Model.TMs;
using CLAS.Model.VMs;

namespace CLAS.Model.VMs
{
    /// <summary>
    /// 策略，调用脚本
    /// </summary>
    public class TacticsVM
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
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public  DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        private List<ScriptExecuteVM> scripts;
        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        public List<ScriptExecuteVM> Scripts
        {
            get
            {
                if (scripts == null)
                {
                    scripts = new List<ScriptExecuteVM>();
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
