using Cmn_v1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Core_v1 {
    /// <summary>
    /// 程序辅助类
    /// </summary>
    public class H_Pro {
        /// <summary>
        /// 获取程序启动路径
        /// </summary>
        /// <param name="_addUri">追加路径</param>
        /// <returns></returns>
        public static string defaultUri(string _addUri) {
            return Cmn.PathFmt(Application.StartupPath + "/" + _addUri);
        }
        /// <summary> 
		/// 获取正在运行的实例,防止多次打开
		/// </summary>
		public static Process RunningInstance() {
            try {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);
                foreach(Process process in processes) {
                    if(process.Id != current.Id) {
                        if(process.MainModule.FileName == current.MainModule.FileName) {
                            return process;
                        }
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[Core_v1][H_Process][asRun][Exception][{ex.Message}]");
            }
            return null;
        }
    }
}
