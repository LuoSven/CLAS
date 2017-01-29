using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLAS.Model.Base
{
    public  class BaseForm : Form
    {

        public DateTime CountDownDate { get; set; }
        /// <summary>
        /// 展示信息
        /// </summary>
        /// <param name="message"></param>
        public virtual void AlertMessage(string message)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 倒计时
        /// </summary>
        /// <param name="seconds"></param>
        public virtual void CountDown(int seconds)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 清除信息
        /// </summary> 
        public virtual void ClearMessage()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 加载信息
        /// </summary>
        public virtual void LoadMessage()
        {
            throw new NotImplementedException();
        }

    }
}
