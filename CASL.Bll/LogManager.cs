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
    public class LogManager
    {
        public static readonly object log = new object();

    
        private LogManager()
        {
        }

        public static readonly LogManager instance = new LogManager();



        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message"></param>
        public void Message(string message)
        {
            CommandManager.LogReord += ("\r\n" + message);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message"></param>
        public void MessageFormat(string message, params object[] args)
        {
            Message(string.Format(message, args));
        }

        public void Error(string error)
        {
            CommandManager.LogReord += ("\r\n" + error);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (CommandManager.instance.IsTest)
                CommandManager.LogReord += ("\r\n" + message);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public void LogFormat(string message, params object[] args)
        {
            Log(string.Format(message, args));
        }


      

    }
}