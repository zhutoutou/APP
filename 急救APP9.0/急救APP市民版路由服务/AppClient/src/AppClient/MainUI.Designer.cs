namespace ZIT.AppClient.UI
{
    partial class MainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.lblBssConnectStatus = new System.Windows.Forms.Label();
            this.lblCallInServerStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCarLocationServerStatus = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblOtherMsgServerStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MenuSystem = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExitSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelStatus.SuspendLayout();
            this.MenuSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBssConnectStatus
            // 
            this.lblBssConnectStatus.AutoSize = true;
            this.lblBssConnectStatus.ForeColor = System.Drawing.Color.Red;
            this.lblBssConnectStatus.Location = new System.Drawing.Point(127, 13);
            this.lblBssConnectStatus.Name = "lblBssConnectStatus";
            this.lblBssConnectStatus.Size = new System.Drawing.Size(29, 12);
            this.lblBssConnectStatus.TabIndex = 2;
            this.lblBssConnectStatus.Text = "断开";
            // 
            // lblCallInServerStatus
            // 
            this.lblCallInServerStatus.AutoSize = true;
            this.lblCallInServerStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCallInServerStatus.Location = new System.Drawing.Point(127, 54);
            this.lblCallInServerStatus.Name = "lblCallInServerStatus";
            this.lblCallInServerStatus.Size = new System.Drawing.Size(29, 12);
            this.lblCallInServerStatus.TabIndex = 4;
            this.lblCallInServerStatus.Text = "断开";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "APP电话呼入通道：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "车辆轨迹信息通道：";
            // 
            // lblCarLocationServerStatus
            // 
            this.lblCarLocationServerStatus.AutoSize = true;
            this.lblCarLocationServerStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCarLocationServerStatus.Location = new System.Drawing.Point(295, 54);
            this.lblCarLocationServerStatus.Name = "lblCarLocationServerStatus";
            this.lblCarLocationServerStatus.Size = new System.Drawing.Size(29, 12);
            this.lblCarLocationServerStatus.TabIndex = 9;
            this.lblCarLocationServerStatus.Text = "断开";
            // 
            // panelStatus
            // 
            this.panelStatus.AutoSize = true;
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelStatus.Controls.Add(this.lblOtherMsgServerStatus);
            this.panelStatus.Controls.Add(this.label4);
            this.panelStatus.Controls.Add(this.label6);
            this.panelStatus.Controls.Add(this.lblCarLocationServerStatus);
            this.panelStatus.Controls.Add(this.lblBssConnectStatus);
            this.panelStatus.Controls.Add(this.label2);
            this.panelStatus.Controls.Add(this.label3);
            this.panelStatus.Controls.Add(this.lblCallInServerStatus);
            this.panelStatus.Location = new System.Drawing.Point(-2, 268);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(637, 76);
            this.panelStatus.TabIndex = 10;
            // 
            // lblOtherMsgServerStatus
            // 
            this.lblOtherMsgServerStatus.AutoSize = true;
            this.lblOtherMsgServerStatus.ForeColor = System.Drawing.Color.Red;
            this.lblOtherMsgServerStatus.Location = new System.Drawing.Point(448, 54);
            this.lblOtherMsgServerStatus.Name = "lblOtherMsgServerStatus";
            this.lblOtherMsgServerStatus.Size = new System.Drawing.Size(29, 12);
            this.lblOtherMsgServerStatus.TabIndex = 11;
            this.lblOtherMsgServerStatus.Text = "断开";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "其他信息通道：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "120业务服务器：";
            // 
            // MenuSystem
            // 
            this.MenuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.MenuSystem.Location = new System.Drawing.Point(0, 0);
            this.MenuSystem.Name = "MenuSystem";
            this.MenuSystem.Size = new System.Drawing.Size(642, 24);
            this.MenuSystem.TabIndex = 11;
            this.MenuSystem.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewLog,
            this.menuItemExitSystem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.系统ToolStripMenuItem.Text = "系统(S)";
            // 
            // menuItemViewLog
            // 
            this.menuItemViewLog.Name = "menuItemViewLog";
            this.menuItemViewLog.Size = new System.Drawing.Size(136, 22);
            this.menuItemViewLog.Text = "查看日志(L)";
            this.menuItemViewLog.Click += new System.EventHandler(this.menuItemViewLog_Click);
            // 
            // menuItemExitSystem
            // 
            this.menuItemExitSystem.Name = "menuItemExitSystem";
            this.menuItemExitSystem.Size = new System.Drawing.Size(136, 22);
            this.menuItemExitSystem.Text = "退出系统(E)";
            this.menuItemExitSystem.Click += new System.EventHandler(this.menuItemExitSystem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(H)";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(113, 22);
            this.menuItemAbout.Text = "关于(A)";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "服务正在运行中...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 37);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 354);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.MenuSystem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuSystem;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 381);
            this.MinimumSize = new System.Drawing.Size(650, 381);
            this.Name = "MainUI";
            this.Text = "App客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.MenuSystem.ResumeLayout(false);
            this.MenuSystem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBssConnectStatus;
        private System.Windows.Forms.Label lblCallInServerStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCarLocationServerStatus;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip MenuSystem;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemExitSystem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOtherMsgServerStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;


    }
}

