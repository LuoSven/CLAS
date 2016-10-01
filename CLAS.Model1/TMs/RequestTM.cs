using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    public class RequestTM
    {
        public string Key { get; set; }

        public DateTime SendTime { get; set; }

        /// <summary>
        /// 本地脚本更新时间，服务器要根据这个时间来进行判断是否要发送脚本更新
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public string Message { get; set; }
    }
}
