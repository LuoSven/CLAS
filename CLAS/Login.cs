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

namespace CLAS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           var result= ActivationCodeManager.instance.CheckActivationCodeAndSave(textBox1.Text);

           if (result)
           {
               var form = new MainForm();
               form.Show();
               this.Hide();
           }
           else
           {
               MessageBox.Show("验证码有误，请联系管理员");
           }
        }
    }
}
