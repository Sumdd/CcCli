using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core_v1
{
    public class m_cFfmpeg
    {
        public static bool m_fInToOut(string m_sIn, string m_sOut, bool m_bIsWaitForExit = false, string m_sAc = "", string m_sWhole = "")
        {
            if (!string.IsNullOrWhiteSpace(m_sWhole))
            {
                ///此处m_sOut只有后缀名即可
                string m_sExt = string.Empty;
                string m_sCmd = string.Empty;
                string m_sI = string.Empty;
                string[] m_lCmd = m_sWhole.Split(' ');

                for (int i = 0; i < m_lCmd.Count(); i++)
                {
                    ///空格请使用%2B;可能用不到,但这里兼容一下
                    string m_sCmdCut = m_lCmd[i].ToLower().Replace("%2b", " ");
                    if (m_sCmdCut.StartsWith("."))
                    {
                        m_sExt = m_sCmdCut;
                    }
                    else if (m_sCmdCut.ToLower().Equals("-i"))
                    {
                        m_sI = "-i";
                    }
                    else
                    {
                        m_sCmd += m_sCmdCut;
                        if (i < m_lCmd.Count() - 1)
                        {
                            m_sCmd += " ";
                        }
                    }
                }

                ///打印出来看下情况
                string m_sTheCmd = $"{m_sI} \"{m_sIn}\" {m_sCmd} \"{m_sOut}{m_sExt}\"";
                ///Log.Instance.Debug($"cmd:{m_sTheCmd}");

                m_fUse(m_sTheCmd, m_bIsWaitForExit);
                return true;
            }
            else
            {
                string m_sCmd = $"-y -i \"{m_sIn}\"{{ac}}\"{m_sOut}\"";
                if (m_sIn.IndexOf(".pcm", StringComparison.OrdinalIgnoreCase) > 0)
                    m_sCmd = $"-y -f s16be -ac 1 -ar 8000 -acodec pcm_s16le -i \"{m_sIn}\" \"{m_sOut}\"";
                else
                {
                    switch (m_sAc)
                    {
                        case "1":
                            m_sCmd = m_sCmd.Replace("{ac}", " -ac 1 ");
                            break;
                        case "2":
                            m_sCmd = m_sCmd.Replace("{ac}", " -ac 2 ");
                            break;
                        default:
                            m_sCmd = m_sCmd.Replace("{ac}", " ");
                            break;
                    }
                }

                m_fUse(m_sCmd, m_bIsWaitForExit);
                return true;
            }

        }

        private static bool m_fUse(string m_sCmd, bool m_bIsWaitForExit)
        {
            string executablePath = Path.Combine(m_fWhere, "ffmpeg.exe");

            var info = new ProcessStartInfo();
            info.FileName = string.Format("\"{0}\"", executablePath);
            info.WorkingDirectory = m_fWhere;
            info.Arguments = m_sCmd;

            info.RedirectStandardInput = false;
            info.RedirectStandardOutput = false;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            string m_sData = "\r\n";

            using (var proc = new Process())
            {
                proc.StartInfo = info;
                ///先监听
                proc.ErrorDataReceived += (a, b) =>
                {
                    if (b != null && b.Data != null)
                    {
                        m_sData += $"{b.Data}\r\n";
                    }
                };
                ///再启动
                proc.Start();
                proc.BeginErrorReadLine();
                if (m_bIsWaitForExit)
                {
                    proc.WaitForExit();
                    int exitCode = proc.ExitCode;
                    if (exitCode != 0 || !m_sData.Contains($"Output #0"))
                    {
                        Log.Instance.Fail($"[Core_v1][m_cFfmpeg][m_fUse][{m_sData}]");
                    }
                    return exitCode == 0;
                }
                return true;
            }
        }

        private static string m_fWhere
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
