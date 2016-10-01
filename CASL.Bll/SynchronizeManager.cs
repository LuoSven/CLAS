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
using System.Diagnostics;

namespace CASL.Bll
{
    /// <summary>
    /// 负责更新的Manager
    /// </summary>
    public class SynchronizeManager
    {         
        public static readonly object log=new object();  

        private SynchronizeManager() { }
        public static readonly SynchronizeManager instance = new SynchronizeManager();
        /// <summary>
        /// 通信间隔，毫秒
        /// </summary>
        public static int CommunicationSpan = 5000;


        /// <summary>
        /// 和服务器进行通信
        /// </summary>
        public void Synchronize()
        {

            var communication = new Thread(() =>
            {
                while (true)
                {
                    var clientRequestTM = new ClientRequestTM()
                    {
                        ActivationCode = ActivationCodeManager.ActivationCode,
                        ScriptLastUpdateTime = CommandManager.ScriptLastUpdateTime,
                        TacticsLastUpdateTime = CommandManager.TacticsLastUpdateTime,
                        ScriptExecuteRecords = ScriptManager.ScriptExecuteRecords,
                        SendTime = DateTime.Now,
                    };
                    //将更新记录记录放到最后
                    ScriptManager.ScriptExecuteRecordsSended.AddRange(ScriptManager.ScriptExecuteRecords);
                    //置空当前更新记录
                    ScriptManager.ScriptExecuteRecords = null;

                    //获取服务器的请求
                    var serverRequestTM = GetServerReponse(clientRequestTM);
                    //同步数据
                    ExecuteServerReponse(serverRequestTM);

                    //停止时间
                    Thread.Sleep(CommunicationSpan);

                }
            });
            communication.IsBackground = true;
            communication.Start();
            #region 测试的部分
            //var date = "2016-9-27 22:14:50.600".ToDateTime();//DateTime.Now.AddSeconds(10);
            //var scripts = new List<string>(){
            //      //找图
            //     "0:0180300600C018000003024018030863046077C830000003F99CE00C018010017D070000000FE673803006004005F41C0$-300$0.0.122$13,-300,00b913-000000|00b913-000000|00b93b-000000|4edad8-000000|4eb913-000000|2cb913-000000|a9d23b-000000",
            //      //移动到框
            //     "1:40,-30",
            //      //鼠标双击
            //     "4:",
            //     //输入
            //     "3:700" ,
            //     //移动到加价
            //     "1:155,0",
            //      //鼠标单击
            //     "2:",
            //     //移动到出价
            //     "1:0,100",
            //      //鼠标单击
            //     "2:",
            //};  
            //    result.Add(new CommandTM(date, scripts)); 


            //return result;
            #endregion

        }


        /// <summary>
        /// 获取服务器发出的指令，并且同步数据到服务器
        /// </summary>
        /// <returns></returns>
        private ServerReponseTM GetServerReponse(ClientRequestTM tm)
        {
            var modelStr = DESEncrypt.EncryptModel(tm);

            var s = RequestHelper.HttpPost(SiteUrl.GetApiUrl("Command"), modelStr);
            var serverRequestTM = (ServerReponseTM)DESEncrypt.DecryptModel(s);
            return serverRequestTM;
        }
        /// <summary>
        /// 根据服务器指令执行命令
        /// </summary>
        public void ExecuteServerReponse(ServerReponseTM tm)
        {
            switch (tm.CommandType)
            {
                    //同步策略
                case ServerCommandType.SynTactics:
                    if (tm.Tactics != null)
                    {
                        CommandManager.Tactics = tm.Tactics;
                    }
                    break;
                    //同步脚本
                case ServerCommandType.SynScript:
                    if (tm.Scripts != null)
                    {
                        foreach (var script in tm.Scripts)
                        {
                            CommandManager.Tactics.Scripts.SetIfEmtpy(script.Key, script.Value);
                        }
                    }
                    break;
            }
        }
    }
}
