using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.TMs
{
    /// <summary>
    /// 截图上传TM
    /// </summary>
    public class ScreenCutTM
    {
        /// <summary>
        /// 脚本Id
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// 执行指令
        /// </summary>
        public DateTime UploadTime { get; set; } 
    }
}
