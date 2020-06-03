using Core_v1;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;
using DataBaseUtil;
using Common;
using CenoSocket;
using System.Timers;
using Model_v1;
using Newtonsoft.Json;

namespace WebSocket_v1 {
    /// <summary>
    /// 客户端内部WebSocket通讯
    /// </summary>
    public sealed class InWebSocketMain {

        private static WebSocket m_WebSocket               = null;
        private static string    m_Uri                     = Call_ParamUtil.GetParamValueByName("InWebSocket");
        private const string     m_Prefix                  = "客户端内部WebSocket";
        private static Timer     m_AutoLoginWebSocketTimer = null;
        private static bool      m_IsCanLogin              = true;

        ///增加问答列表
        public static List<m_mSendAsync> m_lSendAsync = new List<m_mSendAsync>();
        public static object m_oLock = new object();

        /// <summary>
        /// 开启内部WebSocket
        /// </summary>
        public static void Start() {
            try {
                InWebSocketMain.m_WebSocket = new WebSocket(InWebSocketMain.m_Uri);
                InWebSocketMain.m_WebSocket.Opened += (object sender, EventArgs e) => {
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][Start][m_WebSocket][Opened][{InWebSocketMain.m_Uri}]");
                    string _ljfwq = M_Send._ljfwq();
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][Start][m_WebSocket][Opened][注册服务器,{_ljfwq}]");
                    InWebSocketMain.Send(_ljfwq);
                    //InWebSocketMain.TestSend();
                };
                InWebSocketMain.m_WebSocket.Closed += (object sender, EventArgs e) => {
                    Log.Instance.Warn($"[WebSocket_v1][InWebSocketMain][Start][m_WebSocket][Closed][{InWebSocketMain.m_Uri}]");
                    if(InWebSocketMain.m_IsCanLogin) {
                        InWebSocketMain.AutoStartWebSocketDo();
                    }
                };
                InWebSocketMain.m_WebSocket.MessageReceived += (object sender, MessageReceivedEventArgs e) => {

                    if (Call_ParamUtil.m_bWsDebug)
                    {
                        Log.Instance.Debug("From Server WebSocket Start");
                        Log.Instance.Debug(e.Message);
                        Log.Instance.Debug("From Server WebSocket End");
                    }

                    InWebSocketDo.MainStep(e.Message);
                };
                InWebSocketMain.m_WebSocket.DataReceived += (object sender, DataReceivedEventArgs e) => {
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][Start][m_WebSocket][DataReceived][不予处理,{sender},{Encoding.UTF8.GetString(e.Data)}]");
                };
                InWebSocketMain.m_WebSocket.Error += (object sender, ErrorEventArgs e) => {
                    Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][Start][m_WebSocket][Error][{sender},{e.Exception.Message}]");
                };
                InWebSocketMain.m_WebSocket.Open();
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][Start][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// 设置是否能再次登录
        /// </summary>
        /// <param name="_isCanLogin">是否多点登录,默认非</param>
        public static void SetIsCanLogin(bool _isCanLogin) {
            InWebSocketMain.m_IsCanLogin = _isCanLogin;
        }
        /// <summary>
        /// 返回客户端WebSocket实例
        /// </summary>
        public static WebSocket WebSocket {
            get {
                return InWebSocketMain.m_WebSocket;
            }
        }
        /// <summary>
        /// 日志前缀
        /// </summary>
        public static string Prefix {
            get {
                return InWebSocketMain.m_Prefix;
            }
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Send(string msg) {
            try {
                if(InWebSocketMain.m_WebSocket != null && InWebSocketMain.m_WebSocket.State == WebSocketState.Open) {
                    WebSocket.Send(msg);
                    return true;
                } else {
                    Log.Instance.Fail($"[WebSocket_v1][InWebSocketMain][Send][m_WebSocket][失败,内容:{msg}]");
                    return false;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][Send][Exception][{ex.Message}][内容:{msg}]");
                return false;
            }
        }
        /// <summary>
        /// 重启内部WebSocket
        /// </summary>
        public static void ReStart() {
            try {
                if(InWebSocketMain.m_WebSocket == null) {
                    InWebSocketMain.Start();
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][ReStart][m_WebSocket,{InWebSocketMain.m_Uri},正在开启,请稍后...]");
                    return;
                }
                switch(InWebSocketMain.m_WebSocket.State) {
                    case WebSocketState.None:
                    case WebSocketState.Closed:
                        InWebSocketMain.m_WebSocket.Open();
                        break;
                    case WebSocketState.Connecting:
                        Log.Instance.Warn($"[WebSocket_v1][InWebSocketMain][ReStart][m_WebSocket,{InWebSocketMain.m_Uri},正在连接,请稍后...]");
                        break;
                    case WebSocketState.Open:
                        Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][ReStart][m_WebSocket,{InWebSocketMain.m_Uri},已连接]");
                        break;
                    case WebSocketState.Closing:
                        Log.Instance.Warn($"[WebSocket_v1][InWebSocketMain][ReStart][m_WebSocket,{InWebSocketMain.m_Uri},正在关闭,请稍后...]");
                        break;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][ReStart][Exception][{InWebSocketMain.m_Uri},{ex.Message}]");
            }
        }
        /// <summary>
        /// 停止内部WebSocket
        /// </summary>
        public static void Stop() {
            try {
                if(InWebSocketMain.m_WebSocket == null)
                    return;
                switch(InWebSocketMain.m_WebSocket.State) {
                    case WebSocketState.None:
                    case WebSocketState.Closed:
                    case WebSocketState.Closing:
                        break;
                    case WebSocketState.Connecting:
                    case WebSocketState.Open:
                    default:
                        InWebSocketMain.m_WebSocket.Close();
                        break;
                }
                InWebSocketMain.m_WebSocket.Dispose();
                InWebSocketMain.m_WebSocket = null;
                Log.Instance.Warn($"[WebSocket_v1][InWebSocketMain][Stop][m_WebSocket][{InWebSocketMain.m_Uri}]");
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][Stop][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// 是否开启内部WebSocket
        /// </summary>
        /// <returns></returns>
        public static bool IsOpen() {
            try {
                if(InWebSocketMain.m_WebSocket == null)
                    return false;
                if(InWebSocketMain.m_WebSocket.State == WebSocketState.Open) {
                    InWebSocketMain.m_WebSocket.Send(new byte[0], 0, 0);
                    return true;
                }
                return false;
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][IsOpen][Exception][{ex.Message}]");
            }
            return false;
        }
        /// <summary>
        /// 自动开启WebSocket操作
        /// </summary>
        private static void AutoStartWebSocketDo() {
            try {
                if(InWebSocketMain.m_AutoLoginWebSocketTimer == null) {
                    InWebSocketMain.m_AutoLoginWebSocketTimer = new Timer(5000);
                    InWebSocketMain.m_AutoLoginWebSocketTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
                        if(!InWebSocketMain.IsOpen() && InWebSocketMain.m_IsCanLogin) {
                            InWebSocketMain.ReStart();
                        } else {
                            InWebSocketMain.m_AutoLoginWebSocketTimer.Stop();
                            Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][AutoLoginWebSocketTimerDo][已停止连接WebSocket计时器,{InWebSocketMain.m_Uri}]");
                        }
                    };
                }
                InWebSocketMain.m_AutoLoginWebSocketTimer.Start();
                Log.Instance.Success($"[WebSocket_v1][InWebSocketMain][AutoLoginWebSocketTimerDo][已开启计时器,正在连接WebSocket,{InWebSocketMain.m_Uri}]");
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][AutoLoginWebSocketTimerDo][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// 测试丢包粘包情况
        /// </summary>
        private static void TestSend() {
            for(int i = 0; i < 50; i++) {
                new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                    for(int j = 0; j < 50; j++) {
                        InWebSocketMain.Send($"{j}");
                    }
                })).Start();
            }
        }

        #region ***WebSocket问答方法
        /// <summary>
        /// 封装问答方法
        /// </summary>
        /// <param name="m_sPrefix"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static m_mResponseJSON SendAsyncObject(string msg, string m_sUse, string m_sPrefix = Model_v1.m_mWebSocketJsonPrefix._m_sFSCmd)
        {
            m_mResponseJSON _m_mResponseJSON = new m_mResponseJSON();
            try
            {
                m_mSendAsync _m_mSendAsync = new m_mSendAsync();
                lock (InWebSocketMain.m_oLock)
                {
                    var _m_lSendAsync = InWebSocketMain.m_lSendAsync.FindAll(x => x.m_sUUID == _m_mSendAsync.m_sUUID);
                    if (_m_lSendAsync == null || (_m_lSendAsync != null && _m_lSendAsync.Count <= 0))
                    {
                        InWebSocketMain.m_lSendAsync.Add(_m_mSendAsync);
                    }
                    else
                    {
                        Log.Instance.Warn($"[WebSocket_v1][InWebSocketMain][SendAsyncObject][{_m_mSendAsync.m_sUUID}:UUID exist]");
                    }
                }
                m_mWebSocketJson _m_mWebSocketJson = new m_mWebSocketJson();
                _m_mWebSocketJson.m_sUse = m_sUse;
                _m_mWebSocketJson.m_oObject = new
                {
                    m_sUUID = _m_mSendAsync.m_sUUID,
                    m_sSendMessage = msg
                };
                InWebSocketMain.Send(m_sPrefix, JsonConvert.SerializeObject(_m_mWebSocketJson), _m_mSendAsync);
                while (true)
                {
                    if (_m_mSendAsync.m_iStatus != -2)
                    {
                        _m_mResponseJSON.status = _m_mSendAsync.m_iStatus;
                        _m_mResponseJSON.msg = "异步消息应答成功";
                        _m_mResponseJSON.result = _m_mSendAsync.m_oObject;
                        lock (InWebSocketMain.m_oLock)
                            InWebSocketMain.m_lSendAsync.RemoveAll(x => x.m_sUUID == _m_mSendAsync.m_sUUID);
                        return _m_mResponseJSON;
                    }
                    if (((TimeSpan)(DateTime.Now - _m_mSendAsync.m_pDateTime)).TotalSeconds > _m_mSendAsync.m_uTimeout)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                lock (InWebSocketMain.m_oLock)
                    InWebSocketMain.m_lSendAsync.RemoveAll(x => x.m_sUUID == _m_mSendAsync.m_sUUID);
                _m_mResponseJSON.status = -1;
                _m_mResponseJSON.msg = "异步消息应答超时";
                Log.Instance.Fail($"[WebSocket_v1][InWebSocketMain][SendAsyncObject][{_m_mSendAsync.m_sUUID} reply timeout]");
                return _m_mResponseJSON;
            }
            catch (Exception ex)
            {
                _m_mResponseJSON.status = -1;
                _m_mResponseJSON.msg = $"异步发送消息错误:{ex.Message}";
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][SendAsyncObject][Exception][{ex.Message}][内容:{msg}]");
                return _m_mResponseJSON;
            }
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Send(string m_sPrefix, string msg, m_mSendAsync _m_mSendAsync = null)
        {
            try
            {
                if (InWebSocketMain.m_WebSocket != null && InWebSocketMain.m_WebSocket.State == WebSocketState.Open)
                {
                    WebSocket.Send($"{m_sPrefix}{msg}");
                    return true;
                }
                else
                {
                    Log.Instance.Fail($"[WebSocket_v1][InWebSocketMain][Send][m_WebSocket][失败,内容:{msg}]");

                    //失败之后直接给予结果
                    InWebSocketMain.m_fErrReply(_m_mSendAsync, "WebSocket未连接");

                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][Send][Exception][{ex.Message}][内容:{msg}]");

                //失败之后直接给予结果
                InWebSocketMain.m_fErrReply(_m_mSendAsync, $"发送时出错:{ex.Message}");

                return false;
            }
        }

        private static void m_fErrReply(m_mSendAsync _m_mSendAsync, string m_sMsg = null)
        {
            try
            {
                if (_m_mSendAsync != null)
                {
                    lock (InWebSocketMain.m_oLock)
                    {
                        _m_mSendAsync.m_iStatus = -1;
                        _m_mSendAsync.m_oObject = m_sMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketMain][m_fErrReply][Exception][{ex.Message}]");
            }
        }
        #endregion
    }
}
