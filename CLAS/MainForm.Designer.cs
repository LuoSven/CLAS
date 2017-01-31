namespace CLAS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.NowTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SyncMessage = new System.Windows.Forms.TextBox();
            this.CountDownText = new System.Windows.Forms.Label();
            this.Price = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BidderName = new System.Windows.Forms.Label();
            this.KeyDownCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NowTime
            // 
            this.NowTime.AutoSize = true;
            this.NowTime.Location = new System.Drawing.Point(174, 28);
            this.NowTime.Name = "NowTime";
            this.NowTime.Size = new System.Drawing.Size(41, 12);
            this.NowTime.TabIndex = 1;
            this.NowTime.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "时间:";
            // 
            // SyncMessage
            // 
            this.SyncMessage.Location = new System.Drawing.Point(9, 52);
            this.SyncMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SyncMessage.Multiline = true;
            this.SyncMessage.Name = "SyncMessage";
            this.SyncMessage.Size = new System.Drawing.Size(316, 164);
            this.SyncMessage.TabIndex = 3;
            // 
            // CountDownText
            // 
            this.CountDownText.AutoSize = true;
            this.CountDownText.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CountDownText.ForeColor = System.Drawing.Color.Red;
            this.CountDownText.Location = new System.Drawing.Point(6, 220);
            this.CountDownText.Name = "CountDownText";
            this.CountDownText.Size = new System.Drawing.Size(72, 27);
            this.CountDownText.TabIndex = 4;
            this.CountDownText.Text = "倒计时";
            // 
            // Price
            // 
            this.Price.AutoSize = true;
            this.Price.Location = new System.Drawing.Point(51, 28);
            this.Price.Name = "Price";
            this.Price.Size = new System.Drawing.Size(41, 12);
            this.Price.TabIndex = 5;
            this.Price.Text = "label2";
            this.Price.Click += new System.EventHandler(this.Price_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "价格:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "拍手:";
            // 
            // BidderName
            // 
            this.BidderName.AutoSize = true;
            this.BidderName.Location = new System.Drawing.Point(51, 8);
            this.BidderName.Name = "BidderName";
            this.BidderName.Size = new System.Drawing.Size(29, 12);
            this.BidderName.TabIndex = 8;
            this.BidderName.Text = "拍手";
            // 
            // KeyDownCount
            // 
            this.KeyDownCount.AutoSize = true;
            this.KeyDownCount.Location = new System.Drawing.Point(400, 8);
            this.KeyDownCount.Name = "KeyDownCount";
            this.KeyDownCount.Size = new System.Drawing.Size(11, 12);
            this.KeyDownCount.TabIndex = 9;
            this.KeyDownCount.Text = "0";
            this.KeyDownCount.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "按键记录:";
            this.label4.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(256, 220);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(69, 27);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "重置模拟";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(332, 250);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.KeyDownCount);
            this.Controls.Add(this.BidderName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Price);
            this.Controls.Add(this.CountDownText);
            this.Controls.Add(this.SyncMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NowTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "拍牌助手 v1.0";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label NowTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SyncMessage;
        private System.Windows.Forms.Label CountDownText;
        private System.Windows.Forms.Label Price;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label BidderName;
        private System.Windows.Forms.Label KeyDownCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReset;

    }
}