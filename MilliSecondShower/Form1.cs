using CLAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilliSecondShower
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NowTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        private void SynchronizeTime_Click(object sender, EventArgs e)
        {
           var  time= TimerHelper.GetBeijingTime();
            if(time.HasValue)
           TimerHelper.SetDate(time.Value);
           
        }

        private void NowTime_Click(object sender, EventArgs e)
        {

        }
    }
}
