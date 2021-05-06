using CenoSocket;
using Common;
using Core_v1;
using DataBaseUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Cmn_v1;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model_v1;

namespace WebSocket_v1 {
    internal class InWebSocketDo {
        internal static void MainStep(string message) {
            try {

                ///<![CDATA[
                /// 增加freeswitch命令结果的处理分支
                /// ]]>
                if (message != null &&
                    message.Length >= Model_v1.m_mWebSocketJsonPrefix._m_sFSCmd.Length &&
                    message.StartsWith(Model_v1.m_mWebSocketJsonPrefix._m_sFSCmd))
                {

                    ///解析返回
                    string m_sMsg = message.Substring(Model_v1.m_mWebSocketJsonPrefix._m_sFSCmd.Length);
                    try
                    {
                        JObject m_pJObject = JObject.Parse(m_sMsg);
                        string m_sUse = m_pJObject["m_sUse"].ToString();

                        switch (m_sUse)
                        {
                            case m_cFSCmdType._m_sFSCmd:
                            case m_cFSCmdType._m_sDeleteGateway:
                            case m_cFSCmdType._m_sReadGateway:
                            case m_cFSCmdType._m_sCreateGateway:
                            case m_cFSCmdType._m_sWriteGateway:
                                #region ***发送并执行freeswitch命令,服务端处理事宜
                                {
                                    string m_sUUID = m_pJObject["m_oObject"]["m_sUUID"].ToString();
                                    int m_sStatus = Convert.ToInt32(m_pJObject["m_oObject"]["m_sStatus"]);
                                    string m_sResultMessage = m_pJObject["m_oObject"]["m_sResultMessage"].ToString();
                                    lock (InWebSocketMain.m_oLock)
                                    {
                                        var _m_lSendAsync = InWebSocketMain.m_lSendAsync.FindAll(x => x.m_sUUID == m_sUUID);
                                        if (_m_lSendAsync != null && _m_lSendAsync.Count > 0)
                                        {
                                            foreach (Model_v1.m_mSendAsync _m_mSendAsync in _m_lSendAsync)
                                            {
                                                _m_mSendAsync.m_iStatus = m_sStatus;
                                                _m_mSendAsync.m_oObject = m_sResultMessage;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[WebSocket_v1][InWebSocketDo][MainStep][Exception][{m_sMsg}:{ex.Message}]");
                    }
                    return;
                }

                ///<![CDATA[
                /// 文件命令
                /// ]]>
                if (message != null &&
                    message.Length >= Model_v1.m_mWebSocketJsonPrefix._m_sFileCmd.Length &&
                    message.StartsWith(Model_v1.m_mWebSocketJsonPrefix._m_sFileCmd))
                {

                    ///解析返回
                    string m_sMsg = message.Substring(Model_v1.m_mWebSocketJsonPrefix._m_sFileCmd.Length);
                    try
                    {
                        JObject m_pJObject = JObject.Parse(m_sMsg);
                        string m_sUse = m_pJObject["m_sUse"].ToString();

                        switch (m_sUse)
                        {
                            case m_cFileCmdType._m_sFileCreate:
                            case m_cFileCmdType._m_sFileDelete:
                                #region ***文件命令
                                {
                                    string m_sUUID = m_pJObject["m_oObject"]["m_sUUID"].ToString();
                                    int m_sStatus = Convert.ToInt32(m_pJObject["m_oObject"]["m_sStatus"]);
                                    string m_sResultMessage = m_pJObject["m_oObject"]["m_sResultMessage"].ToString();
                                    lock (InWebSocketMain.m_oLock)
                                    {
                                        var _m_lSendAsync = InWebSocketMain.m_lSendAsync.FindAll(x => x.m_sUUID == m_sUUID);
                                        if (_m_lSendAsync != null && _m_lSendAsync.Count > 0)
                                        {
                                            foreach (Model_v1.m_mSendAsync _m_mSendAsync in _m_lSendAsync)
                                            {
                                                _m_mSendAsync.m_iStatus = m_sStatus;
                                                _m_mSendAsync.m_oObject = m_sResultMessage;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[WebSocket_v1][InWebSocketDo][MainStep][Exception][{m_sMsg}:{ex.Message}]");
                    }
                    return;
                }

                Log.Instance.Success($"[WebSocket_v1][InWebSocketDo][MainStep][{message}]");
                ArrayList arrayList = SocketParam.CutSocketData(message);
                if(arrayList == null)
                    return;
                foreach(string item in arrayList) {
                    DataStack dataStack = new DataStack(item);
                    switch(SocketMain.GetHeader(dataStack)) {
                        case M_WebSocket._ljfwqjg:
                            _ljfwqjg_do(dataStack);
                            break;
                        case M_WebSocket._bhzt:
                            _bhzt_do(dataStack);
                            break;
                        case M_WebSocket._fsly:
                            _fsly_do(dataStack);
                            break;
                        case M_WebSocket._fsldhm:
                            _fsldhm_do(dataStack);
                            break;
                        default:
                            Log.Instance.Warn($"[WebSocket_v1][InWebSocketDo][MainStep][default][{InWebSocketMain.Prefix}检测为无效的操作类型]");
                            break;
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[WebSocket_v1][InWebSocketDo][MainStep][Exception][{ex.Message}][内容:{message}]");
            }
        }

        #region 连接服务器结果方法
        /// <summary>
        /// 连接服务器结果方法
        /// </summary>
        /// <param name="dataStack">数据对象</param>
        private static void _ljfwqjg_do(DataStack dataStack) {
            string _result = SocketMain.GetBody(dataStack, M_WebSocket._ljfwqjg, 0);
            string _reason = SocketMain.GetBody(dataStack, M_WebSocket._ljfwqjg, 1);
            switch(_result) {
                case M_WebSocket._ljfwqjg_success:
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketDo][_ljfwqjg_do][WebSocket连接成功]]");
                    break;
                case M_WebSocket._ljfwqjg_more:
                    InWebSocketMain.SetIsCanLogin(false);
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketDo][_ljfwqjg_do][{_reason}]]");
                    InWebSocketMain.WebSocket.Close(_reason);
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_EXIT, (IntPtr)0, Cmn.Sti(_reason));
                    break;
                case M_WebSocket._ljfwqjg_exit:
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketDo][_ljfwqjg_do][服务器退出]]");
                    break;
                default:
                    Log.Instance.Success($"[WebSocket_v1][InWebSocketDo][_ljfwqjg_do][未知的连接服务器消息]]");
                    break;
            }
        }
        #endregion

        #region 拨号状态方法
        private static void _bhzt_do(DataStack dataStack) {
            string _status = SocketMain.GetBody(dataStack, M_WebSocket._bhzt, 0);
            string _reason = SocketMain.GetBody(dataStack, M_WebSocket._bhzt, 1);
            switch(_status) {
                case M_WebSocket._bhzt_fail:
                    switch(_reason) {
                        case null:
                        case "":
                            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_REFUSE, (IntPtr)0, (IntPtr)0);
                            break;
                        default:
                            ///增加通道繁忙强断逻辑
                            if (_reason.Contains("Err通道繁忙") || _reason.Contains("Warn请重拨"))
                            {
                                _reason = "请重新拨号";
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)4);
                            }
                            else
                            {
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL, (IntPtr)0, Cmn.Sti(_reason));
                            }
                            break;
                    }
                    break;
                case M_WebSocket._bhzt_pick:
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP, (IntPtr)0, (IntPtr)0);
                    break;
                case M_WebSocket._bhzt_hang:
                    int m_uLp = 0;//对reason进行判断
                    if (!string.IsNullOrWhiteSpace(_reason) && _reason.Contains("对方")) m_uLp = -2;
                    else if (!string.IsNullOrWhiteSpace(_reason) && _reason.Contains("Err黑名单")) m_uLp = 3;
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)m_uLp);
                    break;
                case M_WebSocket._bhzt_call_busy:
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_ALTER, (IntPtr)0, Cmn.Sti(_reason));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 发送录音方法
        private static void _fsly_do(DataStack dataStack) {
            string _fileName = SocketMain.GetBody(dataStack, M_WebSocket._fsly, 0);
            string _filePath = SocketMain.GetBody(dataStack, M_WebSocket._fsly, 1);
            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_SETRECID, Cmn.Sti(_fileName), Cmn.Sti(_filePath));
        }
        #endregion

        #region 发送来电号码方法
        private static void _fsldhm_do(DataStack dataStack) {
            string _number = SocketMain.GetBody(dataStack, M_WebSocket._fsldhm, 0);
            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_OPENURL, (IntPtr)0, Cmn.Sti(_number));
        }
        #endregion

    }
}
