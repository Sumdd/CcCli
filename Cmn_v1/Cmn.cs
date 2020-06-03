using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cmn_v1 {
    public class Cmn {
        /// <summary>
        /// 生成唯一ID
        /// </summary>
        public static string UniqueID {
            get {
                return DateTime.Now.ToString("yyyyMMddHHmmssffffff_") + Guid.NewGuid();
            }
        }
        /// <summary>
        /// Marshal.StringToHGlobalAnsi简写得到句柄
        /// </summary>
        /// <returns></returns>
        public static IntPtr Sti(string message) {
            return Marshal.StringToHGlobalAnsi(message);
        }
        /// <summary>
        /// Marshal.PtrToStringAnsi简写得到字符串
        /// </summary>
        /// <param name="intptr"></param>
        /// <returns></returns>
        public static string Its(IntPtr intptr) {
            return Marshal.PtrToStringAnsi(intptr);
        }
        /// <summary>
        /// 加俩空格
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string dobSpace(string message) {
            return "  " + message;
        }
        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult MsgError(string message, string title = "错误") {
            return MessageBox.Show(message, Cmn.dobSpace(title), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 警告提示
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult MsgWran(string message, string title = "警告") {
            return MessageBox.Show(message, Cmn.dobSpace(title), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult MsgWranThat(IWin32Window that, string message, string title = "警告")
        {
            return MessageBox.Show(that, message, Cmn.dobSpace(title), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 正确、成功、完成提示
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult MsgOK(string message, string title = "提示") {
            return MessageBox.Show(message, Cmn.dobSpace(title), MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult MsgQuestion(string message, string title = "确认") {
            return MessageBox.Show(message, Cmn.dobSpace(title), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool MsgQ(string message, string title = "确认") {
            return DialogResult.Yes == MessageBox.Show(message, Cmn.dobSpace(title), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 转换Int为Boolean
        /// </summary>
        /// <param name="obj">值</param>
        /// <param name="defaultTrueInt">Int对</param>
        /// <param name="defaultReturnBoolean">默认返回</param>
        /// <returns></returns>
        public static bool IntToBoolean(object obj, int defaultTrueInt = 1, bool defaultReturnBoolean = false) {
            try {
                int _int = Convert.ToInt32(obj);
                if(_int == defaultTrueInt)
                    return true;
            } catch { }
            return defaultReturnBoolean;
        }
        /// <summary>
        /// 转换Boolean为Int
        /// </summary>
        /// <param name="obj">值</param>
        /// <param name="defaultTrueInt">Int对</param>
        /// <param name="defaultReturnInt">默认返回0</param>
        /// <returns></returns>
        public static int BooleanToInt(object obj, int defaultTrueInt = 1, int defaultReturnInt = 0) {
            try {
                bool _bool = Convert.ToBoolean(obj);
                if(_bool)
                    return defaultTrueInt;
            } catch { }
            return defaultReturnInt;
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="_path">路径</param>
        public static void OpenFolder(string _path) {
            Process.Start("Explorer.exe", $@"{Cmn.PathFmt(_path)}");
        }
        /// <summary>
        /// 打开文件夹并选中文件
        /// </summary>
        /// <param name="_path">路径</param>
        public static void OpenFolderAndSelect(string _path) {
            Process.Start("Explorer.exe", $@"/select,{Cmn.PathFmt(_path)}");
        }
        /// <summary>
        /// 格式路径
        /// </summary>
        /// <param name="_path">路径</param>
        /// <param name="_replace">替换符,默认"\"</param>
        /// <returns></returns>
        public static string PathFmt(string _path, string _replace = "\\") {
            return new Regex("[\\\\//]+").Replace(_path, _replace);
        }

        public static string[] SplitRemoveEmpty(string _string, params string[] _strings) {
            return _string.Split(_strings, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 现在的年月日
        /// </summary>
        /// <param name="m_pDateTime"></param>
        /// <returns></returns>
        public static string m_fDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 比较是否同年月日
        /// </summary>
        /// <param name="m_pDateTime"></param>
        /// <returns></returns>
        public static bool m_fEqualsDate(DateTime m_pDateTime)
        {
            DateTime m_dtNow = DateTime.Now;
            if (m_dtNow.Year != m_pDateTime.Year)
                return false;
            if (m_dtNow.Month != m_pDateTime.Month)
                return false;
            if (m_dtNow.Day != m_pDateTime.Day)
                return false;
            return true;
        }
        /// <summary>
        /// 比较时间是否非今日
        /// </summary>
        /// <param name="m_pDateTime"></param>
        /// <returns></returns>
        public static bool m_fLessDate(DateTime _m_pDateTime)
        {
            DateTime m_dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime m_pDateTime = Convert.ToDateTime(_m_pDateTime.ToString("yyyy-MM-dd 00:00:00"));
            return DateTime.Compare(m_pDateTime, m_dtNow) < 0;
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="m_sString"></param>
        /// <param name="m_sIPv4"></param>
        /// <returns></returns>
        public static string m_fReplaceIPv4(string m_sString, string m_sIPv4)
        {
            try
            {
                string m_sRegex = @"(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)";
                if (Regex.IsMatch(m_sIPv4, m_sRegex))
                {
                    return Regex.Replace(m_sString, m_sRegex, m_sIPv4);
                }
            }
            catch { }
            return m_sString;
        }
    }
}
