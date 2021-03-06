﻿using CLAS.Model.TMs;
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
using System.IO;
using System.Net;
using CLAS.Model.Base;
using CLAS.Model.Result;
using CLAS.Web.Core.Base;

namespace CASL.Bll
{
    /// <summary>
    /// 解析和执行指令
    /// </summary>
    public class InstructionManager
    { 

       public CDmSoft dm = new CDmSoft(DownloadManager.dmcDllPath+"dm.dll");
        public static readonly object log=new object();
        private  readonly char splitKey = ':';  
        public  BaseForm form { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }
        private InstructionManager() { }
        /// <summary>
        /// 公共函数名称集合
        /// </summary>
        private static List<string> InstuctionNames { get; set; }

        public static readonly InstructionManager instance = new InstructionManager();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public void Init(int screenWidth,int screenHeight,BaseForm form)
        {
            InstuctionNames = InstructionCommandType.CountDown.GetNames(":");
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;    
            this.form = form;          
        }
          
         
        
            
        ///
        /// <summary>
        /// 完成指令
        /// </summary>
        /// <param name="instruction"></param>
        public InstruqctionResult DoInstruction(string instruction)
        {
            dm.SetShowErrorMsg(0);
            //执行结果
            var instruqctionResult = new InstruqctionResult() { IsSucceed = false };
            //参数类型
            var commandName = IsContainsInstruction(instruction);
            var commandType = (InstructionCommandType)Enum.Parse(typeof(InstructionCommandType),commandName); ;
            var commandValue = instruction.Replace(commandName + ":", "").Trim('(', ')');
            var values = commandValue.Split(','); 
            object x;
            object y;
            var result = 0;
            switch (commandType)
            {
                //0:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.FindAndMoveTo:
                    dm.AddDict(0, values[0]);
                    result = dm.FindStr(0, 0, screenWidth, screenHeight, values[1], values[2], 1.0, out x, out y);
                         if (result==-1)
                        {
                            return instruqctionResult;
                        }

                        instruqctionResult.Data = new PositionResult() { x = (int)x, y = (int)y };
                        dm.MoveTo((int)x, (int)y);
                        break;
                //1:11,12
                case InstructionCommandType.MoveToRelative:
                        dm.MoveTo(values[0].ToInt(), values[1].ToInt());
                        break;
                //2:
                case InstructionCommandType.MouseClick: 
                        dm.LeftClick();
                        break;
                //3:82115
                case InstructionCommandType.KeyPress:
                        foreach (var item in commandValue)
                        {
                            Thread.Sleep(10);
                            dm.KeyPress(item.CharToAsc());
                        }
                        break;
                case InstructionCommandType.KeyPressAsk:

                    foreach (var item in values)
                    {
                        Thread.Sleep(10);
                        dm.KeyPress(item.ToInt());
                    } 
                    break;
                //4:
                case InstructionCommandType.MouseDoubleClick:
                        dm.LeftDoubleClick();
                        break;
                //延时
                //5:500
                case InstructionCommandType.Delay:
                        Thread.Sleep(commandValue.ToInt());
                        break;
                //6:300,200,300
                case InstructionCommandType.Ocr:
                        instruqctionResult.Data = dm.Ocr(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt(), values[4], values.Length > 5 ? values[5].ToDouble() : (double)1.0);
                    break;
                case InstructionCommandType.OcrInFile:
                    instruqctionResult.Data = dm.OcrInFile(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt(), values[4],values[5], 1.0);
                    break;
                //AddDict:1,409FF3FE7FC008010$1$0.0.31$9 
                case InstructionCommandType.AddDict:
                    result = dm.AddDict(values[0].ToInt(), values[1]);
                     if (result == -1)
                     {
                         return instruqctionResult;
                     }
                    break;
                //UseDict:1
                case InstructionCommandType.UseDict: 
                    dm.UseDict(commandValue.ToInt());
                    break;
                //GetDict:0,1
                case InstructionCommandType.GetDict:  ;
                    instruqctionResult.Data=dm.GetDict(values[0].ToInt(), values[1].ToInt());
                    break;
                //GetDictCount:0
                case InstructionCommandType.GetDictCount:
                    instruqctionResult.Data=dm.GetDictCount(commandValue.ToInt());
                    break;
                //GetDictCount:0
                case InstructionCommandType.SetDict:
                    instruqctionResult.Data = dm.SetDict(values[0].ToInt(), values[1]);
                    break;
                case InstructionCommandType.SetPath:
                    instruqctionResult.Data = dm.SetPath(commandValue);
                    break;
                case InstructionCommandType.Capture:
                    instruqctionResult.Data = dm.Capture(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt(), values[4]);
                    //13;OcrHelper.GetFromOcr(values[0]);
                    break;
                    
                //200:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.Find:
                        result = dm.FindStr(0, 0, screenWidth, screenHeight, values[0], values[1], 1.0, out x, out y);
                        if (result == -1)
                        {
                            return instruqctionResult;
                        }
                        instruqctionResult.Data = new PositionResult() { x = (int)x, y = (int)y };
                        break;


                //300:请输入验证码
                case InstructionCommandType.ShowMessage:                        
                        form.AlertMessage(commandValue);                  
                        break;

                //301:10
                case InstructionCommandType.CountDown:                                       
                        form.CountDown(commandValue.ToInt());
                        break;
                //302:10
                case InstructionCommandType.Log:
                        LogManager.instance.Log(commandValue);
                        break;
                //302:10
                case InstructionCommandType.Set48Price:
                        CommandManager.instance.Price48 = CommandManager.Price + CommandManager.Tactics.AddPrice??0;
                        instruqctionResult.Message = "48秒价格：" + CommandManager.instance.Price48;
                        break;
                //302:10
                case InstructionCommandType.SetSendPrice:
                        CommandManager.instance.SendPrice = CommandManager.Price;
                        instruqctionResult.Message = "提交价格：" + CommandManager.instance.SendPrice;
                        break;
                case InstructionCommandType.CaptureAndUpload:
                    var fileName = DateTime.Now.Ticks + ".bmp";
                    instruqctionResult.Data = dm.Capture(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt(), fileName);
                    UploadImg(fileName);
                    break;
                case InstructionCommandType.ClearAllKey:
                    CommandManager.Keyloggers.Clear();
                    break;
                case InstructionCommandType.GetAveRGB:
                    var  averbg = dm.GetAveRGB(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt());
                    instruqctionResult.Data = averbg;
                    break;
                    
            }
            instruqctionResult.IsSucceed = true;
            return instruqctionResult;
        }
        /// <summary>
        /// 是否包含函数名称
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string IsContainsInstruction(string expression)
        {
            var name = "";
            foreach (var item in InstuctionNames)
            {
                if (expression.Contains(item))
                {
                    name = item;
                    break;
                }
            }
            return name.TrimEnd(':');

        }

        /// <summary>
        /// 延时
        /// </summary>
        /// <param name="mis"></param>
        /// <returns></returns>
        public void Delay(int mis)
        {
                Thread.Sleep(mis);
        }
        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns></returns>
        public DateTime? GetNetTime()
        {
            var time = TimerHelper.GetBeijingTime();
            return time;
        }


        private void UploadImg(string file)
        {
            if (!File.Exists(file))
            {
                LogManager.instance.Log("要上传的文件不存在！文件名" + file);
                return;

            }

            try
            {
                var tm = new ScreenCutTM() { ActivationCode = ActivationCodeManager.ActivationCode, UploadTime = DateTime.Now };
                var s = DESEncrypt.EncryptModel(tm);
                new WebClient().UploadFile(SiteUrl.GetApiUrl("upload/ImgUpload?s=" + s), file);
                File.Delete(file);

            }
            catch (Exception ex)
            {
                throw ex;
                LogManager.instance.Error("出错码3-267 请联系开发人员", ex);
            }

            //var  th=new Thread(() =>
            //{
            //    try
            //    {
            //        var tm = new ScreenCutTM() { ActivationCode = ActivationCodeManager.ActivationCode, UploadTime = DateTime.Now };
            //        var s = DESEncrypt.EncryptModel(tm);
            //        new WebClient().UploadFile(SiteUrl.GetApiUrl("upload/ImgUpload?s=" + s), file);
            //        File.Delete(file);

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //        LogManager.instance.Error("出错码3-267 请联系开发人员",ex);
            //    }

               
            //});
            //th.IsBackground = true;
            //th.Start();
        }
        
    }
}
