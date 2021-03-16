using DataBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class convert : Form
    {
        bool m_bConvert = false;
        bool m_bIsWaitForExit = false;
        bool m_bChildFolder = false;
        string m_sSource = string.Empty;
        string m_sTarget = string.Empty;
        string m_sExtension = string.Empty;
        public convert()
        {
            InitializeComponent();

            try
            {
                Call_ClientParamUtil.m_fRecSetting();
                this.cbxTarget.DataSource = Call_ClientParamUtil.m_lSwitch.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                if (!string.IsNullOrWhiteSpace(Call_ClientParamUtil.m_sSwitch))
                    this.cbxTarget.SelectedItem = Call_ClientParamUtil.m_sSwitch;
                else
                    this.cbxTarget.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Core_v1.Log.Instance.Error($"[CenoCC][convert][convert][Exception][{ex.Message}]");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (this.m_bConvert)
            {
                this.m_fMessageBoxShow("有转换任务在执行,请稍后...");
                return;
            }

            this.btnConvert.Text = "转换中...";
            this.btnConvert.Enabled = false;
            this.m_bConvert = true;
            this.m_bIsWaitForExit = this.ckbWaitForExit.Checked;
            this.m_bChildFolder = this.ckbChildFolder.Checked;
            this.m_sExtension = this.cbxTarget.Text;

            ///提示
            if (!this.m_sExtension.StartsWith("."))
            {
                string m_sExt = string.Empty;
                string[] m_lCmd = this.m_sExtension.Split(' ');
                foreach (string m_sCmdCut in m_lCmd)
                {
                    if (m_sCmdCut.StartsWith("."))
                    {
                        m_sExt = m_sCmdCut;
                    }
                }
                if (string.IsNullOrWhiteSpace(m_sExt))
                {
                    this.m_fMessageBoxShow("特殊命令最后位必须为扩展名,如“... .mp3”");
                    return;
                }
            }

            this.m_sSource = this.m_fPathFmt(this.txtSource.Text, "/");
            this.m_sTarget = this.m_fPathFmt(this.txtTarget.Text, "/");

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(this.m_sExtension))
                    {
                        this.m_fMessageBoxShow("请选择转换为哪种格式的录音");
                        return;
                    }

                    DirectoryInfo m_pDirectoryInfo = new DirectoryInfo(this.m_sSource);
                    FileInfo[] m_lFileInfo = m_pDirectoryInfo.GetFiles();

                    this.m_fConvert(m_lFileInfo);

                    if (this.m_bChildFolder)
                    {
                        DirectoryInfo[] m_lDirctoryInfo = m_pDirectoryInfo.GetDirectories();
                        this.m_fDoFolder(m_lDirctoryInfo);
                    }

                    this.m_fMessageBoxShow("转换结束");
                }
                catch (Exception ex)
                {
                    this.m_fMessageBoxShow($"转换开始时报错:{ex.Message}");
                }
                finally
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.btnConvert.Text = "转换";
                        this.btnConvert.Enabled = true;
                    }));

                    this.m_bConvert = false;
                }

            })).Start();
        }

        private void m_fMessageBoxShow(string m_sMsg)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                MessageBox.Show(m_sMsg);
            }));
        }

        private void txtSource_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog m_pFolderBrowserDialog = new FolderBrowserDialog();
            if (m_pFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtSource.Text = m_pFolderBrowserDialog.SelectedPath;
            }
        }

        private void txtTarget_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog m_pFolderBrowserDialog = new FolderBrowserDialog();
            if (m_pFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtTarget.Text = m_pFolderBrowserDialog.SelectedPath;
            }
        }

        private void m_fConvert(FileInfo[] m_lFileInfo, string m_sPrefix = "")
        {
            try
            {
                foreach (FileInfo m_pFileInfo in m_lFileInfo)
                {
                    string _m_sExtension = m_pFileInfo.Extension.ToLower();

                    string m_sPath = $"{this.m_sTarget}/{m_sPrefix}";

                    if (this.m_sExtension == _m_sExtension && string.IsNullOrWhiteSpace(this.cbxAc.Text))
                    {
                        if (!Directory.Exists(m_sPath))
                        {
                            Directory.CreateDirectory(m_sPath);
                        }

                        File.Copy($"{m_pFileInfo.FullName}", $"{m_sPath}/{Path.GetFileName(m_pFileInfo.FullName)}");
                        continue;
                    }

                    ///兼容特殊格式,也可以自己写命令
                    if (!this.m_sExtension.StartsWith("."))
                    {
                        if (!Directory.Exists(m_sPath))
                        {
                            Directory.CreateDirectory(m_sPath);
                        }
                        Core_v1.m_cFfmpeg.m_fInToOut($"{m_pFileInfo.FullName}", $"{m_sPath}/{Path.GetFileNameWithoutExtension(m_pFileInfo.FullName)}", this.m_bIsWaitForExit, this.cbxAc.Text, this.m_sExtension);
                    }
                    else
                    {
                        switch (_m_sExtension)
                        {
                            case ".oga":
                            case ".pcm":
                            case ".mp3":
                            case ".wav":
                            case ".wma":
                            case ".amr":
                            default:
                                {
                                    if (!Directory.Exists(m_sPath))
                                    {
                                        Directory.CreateDirectory(m_sPath);
                                    }
                                    Core_v1.m_cFfmpeg.m_fInToOut($"{m_pFileInfo.FullName}", $"{m_sPath}/{Path.GetFileNameWithoutExtension(m_pFileInfo.FullName)}{this.m_sExtension}", this.m_bIsWaitForExit, this.cbxAc.Text);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_fMessageBoxShow($"转换中错误:{ex.Message}");
            }
        }

        private void m_fDoFolder(DirectoryInfo[] m_lDirectoryInfo)
        {
            try
            {
                foreach (DirectoryInfo m_pDirectoryInfo in m_lDirectoryInfo)
                {
                    FileInfo[] m_lFileInfo = m_pDirectoryInfo.GetFiles();
                    string m_sPrefix = this.m_fPathFmt(m_pDirectoryInfo.FullName, "/").Replace(this.m_sSource, "");
                    this.m_fConvert(m_lFileInfo, m_sPrefix);

                    if (this.m_bChildFolder)
                    {
                        DirectoryInfo[] _m_lDirctoryInfo = m_pDirectoryInfo.GetDirectories();
                        this.m_fDoFolder(_m_lDirctoryInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_fMessageBoxShow($"转换子文件夹时错误:{ex.Message}");
            }
        }

        private string m_fPathFmt(string m_sPath, string m_sReplace = "\\")
        {
            return new Regex("[\\\\//]+").Replace(m_sPath, m_sReplace);
        }
    }
}
