using CLAS.Model.TMs;
using CLAS.Common;
using CLAS.Model.Entities;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CLAS.Model.VMs;
using EM.Utils;

namespace CASL.Bll
{
    /// <summary>
    ///验证码管理
    /// </summary>
    public class ActivationCodeManager
    {
       /// <summary>
       /// 验证码
       /// </summary>
       public static string ActivationCode{get;set;}
        private ActivationCodeManager() { }
        public static readonly ActivationCodeManager instance = new ActivationCodeManager();

        /// <summary>
        /// 验证激活码
        /// </summary>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public bool CheckActivationCodeAndSave(string activationCode)
        {
            activationCode = DESEncrypt.Encrypt(activationCode);
            var result = RequestHelper.HttpGet(SiteUrl.GetApiUrl("command/check"), activationCode);
            if (DESEncrypt.Decrypt(result) == "true")
            {
                ActivationCodeManager.ActivationCode = activationCode;
                return true;
            }
            return false;
        }
    }
}
