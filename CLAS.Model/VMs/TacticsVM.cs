using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;
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


        public int PriceScript { get; set; }

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


        public string SyncStopTimeBeginName
        {
            get { return SyncStopTimeBegin.ToTime(); }
        }

        /// <summary>
        /// 停止同步时间-结束
        /// </summary>
        public DateTime? SyncStopTimeStop { get; set; }
        /// <summary>
        /// 停止同步时间-结束
        /// </summary>
        public string SyncStopTimeStopName
        {
            get { return SyncStopTimeBegin.ToTime(); }
        }

        /// <summary>
        /// 只是用来给mvvm用的 
        /// </summary>
        public ScriptExecuteVM tempScript = new ScriptExecuteVM() { Script =new ScriptVM()};

        /// <summary>
        /// 下拉选择的脚本
        /// </summary>
        public List<ListItem> ScriptSelectItems { get; set; }


        /// <summary>
        /// 符号选择
        /// </summary>
        public List<ListItem> ExpressSelectItems { get; set; } 
    }
}
