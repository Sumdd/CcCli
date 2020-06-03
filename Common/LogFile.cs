using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class LogFile {
        //控制日志文件大小
        public static long maxSize = 10;//日志文件大小
        public static int FileCount = 0;//日志文件个数
        public static string LogDate = DateTime.Now.ToString("yyyy-MM-dd"); //日志文件日期

        [Obsolete("请使用_Extend/Core_v1/Log中方法")]
        public static void Write(Type t, LOGLEVEL lvl, Exception ex) {
            Write(t, lvl, ex.Message, ex);
        }

        [Obsolete("请使用_Extend/Core_v1/Log中方法")]
        public static void Write(Type t, LOGLEVEL lvl, object desobj, Exception ex) {
            log4net.ILog m_log = log4net.LogManager.GetLogger(t);
            switch(lvl) {
                case LOGLEVEL.DEBUG:
                    m_log.Debug(desobj, ex);
                    break;
                case LOGLEVEL.ERROR:
                    m_log.Error(desobj, ex);
                    break;
                case LOGLEVEL.FATAL:
                    m_log.Fatal(desobj, ex);
                    break;
                case LOGLEVEL.INFO:
                    m_log.Info(desobj, ex);
                    break;
                case LOGLEVEL.WARN:
                    m_log.Debug(desobj, ex);
                    break;
                default:
                    break;
            }
        }

        [Obsolete("请使用_Extend/Core_v1/Log中方法")]
        public static void Write(Type t, LOGLEVEL lvl, string Content) {
            log4net.ILog m_log = log4net.LogManager.GetLogger(t);
            switch(lvl) {
                case LOGLEVEL.DEBUG:
                    m_log.Debug(Content);
                    break;
                case LOGLEVEL.ERROR:
                    m_log.Error(Content);
                    break;
                case LOGLEVEL.FATAL:
                    m_log.Fatal(Content);
                    break;
                case LOGLEVEL.INFO:
                    m_log.Info(Content);
                    break;
                case LOGLEVEL.WARN:
                    m_log.Warn(Content);
                    break;
                default:
                    break;
            }
        }
    }

    public enum LOGLEVEL {
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }
}
