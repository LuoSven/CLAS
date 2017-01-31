using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLAS.Common;
using System.Threading;
using CASL.Bll;
using CLAS.Model.Base;

namespace CLAS
{
    public partial class MainForm : BaseForm
    {

        private Hocy_Hook hook_Main = new Hocy_Hook();
 
        public MainForm()
        {          
            InitializeComponent();

            this.Text = "拍牌助手 v1.2";
            BidderName.Text = CommandManager.BidderName;
            CommandManager.instance.IsFrom51 = CommandManager.IsFor51;
            CommandManager.instance.IsTest = false;
            Width = 350;





            InstructionManager.instance.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,this);
            MouseEventHelper.Init(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //同步间隔
            SynchronizeManager.CommunicationSpan = 5000;
            CommandManager.TimeSyncSpan = 20000; 

          


#if DEBUG
            CommandManager.instance.IsFrom51 = true;
            CommandManager.instance.IsTest = true;
#endif
            if (CommandManager.instance.IsFrom51)
            {
                this.Text = "拍牌助手（51测试版）";
                CommandManager.TimeSyncSpan = 5000; 
            }
            if (CommandManager.instance.IsTest)
            {
                label4.Visible = true;
                KeyDownCount.Visible = true;
                Width = 600;
                btnReset.Visible = true;
            }

            CommandManager.instance.BeginWork();  //按键hook是否成功
            CommandManager.instance.IsLogKey = hook_Main.InstallHook("1");
            var message = CommandManager.instance.IsLogKey ? "软件初始化成功！" : "软件初始化失败，请重启软件！";

            LogManager.instance.Message(message);
            this.hook_Main.OnKeyPress += new KeyPressEventHandler(CommandManager.instance.KeyPressWatch);
            var sw = Screen.PrimaryScreen.Bounds.Width; 
            var x = sw - this.Width - 5;
            Location = new Point(x, 0);



        }


 
 
          
         private void MainForm_Load(object sender, EventArgs e)
         {
             //System.Windows.Forms.Timer time1 = new System.Windows.Forms.Timer();
             //time1.Interval = 1000;
             //time1.Tick += new System.EventHandler(this.timer1_Tick);
             //time1.Start();
          
         }
         

         private void timer1_Tick(object sender, EventArgs e)
         {
             Price.Text = CommandManager.Price.ToString();
             NowTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
             LoadMessage();
             if (CountDownDate >= DateTime.Now)
             {
                 var span = CountDownDate - DateTime.Now;
                 CountDownText.Text = string.Format("{0}秒后输入验证码", span.Seconds);
             }
             else
             {
                 CountDownText.Text = "";
             }

             if (CommandManager.instance.IsTest)
             {
                 KeyDownCount.Text = CommandManager.Keyloggers.GetString();
             }
         }

         public override void LoadMessage()
        {
            var message = CommandManager.LogReord;
            if (!string.IsNullOrEmpty(message))
                SyncMessage.AppendText(message);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var time= InstructionManager.instance.GetNetTime();
            if(time.HasValue)
            TimerHelper.SetDate(time.Value);
            System.Environment.Exit(0);
            //Close();
        }
     
        public override void AlertMessage(string message)
        {
            MessageBox.Show(message);
        }
        public override void CountDown(int seconds)
        {
            CountDownDate = DateTime.Now.AddSeconds(seconds);
        }

        public override void ClearMessage()
        {
            SyncMessage.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Price_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CommandManager.instance.ReworkWork();
        }

 
    }
}
