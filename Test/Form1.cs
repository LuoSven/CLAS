using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CASL.Bll;
using CLAS.Common;
using CLAS.Model.TMs;
using CLAS.Web.Core.Base;

namespace Test
{
    public partial class Form1 : BaseForm
    {

        public Form1()
        {
            InitializeComponent();
            InstructionManager.instance.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,this);
            MouseEventHelper.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            ScriptExecTime.CustomFormat = "yyyy/MM/dd HH:mm:ss.fff ";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ExcuteScript_Click(object sender, EventArgs e)
        {
            var instructions =ScriptToExcute.Text.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var time = ScriptExecTime.Value;
            CommandManager.Tactics=new TacticsTM()
            {
                Scripts = new Dictionary<DateTime, ScriptTM>() {{ ScriptExecTime.Value,new ScriptTM() { ExecuteExpressions = instructions }} }
            }; 
             
        }


        public override void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public override void CountDown(int seconds)
        {
            CountDownDate = DateTime.Now.AddSeconds(seconds);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CountDownDate >= DateTime.Now)
            {
                var span = CountDownDate - DateTime.Now;

                CountDownText.Text = string.Format("{0}秒后输入验证码", span.Seconds);
            }
            else
            {
                CountDownText.Text = "";
            }  
        }

        private void RefreshRecord_Click(object sender, EventArgs e)
        {
            var records=new List<string>();
            foreach (var record in ScriptManager.ScriptExecuteRecords)
            {
                records.Add(string.Format("脚本{0}执行{1},指令执行结果:{2}", record.ScriptId, record.IsSucceed ? "成功" : "失败", record.Message));
            }
            RecordInfo.Text = string.Join("\r\n",records);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScriptExecTime.Value = DateTime.Now.AddSeconds(AddedSecond.Text.ToInt());
        }
    }
}
