namespace ZIT.AppRouteServer.UI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblDBConnectStaus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabChannel = new System.Windows.Forms.TabControl();
            this.tabPageCallInChannel = new System.Windows.Forms.TabPage();
            this.GridCallIn = new System.Windows.Forms.DataGridView();
            this.tabPageCarLocationChannel = new System.Windows.Forms.TabPage();
            this.GridCarLocation = new System.Windows.Forms.DataGridView();
            this.tabPageOtherMsgChannel = new System.Windows.Forms.TabPage();
            this.GridOtherMsg = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.tabChannel.SuspendLayout();
            this.tabPageCallInChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCallIn)).BeginInit();
            this.tabPageCarLocationChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCarLocation)).BeginInit();
            this.tabPageOtherMsgChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOtherMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "SysMenu";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewLog,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(59, 21);
            this.menuFile.Text = "系统(S)";
            // 
            // MenuViewLog
            // 
            this.MenuViewLog.Name = "MenuViewLog";
            this.MenuViewLog.Size = new System.Drawing.Size(139, 22);
            this.MenuViewLog.Text = "查看日志(L)";
            this.MenuViewLog.Click += new System.EventHandler(this.MenuViewLog_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(139, 22);
            this.menuExit.Text = "退出系统(E)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 21);
            this.menuHelp.Text = "帮助(H)";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(116, 22);
            this.menuAbout.Text = "关于(A)";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.AutoSize = true;
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelStatus.Controls.Add(this.lblDBConnectStaus);
            this.panelStatus.Controls.Add(this.label1);
            this.panelStatus.Location = new System.Drawing.Point(0, 362);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(624, 42);
            this.panelStatus.TabIndex = 1;
            // 
            // lblDBConnectStaus
            // 
            this.lblDBConnectStaus.AutoSize = true;
            this.lblDBConnectStaus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDBConnectStaus.ForeColor = System.Drawing.Color.Red;
            this.lblDBConnectStaus.Location = new System.Drawing.Point(95, 13);
            this.lblDBConnectStaus.Name = "lblDBConnectStaus";
            this.lblDBConnectStaus.Size = new System.Drawing.Size(29, 12);
            this.lblDBConnectStaus.TabIndex = 1;
            this.lblDBConnectStaus.Text = "断开";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库连接：";
            // 
            // tabChannel
            // 
            this.tabChannel.Controls.Add(this.tabPageCallInChannel);
            this.tabChannel.Controls.Add(this.tabPageCarLocationChannel);
            this.tabChannel.Controls.Add(this.tabPageOtherMsgChannel);
            this.tabChannel.Location = new System.Drawing.Point(0, 28);
            this.tabChannel.Name = "tabChannel";
            this.tabChannel.SelectedIndex = 0;
            this.tabChannel.Size = new System.Drawing.Size(624, 328);
            this.tabChannel.TabIndex = 2;
            // 
            // tabPageCallInChannel
            // 
            this.tabPageCallInChannel.Controls.Add(this.GridCallIn);
            this.tabPageCallInChannel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCallInChannel.Name = "tabPageCallInChannel";
            this.tabPageCallInChannel.Size = new System.Drawing.Size(616, 302);
            this.tabPageCallInChannel.TabIndex = 0;
            this.tabPageCallInChannel.Text = "APP电话呼入通道连接(0)";
            this.tabPageCallInChannel.UseVisualStyleBackColor = true;
            // 
            // GridCallIn
            // 
            this.GridCallIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridCallIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridCallIn.Location = new System.Drawing.Point(0, 0);
            this.GridCallIn.Name = "GridCallIn";
            this.GridCallIn.ReadOnly = true;
            this.GridCallIn.RowHeadersVisible = false;
            this.GridCallIn.RowTemplate.Height = 23;
            this.GridCallIn.Size = new System.Drawing.Size(616, 302);
            this.GridCallIn.TabIndex = 0;
            this.GridCallIn.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridCallIn_DataBindingComplete);
            // 
            // tabPageCarLocationChannel
            // 
            this.tabPageCarLocationChannel.Controls.Add(this.GridCarLocation);
            this.tabPageCarLocationChannel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCarLocationChannel.Name = "tabPageCarLocationChannel";
            this.tabPageCarLocationChannel.Size = new System.Drawing.Size(616, 302);
            this.tabPageCarLocationChannel.TabIndex = 1;
            this.tabPageCarLocationChannel.Text = "车辆轨迹信息通道连接(0)";
            this.tabPageCarLocationChannel.UseVisualStyleBackColor = true;
            // 
            // GridCarLocation
            // 
            this.GridCarLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridCarLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridCarLocation.Location = new System.Drawing.Point(0, 0);
            this.GridCarLocation.Name = "GridCarLocation";
            this.GridCarLocation.RowHeadersVisible = false;
            this.GridCarLocation.RowTemplate.Height = 23;
            this.GridCarLocation.Size = new System.Drawing.Size(616, 302);
            this.GridCarLocation.TabIndex = 0;
            this.GridCarLocation.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridCarLocation_DataBindingComplete);
            // 
            // tabPageOtherMsgChannel
            // 
            this.tabPageOtherMsgChannel.Controls.Add(this.GridOtherMsg);
            this.tabPageOtherMsgChannel.Location = new System.Drawing.Point(4, 22);
            this.tabPageOtherMsgChannel.Name = "tabPageOtherMsgChannel";
            this.tabPageOtherMsgChannel.Size = new System.Drawing.Size(616, 302);
            this.tabPageOtherMsgChannel.TabIndex = 2;
            this.tabPageOtherMsgChannel.Text = "其他信息通道连接(0)";
            this.tabPageOtherMsgChannel.UseVisualStyleBackColor = true;
            // 
            // GridOtherMsg
            // 
            this.GridOtherMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridOtherMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridOtherMsg.Location = new System.Drawing.Point(0, 0);
            this.GridOtherMsg.Name = "GridOtherMsg";
            this.GridOtherMsg.RowHeadersVisible = false;
            this.GridOtherMsg.RowTemplate.Height = 23;
            this.GridOtherMsg.Size = new System.Drawing.Size(616, 302);
            this.GridOtherMsg.TabIndex = 1;
            this.GridOtherMsg.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GridOtherMsg_DataBindingComplete);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(624, 405);
            this.Controls.Add(this.tabChannel);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(640, 443);
            this.MinimumSize = new System.Drawing.Size(640, 443);
            this.Name = "frmMain";
            this.Text = "APP路由服务器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.tabChannel.ResumeLayout(false);
            this.tabPageCallInChannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCallIn)).EndInit();
            this.tabPageCarLocationChannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCarLocation)).EndInit();
            this.tabPageOtherMsgChannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridOtherMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblDBConnectStaus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabChannel;
        private System.Windows.Forms.TabPage tabPageCallInChannel;
        private System.Windows.Forms.TabPage tabPageCarLocationChannel;
        private System.Windows.Forms.DataGridView GridCallIn;
        private System.Windows.Forms.DataGridView GridCarLocation;
        private System.Windows.Forms.ToolStripMenuItem MenuViewLog;
        private System.Windows.Forms.TabPage tabPageOtherMsgChannel;
        private System.Windows.Forms.DataGridView GridOtherMsg;
    }
}

