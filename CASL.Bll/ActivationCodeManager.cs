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
using CLAS.Utils;

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

        #region 客户端函数
        /// <summary>
        /// 发送并且验证激活码
        /// </summary>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public bool SendActivationCodeAndCheck(string activationCode)
        { 
            var loginVm = new ActivationVM()
            {
                ActivationCode = activationCode
            };
            var obStr = DESEncrypt.EncryptModel(loginVm);
            var result = RequestHelper.HttpGet(SiteUrl.GetApiUrl("command/Login?s=" + obStr));
            var isLogin = DESEncrypt.Decrypt(result.Replace("\"", "")); 
            if ( isLogin== "true")
            {
                ActivationCode = activationCode;
                return true;
            }
            return false;
        }
        #endregion

       
    }
}
