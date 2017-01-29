using CASL.Bll;
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

namespace CLAS
{
    public partial class Login : Form
    {
        public bool IsCancel = false; 

        private static string[] ar ;
        private static int spitCount =30;
        public Login()
        {
            InitializeComponent();
            ar = new string[spitCount+1];
            for (var i = 0; i < ar.Length; i++)
            {
                if (i == spitCount)
                {
                    ar[i] = "。";
                }
                else
                {
                    var s = new List<string>();
                    for (var n = 0; n <= i; n++)
                    {
                        s.Add("。");
                    }
                    ar[i] = string.Join("", s);
                }
                
            }
            ;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ActivationCodeManager.instance.SendActivationCodeAndCheck(textBox1.Text);
                button1.Enabled = false;
                LoginMessage.Text = "验证中";
                while (true)
                {
                    if (IsCancel || ActivationCodeManager.instance.IsSuccess.HasValue)
                    {

                        if (ActivationCodeManager.instance.IsSuccess.HasValue && ActivationCodeManager.instance.IsSuccess.Value)
                        {
                            button1.Text = "验证成功！初始化中";
                            Application.DoEvents();
                            ActivationCodeManager.instance.IsSuccess = null;
                            ActivationCodeManager.instance.check = null;
#if !DEBUG
     
                            ShortCutHelper.CreateShorCut("拍牌助手", "ca", "拍牌助手");
                            DownloadManager.instance.DownLoadDmDlls(DownloadManager.dmcDllPath);                       
#endif

#if DEBUG

                            DownloadManager.downLoadStatus = DownloadManager.maxdownLoadStatus;                     
#endif
                        }
                        else
                        {
                            if (ActivationCodeManager.instance.IsSuccess.HasValue)
                            {
                                MessageBox.Show("验证码有误，请联系管理员");
                            }
                            ActivationCodeManager.instance.IsSuccess = null;
                            ActivationCodeManager.instance.check = null;
                            IsCancel = false;
                            button1.Enabled = true;
                            LoginMessage.Text = "";
                            break;
                        }

                    }
                    else if (DownloadManager.downLoadStatus == 3)
                    {
                        var form = new MainForm();
                        form.Show();
                        Hide();
                        break;
                    }
                    else
                    {
                        LoginMessage.Text = "验证中" + ar[LoginMessage.Text.Replace("验证中", "").Length];
                    }

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        

        
        
          
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsCancel = true;
        }
    }
}
