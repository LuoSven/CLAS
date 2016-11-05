using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.Result
{
    public  class InstruqctionResult
    {
        public bool IsSucceed { get; set; }
        /// <summary>
        /// 是否跳出循环
        /// </summary>
        public bool IsBreak { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        /// <summary>
        /// 参数列表，回传用
        /// </summary>
        public Dictionary<string, object> Variables { get; set; }
    }
}
