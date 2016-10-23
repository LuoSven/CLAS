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

        public List<string> Logs { get; set; } 
    }
}
