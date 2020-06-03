using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumiSoft.Net.FTP;
using LumiSoft.Net.FTP.Client;
using Core_v1;
using DataBaseUtil;
using System.Data;

namespace CenoCC {
    public sealed class H_FTP {
        private static FTP_Client m_pFTP_Client;
        private static string     m_Host;
        private static int        m_Port;
        private static string     m_LoginName;
        private static string     m_Password;
        /// <summary>
        /// FTP实例连接
        /// </summary>
        public static void Start(FTP_TransferMode m_TransferMode = FTP_TransferMode.Passive) {
            try {
                H_FTP.m_pFTP_Client = new FTP_Client();
                DataTable dt = Call_ServerListUtil.GetFtpServerInfo();
                H_FTP.m_Host = dt.Rows[0]["ServerIP"].ToString();
                H_FTP.m_Port = Convert.ToInt32(dt.Rows[0]["ServerPort"]);
                H_FTP.m_LoginName = dt.Rows[0]["LoginName"].ToString();
                H_FTP.m_Password = dt.Rows[0]["Password"].ToString();
                H_FTP.m_pFTP_Client.TransferMode = m_TransferMode;
                H_FTP.m_pFTP_Client.Timeout = 10000;
                H_FTP.m_pFTP_Client.Connect(H_FTP.m_Host, H_FTP.m_Port);
                H_FTP.m_pFTP_Client.Authenticate(H_FTP.m_LoginName, H_FTP.m_Password);
                H_FTP.m_pFTP_Client.Noop();
                Log.Instance.Success($"[CenoCC][FtpClient][Start][连接FTP服务器]");
            } catch(Exception ex) {
                Log.Instance.Success($"[CenoCC][FtpClient][Start][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// FTP实例停止
        /// </summary>
        public static void Stop() {
            try {
                if(H_FTP.IsConnect) {
                    H_FTP.m_pFTP_Client.Disconnect();
                    H_FTP.m_pFTP_Client.Dispose();
                    Log.Instance.Success($"[CenoCC][FtpClient][Stop][FTP实例停止]");
                }
            } catch(Exception ex) {
                Log.Instance.Success($"[CenoCC][FtpClient][Stop][{ex.Message}]");
            }
        }
        /// <summary>
        /// FTP连接实例
        /// </summary>
        public static FTP_Client Client {
            get {
                return H_FTP.m_pFTP_Client;
            }
        }
        /// <summary>
        /// 是否连接
        /// </summary>
        public static bool IsConnect {
            get {
                if(
                    H_FTP.m_pFTP_Client != null
                    &&
                    H_FTP.m_pFTP_Client.IsDisposed == false
                    &&
                    H_FTP.m_pFTP_Client.IsConnected == true
                    )
                    return true;
                return false;
            }
        }
    }
}
