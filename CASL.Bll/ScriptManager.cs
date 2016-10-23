using CLAS.Model.TMs;
using CLAS.Common;
using CLAS.Model.Entities;


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CLAS.Model.VMs;
using CLAS.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CLAS.Model.Result;

namespace CASL.Bll
{
    /// <summary>
    /// 负责脚本的执行和更新
    /// </summary>
    public class ScriptManager
    {
        public static readonly object log = new object();

        #region  静态值 


        private static List<ScriptExecuteRecordVM> scriptExecuteRecordsSended { get; set; }

        /// <summary>
        /// 已发送的脚本执行记录
        /// </summary>
        public static List<ScriptExecuteRecordVM> ScriptExecuteRecordsSended
        {
            get
            {
                if (scriptExecuteRecordsSended == null)
                {
                    scriptExecuteRecordsSended = new List<ScriptExecuteRecordVM>();
                }
                return scriptExecuteRecordsSended;
            }
            set { scriptExecuteRecordsSended = value; }
        }

        /// <summary>
        /// 脚本执行记录
        /// </summary>
        private static List<ScriptExecuteRecordVM> scriptExecuteRecords { get; set; }

        /// <summary>
        /// 脚本执行记录
        /// </summary>
        public static List<ScriptExecuteRecordVM> ScriptExecuteRecords
        {
            get
            {
                if (scriptExecuteRecords == null)
                {
                    scriptExecuteRecords = new List<ScriptExecuteRecordVM>();
                }
                return scriptExecuteRecords;
            }
            set { scriptExecuteRecords = value; }
        }
         

        #endregion

        private ScriptManager()
        {
        }

        public static readonly ScriptManager instance = new ScriptManager();





        /// <summary>
        /// 完成脚本
        /// </summary>
        /// <param name="executeTime">executeTime</param>
        /// <param name="script">script</param>
        /// <param name="message">message</param>
        public bool DoScript(DateTime executeTime, ScriptTM script,string message=null)
        {
            //同一时间只能执行一个脚本
            lock (log)
            {
                //变量集合，碰到等于号左边就要存在这个里面，执行下条指令的时候要把参数带进去，参数的写法是一个@
                var variables =new Dictionary<string, object>();
                //带入公共变量

                var scriptMessage =new List<string>();

                //添加脚本记录，记录执行开始时间
                var recordVm = new ScriptExecuteRecordVM()
                {
                    ScriptId = script.Id,
                    ExecuteTime = executeTime,
                    ActualExecutionStartTime = DateTime.Now
                };

                //执行表达式
                scriptMessage.AddRange(ExecExpressions(variables, script.ExecuteExpressions));

 

                //记录执行结束时间
                recordVm.ActualExecutionEndTime = DateTime.Now;
                recordVm.ExecuteMSec =(recordVm.ActualExecutionEndTime - recordVm.ActualExecutionStartTime).Milliseconds;
                CommandManager.instance.Log(string.Format("{0}执行完毕脚本{1}", DateTime.Now, script.Id));
                if (!string.IsNullOrEmpty(script.CheckInstruction))
                {
                    //验证脚本
                    var checkResult = InstructionManager.instance.DoInstruction(script.CheckInstruction);
                    recordVm.IsSucceed = checkResult.IsSucceed;
             
                    CommandManager.instance.Log(string.Format("{0}脚本{1}执行{2}", DateTime.Now, script.Id,
                        checkResult.IsSucceed ? "成功" : "失败"));
                }
                if (!string.IsNullOrEmpty(message))
                {
                    scriptMessage.Insert(0,message);
                }
                recordVm.Message = string.Join(",", scriptMessage);
                //记录脚本执行情况
                ScriptExecuteRecords.Add(recordVm);
            }


            return true;
        }


        #region 私有函数

        /// <summary>
        /// 替换参数的值
        /// </summary>
        /// <param name="variables"></param> 
        /// <param name="instruction"></param>
        /// <param name="isHaveQuotes">替换的变量是否有引号</param>
        /// <returns></returns>  
        private string InputVariable(Dictionary<string, object> variables, string instruction, bool isHaveQuotes=false)
        { 
            //私有变量
            foreach (var item in variables)
            {
                var tempValue = item.Value.ToString(); 
                instruction = instruction.Replace("@" + item.Key,isHaveQuotes? tempValue.AddQuotes():tempValue);
            }

            //函数
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            instruction = instruction.Replace("getDate()", isHaveQuotes ? now.AddQuotes() : now);

            //公共变量
            var publicVariables = GetPublicVariable();
            foreach (var item in publicVariables)
            {
                var tempValue = item.Value.ToString();
                instruction = instruction.Replace("@" + item.Key, isHaveQuotes ? tempValue.AddQuotes() : tempValue);
            }
            return instruction;
        }

        /// <summary>
        /// 变量对象，变成o.x,o.y这样的参数名
        /// </summary>
        private Dictionary<string, object> SetVariable(Dictionary<string, object> variables, string instruction)
        {
            var equals = instruction.Split('=');
            var equalLeft = equals[0];
            //表达式
            var expression = equals[1];
            var values = new Dictionary<string, object>();
            if (expression.Contains(":"))
            {//函数
                if (expression.Split(':')[0].IsInt())
                {
                    var functionResult = ExecInstruction(variables, expression);
                    if (functionResult.IsSucceed)
                    {
                        //遍历对象的属性，变成x.x这种形式
                        values = TransObjects(equalLeft, functionResult.Data);
                    }
                }

            }
            else
                //加减乘除,仅支持变量
                if (expression.Contains("+") || expression.Contains("-") || expression.Contains("*") || expression.Contains("/"))
                {
                    expression = InputVariable(variables, expression);
                    DataTable dt = new DataTable();
                    values = new Dictionary<string, object>() { { equalLeft, dt.Compute(expression, "false").ToString() } };
                }
                else if (expression.IndexOf("@") == 0)
                { //变量赋值

                    var keyName = expression.Replace("@", "");
                    values = new Dictionary<string, object>() { { equalLeft, variables.ContainsKey(keyName) ? variables[keyName] : "" } };

                }
                else
                {//数值赋值
                    if (expression.Split(':')[0].IsInt())
                    {

                        values = new Dictionary<string, object>() { { equalLeft, expression } };
                    }
                }

            foreach (var item in values)
            {
                variables.Set(item.Key, item.Value);
            }
            return variables;
        }

        /// <summary>
        /// 遍历对象的属性，变成x.x这种形式
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="o"></param>
        /// <returns></returns>s
        private Dictionary<string, object> TransObjects(string keyName, object o)
        {
            var values = new Dictionary<string, object>();
            if (o.GetType() == typeof(PositionResult))
            {
                foreach (System.Reflection.PropertyInfo p in o.GetType().GetProperties())
                {
                    values.Add(keyName + "." + p.Name, p.GetValue(o));
                }
            }
            else
            {
                values.Add(keyName, o);
            }
            return values;

        }

        private ScriptResult ExecExpressions(Dictionary<string, object> variables, string expressions)
        {
            var scriptResult = new ScriptResult() {Logs = new List<string>()}; 
            //执行表达式
            foreach (var expression in expressions.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                var result = ExecExpression(variables, expression);
                scriptResult.Logs.Add((result.IsSucceed ? "成功" : "失败") + ":" + expression);
                if (result.IsBreak)
                {
                    scriptResult.IsBreak = result.IsBreak;
                    scriptResult.Logs.Add("跳出了循环");
                    break;
                }
            }
            return scriptResult;
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private InstruqctionResult ExecExpression(Dictionary<string, object> variables, string expression)
        {

            var result = new InstruqctionResult(){IsSucceed = true};
            if (expression == "break")
            {
                result.IsBreak = false;
            }
            else if (expression.IndexOf("while") == 0)
            {
                //while循环
                var isFinish = false;
                //获取while后面括号内的内容
                var whileConditions = expression.SplitByBrackets();
                var whileCondition = whileConditions[0];
                while (!isFinish)
                {
                    var boolResult = ExecBoolInstruction(variables, whileCondition);
                    if (!boolResult)
                    {
                        //如果条件成功,就执行while里面的内容
                        var whileExpression = expression.SplitByBraces();
                        var whileResult=  ExecExpressions(variables, whileExpression[0]);
                        if (whileResult.IsBreak)
                        {
                            break;
                        }
                        //break怎么做。。。

                    }
                }

            }
            else if (expression.IndexOf("if") == 0)
            {
                //if条件
                var ifConditions = expression.SplitByBrackets();
                var ifCondition = ifConditions[0];
                var boolResult = ExecBoolInstruction(variables, ifCondition);
                if (!boolResult)
                { 
                    //如果条件成功,就执行if里面的内容
                        var ifExpression = expression.SplitByBraces();
                        ExecExpressions(variables, ifExpression[0]);
                }
            }
            else if (expression.Contains("=") && expression.Split('=').Length == 2)
            {
                //赋值操作
                variables = SetVariable(variables, expression);
            }
            else
            {
                //指令操作
                result = ExecInstruction(variables, expression);
            }

            return result;
        }

        /// <summary>
        /// 执行操作命令
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private InstruqctionResult ExecInstruction(Dictionary<string, object> variables, string instruction)
        {
            //带入变量
            instruction = InputVariable(variables, instruction);
            //执行脚本
            var result = InstructionManager.instance.DoInstruction(instruction);
            return result;
        }

        /// <summary>
        /// 执行布尔脚本
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private bool ExecBoolInstruction(Dictionary<string, object> variables, string instruction)
        {
            //生成while(1==1&&1==1)括号里面内容的判断式
            var jsExpress = InputVariable(variables, instruction, true);
            //判断的结果
            var jsResult = (bool)jsExpress.ExcuteAsJs();
            return jsResult;
        }

        /// <summary>
        /// 获取公共变量
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetPublicVariable()
        {
            var variables = new Dictionary<string, object>();
            variables.SetIfEmtpy("keyDownCount", CommandManager.Keyloggers.Count);
            return variables;
        }
        #endregion


      

    }
}