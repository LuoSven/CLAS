using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
     public   class KeyDownRecordGroupDetailVM
    {


         public int BidderId { get; set; }

         public int Id { get; set; }
        
         /// <summary>
         /// 按下的按键
         /// </summary>
         public int Key { get; set; }
         /// <summary>
         /// 按钮名
         /// </summary>
         public string KeyName {
             get
             {
                 return ((char) Key).ToString();
             }
         }
         /// <summary>
         /// 按键时间
         /// </summary>
         public DateTime KeyDownTime { get; set; }


         /// <summary>
         /// 按键时间
         /// </summary>
         public string KeyDownTimeName {
             get { return KeyDownTime.ToString("HH:mm:ss.fff"); }
         }
    }
}
