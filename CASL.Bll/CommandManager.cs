using CLAS.Model.TMs;
using CLAS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using CLAS.Model.Result;
using CLAS.Model.VMs;
using CLAS.Model.DTOs;

namespace CASL.Bll
{
    /// <summary>
    /// 负责调度的Manager，
    /// </summary>
    public class CommandManager
    {

        public CommandManager() {
            Logs = new List<BidderlogTM>();
        }

        //策略的获取和更新锁
        public static readonly object lockob = new object();
        //执行的获取和更新锁
        public static readonly object lockobReord = new object();
        //当前脚本内键盘的获取和更新锁
        public static readonly object lockobKeylogger = new object();
        //所有键盘的获取和更新锁
        public static readonly object lockobAllKeylogger = new object();
        //截屏的获取和更新锁
        public static readonly object lockobScreenCutlogger = new object(); 

        private static Thread getPrams;
        private static Thread execute;
        private static Thread setTime; 

        #region 参数

        public List<BidderlogTM> Logs { get; set; }

        /// <summary>
        /// 表示是否记录键值，当hook启动失败的时候此值是false,表示键值拥有成功
        /// </summary>
        public static int TimeSyncSpan { get; set; }
        /// <summary>
        /// 表示是否记录键值，当hook启动失败的时候此值是false,表示键值拥有成功
        /// </summary>
        public static string BidderName{get;set;}
        /// <summary>
        /// 表示是否记录键值，当hook启动失败的时候此值是false,表示键值拥有成功
        /// </summary>
        public bool IsLogKey = true;
        /// <summary>
        /// 控制时间是否从画面上获取
        /// </summary>
        public bool IsFrom51 = false;
        /// <summary>
        /// 控制日志的显示
        /// </summary>
        public bool IsTest = false;

        public int SendPrice;
        public int Price48;
        /// <summary>
        /// 需要记录的key值
        /// </summary> 
        private int[] keyDownStrs = {48,49, 50, 51, 52, 53, 54, 55, 56, 57, 8};

        private static TacticsTM tactics { get; set; }
        /// <summary>
        /// 策略，一个人只能有一个策略
        /// </summary>
        public static TacticsTM Tactics
        {
            get
            {
                lock (lockob)
                {
                    tactics = tactics ?? new TacticsTM();
                    return tactics;
                };
            }
            set
            {
                lock (lockob)
                {
                    tactics = value;
                }
            }
        }
        /// <summary>
        /// 是否是51测试版
        /// </summary>
        public static bool IsFor51 { get; set; }

        private static List<int> keyloggers;
        /// <summary>
        /// 按键记录
        /// </summary>
        public static List<int> Keyloggers
        {
            get
            {
                lock (lockobKeylogger)
                {
                    keyloggers = keyloggers ?? new List<int>();
                    var s = "".ToInt();
                    return keyloggers;
                };
            }
            set
            {
                lock (lockobKeylogger)
                {
                    keyloggers = value;
                }
            }
        }


        private static List<ScreenCutRecordVM> screenCutloggers;
        /// <summary>
        /// 按键记录
        /// </summary>
        public static List<ScreenCutRecordVM> ScreenCutloggers
        {
            get
            {
                lock (lockobScreenCutlogger)
                {
                    screenCutloggers = screenCutloggers ?? new List<ScreenCutRecordVM>();
                    return screenCutloggers;
                };
            }
            set
            {
                lock (lockobScreenCutlogger)
                {
                    screenCutloggers = value;
                }
            }
        }


        private static List<KeyDownRecordVM> allkeyloggers;
        /// <summary>
        /// 按键记录
        /// </summary>
        public static List<KeyDownRecordVM> AllKeyloggers
        {
            get
            {
                lock (lockobAllKeylogger)
                {
                    allkeyloggers = allkeyloggers ?? new List<KeyDownRecordVM>();
                    return allkeyloggers;
                };
            }
            set
            {
                lock (lockobAllKeylogger)
                {
                    allkeyloggers = value;
                }
            }
        }
         
         
        /// <summary>
        /// 当前价格
        /// </summary>
        public static int Price { get; set; }




         

        private static string logReord;
        /// <summary>
        /// 执行日志
        /// </summary>
        public static string LogReord
        {
            get
            {
                //读后就销毁
                lock (lockobReord)
                {
                    var s = logReord;
                    logReord = null;
                    return s;
                }

            }
            set
            {
                lock (lockobReord)
                {
                    logReord = value;
                }
            }
        }

        #endregion
     
        public static readonly CommandManager instance = new CommandManager();





        /// <summary>
        /// 重新开始工作
        /// </summary> 
        public void ReworkWork()
        {
            Tactics = null;
            LogReord = null;
            AllKeyloggers = null;
            Keyloggers = null;
            InstructionManager.instance.form.ClearMessage();
            BeginWork(); 
        }

        /// <summary>
        /// 开始工作
        /// </summary> 
        public void BeginWork()
        {
            if (IsFrom51)
            {
                var now = DateTime.Now;
                var s=new  DateTime(now.Year,now.Month,now.Day,11,29,00);
                TimerHelper.SetDate(s);
            }

            //和服务器进行同步数据
            SynchronizeManager.instance.Synchronizes();
            //定时执行策略中的脚本
            Execute();
            //同步时间
            SetTime();
            //获取价格
            GetPrice();


        }
        /// <summary>
        /// 开始工作
        /// </summary> 
        public void BeginTest()
        { 
            //定时执行策略中的脚本
            Execute();

        }

        /// <summary>
        /// 记录按钮信息
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(char key)
        {
            var keyLog = new KeyDownRecordVM(key);
            if (keyDownStrs.Contains((int)key))
            {
                keyLog.IsEffictive = true;
                if (key == 8) //按了删除
                { 
                    if (Keyloggers.Count > 0)
                        Keyloggers.RemoveAt(Keyloggers.Count - 1);
                }
                else
                { 
                    Keyloggers.Add(key);
                }
            }
            LogManager.instance.Log("按下了" + key + "目前已经按下了" + Keyloggers.Count + "次按键");
            AllKeyloggers.Add(keyLog);

        }



    
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (IsTest)
            LogReord+=("\r\n" + message);
        }
  
        #region 私有函数

        /// <summary>
        /// 定时执行任务
        /// </summary>
        public void Execute()
        {
             execute = new Thread(() =>
            {
                while (true)
                {

                    try
                    {
                        //执行完毕要销毁的脚本 
                        var commands = Tactics.Scripts;
                        if (!commands.Any())
                        {
                            continue;
                        }
                        var now = DateTime.Now;
                        var script = GetExecteScript(Tactics.Scripts, now);
                        if (script != null)
                        {
                            //执行脚本
                            ScriptManager.instance.DoScript(script.Script, script.ExecuteConditionExpress);
                            //执行完毕后销毁
                            Tactics.Scripts.Remove(script);
                        }


                        //执行过期脚本
                        var overTimeScript = GetExecteScript(Tactics.Scripts, now, true);
                        if (overTimeScript != null)
                        {
                            //执行脚本
                            ScriptManager.instance.DoScript(overTimeScript.Script, overTimeScript.ExecuteConditionExpress);
                            //执行完毕后销毁
                            Tactics.Scripts.Remove(overTimeScript);
                        }

                        
                    }
                    catch (Exception ex)
                    {
                         InstructionManager.instance.form.AlertMessage(ex.Message);
                    }
                 
                    ////线程池--Alt+A快捷键 
                    //for (int i = 48; i < 58; i++)
                    //{
                    //    if (dm.WaitKey(i, 0) == 1)
                    //    {
                    //        numberPutCount++;
                    //        break;
                    //    }
                    //}
                    //if (dm.WaitKey(13, 0) == 1)
                    //{
                    //    numberPutCount--;
                    //}
                    //if (numberPutCount == 4)
                    //{
                    //    ;
                    //}
                }
            });
            execute.IsBackground = true;
            execute.Start();
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        private void SetTime()
        {
             setTime = new Thread(() =>
            {
                while (true)
                {
                    DateTime? time = DateTime.Now;
                    if (IsFrom51)
                    {
                        time = ScriptManager.instance.GetTimeBy51();
                    }
                    else
                    {
                        time = InstructionManager.instance.GetNetTime();
                    }

                    if (time.HasValue)
                    {
                        var span = time.Value - DateTime.Now;
                        LogManager.instance.MessageFormat("{0}同步时间完毕,差{1}毫秒", DateTime.Now, span.Milliseconds);
                    }
                    else
                    {
                        LogManager.instance.MessageFormat("{0}同步时间失败", DateTime.Now);
                    }


                    if (time.HasValue)
                    {
                        TimerHelper.SetDate(time.Value);
                    }
                    InstructionManager.instance.Delay(TimeSyncSpan);
                      
                }
            });
            setTime.IsBackground = true;
            setTime.Start();

        }


        /// <summary>
        /// 同步时间
        /// </summary>
        private void GetPrice()
        {
            setTime = new Thread(() =>
            {
                while (true)
                {

                    var price = ScriptManager.instance.GetPrice();
                    if (price.HasValue)
                    {
                        LogManager.instance.LogFormat("获取价格成功，当前价格:{0}", price);
                        Price = price.Value;
                    }
                    else
                    {
                        LogManager.instance.Log("获取价格失败");
                    }
                    InstructionManager.instance.Delay(100);

                }
            });
            setTime.IsBackground = true;
            setTime.Start();

        }
       

     

        /// <summary>
        /// 选出符合条件的脚本来执行
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="now"></param>
        /// <param name="isOverTime">是否是选择超时</param>
        /// <returns></returns>
        private ScriptExecuteTM GetExecteScript(List<ScriptExecuteTM> scripts,DateTime now,bool isOverTime=false)
        {
            var timeInScripts = new List<ScriptExecuteTM>();
            if (!isOverTime)
            {
                //获取当时的脚本
                timeInScripts =scripts.Where(
                        o =>
                            (o.ExecuteTime.HasValue  
                            && o.ExecuteTime.Value>=now
                            && o.ExecuteTime.Value.TimeOfDay <= now.AddMilliseconds(30).TimeOfDay) ||
                            !o.ExecuteTime.HasValue).ToList();
            }
            else
            {
                //获取过期脚本
            timeInScripts=
                scripts.Where(
                       o =>
                           (o.ExecuteTime.HasValue &&o.ExecuteTime.Value.TimeOfDay < now.TimeOfDay)).ToList();
            }
            foreach (var script in timeInScripts)
            {
                var result = CheckExecuteCondition(script.ExecuteCondition);
                if (result.Result)
                {
                    script.ExecuteConditionExpress = result.Express;
                    return script;
                }
            }
            return null;
        }
        /// <summary>
        /// 检测表达式结果
        /// </summary>
        /// <param name="executeCondition"></param>
        /// <returns></returns>
        private BoolInstructionResult CheckExecuteCondition(string executeCondition)
        {
            var result = new BoolInstructionResult(){Result = true};
            if (string.IsNullOrWhiteSpace(executeCondition))
            { 
                return result;
            }
            return ScriptManager.instance.ExecBoolInstruction(ScriptManager.instance.GetPublicVariable(), executeCondition);
 
        }


        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyPressWatch(object sender, KeyPressEventArgs e)
        {
            KeyDown(e.KeyChar);
        }
     
        #endregion
       
        
    }
}
