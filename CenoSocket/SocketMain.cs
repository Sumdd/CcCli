using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Common;
using DataBaseUtil;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;

namespace CenoSocket {
    public class SocketMain {

        /* 数据库里有好多Socket链接字符串,不知道为什么这么写,而实际上只是用到了一个
         * UnKnow */

        public static List<SocketStack> m_Sockets = null;

        #region Socket链接
        [Obsolete("请使用_Extend/WebSocket_v1/InWebSocketMain/Start方法")]
        public static string ConnectSocketServer() {
            DataTable ServerDt = Call_ServerListUtil.GetCallServerInfo();
            string ConnectInfo = "";

            if(m_Sockets == null)
                m_Sockets = new List<SocketStack>();
            else
                Close();

            if(ServerDt.Rows.Count <= 0)
                return "未找到服务器";

            for(int i = 0; i < ServerDt.Rows.Count; i++) {
                SocketStack _Stack = new SocketStack();
                _Stack._serverIP = ServerDt.Rows[i]["ServerIP"].ToString();
                _Stack._serverPort = int.Parse(ServerDt.Rows[i]["ServerPort"].ToString());
                _Stack._IsDefault = ServerDt.Rows[i]["IsDefault"].ToString() == "是";
                if(!_Stack.CreateNewConn()) {
                    ConnectInfo += ServerDt.Rows[i]["ServerName"].ToString() + "(" + ServerDt.Rows[i]["ServerIP"].ToString() + ")" + " 连接失败\r\n";
                    continue;
                }
                m_Sockets.Add(_Stack);
                SendData.SendMsgToServer(Call_SocketCommandUtil.GetConnectServerStr(new string[] { AgentInfo.AgentID, CommonParam.GetLocalIpAddress }), _Stack._Client);
            }
            return ConnectInfo;
        }
        #endregion

        #region Socket关闭
        public static void Close() {
            try {
                foreach(SocketStack item in m_Sockets) {
                    item._Client.Close();
                }
                m_Sockets = null;
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketMain), LOGLEVEL.ERROR, "释放Socket出错,原因:" + ex.Message);
            }
        }
        #endregion

        #region Socket信息处理
        /* UPDATE 考虑之后,觉得这里不能接收其他与拨号有关的内容了,客户端只有接的功能
         * 但这里的socket不是问答机制,也就是判断socket有没有发送成功就行了 */

        public static string GetHeader(DataStack dataStack) {
            return Call_SocketCommandUtil.GetHeadInfo(dataStack.HeadInfo);
        }

        public static string GetBody(DataStack dataStack, string name, int index) {
            return DataStack.GetValueByKey(dataStack.Content, Call_SocketCommandUtil.GetParamByHeadName(name)[index]);
        }

        public static void ExecuteData(string msg) {
            LogFile.Write(typeof(SocketMain), LOGLEVEL.INFO, "revice socket data:" + msg);
            if(msg.Length <= 0 || !msg.Contains("{") || !msg.Contains("}"))
                return;

            ArrayList SocketDatas = SocketParam.CutSocketData(msg);

            if(SocketDatas == null)
                return;
            for(int i = 0; i < SocketDatas.Count; i++) {
                DataStack _SocketInfo = new DataStack(SocketDatas[i].ToString());
                switch(Call_SocketCommandUtil.GetHeadInfo(_SocketInfo.HeadInfo)) {
                    case "LJFWQJG":
                        //LogFile.Write("连接服务器：" + _Socket.RemoteEndPoint.ToString() + "成功", "SOCKET");
                        //this.Sys_NoIc.ShowBalloonTip(5000, "连接服务器", "连接服务器：" + _Socket.RemoteEndPoint.ToString() + "成功", ToolTipIcon.Info);
                        break;
                    case "BHZT": {
                            string Status = DataStack.GetValueByKey(_SocketInfo.Content, Call_SocketCommandUtil.GetParamByHeadName("BHZT")[0]);
                            string Reason = DataStack.GetValueByKey(_SocketInfo.Content, Call_SocketCommandUtil.GetParamByHeadName("BHZT")[1]);
                            if(Status == "Fail") {
                                if(string.IsNullOrWhiteSpace(Reason)) {
                                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_REFUSE, (IntPtr)0, (IntPtr)0);
                                    return;
                                }
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL, (IntPtr)1, Marshal.StringToHGlobalAnsi(Reason));
                            } else if(Status == "Pick") {
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP, (IntPtr)0, (IntPtr)0);
                            }
                        }
                        break;
                    case "FSLY": {
                            string FileName = DataStack.GetValueByKey(_SocketInfo.Content, Call_SocketCommandUtil.GetParamByHeadName("FSLY")[0]);
                            string FilePath = DataStack.GetValueByKey(_SocketInfo.Content, Call_SocketCommandUtil.GetParamByHeadName("FSLY")[1]);
                            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_SETRECID, Marshal.StringToHGlobalAnsi(FileName), Marshal.StringToHGlobalAnsi(FilePath));
                        }
                        break;
                    case "FSLDHM": {
                            string FullStr = DataStack.GetValueByKey(_SocketInfo.Content, Call_SocketCommandUtil.GetParamByHeadName("FSLDHM")[0]);
                            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_OPENURL, (IntPtr)0, Marshal.StringToHGlobalAnsi(FullStr));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 

        #endregion

        #region Socket默认第一个吧
        public static SocketStack MainSocket() {
            try {
                if(m_Sockets == null) {
                    LogFile.Write(typeof(SocketMain), LOGLEVEL.FATAL, "默认Socket未初始化");
                } else {
                    SocketStack m_Socket = SocketMain.m_Sockets.Find(x => x._IsDefault == true);
                    return m_Socket;
                }
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketMain), LOGLEVEL.FATAL, "默认Socket获取失败:" + ex.Message);
            }
            return null;
        }
        #endregion

        #region Socket是否正常
        public static bool IsConnect(SocketStack socket) {
            try {
                socket._Client.Send(new byte[0] { }, 0, SocketFlags.None);
                return true;
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketMain), LOGLEVEL.FATAL, "测试Socket未连接:" + ex.Message);
                return false;
            }
        }
        #endregion
    }
}
