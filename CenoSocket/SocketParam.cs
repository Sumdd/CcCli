using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataBaseUtil;
using System.Collections;
using Common;

namespace CenoSocket {
    public class SocketParam {
        private static int? _TcpPort;
        public static int TcpPort {
            get {
                if(_TcpPort.HasValue)
                    return _TcpPort.Value;
                else {
                    string TcpPortStr = Call_ParamUtil.GetParamValueByName("TcpPort");
                    return (_TcpPort = int.Parse(string.IsNullOrEmpty(TcpPortStr) ? "-1" : TcpPortStr)).Value;
                }
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("TcpPort", (TcpPort = value).ToString());
            }
        }

        /// <summary>
        /// 分割socket数据
        /// </summary>
        /// <param name="SocketData"></param>
        /// <returns></returns>
        public static ArrayList CutSocketData(string SocketData) {

            // try {
            //    ArrayList SocketDatas = new ArrayList();
            //    for(int i = 0; i < ParamLib.SocketEndStr.Length; i++) {
            //        if(SocketData.Contains(ParamLib.SocketStartStr[i].ToString()) && SocketData.Contains(ParamLib.SocketEndStr[i].ToString())) {
            //            string[] data = SocketData.Split(new string[1] { ParamLib.SocketEndStr[i] }, StringSplitOptions.RemoveEmptyEntries);
            //            for(int j = 0; j < data.Length; j++) {
            //                if(!string.IsNullOrEmpty(data[j]) && data[j].StartsWith(ParamLib.SocketStartStr[i]) && data[j].EndsWith("}")) {
            //                    SocketDatas.Add(data[j].Replace(ParamLib.SocketStartStr[i], ""));
            //                }
            //            }
            //        }
            //    }
            //    return SocketDatas;
            //} catch(Exception ex) {
            //    _Ilog.Error("cut recive socket data (" + SocketData + ") error.", ex);

            //    return null;
            //}

            try {
                ArrayList SocketDatas = new ArrayList();
                for(int i = 0; i < Call_SocketCommandUtil.GetEndStr().Length; i++) {
                    if(SocketData.Contains(Call_SocketCommandUtil.GetStartStr()[i].ToString()) && SocketData.Contains(Call_SocketCommandUtil.GetEndStr()[i].ToString())) {
                        string[] data = SocketData.Split(new string[1] { Call_SocketCommandUtil.GetEndStr()[i] }, StringSplitOptions.RemoveEmptyEntries);
                        for(int j = 0; j < data.Length; j++) {
                            if(!string.IsNullOrEmpty(data[j]) && data[j].StartsWith(Call_SocketCommandUtil.GetStartStr()[i]) && data[j].EndsWith("}")) {
                                SocketDatas.Add(data[j].Replace(Call_SocketCommandUtil.GetStartStr()[i], ""));
                            }
                        }
                    }
                }
                return SocketDatas;
            } catch(Exception ex) {
                LogFile.Write(typeof(SocketParam), LOGLEVEL.ERROR, "cut Recive Socket Data (" + SocketData + ") Error", ex);
                return null;
            }
        }

    }
}
