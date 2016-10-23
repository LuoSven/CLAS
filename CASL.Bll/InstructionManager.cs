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
using CLAS.Model.Result;
using CLAS.Web.Core.Base;

namespace CASL.Bll
{
    /// <summary>
    /// 解析和执行指令
    /// </summary>
    public class InstructionManager
    { 

        CDmSoft dm = new CDmSoft();
        public static readonly object log=new object();
        private  readonly char splitKey = ':';  
        private  BaseForm form { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }
        private InstructionManager() { }
        public static readonly InstructionManager instance = new InstructionManager();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public void Init(int screenWidth,int screenHeight,BaseForm form )
        {
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
            //执行结果
            var instruqctionResult = new InstruqctionResult() { IsSucceed = false };
            //参数类型
            var commandType = instruction.Split(splitKey)[0].ToInt();
            var commandValue = instruction.Split(splitKey)[1];
            object x;
            object y;
            var result = 0;
            switch ((InstructionCommandType)commandType)
            {
                //0:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.FindAndMoveTo: 
                       
                        dm.AddDict(0, commandValue.Split(',')[0]);
                         result = dm.FindStr(0, 0, screenWidth, screenHeight, commandValue.Split(',')[1], commandValue.Split(',')[2], 1.0, out x, out y);
                         if (result==-1)
                        {
                            return instruqctionResult;
                        }

                        instruqctionResult.Data = new PositionResult() { x = (int)x, y = (int)y };
                        dm.MoveTo((int)x, (int)y);
                        break;
                //1:11,12
                case InstructionCommandType.MoveToRelative:
                        dm.MoveTo(commandValue.Split(',')[0].ToInt(), commandValue.Split(',')[1].ToInt());
                        break;
                //2:
                case InstructionCommandType.MouseClick: 
                        dm.LeftClick();
                        break;
                //3:82115
                case InstructionCommandType.KeyPress:
                        foreach (var item in commandValue)
                        {
                            Thread.Sleep(50);
                            dm.KeyPress(item.CharToAsc());
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
                case InstructionCommandType.GetString:
                    var values = commandValue.Split(',');
                    instruqctionResult.Data = dm.Ocr(values[0].ToInt(), values[1].ToInt(), values[2].ToInt(), values[3].ToInt(), values[4], 1.0);
                        break;
                    
                //200:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.Find:                        
                        dm.AddDict(0, commandValue.Split(',')[0]);
                        result = dm.FindStr(0, 0, screenWidth, screenHeight, commandValue.Split(',')[1], commandValue.Split(',')[2], 1.0, out x, out y);
                        
                        if (result == -1)
                        {
                            return instruqctionResult;
                        }
                        instruqctionResult.Data = new PositionResult() { x = (int)x, y = (int)y };
                        break;


                //300:请输入验证码
                case InstructionCommandType.ShowMessage:                        
                        form.ShowMessage(commandValue);                  
                        break;

                //301:10
                case InstructionCommandType.CountDown:                                       
                        form.CountDown(commandValue.ToInt());                             
                        break;
            }
            instruqctionResult.IsSucceed = true;
            return instruqctionResult;
        }

 
        
    }
}
