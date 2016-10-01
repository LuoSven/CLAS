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
    /// 负责脚本的执行和更新
    /// </summary>
    public class ScriptManager
    {         
        public static readonly object log=new object(); 
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
            set
            {
                scriptExecuteRecordsSended = value;
            }
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
            set
            {
                scriptExecuteRecords = value;
            }
        } 
        #endregion
         
        private ScriptManager() { }
        public static readonly ScriptManager instance = new ScriptManager();
         
         
        
             
         
        /// <summary>
        /// 完成脚本
        /// </summary>
        /// <param name="instructions">要</param>
        public bool DoScript(DateTime executeTime, ScriptVM script)
        {
            //同一时间只能执行一个脚本
            lock (log)
            {
                //添加脚本记录
                var recordVm = new ScriptExecuteRecordVM()
                {
                    ScriptId = script.Id,
                    ExecuteTime = executeTime,
                    ActualExecutionStartTime=DateTime.Now
                }; 
                //执行脚本
                foreach (var instruction in script.ExecuteInstructions)
                {
                    var result = InstructionManager.instance.DoInstruction(instruction);
                   
                }
                //验证脚本
                var checkResult = InstructionManager.instance.DoInstruction(script.CheckInstruction);

                recordVm.ActualExecutionEndTime = DateTime.Now;
                recordVm.IsSucceed = checkResult;
                //记录脚本执行情况
                ScriptManager.ScriptExecuteRecords.Add(recordVm);
            }
           

            return true;
        }
        
    }
}
