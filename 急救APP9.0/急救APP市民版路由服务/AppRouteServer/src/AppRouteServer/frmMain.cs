using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZIT.AppRouteServer.Controller;
using ZIT.LOG;

namespace ZIT.AppRouteServer.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化应用程序
        /// </summary>
        private void InitProgram()
        {
            try
            {
                CoreService control = CoreService.GetInstance();
                control.DBConnectStatusChanged += DB_ConnectionStatusChanged;
                control.CallInServerConnectedClientChanged += CallInServer_ConnectedClientChanged;
                control.CarLocationServerConnectedClientChanged += CarLocationServer_ConnectedClientChanged;
                control.OtherMsgServerConnectedClientChanged += OtherMsgServer_ConnectedClientChanged;
                control.Start();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("InitProgram", ex);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "确定要退出么？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    CoreService.GetInstance().Stop();
                    Thread.Sleep(1000);
                    System.Environment.Exit(System.Environment.ExitCode);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                InitGridCallIn();
                InitGridCarLocation();
                InitGridOtherMsg();

                Application.DoEvents();
                MethodInvoker init = new MethodInvoker(InitProgram);
                init.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("主窗体加载出错", ex);
            }
        }

        /// <summary>
        /// 与数据库连接状态改变事件
        /// </summary>
        private void DB_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            lblDBConnectStaus.BeginInvoke(new MethodInvoker(() =>
            {
                switch (e.Status)
                {
                    case NetStatus.DisConnected:
                        lblDBConnectStaus.Text = "断开";
                        lblDBConnectStaus.ForeColor = Color.Red;
                        break;
                    case NetStatus.Connected:
                        lblDBConnectStaus.Text = "已连接";
                        lblDBConnectStaus.ForeColor = Color.Green;
                        break;
                    default:
                        break;
                }
            }));
        }


        /// <summary>
        /// 处理车辆轨迹信息通道连接客户端改变事件
        /// </summary>
        private void CarLocationServer_ConnectedClientChanged(object sender, UnitsEventArgs e)
        {
            tabPageCarLocationChannel.BeginInvoke(new MethodInvoker(() =>
            {
                tabPageCarLocationChannel.Text = "车辆轨迹信息通道连接(" + e.Units.Count.ToString() + ")";
            }));

            GridCarLocation.BeginInvoke(new MethodInvoker(() =>
            {
                GridCarLocation.DataSource = e.Units;
                GridCarLocation.Refresh();
            }));
        }

        /// <summary>
        /// 处理其他信息通道连接客户端改变事件
        /// </summary>
        private void OtherMsgServer_ConnectedClientChanged(object sender, UnitsEventArgs e)
        {
            tabPageOtherMsgChannel.BeginInvoke(new MethodInvoker(() =>
            {
                tabPageOtherMsgChannel.Text = "其他信息通道连接(" + e.Units.Count.ToString() + ")";
            }));

            GridOtherMsg.BeginInvoke(new MethodInvoker(() =>
            {
                GridOtherMsg.DataSource = e.Units;
                GridOtherMsg.Refresh();
            }));
        }
        
        /// <summary>
        /// 处理APP电话呼入通道连接客户端改变事件
        /// </summary>
        private void CallInServer_ConnectedClientChanged(object sender, UnitsEventArgs e)
        {

            tabPageCallInChannel.BeginInvoke(new MethodInvoker(() =>
            {
                tabPageCallInChannel.Text = "APP电话呼入通道连接(" + e.Units.Count.ToString() + ")";
            }));

            GridCallIn.BeginInvoke(new MethodInvoker(() =>
            {
                GridCallIn.DataSource = e.Units;
                GridCallIn.Refresh();
            }));
        }
        
        /// <summary>
        /// 初始化GridCallIn列
        /// </summary>
        private void InitGridCallIn()
        {
            GridCallIn.ReadOnly = true;
            GridCallIn.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.Name = "IPAddress";
            c1.HeaderText = "IP地址";
            c1.DataPropertyName = "IPAddress";
            c1.Width = 120;

            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.Name = "UnitName";
            c2.HeaderText = "单位名称";
            c2.DataPropertyName = "UnitName";
            c2.Width = 190;

            DataGridViewTextBoxColumn c3 = new DataGridViewTextBoxColumn();
            c3.Name = "UnitCode";
            c3.HeaderText = "行政编码";
            c3.DataPropertyName = "UnitCode";
            c3.Width = 80;

            DataGridViewTextBoxColumn c4 = new DataGridViewTextBoxColumn();
            c4.Name = "Status";
            c4.DataPropertyName = "Status";
            c4.HeaderText = "连接状态";
            c4.Width = 80;

            DataGridViewTextBoxColumn c5 = new DataGridViewTextBoxColumn();
            c5.Name = "ConnectedTime";
            c5.DataPropertyName = "ConnectedTime";
            c5.HeaderText = "连接时间";
            c5.Width = 130;

            GridCallIn.Columns.Add(c1);
            GridCallIn.Columns.Add(c2);
            GridCallIn.Columns.Add(c3);
            GridCallIn.Columns.Add(c4);
            GridCallIn.Columns.Add(c5);
        }
        /// <summary>
        /// 初始化GridCarLocation列
        /// </summary>
        private void InitGridCarLocation()
        {
            GridCarLocation.ReadOnly = true;
            GridCarLocation.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.Name = "IPAddress";
            c1.HeaderText = "IP地址";
            c1.DataPropertyName = "IPAddress";
            c1.Width = 120;

            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.Name = "UnitName";
            c2.HeaderText = "单位名称";
            c2.DataPropertyName = "UnitName";
            c2.Width = 190;

            DataGridViewTextBoxColumn c3 = new DataGridViewTextBoxColumn();
            c3.Name = "UnitCode";
            c3.HeaderText = "行政编码";
            c3.DataPropertyName = "UnitCode";
            c3.Width = 80;

            DataGridViewTextBoxColumn c4 = new DataGridViewTextBoxColumn();
            c4.Name = "Status";
            c4.DataPropertyName = "Status";
            c4.HeaderText = "连接状态";
            c4.Width = 80;

            DataGridViewTextBoxColumn c5 = new DataGridViewTextBoxColumn();
            c5.Name = "ConnectedTime";
            c5.DataPropertyName = "ConnectedTime";
            c5.HeaderText = "连接时间";
            c5.Width = 130;

            GridCarLocation.Columns.Add(c1);
            GridCarLocation.Columns.Add(c2);
            GridCarLocation.Columns.Add(c3);
            GridCarLocation.Columns.Add(c4);
            GridCarLocation.Columns.Add(c5);
        }
        /// <summary>
        /// 初始化GridOtherMsg列
        /// </summary>
        private void InitGridOtherMsg()
        {
            GridOtherMsg.ReadOnly = true;
            GridOtherMsg.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.Name = "IPAddress";
            c1.HeaderText = "IP地址";
            c1.DataPropertyName = "IPAddress";
            c1.Width = 120;

            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.Name = "UnitName";
            c2.HeaderText = "单位名称";
            c2.DataPropertyName = "UnitName";
            c2.Width = 190;

            DataGridViewTextBoxColumn c3 = new DataGridViewTextBoxColumn();
            c3.Name = "UnitCode";
            c3.HeaderText = "行政编码";
            c3.DataPropertyName = "UnitCode";
            c3.Width = 80;

            DataGridViewTextBoxColumn c4 = new DataGridViewTextBoxColumn();
            c4.Name = "Status";
            c4.DataPropertyName = "Status";
            c4.HeaderText = "连接状态";
            c4.Width = 80;

            DataGridViewTextBoxColumn c5 = new DataGridViewTextBoxColumn();
            c5.Name = "ConnectedTime";
            c5.DataPropertyName = "ConnectedTime";
            c5.HeaderText = "连接时间";
            c5.Width = 130;

            GridOtherMsg.Columns.Add(c1);
            GridOtherMsg.Columns.Add(c2);
            GridOtherMsg.Columns.Add(c3);
            GridOtherMsg.Columns.Add(c4);
            GridOtherMsg.Columns.Add(c5);
        }


        private void GridCallIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (GridCallIn.CurrentRow != null)
            {
                GridCallIn.CurrentRow.Selected = false;
            }  
        }

        private void GridCarLocation_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (GridCarLocation.CurrentRow != null)
            {
                GridCarLocation.CurrentRow.Selected = false;
            } 
        }

        private void GridOtherMsg_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (GridOtherMsg.CurrentRow != null)
            {
                GridOtherMsg.CurrentRow.Selected = false;
            }
        }


        /// <summary>
        /// 查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuViewLog_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogPath;
                strLogPath = Directory.GetCurrentDirectory();
                strLogPath += "\\Log\\";
                if (Directory.Exists(strLogPath))
                {
                    Process.Start("explorer.exe", strLogPath);
                }
                else
                {
                    MessageBox.Show("未检测到日志文件目录！");
                }
            }
            catch { }
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }
    }
}
