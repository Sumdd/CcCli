using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Collections;
using System.IO;

using Common;
using CenoSocket;
using WebBrowser;
using DataBaseUtil;
using CenoQnviccub;
using CenoSip;
using System.Timers;
using WebSocket_v1;
using Core_v1;
using Cmn_v1;
using System.Text.RegularExpressions;
using Model_v1;
using Newtonsoft.Json;
//using WebSocket_v1;

namespace CenoCC {
    public partial class MinChat : MoveForm {
        private bool CopyFlag = false;
        public static bool isRightNeedLoad = true;
        private IntPtr hWndActive;
        private IntPtr hWndCaret;
        private IntPtr nextClipboardViewer = (IntPtr)0;
        public static int CurrentCh = CCFactory.CurrentCh;
        public static int CallTimeLength = 0;
        public static PhoneNumPanel m_PhoneNumPanel;
        public static string m_PhoneNumber = string.Empty;
        public static string m_sSecretNumber = string.Empty;
        public static ShareNumber m_pShareNumber;
        private static string m_sUseNumber = string.Empty;

        public static IntPtr handlethis;

        public static Main_Frm MainBrowserForm;
        public static MinChat _MinChat;

        public static System.Timers.Timer SessionFlagTimer;
        public static System.Timers.Timer SessionTimeLenTimer;
        public static System.Timers.Timer SessionNoAnswerFlagTimer;

        private static object m_oLoadNumberLock = new object();
        private static bool m_bShareCopying = false;
        private static bool m_bApiShareCopying = false;

        public MinChat() {
            InitializeComponent();
            _MinChat = this;
            CCFactory.MainHandle = handlethis = this.Handle;

            {
                //加载一下下载配置
                Call_ClientParamUtil.m_fRecSetting();
                ///预加载是否显示联系人姓名
                if (Call_ParamUtil.m_bUseHomeSearch)
                {
                    List<string> m_lShowStyleList = Call_ClientParamUtil.ShowStyleString?.Split(',')?.ToList();
                    if (m_lShowStyleList?.Count > 1) Call_ClientParamUtil.m_bName = m_lShowStyleList[1] == "1";
                }
            }

            ///操作权限
            MinChat.m_fLoadOperatePower();

            IntiForm();
        }

        #region ***操作权限
        public static void m_fLoadOperatePower()
        {
            ///菜单显隐
            foreach (var item in MinChat._MinChat.contextMenuStrip1.Items)
            {
                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem m_pToolStripMenuItem = (ToolStripMenuItem)item;
                    if (m_pToolStripMenuItem.Tag == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(m_pToolStripMenuItem.Tag.ToString()))
                        continue;
                    ///拨号盘不做限制
                    if (m_pToolStripMenuItem.Name == "ShowDialPad_TSMI")
                        continue;
                    if (m_cPower.Has(m_pToolStripMenuItem.Tag.ToString()))
                        m_pToolStripMenuItem.Visible = true;
                    else
                        m_pToolStripMenuItem.Visible = false;
                }
                else if (item.GetType() == typeof(ToolStripSeparator))
                {
                    ToolStripSeparator m_pToolStripSeparator = (ToolStripSeparator)item;
                    if (m_pToolStripSeparator.Tag == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(m_pToolStripSeparator.Tag.ToString()))
                        continue;
                    if (m_cPower.Has(m_pToolStripSeparator.Tag.ToString()))
                        m_pToolStripSeparator.Visible = true;
                    else
                        m_pToolStripSeparator.Visible = false;
                }
            }
            ///防止超级管理员的权限消失
            if (AgentInfo.AgentID == "1000")
            {
                MinChat._MinChat.tsmi_Power.Visible = true;
            }
        }
        #endregion

        protected override void DefWndProc(ref Message m) {
            #region 板卡事件
            //if(m.Msg == BriSDKLib.BRI_EVENT_MESSAGE && CCFactory._PhoneType == GlobalData.PhoneType.TELEPHONE_BOX) {
            //    DriverMessage.PhoneMessage(m);
            //    return;
            //}
            #endregion
            #region 自定义事件
            if(m.Msg > CCFactory.WM_USER) {
                CusEvent.Cusdoo(this, m);
            }
            #endregion
            #region 系统回调函数
            else {
                switch(m.Msg) {
                    case (int)Win32API.WinMsg.WM_DRAWCLIPBOARD: //复制事件
                        {
                            #region ***监听剪切板,修正进入条件
                            try
                            {
                                if (//进入条件
                                    CopyFlag && Clipboard.ContainsText() && DataBaseUtil.Call_ClientParamUtil.m_bIsUseCopy &&
                                    CCFactory.ChInfo != null && CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE && !CCFactory.IsInCall
                                    )
                                {
                                    this.CopyFlag = false;
                                    string temp = Clipboard.GetText();
                                    string numbertemp = "";

                                    ///特殊指令前缀,无需进入
                                    if (temp.Contains("[cpy1]"))
                                    {
                                        this.CopyFlag = true;
                                        return;
                                    }

                                    #region ***融入32位加密号码判断
                                    bool m_bIsSureNumber = false;
                                    if (Call_ClientParamUtil.m_bQNRegexNumber)
                                    {
                                        Regex m_pRegex = new Regex("^[A-Z0-9]{32}$");
                                        if (m_pRegex.IsMatch(temp))
                                        {
                                            m_bIsSureNumber = true;
                                            numbertemp = temp;
                                            number_GlobelContextMenu_MI.ReadOnly = true;
                                        }
                                    }
                                    else if (temp.Length >= 3 && temp.Length <= 20)
                                    {
                                        Regex m_rReplaceRegex = new Regex("[^(0-9*#)]+");
                                        Regex m_rIsMatchRegex = new Regex("^[0-9*#]{3,20}$");
                                        temp = m_rReplaceRegex.Replace(temp, string.Empty);
                                        if (m_rIsMatchRegex.IsMatch(temp))
                                        {
                                            numbertemp = temp;
                                            m_bIsSureNumber = true;
                                            number_GlobelContextMenu_MI.ReadOnly = false;
                                        }
                                    }
                                    #endregion

                                    if (m_bIsSureNumber)
                                    {
                                        //设置可修改号码
                                        this.number_GlobelContextMenu_MI.Text = numbertemp;

                                        //移除
                                        lock (MinChat.m_oLoadNumberLock)
                                        {
                                            for (int _i = this.GlobleContextMenu.Items.Count - 1; _i >= 0; _i--)
                                            {
                                                if (this.GlobleContextMenu.Items[_i].Name.Contains(Special.ADD_))
                                                    this.GlobleContextMenu.Items.Remove(this.GlobleContextMenu.Items[_i]);
                                            }
                                        }

                                        if (Call_ClientParamUtil.m_bIsUseCopyNumber)
                                        {
                                            if (Call_ClientParamUtil.m_bIsUseSpRandom && !Call_ClientParamUtil.m_bIsUseShare)
                                            {
                                                this.dial_GlobelContextMenu_MI.Tag = Special.LOCAL_1_;
                                                this.dial_GlobelContextMenu_MI.Visible = true;
                                                this.tsmiAddZeroDial.Tag = Special.LOCAL_1_;
                                                //是否显示加零拨打
                                                if (Call_ClientParamUtil.m_bAutoAddNumDialFlag) this.tsmiAddZeroDial.Visible = false;
                                                else this.tsmiAddZeroDial.Visible = true;
                                            }
                                            else
                                            {
                                                #region ***专线号码,共享号码加入复制面板中
                                                //增加拨号号码
                                                try
                                                {
                                                    //查询
                                                    DataTable m_pLoaclDataTable = m_cEsyMySQL.m_fGetLocalNumberList(AgentInfo.AgentID);
                                                    //如果只有一个单线号码,不增加即可
                                                    if (m_pLoaclDataTable != null && m_pLoaclDataTable.Rows.Count > 0)
                                                    {
                                                        if (m_pLoaclDataTable.Rows.Count > 1)
                                                        {
                                                            this.dial_GlobelContextMenu_MI.Tag = null;
                                                            this.dial_GlobelContextMenu_MI.Visible = true;
                                                            this.tsmiAddZeroDial.Tag = null;
                                                            //是否显示加零拨打
                                                            if (Call_ClientParamUtil.m_bAutoAddNumDialFlag) this.tsmiAddZeroDial.Visible = false;
                                                            else this.tsmiAddZeroDial.Visible = true;
                                                            //加载专线
                                                            lock (MinChat.m_oLoadNumberLock)
                                                            {
                                                                foreach (DataRow item in m_pLoaclDataTable.Rows)
                                                                {
                                                                    ToolStripMenuItem m_pLoaclToolStripMenuItem = new ToolStripMenuItem();
                                                                    string m_sName = $"{Special.ADD_LOCAL_}{item["number"]}";
                                                                    if (this.GlobleContextMenu.Items.ContainsKey(m_sName)) continue;
                                                                    m_pLoaclToolStripMenuItem.Name = m_sName;
                                                                    string m_sArea = item["areaname"]?.ToString();
                                                                    if (string.IsNullOrWhiteSpace(m_sArea)) m_sArea = "-";
                                                                    string tnumber = item["tnumber"]?.ToString();
                                                                    if (string.IsNullOrWhiteSpace(tnumber)) tnumber = item["number"]?.ToString();
                                                                    m_pLoaclToolStripMenuItem.Text = $"专线：{tnumber}({m_sArea})";
                                                                    m_pLoaclToolStripMenuItem.Image = global::CenoCC.Properties.Resources.PickUp;
                                                                    m_pLoaclToolStripMenuItem.Tag = m_sName;
                                                                    m_pLoaclToolStripMenuItem.Click += new System.EventHandler(this.dial_GlobelContextMenu_MI_Click);
                                                                    int m_uCount = this.GlobleContextMenu.Items.Count - 1;
                                                                    this.GlobleContextMenu.Items.Insert(m_uCount, m_pLoaclToolStripMenuItem);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            DataRow item = m_pLoaclDataTable.Rows[0];
                                                            this.dial_GlobelContextMenu_MI.Tag = Special.LOCAL_1_;
                                                            this.dial_GlobelContextMenu_MI.Visible = true;
                                                            this.tsmiAddZeroDial.Tag = Special.LOCAL_1_;
                                                            //是否显示加零拨打
                                                            if (Call_ClientParamUtil.m_bAutoAddNumDialFlag) this.tsmiAddZeroDial.Visible = false;
                                                            else this.tsmiAddZeroDial.Visible = true;
                                                        }
                                                    }
                                                    //如果使用共享号码
                                                    if (Call_ClientParamUtil.m_bIsUseShare && !m_bShareCopying)
                                                    {
                                                        m_bShareCopying = true;
                                                        new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                                                        {
                                                            try
                                                            {
                                                                List<share_number> m_lShareNumber = Core_v1.Redis2.m_fGetShareNumberList();
                                                                if (m_lShareNumber != null && m_lShareNumber.Count > 0)
                                                                {
                                                                    lock (MinChat.m_oLoadNumberLock)
                                                                    {
                                                                        foreach (share_number m_pShareNumber in m_lShareNumber)
                                                                        {
                                                                            ToolStripMenuItem m_pLoaclToolStripMenuItem = new ToolStripMenuItem();
                                                                            string m_sName = $"{Special.ADD_SHARE_}{m_pShareNumber.number}";
                                                                            if (this.GlobleContextMenu.Items.ContainsKey(m_sName)) continue;
                                                                            m_pLoaclToolStripMenuItem.Name = m_sName;
                                                                            string m_sArea = m_pShareNumber.areaname;
                                                                            if (string.IsNullOrWhiteSpace(m_sArea)) m_sArea = "-";
                                                                            string tnumber = m_pShareNumber.tnumber;
                                                                            if (string.IsNullOrWhiteSpace(tnumber)) tnumber = m_pShareNumber.number;
                                                                            m_pLoaclToolStripMenuItem.Text = $"共享：{tnumber}({m_sArea})";
                                                                            m_pLoaclToolStripMenuItem.ForeColor = Color.Blue;
                                                                            m_pLoaclToolStripMenuItem.Image = global::CenoCC.Properties.Resources.PickUp;
                                                                            m_pLoaclToolStripMenuItem.Tag = m_sName;
                                                                            m_pLoaclToolStripMenuItem.Click += new System.EventHandler(this.dial_GlobelContextMenu_MI_Click);
                                                                            int m_uCount = this.GlobleContextMenu.Items.Count - 1;
                                                                            this.Invoke(new MethodInvoker(() =>
                                                                            {
                                                                                this.GlobleContextMenu.Items.Insert(m_uCount, m_pLoaclToolStripMenuItem);
                                                                            }));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Core_v1.Log.Instance.Error($"[CenoCC][MinChat][DefWndProc][Share][Thread][Exception][{ex.Message}]");
                                                            }
                                                            finally
                                                            {
                                                                m_bShareCopying = false;
                                                            }

                                                        })).Start();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Log.Instance.Error($"[CenoCC][MinChat][DefWndProc][WM_DRAWCLIPBOARD][Share][Exception][{ex.Message}]");
                                                    Log.Instance.Debug(ex);
                                                }
                                                #endregion
                                            }

                                            ///独立与专线、共享号码之外
                                            #region ***追加独立服务中的共享号码
                                            if (Call_ParamUtil.m_bUseApply && Call_ClientParamUtil.m_bUseApply && !m_bApiShareCopying)
                                            {
                                                m_bApiShareCopying = true;
                                                ///执行api加载号码,这里直接走自己的9464接口api,尽可能少调整客户端即可
                                                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                                                {
                                                    try
                                                    {
                                                        ///得到参数
                                                        string m_sResultString = H_Web.Get(m_cProfile.m_fUseShareApi(AgentInfo.UniqueID));
                                                        string m_sErrMsg = "无返回";
                                                        if (!string.IsNullOrWhiteSpace(m_sResultString))
                                                        {
                                                            m_mResponseJSON _m_mResponseJSON = JsonConvert.DeserializeObject<m_mResponseJSON>(m_sResultString);
                                                            if (_m_mResponseJSON.status == 0)
                                                            {
                                                                m_sErrMsg = string.Empty;
                                                                List<m_mShareApi> m_lShareApi = JsonConvert.DeserializeObject<List<m_mShareApi>>(_m_mResponseJSON.result.ToString());
                                                                //加载独立号码
                                                                lock (MinChat.m_oLoadNumberLock)
                                                                {
                                                                    for (int i = 0; i < m_lShareApi.Count; i++)
                                                                    {
                                                                        m_mShareApi item = m_lShareApi[i];
                                                                        if (string.IsNullOrWhiteSpace(item.gw_name)) continue;
                                                                        string m_sName = $"{Special.ADD_APISHARE_}{item.gw_name}&{item.call_server}";
                                                                        if (this.GlobleContextMenu.Items.ContainsKey(m_sName)) continue;
                                                                        ToolStripMenuItem m_pLoaclToolStripMenuItem = new ToolStripMenuItem();
                                                                        m_pLoaclToolStripMenuItem.Name = m_sName;
                                                                        m_pLoaclToolStripMenuItem.Text = $"服务：{item.gw_name}(-)";
                                                                        m_pLoaclToolStripMenuItem.ForeColor = Color.Purple;
                                                                        m_pLoaclToolStripMenuItem.Image = global::CenoCC.Properties.Resources.PickUp;
                                                                        m_pLoaclToolStripMenuItem.Tag = m_sName;
                                                                        m_pLoaclToolStripMenuItem.Click += new System.EventHandler(this.dial_GlobelContextMenu_MI_Click);
                                                                        int m_uCount = this.GlobleContextMenu.Items.Count - 1;
                                                                        this.GlobleContextMenu.Items.Insert(m_uCount, m_pLoaclToolStripMenuItem);
                                                                    }
                                                                }
                                                            }
                                                            else m_sErrMsg = _m_mResponseJSON.msg;
                                                        }
                                                        else
                                                        {
                                                            m_sErrMsg = "无返回";
                                                        }
                                                        if (!string.IsNullOrWhiteSpace(m_sErrMsg))
                                                        {
                                                            Log.Instance.Warn($"[CenoCC][MinChat][DefWndProc][WM_DRAWCLIPBOARD][ShareApi][{m_sErrMsg}]");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Instance.Error($"[CenoCC][MinChat][DefWndProc][WM_DRAWCLIPBOARD][ShareApi][Exception][{ex.Message}]");
                                                    }
                                                    finally
                                                    {
                                                        m_bApiShareCopying = false;
                                                    }

                                                })).Start();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            this.dial_GlobelContextMenu_MI.Tag = null;
                                            this.dial_GlobelContextMenu_MI.Visible = true;
                                            //是否显示加零拨打
                                            if (Call_ClientParamUtil.m_bAutoAddNumDialFlag) this.tsmiAddZeroDial.Visible = false;
                                            else this.tsmiAddZeroDial.Visible = true;
                                        }

                                        Point p = GetPopupPosition();
                                        //不加会有Bug
                                        this.Select();
                                        this.GlobleContextMenu.Show(p);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Instance.Error($"[CenoCC][MinChat][DefWndProc][Exception][{ex.Message}]");
                                m_bShareCopying = false;
                                m_bApiShareCopying = false;
                            }
                            #endregion
                            if (!CopyFlag) this.CopyFlag = true;
                        }
                        break;
                    case (int)Win32API.WinMsg.WM_HOTKEY: {
                            string w = m.WParam.ToString();
                            string l = m.LParam.ToString();
                            string r = m.Result.ToString();
                        }
                        break;
                    default:
                        base.DefWndProc(ref m);
                        break;
                }
            }
            #endregion
        }

        #region 获取鼠标位置
        private Point GetPopupPosition() {
            Point p = new Point();
            this.hWndActive = Win32API.GetForegroundWindow();
            // 如果是本弹出窗口，则返回鼠标位置坐标
            if(this.hWndActive.Equals(this.Handle)) {
                p = Control.MousePosition;
                this.hWndCaret = IntPtr.Zero;
                return p;
            }
            // 获取光标位置
            Win32API.GUITHREADINFO guiThreadInfo;
            Win32API.GetGUIInfo(hWndActive, out guiThreadInfo);
            p = new Point(guiThreadInfo.rectCaret.iRight, guiThreadInfo.rectCaret.iBottom);
            // 空坐标，返回鼠标坐标
            if(p.IsEmpty) {
                p = Control.MousePosition;
                this.hWndCaret = IntPtr.Zero;
                return p;
            }
            // 获取屏幕坐标
            this.hWndCaret = guiThreadInfo.hwndCaret;
            Win32API.ClientToScreen(guiThreadInfo.hwndCaret, out p);
            return p;
        }
        #endregion

        #region 窗体焦点快捷键
        protected override bool ProcessDialogKey(Keys keyData) {
            if(!CCFactory.IsInCall && CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                if(keyData == (Keys.Control | Keys.V)) {
                    if(CopyFlag && Clipboard.ContainsText()) {
                        var _chars = new Regex("[^(0-9*#)]+").Replace(Clipboard.GetText(), "");
                        if(new Regex("^[0-9*#]{3,20}$").IsMatch(_chars)) {
                            this.PhoneNum_Contact_Lbl.Text = _chars;
                            this.PhoneNum_Contact_Lbl.Tag = _chars;
                            this.Width = 180;
                            this.CallStatus_Lbl.Text = "空闲中";
                            this.CallInfo_Pnl.Visible = true;
                            CallTimeLength = 0;
                            this.DialTime_Lbl.Text = "00:00:00";
                            return true;
                        }
                    }
                } else if(keyData == Keys.Back) {
                    this.PhoneNumPanel_PhoneNumDel();
                    return true;
                } else if(keyData == Keys.Enter) {
                    this.Dial();
                    return true;
                }
            } else if(CCFactory.IsInCall && CCFactory.ChInfo[CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                if(keyData == Keys.Z) {
                    if(Cmn.MsgQuestion("确定挂断电话?") == DialogResult.Yes) {
                        Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)1);
                        return true;
                    }
                }
            }
            if(keyData == Keys.D0 || keyData == Keys.NumPad0) {
                return this.appendNumber(0);
            } else if(keyData == Keys.D1 || keyData == Keys.NumPad1) {
                return this.appendNumber(1);
            } else if(keyData == Keys.D2 || keyData == Keys.NumPad2) {
                return this.appendNumber(2);
            } else if(keyData == Keys.D3 || keyData == Keys.NumPad3) {
                return this.appendNumber(3);
            } else if(keyData == Keys.D4 || keyData == Keys.NumPad4) {
                return this.appendNumber(4);
            } else if(keyData == Keys.D5 || keyData == Keys.NumPad5) {
                return this.appendNumber(5);
            } else if(keyData == Keys.D6 || keyData == Keys.NumPad6) {
                return this.appendNumber(6);
            } else if(keyData == Keys.D7 || keyData == Keys.NumPad7) {
                return this.appendNumber(7);
            } else if(keyData == Keys.D8 || keyData == Keys.NumPad8) {
                return this.appendNumber(8);
            } else if(keyData == Keys.D9 || keyData == Keys.NumPad9) {
                return this.appendNumber(9);
            } else if(keyData == (Keys.Shift | Keys.D8) || keyData == Keys.Multiply) {
                return this.appendNumber("*");
            } else if(keyData == (Keys.Shift | Keys.D3)) {
                return this.appendNumber("#");
            } else if(keyData == Keys.B) {
                this.OpenBrowser();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region 键盘输入
        private bool appendNumber(object _number) {
            return PhoneNumPanel_PhoneNumDown(_number.ToString());
        }
        #endregion

        #region 窗体关闭
        protected override void OnFormClosing(FormClosingEventArgs e) {
            try {
                Log.Instance.Success($"[CenoCC][MinChat][OnFormClosed][停止通话]");
                if(SipParam.m_pCall != null && CCFactory.IsInCall) {
                    SipParam.m_pCall.Terminate("{C}hung up", true);
                }

                Log.Instance.Success($"[CenoCC][MinChat][OnFormClosed][停止WebSocket]");
                InWebSocketMain.SetIsCanLogin(false);
                InWebSocketMain.Stop();

                Log.Instance.Success($"[CenoCC][MinChat][OnFormClosed][注销电话]");
                if(SipRegister.IsRegister()) {
                    SipRegister.UnregSipServer();
                }

                Log.Instance.Success($"[CenoCC][MinChat][OnFormClosed][系统退出成功]");
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MinChat][OnFormClosed][Exception][系统退出时错误:{ex.Message}]");
            }

            base.OnFormClosing(e);
        }
        #endregion

        #region 窗体初始化
        private void IntiForm() {

            SessionNoAnswerFlagTimer = new System.Timers.Timer();
            SessionNoAnswerFlagTimer.Interval = 500;
            SessionNoAnswerFlagTimer.Elapsed += new System.Timers.ElapsedEventHandler(SessionNoAnswerFlagTimer_Tick);

            SessionFlagTimer = new System.Timers.Timer();
            SessionFlagTimer.Interval = 500;
            SessionFlagTimer.Elapsed += new System.Timers.ElapsedEventHandler(SessionFlagTimer_Tick);

            SessionTimeLenTimer = new System.Timers.Timer();
            SessionTimeLenTimer.Interval = 1000;
            SessionTimeLenTimer.Elapsed += new System.Timers.ElapsedEventHandler(SessionTimeLenTimer_Timer_Tick);

            //BackgroundWorker ConnectServerBW = new BackgroundWorker();
            //ConnectServerBW.DoWork += new DoWorkEventHandler(ConnectServerBW_DoWork);
            //ConnectServerBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ConnectServerBW_RunWorkerCompleted);
            //ConnectServerBW.RunWorkerAsync();
            BackgroundWorker bw_WebSocket = new BackgroundWorker();
            bw_WebSocket.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => {
                InWebSocketMain.Start();
            });
            bw_WebSocket.RunWorkerAsync();

            BackgroundWorker PhoneTypeBW = new BackgroundWorker();
            PhoneTypeBW.DoWork += new DoWorkEventHandler(PhoneTypeBW_DoWork);
            PhoneTypeBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PhoneTypeBW_RunWorkerCompleted);
            PhoneTypeBW.RunWorkerAsync();

            if(BrowserParam.AutoOpenPage == "1" && (!string.IsNullOrEmpty(BrowserParam.HomeUrl))) {
                if(MinChat.MainBrowserForm == null) {
                    MinChat.MainBrowserForm = new Main_Frm();
                    MinChat.MainBrowserForm.Show();
                }
                MinChat.MainBrowserForm.AddTab(BrowserParam.HomeUrl);
            }

            this.nextClipboardViewer = (IntPtr)Win32API.SetClipboardViewer((int)this.Handle);

            SessionTimer.StartTimerServices();
        }
        #endregion

        #region 原来连接电话服务器的方法
        //连接电话服务器    开始
        void ConnectServerBW_DoWork(object sender, DoWorkEventArgs e) {
            CCFactory.ConnectServerSeen = Call_ClientParamUtil.GetParamValueByName("ConnectServerFlag") == "1";
            if(CCFactory.ConnectServerSeen)
                e.Result = SocketMain.ConnectSocketServer();
        }

        void ConnectServerBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if(e.Result == null || string.IsNullOrEmpty(e.Result.ToString()))
                return;
        }
        //连接电话服务器    结束
        #endregion

        private void dial_GlobelContextMenu_MI_Click(object sender, EventArgs e) {

            ToolStripMenuItem m_pToolStripMenuItem = (ToolStripMenuItem)sender;
            if (m_pToolStripMenuItem.Tag != null)
            {
                MinChat.m_sUseNumber = m_pToolStripMenuItem.Tag.ToString();
            }

            ///<![CDATA[
            /// 增加号码隐藏逻辑
            /// ]]>
            if (!string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber))
            {
                MinChat.m_sSecretNumber = string.Empty;
                MinChat.m_PhoneNumber = string.Empty;
                this.PhoneNum_Contact_Lbl.Tag = null;
            }

            string number = this.number_GlobelContextMenu_MI.Text.Trim();
            if(!string.IsNullOrEmpty(number)) {
                if (((ToolStripMenuItem)sender).Name == "tsmiAddZeroDial")
                    number = $"0{number}";
                this.Dial(number);
            }
            this.Activate();
        }

        //查询电话类型    开始
        void PhoneTypeBW_DoWork(object sender, DoWorkEventArgs e) {
            CCFactory._PhoneType = Call_AgentUtil.GetPhoneType(AgentInfo.AgentID);
            if(CCFactory._PhoneType == GlobalData.PhoneType.TELEPHONE) {
                IntPtr i = handlethis;
                CCFactory.ChInfo = PhoneDeviceLib.OpenDevInfo(i);

                if(CCFactory.ChInfo != null && CCFactory.ChInfo.Length > 0)
                    CCFactory._PhoneType = GlobalData.PhoneType.TELEPHONE_BOX;
            }
        }

        void PhoneTypeBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            CCFactory.ChInfo = new ChannelInfo.CH_INFO[1];
            CCFactory.ChInfo[0].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE;
            CCFactory.ChInfo[0].uCallType = -1;
            CCFactory.ChInfo[0].lPlayFileHandle = -1;
            CCFactory.ChInfo[0].lRecFileHandle = -1;
            CCFactory.ChInfo[0].uChannelID = (short)0;
            CCFactory.ChInfo[0].szCalleeId = new StringBuilder(20);
            CCFactory.ChInfo[0].szCallerId = new StringBuilder(20);
            CCFactory.ChInfo[0].CallInRingCount = -1;
            CCFactory.ChInfo[0].lRecInfo.Caller = "";
            CCFactory.ChInfo[0].lRecInfo.IsUsable = false;
            CCFactory.ChInfo[0].lRecInfo.PhoneNumber = "";
            CCFactory.ChInfo[0].lRecInfo.UserID = "-1";
            CCFactory.ChInfo[0].IsSoftDial = false;
            switch(CCFactory._PhoneType) {
                case GlobalData.PhoneType.TELEPHONE:
                    BackgroundImage = global::CenoCC.Properties.Resources.Tubiao_Green;
                    break;
                case GlobalData.PhoneType.TELEPHONE_BOX:
                    BackgroundImage = global::CenoCC.Properties.Resources.Tubiao;
                    break;
                case GlobalData.PhoneType.SIP_SOFT_PHONE:
                case GlobalData.PhoneType.SIP_BOARD_PHONE:
                    BackgroundImage = global::CenoCC.Properties.Resources.Tubiao;
                    SipMain.GetAccountInfo();
                    /* MARK 这里是部分初始化,将其委托出来了 */
                    SipRegister._RegStateChange += new SipRegister.RegStateChange(SessionControl.SipRegStateHandle);
                    SipRegister.RegSipServer();
                    break;
                default:
                    BackgroundImage = global::CenoCC.Properties.Resources.Tubiao_Blue;
                    break;
            }
        }
        //查询电话类型    结束

        private void MinChat_MouseDoubleClick(object sender, MouseEventArgs e) {
            //return;
            if(DragTimer != null && DragTimer.Enabled)
                DragTimer.Stop();

            switch(CCFactory.ChInfo[CurrentCh].chStatus) {
                case ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE:
                    this.Dial();
                    break;
                case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP, (IntPtr)0, (IntPtr)1);
                    break;
                ///case ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING:
                ///case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK:
                ///防止错误,测试强度小一点,防止挂不断问题
                case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)1);
                    break;
                case ChannelInfo.APP_USER_STATUS.US_STATUS_HOLD:
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_HOLD, (IntPtr)0, (IntPtr)0);
                    break;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            try {
                this.AgentInfo_TSMI.Text = $"{AgentInfo.AgentName}({AgentInfo.RoleName})";

                switch(CCFactory.ChInfo[CurrentCh].chStatus) {
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE: {
                            this.CurrentStatus_TSMI.Text = "当前状态：空闲中";
                            this.Call_Tmsi.Enabled = true;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = true;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP: {
                            this.CurrentStatus_TSMI.Text = "当前状态：摘机中";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = true;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK: {
                            this.CurrentStatus_TSMI.Text = "当前状态：等待接听中";
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
                        {
                            this.CurrentStatus_TSMI.Text = "当前状态：来电响铃中";
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING: {
                            this.CurrentStatus_TSMI.Text = "当前状态：正在通话中";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = true;
                            this.Hold_TSMI.Enabled = true;
                            this.Transfer_TSMI.Enabled = true;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_HOLD: {
                            this.CurrentStatus_TSMI.Text = "当前状态：呼叫保持";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = true;
                            this.Transfer_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE: {
                            this.CurrentStatus_TSMI.Text = "当前状态：不可用";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_ERR_NODEVICEIN:
                        {
                            this.CurrentStatus_TSMI.Text = "当前状态：无音频输入设备";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_ERR_NODEVICEOUT:
                        {
                            this.CurrentStatus_TSMI.Text = "当前状态：无音频输出设备";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_ERR_NOTREGISTER:
                        {
                            this.CurrentStatus_TSMI.Text = "当前状态：非注册(话机)";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_WEB_NOTREGISTER:
                        {
                            this.CurrentStatus_TSMI.Text = "当前状态：非注册(IP话机Web调用)";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = false;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                            this.ShowDialPad_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_WAIT_LOCAL_HUNGDOW: {
                            this.CurrentStatus_TSMI.Text = "当前状态：请挂机";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = true;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_GETDTMF: {
                            this.CurrentStatus_TSMI.Text = "当前状态：等待按键";
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_DIALOUTTIME: {
                            this.CurrentStatus_TSMI.Text = "当前状态：拨号超时";
                        }
                        break;
                    case ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING: {
                            this.CurrentStatus_TSMI.Text = "当前状态：正在拨号中";
                            this.Call_Tmsi.Enabled = false;
                            this.HangUp_Tmsi.Enabled = true;
                            this.Hold_TSMI.Enabled = false;
                            this.Transfer_TSMI.Enabled = false;
                        }
                        break;
                    default: {
                            this.CurrentStatus_TSMI.Text = "当前状态：未知错误";
                        }
                        break;
                }

                this.RecentNoanswerCalls_TSMI.DropDownItems.Clear();
                if(CCFactory.RecentNoanswerRecords != null && CCFactory.RecentNoanswerRecords.Count > 0) {
                    this.RecentNoanswerCalls_TSMI.ForeColor = Color.Red;
                    this.RecentNoanswerCalls_TSMI.Text = "未接来电 (" + CCFactory.RecentNoanswerRecords.Count.ToString() + ")";
                    int a_int = 0;
                    foreach(M_kv _ in CCFactory.RecentNoanswerRecords) {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();
                        tsmi.Text = _.value;
                        tsmi.ToolTipText = "点击拨打此号码";
                        tsmi.Click += new EventHandler(delegate (object o, EventArgs ea) {
                            if(CCFactory.IsInCall || CCFactory.ChInfo[CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;
                            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                                try {
                                    Log.Instance.Error($"[CenoCC][MinChat][contextMenuStrip1_Opening][RecentNoanswerRecords][Thread][Exception][未接来电:{_.value},已拨打]");
                                    Call_Record.UpdateHandler(_.tag.ToString());
                                } catch(Exception ex) {
                                    Log.Instance.Error($"[CenoCC][MinChat][contextMenuStrip1_Opening][RecentNoanswerRecords][Thread][Exception][{ex.Message}]");
                                }
                            })).Start();
                            ///号码隐藏逻辑
                            MinChat.m_sSecretNumber = _.key?.ToString();
                            this.Dial(_.tag.ToString());
                        });
                        if(CCFactory.IsInCall || CCFactory.ChInfo[CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                            tsmi.Enabled = false;
                            tsmi.ToolTipText = "通话中,请稍后...";
                        }
                        this.RecentNoanswerCalls_TSMI.DropDownItems.Add(tsmi);
                        a_int++;
                        if(a_int < CCFactory.RecentNoanswerRecords.Count) {
                            ToolStripSeparator tss = new ToolStripSeparator();
                            this.RecentNoanswerCalls_TSMI.DropDownItems.Add(tss);
                        }
                    }
                } else {
                    this.RecentNoanswerCalls_TSMI.ForeColor = Color.Black;
                    this.RecentNoanswerCalls_TSMI.Text = "未接来电";
                }

                if(MinChat.isRightNeedLoad) {
                    MinChat.isRightNeedLoad = false;
                    this.OpenBrowser_Tsmi.DropDownItems.Clear();
                    var q_str = Call_ClientParamUtil.GetParamValueByName("QuickWebsite");
                    string[] l_str = q_str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (l_str == null) l_str = new string[] { };
                    if (l_str.Count() >= 0)
                    {
                        List<string> m_lUrlList = new List<string>();
                        #region ***是否启用共享文件路径
                        if (Call_ParamUtil.m_bIsUseHttpShare && !string.IsNullOrWhiteSpace(Call_ParamUtil.m_sHttpShareUrl))
                        {
                            if (!l_str.Contains(Call_ParamUtil.m_sHttpShareUrl, StringComparer.OrdinalIgnoreCase))
                                m_lUrlList.Add(Call_ParamUtil.m_sHttpShareUrl);
                            m_lUrlList.AddRange(l_str);
                        }
                        else
                        {
                            m_lUrlList.AddRange(l_str.ToList().Where(x => !x.Equals(Call_ParamUtil.m_sHttpShareUrl, StringComparison.OrdinalIgnoreCase)));
                        }
                        #endregion
                        #region ***增加统一的快捷网站
                        //暂无
                        #endregion
                        foreach (string i_str in m_lUrlList)
                        {
                            ToolStripMenuItem tsmi = new ToolStripMenuItem();
                            tsmi.Text = i_str;
                            tsmi.Click += new EventHandler((o, ea) =>
                            {
                                this.OpenBrowser(i_str);
                            });
                            this.OpenBrowser_Tsmi.DropDownItems.Add(tsmi);
                        }
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MinChat][contextMenuStrip1_Opening][Exception][{ex.Message}]");
            }
        }
    }
}
