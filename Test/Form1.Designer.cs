namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SyncMessage = new System.Windows.Forms.TextBox();
            this.ExecuteConditionText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AddedSecond = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MillSecond = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ScriptExecTime = new System.Windows.Forms.DateTimePicker();
            this.CountDownText = new System.Windows.Forms.Label();
            this.ExcuteScript = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ScriptToExcute = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RefreshRecord = new System.Windows.Forms.Button();
            this.RecordInfo = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Code = new System.Windows.Forms.TextBox();
            this.DCoding = new System.Windows.Forms.Button();
            this.ECoding = new System.Windows.Forms.Button();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SyncMessage);
            this.tabPage1.Controls.Add(this.ExecuteConditionText);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.AddedSecond);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.MillSecond);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.ScriptExecTime);
            this.tabPage1.Controls.Add(this.CountDownText);
            this.tabPage1.Controls.Add(this.ExcuteScript);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.ScriptToExcute);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(215, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "测试脚本";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // SyncMessage
            // 
            this.SyncMessage.Location = new System.Drawing.Point(43, 240);
            this.SyncMessage.Multiline = true;
            this.SyncMessage.Name = "SyncMessage";
            this.SyncMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SyncMessage.Size = new System.Drawing.Size(162, 90);
            this.SyncMessage.TabIndex = 14;
            // 
            // ExecuteConditionText
            // 
            this.ExecuteConditionText.Location = new System.Drawing.Point(43, 63);
            this.ExecuteConditionText.Name = "ExecuteConditionText";
            this.ExecuteConditionText.Size = new System.Drawing.Size(162, 21);
            this.ExecuteConditionText.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F);
            this.label5.Location = new System.Drawing.Point(7, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "条件:";
            // 
            // AddedSecond
            // 
            this.AddedSecond.Font = new System.Drawing.Font("宋体", 12F);
            this.AddedSecond.Location = new System.Drawing.Point(43, 36);
            this.AddedSecond.Multiline = true;
            this.AddedSecond.Name = "AddedSecond";
            this.AddedSecond.Size = new System.Drawing.Size(33, 21);
            this.AddedSecond.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F);
            this.button1.Location = new System.Drawing.Point(82, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 22);
            this.button1.TabIndex = 10;
            this.button1.Text = "添加时间";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MillSecond
            // 
            this.MillSecond.Font = new System.Drawing.Font("宋体", 9F);
            this.MillSecond.Location = new System.Drawing.Point(176, 8);
            this.MillSecond.Multiline = true;
            this.MillSecond.Name = "MillSecond";
            this.MillSecond.Size = new System.Drawing.Size(29, 21);
            this.MillSecond.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(7, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "脚本:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "时间:";
            // 
            // ScriptExecTime
            // 
            this.ScriptExecTime.Font = new System.Drawing.Font("宋体", 9F);
            this.ScriptExecTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ScriptExecTime.Location = new System.Drawing.Point(43, 8);
            this.ScriptExecTime.MaxDate = new System.DateTime(2020, 12, 1, 0, 0, 0, 0);
            this.ScriptExecTime.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.ScriptExecTime.Name = "ScriptExecTime";
            this.ScriptExecTime.Size = new System.Drawing.Size(127, 21);
            this.ScriptExecTime.TabIndex = 5;
            this.ScriptExecTime.Value = new System.DateTime(2016, 10, 23, 0, 0, 0, 0);
            // 
            // CountDownText
            // 
            this.CountDownText.AutoSize = true;
            this.CountDownText.Location = new System.Drawing.Point(7, 240);
            this.CountDownText.Name = "CountDownText";
            this.CountDownText.Size = new System.Drawing.Size(0, 12);
            this.CountDownText.TabIndex = 3;
            // 
            // ExcuteScript
            // 
            this.ExcuteScript.Font = new System.Drawing.Font("宋体", 9F);
            this.ExcuteScript.Location = new System.Drawing.Point(43, 210);
            this.ExcuteScript.Name = "ExcuteScript";
            this.ExcuteScript.Size = new System.Drawing.Size(71, 24);
            this.ExcuteScript.TabIndex = 2;
            this.ExcuteScript.Text = "执行脚本";
            this.ExcuteScript.UseVisualStyleBackColor = true;
            this.ExcuteScript.Click += new System.EventHandler(this.ExcuteScript_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-121, -197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试的脚本";
            // 
            // ScriptToExcute
            // 
            this.ScriptToExcute.Location = new System.Drawing.Point(43, 90);
            this.ScriptToExcute.Multiline = true;
            this.ScriptToExcute.Name = "ScriptToExcute";
            this.ScriptToExcute.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ScriptToExcute.Size = new System.Drawing.Size(162, 115);
            this.ScriptToExcute.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 407);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RefreshRecord);
            this.tabPage2.Controls.Add(this.RecordInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(215, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "脚本执行结果";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RefreshRecord
            // 
            this.RefreshRecord.Location = new System.Drawing.Point(3, 311);
            this.RefreshRecord.Name = "RefreshRecord";
            this.RefreshRecord.Size = new System.Drawing.Size(47, 21);
            this.RefreshRecord.TabIndex = 1;
            this.RefreshRecord.Text = "刷新";
            this.RefreshRecord.UseVisualStyleBackColor = true;
            this.RefreshRecord.Click += new System.EventHandler(this.RefreshRecord_Click);
            // 
            // RecordInfo
            // 
            this.RecordInfo.Location = new System.Drawing.Point(3, 3);
            this.RecordInfo.Multiline = true;
            this.RecordInfo.Name = "RecordInfo";
            this.RecordInfo.Size = new System.Drawing.Size(209, 305);
            this.RecordInfo.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ECoding);
            this.tabPage3.Controls.Add(this.DCoding);
            this.tabPage3.Controls.Add(this.Code);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(624, 381);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "解密";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Code
            // 
            this.Code.Location = new System.Drawing.Point(6, 6);
            this.Code.Multiline = true;
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(610, 331);
            this.Code.TabIndex = 0;
            // 
            // DCoding
            // 
            this.DCoding.Location = new System.Drawing.Point(520, 343);
            this.DCoding.Name = "DCoding";
            this.DCoding.Size = new System.Drawing.Size(45, 32);
            this.DCoding.TabIndex = 1;
            this.DCoding.Text = "解密";
            this.DCoding.UseVisualStyleBackColor = true;
            this.DCoding.Click += new System.EventHandler(this.DCoding_Click);
            // 
            // ECoding
            // 
            this.ECoding.Location = new System.Drawing.Point(573, 343);
            this.ECoding.Name = "ECoding";
            this.ECoding.Size = new System.Drawing.Size(45, 32);
            this.ECoding.TabIndex = 2;
            this.ECoding.Text = "加密";
            this.ECoding.UseVisualStyleBackColor = true;
            this.ECoding.Click += new System.EventHandler(this.ECoding_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 407);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "测试";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button ExcuteScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ScriptToExcute;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label CountDownText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button RefreshRecord;
        private System.Windows.Forms.TextBox RecordInfo;
        private System.Windows.Forms.DateTimePicker ScriptExecTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MillSecond;
        private System.Windows.Forms.TextBox AddedSecond;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ExecuteConditionText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SyncMessage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button ECoding;
        private System.Windows.Forms.Button DCoding;
        private System.Windows.Forms.TextBox Code;

    }
}

