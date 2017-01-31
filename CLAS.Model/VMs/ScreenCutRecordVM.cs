using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Model.VMs
{
     public   class ScreenCutRecordVM
    {

         public ScreenCutRecordVM(long img)
         {
             KeyDownTime=DateTime.Now;
             Img = img;
         }
         /// <summary>
         /// 按下的按键
         /// </summary>
         public long Img { get; set; }
         /// <summary>
         /// 按键时间
         /// </summary>
         public DateTime KeyDownTime { get; set; }
    }
}
