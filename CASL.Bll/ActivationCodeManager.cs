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

        public bool? IsSuccess;
        public string Message;
        public Thread check;
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
        public void SendActivationCodeAndCheck(string activationCode)
        {
            check = new Thread(CheckActivationCode);
            check.IsBackground = true;
            check.Start(activationCode);
        }

        private void CheckActivationCode(object code)
        {
            var result = "";
            var activationCode = (string) code;
            var loginVm = new ActivationVM()
            {
                ActivationCode = activationCode
            };
            var obStr = DESEncrypt.EncryptModel(loginVm);
            try
            {
                 result = RequestHelper.HttpGet(SiteUrl.GetApiUrl("command/Login?s=" + obStr));
            }
            catch (Exception)
            {
                Message = "服务器发生错误，请重试";
                IsSuccess = false;
                return;
            }
            var loginInfo = DESEncrypt.Decrypt(result.Replace("\"", ""));
            if (loginInfo != "false")
            { 
                CommandManager.BidderName =  loginInfo.Split(',')[0];
                CommandManager.IsFor51 = loginInfo.Split(',')[1]=="1";
                ActivationCode = activationCode;
                IsSuccess = true;
            }
            else
            {
                Message = "验证码错误，请重试";
                IsSuccess = false;
            }
        }
        #endregion

       
    }
}
