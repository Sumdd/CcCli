using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Core_v1;
using Cmn_v1;
using Common;
using DataBaseUtil;
using System.Diagnostics;

namespace CenoCC {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(delegate (object sender, System.Threading.ThreadExceptionEventArgs e) {
                Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][{e.Exception.Message}]");
                Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][Memo,Start]");
                Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][{e.Exception}]");
                Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][Memo,Stop]");
            });

            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(delegate (object sender, UnhandledExceptionEventArgs e) {
                if(e.ExceptionObject is Exception) {
                    var ex = e.ExceptionObject as Exception;
                    Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][{ex.Message}]");
                    Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][Memo,Start]");
                    Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][{ex}]");
                    Log.Instance.Error($"[CenoCC][Program][Main][ThreadException][Memo,Stop]");
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var asRun = H_Pro.RunningInstance();
                if (asRun != null)
                {
                    if (Cmn.MsgQ("呼叫中心客户端已经打开,是否关闭后重新打开"))
                    {
                        asRun.Kill();
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Program][Main][Kill][{ex.Message}]");
                if (ex.Message.Contains("权限"))
                {
                    MessageBox.Show($"{ex.Message},请不要多次开启电话客户端");
                    return;
                }
            }

            //将更新提升到对应的登陆界面中,这样如果IP错误有修改的可能性
            //问题1:界面问题
            //解决1:ini与数据库均有登陆界面参数

            string _v1 = string.Empty;
            try {
                Version v1 = new Version(H_Json.updateJsonModel?.version);
                _v1 = v1.ToString();
                Version v2 = new Version(_load_update());
                if(v1 < v2) {
                    if(Cmn.MsgQ("检查到有新版本,点击确定立即更新")) {
                        Process.Start("Update.exe");
                        return;
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Program][Main][Exception][客户端更新出错:{ex.Message}]");
                if(!Cmn.MsgQ($"客户端更新出错:{ex.Message},是否继续打开客户端{_v1}?")) {
                    return;
                }
            }

            #region ***自定义登陆界面
            try
            {
                CCFactory.m_sLoginString = m_cProfile.login;
                if (!string.IsNullOrWhiteSpace(Call_ParamUtil.m_sLoginType))
                {
                    CCFactory.m_sLoginString = Call_ParamUtil.m_sLoginType;
                }
                Login LF = new Login(CCFactory.m_sLoginString);
                if (LF.ShowDialog() == DialogResult.OK)
                {
                    MinChat minchat = new MinChat();
                    Application.Run(minchat);
                    minchat.Close();
                }
            }
            catch { }
            finally
            {
                ///退出所有的线程
                System.Environment.Exit(0);
            }
            #endregion
        }

        private static string _load_update() {
            var _updateJsonModel = H_Json.DescUpdateJsonModel(H_Web.Get($"{Call_ParamUtil.SystemUpgradelPath}/client_update.json"));
            return _updateJsonModel?.version;
        }
    }
}
