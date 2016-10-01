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
    /// 解析和执行指令
    /// </summary>
    public class InstructionManager
    {

        CDmSoft dm = new CDmSoft();
        public static readonly object log=new object();
        private  readonly char splitKey = ':';  
        
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }
        private InstructionManager() { }
        public static readonly InstructionManager instance = new InstructionManager();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public void Init(int screenWidth,int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;          
        }
          
         
        
            
        ///
        /// <summary>
        /// 完成指令
        /// </summary>
        /// <param name="instruction"></param>
        public bool DoInstruction(string instruction)
        {

            var commandType = instruction.Split(splitKey)[0].ToInt();
            var commandValue = instruction.Split(splitKey)[1];
            object x;
            object y;
            switch ((InstructionCommandType)commandType)
            {
                //0:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.FindAndMoveTo: 
                       
                        dm.AddDict(0, commandValue.Split(',')[0]);
                        var result = dm.FindStr(0, 0, screenWidth, screenHeight, commandValue.Split(',')[1], commandValue.Split(',')[2], 1.0, out x, out y);
                        if (result < 0)
                        {
                            return false;
                        }
                        dm.MoveTo((int)x, (int)y);
                        break;
                //1:11,12
                case InstructionCommandType.MoveToRelative: 
                        var pxy=commandValue.ToInts();
                         dm.GetCursorPos(out x, out y);
                         dm.MoveTo((int)x + pxy[0], (int)y + pxy[1]);
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
                //5:
                case InstructionCommandType.Delay:
                        Thread.Sleep(commandValue.ToInt());
                        break;
                    
                //200:0180300600C00000081B01C018C3186307B203C000001FCE0F00600C01801C18FE0000007F383C01803006007063F8$-300$0.0.133$13,-300,00b913-000000
                case InstructionCommandType.Find:                        
                        dm.AddDict(0, commandValue.Split(',')[0]); 
                        return dm.FindStr(0, 0, screenWidth, screenHeight, commandValue.Split(',')[1], commandValue.Split(',')[2], 1.0, out x, out y)>0;                      
                        break;
            }
            return false;
        }

 
        
    }
}
