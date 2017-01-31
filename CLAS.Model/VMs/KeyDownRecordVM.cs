using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
     public   class KeyDownRecordVM
    {

         public KeyDownRecordVM(int key)
         {
             KeyDownTime=DateTime.Now;
             Key = key;
         }
         /// <summary>
         /// 按下的按键
         /// </summary>
         public int Key { get; set; }
         /// <summary>
         /// 按键时间
         /// </summary>
         public DateTime KeyDownTime { get; set; }
         /// <summary>
         /// 是否有效
         /// </summary>
         public bool IsEffictive { get; set; }
    }
}
