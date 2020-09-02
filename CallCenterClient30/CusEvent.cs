using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CenoCC;
using System.Windows.Forms;
using Common;
using CenoSip;
using System.IO;
using System.Runtime.InteropServices;
using Core_v1;
using Cmn_v1;
using DataBaseUtil;

namespace CenoCC {
    public static class CusEvent {

        private static MinChat _MainChat = null;
        public static void Cusdoo(MinChat Mc, Message m) {
            try {
                _MainChat = Mc;
                if(_MainChat == null) {
                    LogFile.Write(typeof(CusEvent), LOGLEVEL.ERROR, "_MainChat未实例化");
                    return;
                }
                var Msg = m.Msg - CCFactory.WM_USER;
                switch(Msg) {

                    #region 摘机
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP: {

                            ///Log.Instance.Debug(CCFactory.ChInfo[CCFactory.CurrentCh].chStatus.ToString());
                            ///只有回铃声中、振铃中可以摘机
                            if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING ||
                                CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK)
                            {
                                if (_MainChat.InvokeRequired)
                                {
                                    _MainChat.BeginInvoke(new MethodInvoker(() =>
                                    {
                                        _MainChat.CallStatus_Lbl.Text = "通话中";
                                        _MainChat.Width = 180;
                                        MinChat.CallTimeLength = 0;
                                        _MainChat.DialTime_Lbl.Text = "00:00:00";
                                        MinChat.SessionNoAnswerFlagTimer.Stop();
                                        _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                        MinChat.SessionFlagTimer.Start();
                                        MinChat.SessionTimeLenTimer.Start();
                                        _MainChat.Call_Tmsi.Enabled = false;
                                        _MainChat.HangUp_Tmsi.Enabled = true;
                                        _MainChat.Hold_TSMI.Enabled = true;
                                        _MainChat.Transfer_TSMI.Enabled = true;
                                        _MainChat.ShowDialPad_TSMI.Enabled = true;
                                    }));
                                }
                                else
                                {
                                    _MainChat.CallStatus_Lbl.Text = "通话中";
                                    _MainChat.Width = 180;
                                    MinChat.CallTimeLength = 0;
                                    _MainChat.DialTime_Lbl.Text = "00:00:00";
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                    MinChat.SessionFlagTimer.Start();
                                    MinChat.SessionTimeLenTimer.Start();
                                    _MainChat.Call_Tmsi.Enabled = false;
                                    _MainChat.HangUp_Tmsi.Enabled = true;
                                    _MainChat.Hold_TSMI.Enabled = true;
                                    _MainChat.Transfer_TSMI.Enabled = true;
                                    _MainChat.ShowDialPad_TSMI.Enabled = true;
                                }
                                CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;
                                SipMain.Stop();
                                if (m.LParam == (IntPtr)1)
                                    SipParam.m_pCall.AcceptCall();
                                Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][接听电话]");
                            }
                        }
                        break;
                    #endregion

                    #region 软拨号
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].uCallType == -1) {
                                CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = 2;
                                SipMain.Stop();
                                SipMain.Play("Audio\\a_dial.wav", 1);
                            }
                        }
                        break;
                    #endregion

                    #region 获取号码
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_GETPHONE: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;
                            string Phone = Marshal.PtrToStringAnsi(m.LParam);
                            if(_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    _MainChat.CallStatus_Lbl.Text = "空闲中";
                                    _MainChat.CallInfo_Pnl.Visible = true;
                                    MinChat.CallTimeLength = 0;
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                    MinChat.SessionFlagTimer.Stop();
                                    _MainChat.Call_Flag_Pnl.Visible = false;
                                    MinChat.SessionTimeLenTimer.Stop();
                                    _MainChat.DialTime_Lbl.Text = "00:00:00";
                                    _MainChat.PhoneNum_Contact_Lbl.Tag = "";
                                    _MainChat.PhoneNumPanel_PhoneNumDown(Phone);
                                    _MainChat.Width = 180;
                                }));
                            } else {
                                _MainChat.CallStatus_Lbl.Text = "空闲中";
                                _MainChat.CallInfo_Pnl.Visible = true;
                                MinChat.CallTimeLength = 0;
                                MinChat.SessionNoAnswerFlagTimer.Stop();
                                _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                MinChat.SessionFlagTimer.Stop();
                                _MainChat.Call_Flag_Pnl.Visible = false;
                                MinChat.SessionTimeLenTimer.Stop();
                                _MainChat.DialTime_Lbl.Text = "00:00:00";
                                _MainChat.PhoneNum_Contact_Lbl.Tag = "";
                                _MainChat.PhoneNumPanel_PhoneNumDown(Phone);
                                _MainChat.Width = 180;
                            }
                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_DO_GETPHONE][催收号码获取{Phone}]");
                        }
                        break;
                    #endregion

                    #region 设置录音ID
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_SETRECID: {
                            string _RecID = Marshal.PtrToStringAnsi(m.WParam);
                            if(MinChat.MainBrowserForm != null) {
                                var wb = MinChat.MainBrowserForm.tew;
                                if(wb != null) {
                                    HtmlElement __RecID = null;
                                    if(wb.Document.Url.ToString().ToLower().Contains("mainframset"))
                                        __RecID = wb.Document.Window.Frames["mainFrame"].Document.GetElementById("RecID");
                                    else
                                        __RecID = wb.Document.GetElementById("RecID");
                                    if(__RecID != null) {
                                        __RecID.InnerText = _RecID;
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][写入录音ID成功,{_RecID}]");
                                        return;
                                    }
                                }
                            }
                            Log.Instance.Fail($"[CenoCC][CusEvent][Cusdoo][写入录音ID失败,{_RecID}]");
                        }
                        break;
                    #endregion

                    #region 来电、本地响铃
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING: {
                            string m_sCaller = Marshal.PtrToStringAnsi(m.WParam);
                            string m_sCallee = Marshal.PtrToStringAnsi(m.LParam);
                            if (_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    _MainChat.CallStatus_Lbl.Text = "来电";
                                    MinChat.CallTimeLength = 0;
                                    _MainChat.PhoneNum_Contact_Lbl.Text = m_sCaller;
                                    _MainChat.CallInfo_Pnl.Visible = true;
                                    _MainChat.Width = 180;
                                    _MainChat.DialTime_Lbl.Text = "00:00:00";
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                    MinChat.SessionFlagTimer.Start();
                                    MinChat.SessionTimeLenTimer.Start();
                                    _MainChat.Call_Tmsi.Enabled = true;
                                    _MainChat.HangUp_Tmsi.Enabled = true;
                                    _MainChat.Hold_TSMI.Enabled = false;
                                    _MainChat.Transfer_TSMI.Enabled = false;
                                    _MainChat.ShowDialPad_TSMI.Enabled = true;
                                }));
                            } else {
                                _MainChat.CallStatus_Lbl.Text = "来电";
                                MinChat.CallTimeLength = 0;
                                _MainChat.PhoneNum_Contact_Lbl.Text = m_sCaller;
                                _MainChat.CallInfo_Pnl.Visible = true;
                                _MainChat.Width = 180;
                                _MainChat.DialTime_Lbl.Text = "00:00:00";
                                MinChat.SessionNoAnswerFlagTimer.Stop();
                                _MainChat.NoAnswer_Flag_Pnl.Visible = false;
                                MinChat.SessionFlagTimer.Start();
                                MinChat.SessionTimeLenTimer.Start();
                                _MainChat.Call_Tmsi.Enabled = true;
                                _MainChat.HangUp_Tmsi.Enabled = true;
                                _MainChat.Hold_TSMI.Enabled = false;
                                _MainChat.Transfer_TSMI.Enabled = false;
                                _MainChat.ShowDialPad_TSMI.Enabled = true;
                            }

                            string m_sPhoneAddress = "未知";
                            m_cPhone.m_fSetShow(m_sCaller, out m_sPhoneAddress, m_sCallee);

                            if (Call_ClientParamUtil.m_bIsSysMsgCall)
                            {
                                string m_sLParam = $"{m_sCaller},{m_sPhoneAddress},{m_sCallee}";
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_ALTER, (IntPtr)0, Cmn.Sti(m_sLParam));
                            }

                            CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING;
                            CCFactory.ChInfo[CCFactory.CurrentCh].szCallerId = new StringBuilder(m_sCaller);
                            SipMain.Stop();
                            SipMain.Play("Audio\\a_ring.wav", 20);
                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][来电][{m_sCaller}]");
                        }
                        break;
                    #endregion

                    #region 回铃声中
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK: {
                            if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;

                            ///设置为回铃声中状态
                            CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK;

                            if(_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    _MainChat.CallStatus_Lbl.Text = "回铃声中";
                                }));
                            } else {
                                _MainChat.CallStatus_Lbl.Text = "回铃声中";
                            }
                            SipParam.m_pCall.AcceptCall();
                            SipMain.Stop();
                            SipMain.Play("Audio\\a_calling.wav", 20);
                            LogFile.Write(typeof(CusEvent), LOGLEVEL.INFO, "回铃声中");
                        }
                        break;
                    #endregion

                    #region 通话中
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].uCallType == -1) {
                                CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = 2;
                                SipMain.Stop();
                                SipMain.Play("Audio\\a_dial.wav", 1);
                            }
                        }
                        break;
                    #endregion

                    #region 挂机
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;

                            #region ***号码池页面关闭
                            if (MinChat.m_pShareNumber != null && !MinChat.m_pShareNumber.IsDisposed)
                            {
                                MinChat.m_pShareNumber.Close();
                                MinChat.m_pShareNumber = null;
                            }
                            #endregion
                            ///状态放置在此位置
                            int ABI = (int)m.LParam;
                            string ABHang = string.Empty;
                            string m_sText = "挂机";
                            if (ABI == 1)
                            {
                                ABHang = "A";
                            }
                            else if (ABI == 2)
                            {
                                ABHang = "B";
                                m_sText = "对方挂机";
                            }
                            else if (ABI == 3)
                            {
                                m_sText = "Err黑名单";
                            }
                            else if (ABI == 4)
                            {
                                ABHang = "X";
                                m_sText = "请重新拨号";
                            }
                            else if (ABI == -2)
                            {
                                m_sText = "对方挂机";
                            }
                            int m_uWParam = (int)m.WParam;
                            if (m_uWParam == 1) ABHang = string.Empty;
                            SessionControl.Phone_Temminate(ABHang);
                            SipMain.Stop();
                            if (_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                    _MainChat.CallStatus_Lbl.Text = m_sText;
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                    MinChat.SessionFlagTimer.Stop();
                                    _MainChat.Call_Flag_Pnl.Visible = false;
                                    MinChat.SessionTimeLenTimer.Stop();
                                }));
                            } else {
                                _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                _MainChat.CallStatus_Lbl.Text = m_sText;
                                MinChat.SessionNoAnswerFlagTimer.Stop();
                                _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                MinChat.SessionFlagTimer.Stop();
                                _MainChat.Call_Flag_Pnl.Visible = false;
                                MinChat.SessionTimeLenTimer.Stop();
                            }
                            CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = -1;
                            CCFactory.ChInfo[CCFactory.CurrentCh].szCalleeId = new StringBuilder();
                            CCFactory.ChInfo[CCFactory.CurrentCh].szCallerId = new StringBuilder();
                            SipMain.Stop();
                            if (ABI == 2 || ABI == -2)
                                SipMain.Play("Audio\\a_hungup.wav", 1);
                            CCFactory.IsInCall = false;
                            LogFile.Write(typeof(CusEvent), LOGLEVEL.INFO, m_sText);
                        }
                        break;
                    #endregion

                    #region 拨号失败
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;
                            string m_sReason = Marshal.PtrToStringAnsi(m.LParam);
                            if(_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    //_MainChat.CallStatus_Lbl.Text = "拨号失败";
                                    _MainChat.CallStatus_Lbl.Text = m_sReason;
                                    //_MainChat.DialTime_Lbl.Text = "00:00:00";
                                    _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                    MinChat.SessionFlagTimer.Stop();
                                    _MainChat.Call_Flag_Pnl.Visible = false;
                                    MinChat.SessionTimeLenTimer.Stop();
                                }));
                            } else {
                                //_MainChat.CallStatus_Lbl.Text = "拨号失败";
                                _MainChat.CallStatus_Lbl.Text = m_sReason;
                                //_MainChat.DialTime_Lbl.Text = "00:00:00";
                                _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                MinChat.SessionNoAnswerFlagTimer.Stop();
                                _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                MinChat.SessionFlagTimer.Stop();
                                _MainChat.Call_Flag_Pnl.Visible = false;
                                MinChat.SessionTimeLenTimer.Stop();
                            }
                            CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = -1;
                            SipMain.Stop();
                            CCFactory.IsInCall = false;
                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][{m_sReason}]");

                            if (m_sReason == "Err呼叫主叫")
                            {
                                #region ***IP话机无需注册
                                if (CCFactory.IsRegister == 0)
                                {
                                    Log.Instance.Warn($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][IP话机,不进行客户端的电话注册,直接注册成功即可]");
                                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                                    return;
                                }
                                #endregion

                                if (CCFactory.isReStartSip) {
                                    Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][SIP重新注册中,请稍后...]");
                                    return;
                                }
                                CCFactory.isReStartSip = true;
                                _MainChat.BeginInvoke(new MethodInvoker(() =>
                                {
                                    _MainChat.CurrentStatus_TSMI.Text = "正在重新连接...";
                                }));
                                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                                {
                                    try
                                    {
                                        CenoSip.SipRegister.UnregSipServer();
                                        System.Threading.Thread.Sleep(333);
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][SIP注销]");
                                        CenoSip.SipRegister.RegSipServer();
                                        System.Threading.Thread.Sleep(666);
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][SIP注册]");
                                        if (!SipRegister.IsRegister()) {
                                            if(SipRegister.Registration != null) {
                                                SipRegister.Registration.BeginRegister(true);
                                                Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][SIP再次注册]");
                                            }
                                        }
                                        _MainChat.BeginInvoke(new MethodInvoker(() =>
                                        {
                                            _MainChat.CallStatus_Lbl.Text = "请重新拨号";
                                        }));
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Instance.Error($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][thread][{ex.Message}]");
                                    }
                                    finally
                                    {
                                        CCFactory.isReStartSip = false;
                                        CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_STATUS_DIALFAIL][SIP重新注册完成]");
                                    }

                                })).Start();
                            }
                            else
                            {
                                CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                            }
                        }
                        break;
                    #endregion

                    #region 拒接未接
                    case (int)ChannelInfo.APP_USER_STATUS.US_STATUS_REFUSE: {
                            if(CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                                return;
                            if(_MainChat.InvokeRequired) {
                                _MainChat.BeginInvoke(new MethodInvoker(() => {
                                    _MainChat.CallStatus_Lbl.Text = "对方未接";
                                    _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                    MinChat.SessionNoAnswerFlagTimer.Stop();
                                    _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                    MinChat.SessionFlagTimer.Stop();
                                    _MainChat.Call_Flag_Pnl.Visible = false;
                                    MinChat.SessionTimeLenTimer.Stop();
                                }));
                            } else {
                                _MainChat.CallStatus_Lbl.Text = "对方未接";
                                _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                                MinChat.SessionNoAnswerFlagTimer.Stop();
                                _MainChat.NoAnswer_Flag_Pnl.Visible = true;
                                MinChat.SessionFlagTimer.Stop();
                                _MainChat.Call_Flag_Pnl.Visible = false;
                                MinChat.SessionTimeLenTimer.Stop();
                            }
                            CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                            CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = -1;
                            SipMain.Stop();
                            SipMain.Play("Audio\\a_hungup.wav", 1);
                            CCFactory.IsInCall = false;
                            LogFile.Write(typeof(CusEvent), LOGLEVEL.INFO, "对方未接");
                        }
                        break;
                    #endregion

                    #region 来电弹屏
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_OPENURL: {
                            var splits = Marshal.PtrToStringAnsi(m.LParam).Split(',');
                            var PhoneNumber = splits[0];
                            var FileName = splits[1];
                            var FilePath = splits[2];

                            ///兼容加密号码
                            if (Call_ClientParamUtil.m_bQNRegexNumber)
                            {
                                ///所有都视为呼出即可
                                if (MinChat.MainBrowserForm != null)
                                {
                                    var wb = MinChat.MainBrowserForm.tew;
                                    if (wb != null)
                                    {
                                        HtmlElement __RecID = null;
                                        if (wb.Document.Url.ToString().ToLower().Contains("mainframset"))
                                            __RecID = wb.Document.Window.Frames["mainFrame"].Document.GetElementById("RecID");
                                        else
                                            __RecID = wb.Document.GetElementById("RecID");
                                        if (__RecID != null)
                                        {
                                            __RecID.InnerText = FileName;
                                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_DO_OPENURL][写入录音ID成功,{FileName}]");
                                            return;
                                        }
                                    }
                                }
                                Log.Instance.Fail($"[CenoCC][CusEvent][Cusdoo][US_DO_OPENURL][写入录音ID失败,{FileName}]");
                                return;
                            }

                            var _autoOpenDial = Call_ClientParamUtil.GetParamValueByName("AutoOpenDial");
                            if(!Cmn.IntToBoolean(_autoOpenDial)) {
                                Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][来电弹屏电话:{PhoneNumber},录音:{FileName},录音路径:{FilePath},来电弹屏已经禁用]");
                                return;
                            }
                            string arg = "TelNumber=" + PhoneNumber + "&RecID=" + FileName;
                            _MainChat.OpenBrowser(WebBrowser.BrowserParam.ExtendUrl + arg);
                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][来电弹屏电话:{PhoneNumber},录音:{FileName},录音路径:{FilePath}]");
                        }
                        break;
                    #endregion

                    #region 拨号
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_DAIL: {
                            var _ = Cmn.Its(m.LParam);
                            Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][呼叫{_}]");
                            ///<![CDATA[
                            /// 增加号码隐藏逻辑
                            /// ]]>
                            if (!string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber))
                            {
                                MinChat.m_sSecretNumber = string.Empty;
                                MinChat.m_PhoneNumber = string.Empty;
                                _MainChat.PhoneNum_Contact_Lbl.Tag = null;
                            }
                            _MainChat.Dial(_);
                        }
                        break;
                    #endregion

                    #region 空闲中
                    case (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_IDLE: {
                            CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                            _MainChat.BeginInvoke(new MethodInvoker(() =>
                            {
                                _MainChat.CurrentStatus_TSMI.Text = "当前状态：空闲中";
                            }));
                        }
                        break;
                    #endregion

                    #region 不可用
                    case (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE: {

                            #region ***兼容性:IP话机,Web调用
                            if (CCFactory.IsRegister == 0)
                            {
                                Log.Instance.Warn($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][IP话机,不进行客户端的电话注册,直接注册成功即可]");
                                CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                                return;
                            }
                            #endregion

                            var _number = Cmn.Its(m.WParam);
                            if(CCFactory.isReStartSip) {
                                Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][SIP重新注册中,请稍后...]");
                                if(!string.IsNullOrWhiteSpace(_number)) {
                                    _MainChat.Dial(_number, true);
                                }
                                return;
                            }
                            CCFactory.isReStartSip = true;
                            _MainChat.BeginInvoke(new MethodInvoker(() => {
                                _MainChat.CurrentStatus_TSMI.Text = "正在重新连接...";
                            }));
                            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                                try {
                                    var _sleep = (int)m.LParam;
                                    if(_sleep > 0) {
                                        SipRegister.UnregSipServer();
                                        System.Threading.Thread.Sleep(_sleep);
                                    }
                                    if(!SipRegister.IsRegister()) {
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][不可用时SIP重新注册]");
                                        SipRegister.RegSipServer();
                                        System.Threading.Thread.Sleep(_sleep);
                                        if(!SipRegister.IsRegister()) {
                                            if(SipRegister.Registration != null) {
                                                SipRegister.Registration.BeginRegister(true);
                                            }
                                        }
                                    } else {
                                        Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][不可用时,SIP重新注册时检测到已经注册]");
                                        CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                                    }
                                    if(!string.IsNullOrWhiteSpace(_number)) {
                                        System.Threading.Thread.Sleep(1000);
                                        _MainChat.Dial(_number, true);
                                    }
                                } catch(Exception ex) {
                                    Log.Instance.Error($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][Exception][当前状态：不可用时SIP重新注册时错误：{ex.Message}]");
                                } finally {
                                    CCFactory.isReStartSip = false;
                                    Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_LOAD_STATUS_UNAVAILABLE][SIP重新注册中完成]");
                                }
                            })).Start();
                        }
                        break;
                    #endregion

                    #region 重置SIP协议栈
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_SIP_RESET:

                        #region ***兼容性:IP话机,Web调用
                        if (CCFactory.IsRegister == 0)
                        {
                            Log.Instance.Warn($"[CenoCC][CusEvent][Cusdoo][US_DO_SIP_RESET][IP话机,不进行客户端的电话注册,直接注册成功即可]");
                            CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                            return;
                        }
                        #endregion

                        new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                            try {
                                SipRegister.UnregSipServer();
                                SipStack.Dispose();
                                SipStack.InitStack();
                                SipRegister.RegSipServer();
                            } catch(Exception ex) {
                                Log.Instance.Success($"[CenoCC][CusEvent][Cusdoo][US_DO_SIP_RESET][{ex.Message}]");
                            } finally {
                                SipStack.m_pReStarting = false;
                                SipStack.m_pReStartingTime = DateTime.Now;
                                SipStack.m_pResetCount = 0;
                            }
                        })).Start();
                        break;
                    #endregion

                    #region 强制退出
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_EXIT: {
                            Cmn.MsgWran($"呼叫中心客户端检测到:{Cmn.Its(m.LParam)}", "强制退出");
                            Application.Exit();
                        }
                        break;
                    #endregion

                    #region 呼叫保持动作
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_HOLD: {
                            bool m_bCancelHold = (int)m.LParam == 1;
                            if (m_bCancelHold)
                            {
                                _MainChat.Hold_TSMI.Tag = null;
                                _MainChat.BeginInvoke(new MethodInvoker(()=> {
                                    //_MainChat.CallStatus_Lbl.Text = "空闲中";
                                    _MainChat.Hold_TSMI.Text = "保持(&K)";
                                    _MainChat.Hold_TSMI.Image = global::CenoCC.Properties.Resources.sound_off;
                                }));
                                if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_HOLD)
                                {
                                    RtpStack.DO_F_Hold_OFF();
                                }
                                Log.Instance.Success($"挂断后,强制关闭呼叫保持");
                            }
                            else
                            {
                                if (_MainChat.Hold_TSMI.Tag == null)
                                {
                                    if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING)
                                    {
                                        CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_HOLD;
                                        _MainChat.Hold_TSMI.Tag = "ON";
                                        _MainChat.BeginInvoke(new MethodInvoker(() => {
                                            _MainChat.Hold_TSMI.Text = "取消呼叫保持(&K)";
                                            _MainChat.CallStatus_Lbl.Text = "呼叫保持";
                                            _MainChat.Hold_TSMI.Image = global::CenoCC.Properties.Resources.sound_on;
                                        }));
                                        RtpStack.DO_F_Hold_ON();
                                        Log.Instance.Success($"开启呼叫保持");
                                    }
                                }
                                else
                                {
                                    _MainChat.Hold_TSMI.Tag = null;
                                    _MainChat.BeginInvoke(new MethodInvoker(() => {
                                        _MainChat.Hold_TSMI.Text = "保持(&K)";
                                        _MainChat.Hold_TSMI.Image = global::CenoCC.Properties.Resources.sound_off;
                                    }));
                                    if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_HOLD)
                                    {
                                        RtpStack.DO_F_Hold_OFF();
                                        CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;
                                        _MainChat.BeginInvoke(new MethodInvoker(() => {
                                            _MainChat.CallStatus_Lbl.Text = "通话中";
                                        }));
                                    }
                                    Log.Instance.Success($"关闭呼叫保持");
                                }
                            }
                        }
                        break;
                    #endregion

                    #region ***弹屏
                    case (int)ChannelInfo.APP_USER_STATUS.US_DO_ALTER:
                        {
                            string m_sMsg = Marshal.PtrToStringAnsi(m.LParam);
                            string[] m_lMsg = m_sMsg?.Split(',');
                            if (m_lMsg.Length > 2)
                            {
                                _MainChat.notifyIcon.ShowBalloonTip(0, $"分机号：{m_lMsg[2]}\r\n来　电：{m_lMsg[0]}\r\n归属地：{m_lMsg[1]}", $" ", ToolTipIcon.Info);
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][CusEvent][Cusdoo][Exception][{ex.Message}]");
            }
        }
    }
}
