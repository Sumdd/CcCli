using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using LumiSoft.Net;
using LumiSoft.Net.SIP.Stack;
using System.Threading;
using System.Windows.Forms;
using Common;
using System.IO;
using Core_v1;

namespace CenoSip {
    public class SipRegister {
        public static SIP_UA_Registration Registration = null;

        public delegate void RegStateChange(bool RegState);
        public static event RegStateChange _RegStateChange;

        public static bool RegSipServer() {
            bool RegState = false;
            try {

                #region ***兼容性:IP话机,Web调用
                ///<![CDATA[
                /// IP话机,与客户端连用,但是不进行注册SIP
                /// ]]>
                if (CCFactory.IsRegister == 0)
                {
                    //逻辑暂定这样处理:任意时刻都是注册成功即可
                    Log.Instance.Warn($"[CenoSip][SipRegister][RegSipServer][IP话机,不进行客户端的电话注册,直接注册成功即可]");
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                    return true;
                }
                if (CCFactory.IsRegister == -1)
                {
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_WEB_NOTREGISTER;
                    Log.Instance.Warn($"[CenoSip][SipRegister][RegSipServer][IP话机Web调用,不进行客户端的电话注册]");
                    return false;
                }
                #endregion

                if (SipParam.m_pStack == null)
                    if(!SipStack.InitStack())
                        return false;

                if(SipParam.m_pStack.Registrations != null) {
                    foreach(var item in SipParam.m_pStack.Registrations) {
                        item.Dispose();
                    }
                }

                AbsoluteUri url = AbsoluteUri.Parse("sip:" + Account.AccountID + "@" + CommonParam.GetLocalIpAddress + ":" + Account.LocalSipPort);
                SIP_Uri serveruri = new SIP_Uri();
                serveruri.Host = Account.HostAddess;
                serveruri.Port = Account.ServerSipPort;
                string aro = Account.AccountID + "@" + serveruri.Host;
                serveruri.User = Account.AccountID;

                Registration = SipParam.m_pStack.CreateRegistration(serveruri, aro, url, Account.Express);

                Log.Instance.Success($"[CenoSip][SipRegister][RegSipServer][ip:{CommonParam.GetLocalIpAddress}]");
                Log.Instance.Success($"[CenoSip][SipRegister][RegSipServer][serveruri:{serveruri.Value}]");
                Log.Instance.Success($"[CenoSip][SipRegister][RegSipServer][aro:{aro}]");
                Log.Instance.Success($"[CenoSip][SipRegister][RegSipServer][url:{url.Value}]");

                //Registration.StateChanged += new EventHandler(Registration_StateChanged);
                //Registration.Unregistered += new EventHandler(Registration_Unregistered);
                Registration.Registered += new EventHandler(Registration_Registered);
                Registration.Error += new EventHandler<SIP_ResponseReceivedEventArgs>(Registration_Error);

                Registration.BeginRegister(true);
                RegState = true;
            } catch(Exception ex) {
                Log.Instance.Success($"[CenoSip][SipRegister][RegSipServer][Exception][{ex.Message}]");
            }
            return RegState;
        }

        private static void Registration_Registered(object sender, EventArgs e) {
            if(_RegStateChange != null) {
                _RegStateChange(true);
            }
        }

        //private static void Registration_Unregistered(object sender, EventArgs e) {
        //    if(_RegStateChange != null) {
        //        _RegStateChange(false);
        //    }
        //}

        private static void Registration_Error(object sender, SIP_ResponseReceivedEventArgs e) {
            try {
                Log.Instance.Error($"[CenoSip][SipRegister][Registration_Error][{e.Response.StatusCodeType},{e.Response.StatusCode}]");
                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE, (IntPtr)0, (IntPtr)1000);
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipRegister][Registration_Error][Exception][{e.Response.StatusCodeType},{e.Response.StatusCode}][{ex.Message}]");
            }
        }

        //private static void Registration_StateChanged(object sender, EventArgs e) {
        //    if(_RegStateChange != null) {
        //        var _sender = ((SIP_UA_Registration)sender);
        //        if(_sender.State == SIP_UA_RegistrationState.Error) {
        //            _RegStateChange(false);
        //        }
        //    }
        //}

        public static bool UnregSipServer() {
            try {
                if(Registration == null)
                    return false;
                Registration.BeginUnregister(true);
                Log.Instance.Fail($"[CenoSip][SipRegister][UnregSipServer][SIP注销成功]");
                return true;
            } catch(Exception ex) {
                Log.Instance.Fail($"[CenoSip][SipRegister][UnregSipServer][Exception][{ex.Message}]");
                return false;
            } finally {
                Registration = null;
            }
        }

        public static bool IsRegister() {

            #region ***兼容性:IP话机,Web调用
            if (CCFactory.IsRegister == 0)
            {
                //Log.Instance.Warn($"[CenoSip][SipRegister][IsRegister][IP话机,总是注册即可]");
                return true;
            }
            #endregion

            if (Registration == null)
                return false;
            if(Registration.State == SIP_UA_RegistrationState.Registered)
                return true;
            return false;
        }
    }
}
