using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
    /// <summary>
    /// 激活vm
    /// </summary>
    public class ActivationVM
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ActivationCode { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        public string SystemInfo { get; set; }
    }
}
