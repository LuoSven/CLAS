using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.SMs
{
    public class ScriptSM
    {
        /// <summary>
        /// 名称，描述
        /// </summary>
        public string KeyWords { get; set; }


        public int? ScriptId { get; set; }

        public int? BidderId { get; set; }

        public int? Express { get; set; }
    }
}
