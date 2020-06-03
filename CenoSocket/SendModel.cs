using Common;
using DataBaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenoSocket {
    public class M_Send {
        /// <summary>
        /// 连接服务器模型
        /// </summary>
        /// <returns></returns>
        public static string _ljfwq() {
            return Call_SocketCommandUtil.GetConnectServerStr(new string[] { AgentInfo.AgentID, CommonParam.GetLocalIpAddress });
        }

        public static string _bhzt(params string[] args) {
            return Call_SocketCommandUtil.SendCommonStr(M_WebSocket._bhzt, args);
        }
        public static string _bhzt_hang(string _reason = "拒接") {
            return _bhzt(M_WebSocket._bhzt_hang, _reason);
        }
        public static string _zdwh(string m_sType = "") {
            return Call_SocketCommandUtil.SendCommonStr(M_WebSocket._zdwh, m_sType);
        }
    }
}
