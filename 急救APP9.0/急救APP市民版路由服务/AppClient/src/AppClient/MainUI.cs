using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using ZIT.AppClient.Controller;
using ZIT.LOG;
using ZIT.AppClient.Model;


namespace ZIT.AppClient.UI
{
    public partial class MainUI : Form
    {
        /// <summary>
        /// 调用初始化网络委托
        /// </summary>
        public delegate void InvokeInitNetwork();

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainUI()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainUI_Load(object sender, EventArgs e)
        {
            try
            {
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
        /// 初始化应用程序
        /// </summary>
        private void InitProgram()
        {
            try
            {
                CoreService control = CoreService.GetInstance();
                control.BServerConnectionStatusChanged += BServer_ConnectionStatusChanged;
                //control.GServerConnectionStatusChanged += GServer_ConnectionStatusChanged;
                control.CallInServerConnectionStatusChanged += CallInServer_ConnectionStatusChanged;
                control.CarLocationServerConnectionStatusChanged += CarLocationServer_ConnectionStatusChanged;
                control.OtherMsgServerConnectionStatusChanged += OtherMsgServer_ConnectionStatusChanged;
                control.StartService();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("InitProgram", ex);

            }
        }

        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        private void BServer_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            lblBssConnectStatus.BeginInvoke(new MethodInvoker(() =>
            {
                switch (e.Status)
                {
                    case NetStatus.DisConnected:
                        lblBssConnectStatus.Text = "断开";
                        lblBssConnectStatus.ForeColor = Color.Red;
                        break;
                    case NetStatus.Connected:
                        lblBssConnectStatus.Text = "已连接";
                        lblBssConnectStatus.ForeColor = Color.Green;
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 与GPS业务服务器连接状态改变事件
        /// </summary>
        private void GServer_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            //lblGpsConnectStatus.BeginInvoke(new MethodInvoker(() =>
            //{
            //    switch (e.Status)
            //    {
            //        case NetStatus.DisConnected:
            //            lblGpsConnectStatus.Text = "断开";
            //            lblGpsConnectStatus.ForeColor = Color.Red;
            //            break;
            //        case NetStatus.Connected:
            //            lblGpsConnectStatus.Text = "已连接";
            //            lblGpsConnectStatus.ForeColor = Color.Green;
            //            break;
            //        default:
            //            break;
            //    }
            //}));
        }

        /// <summary>
        /// 与CarLocationServer连接状态改变事件
        /// </summary>
        private void CarLocationServer_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            lblCarLocationServerStatus.BeginInvoke(new MethodInvoker(() =>
            {
                switch (e.Status)
                {
                    case NetStatus.DisConnected:
                        lblCarLocationServerStatus.Text = "断开";
                        lblCarLocationServerStatus.ForeColor = Color.Red;
                        break;
                    case NetStatus.Connected:
                        lblCarLocationServerStatus.Text = "已连接";
                        lblCarLocationServerStatus.ForeColor = Color.Blue;
                        break;
                    case NetStatus.Login:
                        lblCarLocationServerStatus.Text = "已登录";
                        lblCarLocationServerStatus.ForeColor = Color.Green;
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 与CallInServer连接状态改变事件
        /// </summary>
        private void CallInServer_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            lblCallInServerStatus.BeginInvoke(new MethodInvoker(() =>
            {
                switch (e.Status)
                {
                    case NetStatus.DisConnected:
                        lblCallInServerStatus.Text = "断开";
                        lblCallInServerStatus.ForeColor = Color.Red;
                        break;
                    case NetStatus.Connected:
                        lblCallInServerStatus.Text = "已连接";
                        lblCallInServerStatus.ForeColor = Color.Blue;
                        break;
                    case NetStatus.Login:
                        lblCallInServerStatus.Text = "已登录";
                        lblCallInServerStatus.ForeColor = Color.Green;
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 与OtherMsgServer连接状态改变事件
        /// </summary>
        private void OtherMsgServer_ConnectionStatusChanged(object sender, StatusEventArgs e)
        {
            lblOtherMsgServerStatus.BeginInvoke(new MethodInvoker(() =>
            {
                switch (e.Status)
                {
                    case NetStatus.DisConnected:
                        lblOtherMsgServerStatus.Text = "断开";
                        lblOtherMsgServerStatus.ForeColor = Color.Red;
                        break;
                    case NetStatus.Connected:
                        lblOtherMsgServerStatus.Text = "已连接";
                        lblOtherMsgServerStatus.ForeColor = Color.Blue;
                        break;
                    case NetStatus.Login:
                        lblOtherMsgServerStatus.Text = "已登录";
                        lblOtherMsgServerStatus.ForeColor = Color.Green;
                        break;
                    default:
                        break;
                }
            }));
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "确定要退出么？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    CoreService.GetInstance().StopService();
                    Thread.Sleep(1000);
                    System.Environment.Exit(System.Environment.ExitCode);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void menuItemViewLog_Click(object sender, EventArgs e)
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

        private void menuItemExitSystem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "确定要退出么？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    CoreService.GetInstance().StopService();
                    Thread.Sleep(1000);
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogHelper.WriteBssNetMsgLog("你好");

        }

    }
}
