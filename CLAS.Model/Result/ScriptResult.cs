using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.Result
{
    public class ScriptResult
    {
        public bool IsSucceed { get; set; }
        /// <summary>
        /// 是否跳出循环
        /// </summary>
        public bool IsBreak { get; set; }

        /// <summary>
        /// 每个命令的执行记录
        /// </summary>
        public List<string> Logs { get; set; }

        /// <summary>
        /// 执行的参数集合，其实就是传入的variables
        /// </summary>
        public Dictionary<string, object> Results { get; set; }
    }
}
