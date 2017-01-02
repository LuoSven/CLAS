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
        private List<ScriptExecuteTM> scripts;
        /// <summary>
        /// 要执行的脚本列表
        /// </summary>
        public List<ScriptExecuteTM> Scripts
        {
            get
            {
                if (scripts == null)
                {
                    scripts = new List<ScriptExecuteTM>();
                }
                return scripts;
            }
            set
            {
                scripts = value;
            }
        } 

        /// <summary>
        /// 价格脚本
        /// </summary>
        public string PriceScript { get; set; }



        /// <summary>
        /// 获取时间的脚本，51用
        /// </summary>
        public string TimeScript { get; set; }
        /// <summary>
        /// 停止同步时间-开始
        /// </summary>
        public DateTime? SyncStopTimeBegin { get; set; }
        /// <summary>
        /// 停止同步时间-结束
        /// </summary>
        public DateTime? SyncStopTimeStop { get; set; }

        /// <summary>
        ///加价价格
        /// </summary>
        public int? AddPrice { get; set; }
        /// <summary>
        /// 差多少提交
        /// </summary>
        public int? DownReducePrice { get; set; }
        /// <summary>
        /// 延时时间（毫秒）
        /// </summary>
        public int? DelayTime { get; set; }
    }
}
