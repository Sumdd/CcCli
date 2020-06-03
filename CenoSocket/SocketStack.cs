using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Common;
using System.Net;
using System.Threading;
using DataBaseUtil;

namespace CenoSocket {
    public class SocketStack {
        public Socket _Client = null;
        public bool IsExit = true;
        public string _serverIP {
            set; get;
        }
        public int _serverPort {
            set; get;
        }
        public bool _IsDefault {
            set; get;
        }

        /// <summary>
        /// 新建Socket连接
        /// </summary>
        /// <returns>返回连接是否成功</returns>
        public bool CreateNewConn() {
            bool result = false;
            try {
                _Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                if(string.IsNullOrEmpty(CommonParam.GetLocalIpAddress) || SocketParam.TcpPort == -1) {
                    IPEndPoint i = new IPEndPoint(IPAddress.Parse(CommonParam.GetLocalIpAddress), SocketParam.TcpPort);
                    _Client.Bind(i);
                }

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(this._serverIP), this._serverPort);
                try {
                    _Client.Connect(ipep);
                    this.IsExit = false;
                    new Thread(new ThreadStart(ReceiveData)).Start();
                    result = true;
                } catch(Exception ex) {
                    LogFile.Write(typeof(SocketStack), LOGLEVEL.FATAL, "连接目标服务器失败：" + ex.Message);
                }
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketStack), LOGLEVEL.FATAL, "创建Socket连接失败：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 接收服务器发送的消息
        /// </summary>
        private void ReceiveData() {
            try {
                string dataStr = string.Empty;
                while(!this.IsExit) {
                    byte[] databyte = new byte[65535];
                    int recv = _Client.Receive(databyte);

                    if(recv == 0)
                        continue;

                    dataStr += Encoding.UTF8.GetString(databyte, 0, recv);
                    if(dataStr == "Error") {
                        LogFile.Write(typeof(SocketStack), LOGLEVEL.ERROR, "receive error data:" + dataStr);
                        break;
                    }

                    bool RecFlag = false;
                    string EndChar = string.Empty;
                    foreach(string s in Call_SocketCommandUtil.GetEndStr()) {
                        if(dataStr.Contains(s)) {
                            EndChar = s;
                            RecFlag = true;
                            break;
                        }
                    }

                    if(!RecFlag && string.IsNullOrEmpty(EndChar))
                        continue;

                    SocketMain.ExecuteData(dataStr);
                    dataStr = dataStr.Substring(dataStr.LastIndexOf(EndChar) + EndChar.Length);
                }
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketStack), LOGLEVEL.ERROR, "receive socket data error", ex);
                IsExit = true;
            }
        }

    }
}
