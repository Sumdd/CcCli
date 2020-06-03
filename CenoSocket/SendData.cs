using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Net.Sockets;

namespace CenoSocket
{
	public class SendData
	{
        [Obsolete("请使用_Extend/WebSocket_v1/InWebSocketMain/Send方法")]
		public static void SendMsgToServer(string Msg, Socket _Socket)
		{
			LogFile.Write(typeof(SendData), LOGLEVEL.INFO, "send message to server:" + Msg);
			if (Msg.Length <= 0)
				return;
			_Socket.Send(Encoding.UTF8.GetBytes(Msg));
		}
	}
}
