using CLAS.Model.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    public class ClientRequestTM
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 本地脚本最后更新时间，服务器要根据这个时间来进行判断是否要发送脚本更新
        /// </summary>
        public DateTime? ScriptLastUpdateTime { get; set; }

        /// <summary>
        /// 策略更新时间
        /// </summary>
        public DateTime? TacticsLastUpdateTime { get; set; }

        public List<ScriptExecuteRecordVM> ScriptExecuteRecords { get; set; }
    }
}
