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
        public virtual void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }
        public virtual void CountDown(int seconds)
        {
            throw new NotImplementedException();
        }

    }
}
