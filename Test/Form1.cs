using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Deployment;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CASL.Bll;
using CLAS.Common;
using CLAS.Model.Base;
using CLAS.Model.TMs;
using CLAS.Utils;

namespace Test
{
    public partial class Form1 : BaseForm
    {

        private Hocy_Hook hook_Main = new Hocy_Hook();
        public Form1()
        {
            InitializeComponent();
             
            CommandManager.instance.IsFrom51 = true;
            CommandManager.instance.IsTest = true;

            //定时执行策略中的脚本
            CommandManager.instance.BeginTest();
            var result = hook_Main.InstallHook("1");
            CommandManager.instance.Log("hook注册" + (result ? "成功" : "失败"));
            this.hook_Main.OnKeyPress += new KeyPressEventHandler(CommandManager.instance.KeyPressWatch);

            InstructionManager.instance.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, this);
            MouseEventHelper.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            ScriptExecTime.CustomFormat = "HH:mm:ss.fff ";
              
        }

        private void tabPage1_Click(object sender, EventArgs e)
        { 
        }
 
   

        private void ExcuteScript_Click(object sender, EventArgs e)
        {
            CommandManager.Keyloggers = new List<int>();
            CommandManager.Tactics = new TacticsTM()
            {
                Scripts = new List<ScriptExecuteTM>() { new ScriptExecuteTM() { ExecuteTime = ScriptExecTime.Value, ExecuteCondition = ExecuteConditionText.Text, Script = new ScriptTM() { ExecuteExpressions = ScriptToExcute.Text } } }
            }; 
             
        }


        public override void AlertMessage(string message)
        {
            MessageBox.Show(message);
        }
        public override void CountDown(int seconds)
        {
            CountDownDate = DateTime.Now.AddSeconds(seconds);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            var message = CommandManager.LogReord;
            if (!string.IsNullOrEmpty(message))
                SyncMessage.AppendText(message);
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

        private void Form1_Load(object sender, EventArgs e)
        { 
          
        }

        private void DCoding_Click(object sender, EventArgs e)
        {
            Code.Text = DESEncrypt.Decrypt(Code.Text);
        }

        private void ECoding_Click(object sender, EventArgs e)
        {
            Code.Text = DESEncrypt.Encrypt(Code.Text);
        } 
         
    }
}
