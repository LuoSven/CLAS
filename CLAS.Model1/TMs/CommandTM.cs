using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    public class CommandTM
    {
      
        public CommandTM() { }

        public CommandTM(DateTime timeKey, List<string> scripts)
        {
            this.TimeKey = timeKey;
            this.Scripts = scripts;
        }

        public string ScriptName { get; set; }
        /// <summary>
        /// 执行的脚本的时间
        /// </summary>
        public DateTime TimeKey { get; set; } 

       
        /// <summary>
        /// 要执行的脚本
        /// </summary>
        public List<string> Scripts { get; set; }
    }
}
