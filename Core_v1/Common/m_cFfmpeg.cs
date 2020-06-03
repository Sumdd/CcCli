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
        public static bool m_fInToOut(string m_sIn, string m_sOut, bool m_bIsWaitForExit = false, string m_sAc = "")
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

        private static void m_fUse(string m_sCmd, bool m_bIsWaitForExit)
        {
            string executablePath = Path.Combine(m_fWhere, "ffmpeg.exe");

            var info = new ProcessStartInfo();
            info.FileName = string.Format("\"{0}\"", executablePath);
            info.WorkingDirectory = m_fWhere;
            info.Arguments = m_sCmd;

            info.RedirectStandardInput = false;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            using (var proc = new Process())
            {
                proc.StartInfo = info;
                proc.Start();
                if (m_bIsWaitForExit)
                {
                    proc.WaitForExit(100 * 15);
                }
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
