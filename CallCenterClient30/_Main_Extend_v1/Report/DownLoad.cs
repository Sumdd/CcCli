using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Threading;
using Core_v1;
using System.IO;
using DataBaseUtil;
using Cmn_v1;
using System.Net;

namespace CenoCC {
    public partial class DownLoad : _metorform {

        private DataSet m_ds;
        private bool    m_breakThread = false;
        private string  m_sRecordPath = string.Empty;
        private int     m_uThread     = Convert.ToInt32( Call_ClientParamUtil.m_uHttpLoadThread);
        private object  m_oLock       = new object();
        private int     m_uEndThread  = 0;

        public delegate void downloadExit();
        public downloadExit downloadExitEvent;
        public DownLoad(DataSet _ds) {
            InitializeComponent();
            this.m_ds = _ds;
            this.HandleCreated += new EventHandler((o, e) => {
                this.GetListBody();
            });
        }
        /// <summary>
        /// 下载队列
        /// </summary>
        private void GetListBody() {
            if(this.m_ds != null && this.m_ds.Tables.Count > 0 && this.m_ds.Tables[0] != null) {
                this.metroProgressSpinnerdo(true);
                int index = 1;
                bool m_bHasType = m_ds.Tables[0].Columns.Contains("m_sType");
                bool m_bHasIP = m_ds.Tables[0].Columns.Contains("m_sIP");
                bool m_bHasPrefix = m_ds.Tables[0].Columns.Contains("m_sPrefix");
                bool m_bHasFreeSWITCHIPv4 = m_ds.Tables[0].Columns.Contains("m_sFreeSWITCHIPv4");
                bool m_bHasAgentID = m_ds.Tables[0].Columns.Contains("m_uAgentID");

                ///是否全号显示
                bool m_bSeeNumber = Model_v1.m_mOperate.m_bSeeNumber;

                foreach (DataRow dr in this.m_ds.Tables[0].Rows) {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.UseItemStyleForSubItems = false;
                    var _recordFile = dr["RecordFile"].ToString();
                    listViewItem.ImageIndex = string.IsNullOrWhiteSpace(_recordFile) ? 2 : 0;
                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "index", Text = $"{index++}" });
                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "msgTips", Text = $"等待下载" });

                    ///是否全号显示
                    string m_sRecordFile = _recordFile;
                    if (!m_bSeeNumber)
                    {
                        m_sRecordFile = Cmn.m_fSecretRec(m_sRecordFile);
                    }

                    ///处理后的录音路径
                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "S_fileName", Text = $"{m_sRecordFile}" });

                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "progress", Text = "0.00%" });
                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ID", Text = $"{dr["ID"]}" });
                    listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "status", Text = $"{listViewItem.ImageIndex}" });

                    {
                        //添加前缀,进行分支
                        var m_sType = m_bHasType ? $"{dr["m_sType"]}" : string.Empty;
                        var m_sIP = m_bHasIP ? $"{dr["m_sIP"]}" : string.Empty;
                        var m_sPrefix = m_bHasPrefix ? $"{dr["m_sPrefix"]}" : string.Empty;
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "m_sType", Text = $"{m_sType}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "m_sIP", Text = $"{m_sIP}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "m_sPrefix", Text = $"{m_sPrefix}" });

                        //下载分支,请求不同的电话服务器
                        var m_sFreeSWITCHIPv4 = m_bHasFreeSWITCHIPv4 ? $"{dr["m_sFreeSWITCHIPv4"]}" : string.Empty;
                        var m_uAgentID = m_bHasAgentID ? $"{dr["m_uAgentID"]}" : "-1";
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "m_sFreeSWITCHIPv4", Text = $"{m_sFreeSWITCHIPv4}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "m_uAgentID", Text = $"{m_uAgentID}" });

                        ///真实录音路径
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "fileName", Text = $"{_recordFile}" });
                    }

                    this.list.Items.Add(listViewItem);
                }

                //多线程分支,Http
                bool m_sIsHttp = !string.IsNullOrWhiteSpace(Call_ParamUtil.m_sDialTaskRecDownLoadHTTP);
                if (m_sIsHttp)
                {
                    this.m_fHttpThreadLoad();
                    return;
                }

                //原来的分支
                this.DownLoadQueue();
            } else {
                this.metroProgressSpinnerdo(false);
            }
        }
        /// <summary>
        /// 下载队列
        /// </summary>
        private void DownLoadQueue() {
            new Thread(new ThreadStart(() => {
                try {
                    this.refreshListdo();
                    this.metroProgressSpinnerdo(false);
                } catch(Exception ex) {
                    Log.Instance.Error($"[CenoCC][DownLoad][DownLoadQueue][Exception][{ex.Message}]");
                }
            })).Start();
        }
        /// <summary>
        /// Invoke伪进度
        /// </summary>
        /// <param name="visible"></param>
        private void metroProgressSpinnerdo(bool visible) {
            if(this.metroProgressSpinner.InvokeRequired) {
                this.metroProgressSpinner.Invoke(new MethodInvoker(() => {
                    this.metroProgressSpinner.Visible = visible;
                }));
            } else {
                this.metroProgressSpinner.Visible = visible;
            }
        }
        /// <summary>
        /// 列表操作并刷新
        /// </summary>
        private void refreshListdo() {
            if(this.list.InvokeRequired) {
                while(!this.list.IsHandleCreated) {
                    continue;
                }
                this.list.Invoke(new MethodInvoker(refreshListdoInvokeRequired));
            } else {
                refreshListdoInvokeRequired();
            }
        }
        /// <summary>
        /// 列表鼠标右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                if(this.list.SelectedItems == null) {
                    this.openRecord.Enabled = false;
                    this.openRecord.ToolTipText = "无选中项";
                } else if(this.list.SelectedItems.Count == 1) {
                    var _recordPath = this.list.SelectedItems[0].SubItems["fileName"].Text;
                    if(!string.IsNullOrWhiteSpace(_recordPath)) {
                        this.openRecord.Enabled = true;
                        this.openRecord.ToolTipText = "在文件夹中显示";
                    } else {
                        this.openRecord.Enabled = false;
                        this.openRecord.ToolTipText = "路径错误";
                    }
                } else {
                    this.openRecord.Enabled = true;
                    this.openRecord.ToolTipText = "在文件夹中显示";
                }
                this.contextListMenu.Show(list, e.Location);
            }
        }
        /// <summary>
        /// 列表右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextListMenu_Click(object sender, EventArgs e) {
            try {
                switch(((ToolStripMenuItem)sender).Name) {
                    case "openRecord": {
                            string _saveRecordPath = Call_ClientParamUtil.GetSaveRecordPath();
                            if(string.IsNullOrWhiteSpace(_saveRecordPath)) {
                                if (string.IsNullOrWhiteSpace(this.m_sRecordPath))
                                {
                                    Log.Instance.Fail($"[CenoCC][DownLoad][contextListMenu_Click][获取文件所在路径失败]");
                                    return;
                                }
                                else
                                {
                                    _saveRecordPath = this.m_sRecordPath;
                                }
                            }
                            var _selectList = this.list.SelectedItems;
                            if(_selectList.Count != 1) {
                                Log.Instance.Success($"[CenoCC][DownLoad][contextListMenu_Click][打开所在文件夹,{_saveRecordPath}]");
                                Cmn.OpenFolder(_saveRecordPath);
                            } else {
                                var _recordFile = this.list.SelectedItems[0].SubItems["fileName"].Text;
                                var _path = Cmn.PathFmt($"{_saveRecordPath}/{Path.GetFileName(_recordFile)}");
                                Log.Instance.Success($"[CenoCC][DownLoad][contextListMenu_Click][在文件夹中显示,{_path}]");
                                Cmn.OpenFolderAndSelect(_path);
                            }
                        }
                        break;
                    default:
                        break;
                }

            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Report][metroListMenu_Click][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// Invoke列表操作并刷新
        /// </summary>
        private void refreshListdoInvokeRequired() {
            string _saveRecordPath = Call_ClientParamUtil.GetSaveRecordPath();

            #region 增加下载地址的选择,而且不保存至数据库
            if (string.IsNullOrWhiteSpace(_saveRecordPath)) {
                this.Invoke(new MethodInvoker(() => {
                    FolderBrowserDialog m_sFbDialog = new FolderBrowserDialog();
                    if (DialogResult.OK == m_sFbDialog.ShowDialog()) {
                        _saveRecordPath = m_sFbDialog.SelectedPath.Replace("\\", "/");
                        this.m_sRecordPath = _saveRecordPath;
                    }
                }));
            }
            #endregion

            if (string.IsNullOrWhiteSpace(_saveRecordPath)) {
                Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][未设置录音默认保存路径,请先设置后再下载]");
                Cmn.MsgWran("未设置录音默认保存路径,请先设置后再下载");
                if(this.downloadExitEvent != null) {
                    this.downloadExitEvent();
                }
                return;
            }

            #region FTP下载,有时候会不管用,但保留此次内容,后续不再为其增加内容,以后的更新均基于Htpp上即可
            H_FTP.Start();
            foreach (ListViewItem listViewItem in this.list.Items)
            {
                if (this.m_breakThread)
                {
                    break;
                }
                switch (Convert.ToInt32(listViewItem.SubItems["status"].Text))
                {
                    case 0:
                        {
                            string _recordFile = listViewItem.SubItems["fileName"].Text;
                            try
                            {
                                listViewItem.SubItems["msgTips"].Text = "下载中";
                                Log.Instance.Success($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},开始下载...]");
                                string _recordPath = Cmn.PathFmt($"{_saveRecordPath}/{Path.GetFileName(_recordFile)}");
                                H_IO.CreateDir(_recordPath);
                                string _storePath = Call_ParamUtil.ReplacePath(_recordFile);
                                H_FTP.Client.GetFile(_storePath, _recordPath);
                                listViewItem.ImageIndex = 1;
                                listViewItem.SubItems["msgTips"].Text = "下载成功";
                                listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                                listViewItem.SubItems["progress"].Text = "100.00%";
                                listViewItem.SubItems["progress"].ForeColor = Color.Green;
                                listViewItem.SubItems["status"].Text = "1";
                                Log.Instance.Success($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载成功]");
                            }
                            catch (Exception ex)
                            {
                                listViewItem.ImageIndex = 3;
                                listViewItem.SubItems["msgTips"].Text = $"下载失败({ex.Message})";
                                listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                                listViewItem.SubItems["progress"].Text = "0.00%";
                                listViewItem.SubItems["progress"].ForeColor = Color.Red;
                                listViewItem.SubItems["status"].Text = "3";
                                Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载失败,{ex.Message}]");
                                H_FTP.Stop();
                                H_FTP.Start();
                            }
                        }
                        break;
                    case 1:
                        listViewItem.ImageIndex = 1;
                        listViewItem.SubItems["msgTips"].Text = "下载成功";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                        listViewItem.SubItems["progress"].Text = "100.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Green;
                        Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},录音已存在]");
                        break;
                    case 2:
                        listViewItem.ImageIndex = 2;
                        listViewItem.SubItems["msgTips"].Text = "路径错误";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Orange;
                        listViewItem.SubItems["progress"].Text = "0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Orange;
                        Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},录音路径为空]");
                        break;
                    case 3:
                    default:
                        listViewItem.ImageIndex = 3;
                        listViewItem.SubItems["msgTips"].Text = "无法下载";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                        listViewItem.SubItems["progress"].Text = "0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Red;
                        Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},无效的下载状态]");
                        break;
                }
            }
            H_FTP.Stop();
            #endregion

            if (this.downloadExitEvent != null) {
                this.downloadExitEvent();
            }
        }
        /// <summary>
        /// 重写窗体退出中
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e) {
            this.m_breakThread = true;
            base.OnFormClosing(e);
        }
        /// <summary>
        /// 重写窗体退出后
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e) {
            if(this.downloadExitEvent != null) {
                this.downloadExitEvent();
            }
            base.OnFormClosed(e);
        }

        #region ***多线程下载
        ///<![CDATA[
        /// 后续完善
        /// 多线程
        /// 界面流畅性
        /// 进度百分比
        /// 完成后
        /// ]]>
        private void m_fHttpThreadLoad()
        {
            string _saveRecordPath = Call_ClientParamUtil.GetSaveRecordPath();
            string m_sDialTaskRecDownLoadHTTP = Call_ParamUtil.m_sDialTaskRecDownLoadHTTP.TrimEnd('/');

            #region 增加录音格式转换的弹出框
            string m_sSwitch = string.Empty;
            if (Call_ClientParamUtil.m_bSwitch)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    Fc m_cFc = new Fc();
                    m_cFc.ShowDialog();

                }));
            }
            m_sSwitch = Call_ClientParamUtil.m_sSwitch;
            #endregion

            #region 增加下载地址的选择,而且不保存至数据库
            if (string.IsNullOrWhiteSpace(_saveRecordPath))
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    FolderBrowserDialog m_sFbDialog = new FolderBrowserDialog();
                    if (DialogResult.OK == m_sFbDialog.ShowDialog())
                    {
                        _saveRecordPath = m_sFbDialog.SelectedPath.Replace("\\", "/");
                        this.m_sRecordPath = _saveRecordPath;
                    }
                }));
            }
            #endregion

            #region ***无下载路径
            if (string.IsNullOrWhiteSpace(_saveRecordPath))
            {
                Log.Instance.Fail($"[CenoCC][DownLoad][refreshListdoInvokeRequired][未设置录音默认保存路径,请先设置后再下载]");
                Cmn.MsgWran("未设置录音默认保存路径,请先设置后再下载");
                if (this.downloadExitEvent != null)
                {
                    this.downloadExitEvent();
                }
                this.metroProgressSpinnerdo(false);
                return;
            }
            #endregion

            ///线程每次获取的任务数
            int m_uThreadCount = 512;

            ///记录索引位置
            int m_uIndex = 0;

            #region ***多线程处理
            for (int i = 0; i < m_uThread; i++)
            {
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        while (true)
                        {
                            try
                            {
                                //页退出
                                if (m_breakThread) break;

                                //任务缓存
                                List<ListViewItem> m_lListViewItem = new List<ListViewItem>();

                                lock (this.m_oLock)
                                {
                                    for (int k = m_uIndex; k < this.list.Items.Count; k++)
                                    {
                                        //索引增加
                                        m_uIndex++;

                                        ListViewItem m_plistViewItem = this.list.Items[k];

                                        if (m_plistViewItem.SubItems["status"].Text == "0")
                                        {
                                            this.Invoke(new MethodInvoker(() =>
                                            {
                                                m_plistViewItem.SubItems["status"].Text = "100";
                                                m_plistViewItem.SubItems["msgTips"].Text = "下载中...";
                                            }));

                                            //放入集合
                                            m_lListViewItem.Add(m_plistViewItem);

                                            //判断是否达到缓存上限
                                            if (m_lListViewItem.Count >= m_uThreadCount) break;
                                        }

                                        if (m_plistViewItem.SubItems["status"].Text == "2")
                                        {
                                            this.Invoke(new MethodInvoker(() =>
                                            {
                                                m_plistViewItem.ImageIndex = 2;
                                                m_plistViewItem.SubItems["status"].Text = "3";
                                                m_plistViewItem.SubItems["msgTips"].Text = "路径错误";
                                                m_plistViewItem.SubItems["msgTips"].ForeColor = Color.Orange;
                                                m_plistViewItem.SubItems["progress"].Text = "0.00%";
                                                m_plistViewItem.SubItems["progress"].ForeColor = Color.Orange;
                                            }));
                                        }
                                    }
                                }

                                //跳出循环
                                if (m_lListViewItem.Count <= 0) break;
                                for (int j = 0; j < m_lListViewItem.Count(); j++)
                                {
                                    ///处理行,看下速度快不快
                                    ListViewItem listViewItem = m_lListViewItem[j];

                                    #region ***Http的下载逻辑,直接对100进行处理,其余的不再做处理
                                    string _recordFile = listViewItem.SubItems["fileName"].Text;
                                    try
                                    {
                                        this.BeginInvoke(new MethodInvoker(() =>
                                        {
                                            listViewItem.SubItems["msgTips"].Text = "下载中";
                                        }));

                                        Log.Instance.Success($"[CenoCC][DownLoad][m_fHttpThreadLoad][Thread][while][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},开始下载...]");
                                        string m_sInsertFolder = "";
                                        string m_sInsertFile = "";
                                        #region ***增加要求,分文件夹、填充文件名等要求
                                        {
                                            string m_sType = listViewItem.SubItems["m_sType"]?.Text;
                                            switch (m_sType)
                                            {
                                                case ">":
                                                    m_sInsertFolder = listViewItem.SubItems["m_sPrefix"]?.Text;
                                                    break;
                                                case "<":
                                                    m_sInsertFile = listViewItem.SubItems["m_sPrefix"]?.Text?.Replace("\\", "_")?.Replace("/", "_");
                                                    if (!string.IsNullOrWhiteSpace(m_sInsertFile)) m_sInsertFile = $"{m_sInsertFile}_";
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        #endregion
                                        string _recordPath = Cmn.PathFmt($"{_saveRecordPath}{m_sInsertFolder}/{m_sInsertFile}{Path.GetFileName(_recordFile)}");
                                        H_IO.CreateDir(_recordPath);
                                        string _storePath = Call_ParamUtil.ReplacePath(_recordFile);
                                        string m_sHttp = m_sDialTaskRecDownLoadHTTP;
                                        #region ***替换IP地址
                                        {
                                            string m_sIP = listViewItem.SubItems["m_sIP"]?.Text;
                                            if (!string.IsNullOrWhiteSpace(m_sIP))
                                            {
                                                string[] m_lHttp = m_sDialTaskRecDownLoadHTTP.Split(':');
                                                if (m_lHttp.Length >= 2)
                                                {
                                                    m_sHttp = $"{m_sDialTaskRecDownLoadHTTP.Replace(m_lHttp[1], $"//{m_sIP}")}";
                                                }
                                            }
                                            else
                                            {
                                                var m_sFreeSWITCHIPv4 = listViewItem.SubItems["m_sFreeSWITCHIPv4"]?.Text;
                                                var m_uAgentID = listViewItem.SubItems["m_uAgentID"]?.Text;
                                                if (m_uAgentID == "-1" && !string.IsNullOrWhiteSpace(m_sFreeSWITCHIPv4))
                                                {
                                                    string[] m_lHttp = m_sDialTaskRecDownLoadHTTP.Split(':');
                                                    if (m_lHttp.Length >= 2)
                                                    {
                                                        m_sHttp = $"{m_sDialTaskRecDownLoadHTTP.Replace(m_lHttp[1], $"//{m_sFreeSWITCHIPv4}")}";
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        string m_sSureHttpPath = $"{m_sHttp}/{Cmn.PathFmt(_storePath, "/").TrimStart('/')}";
                                        string _m_sOut = string.Empty;
                                        this.m_fLoad(listViewItem, m_sSwitch, m_sSureHttpPath, _recordPath, out _m_sOut);
                                        Log.Instance.Success($"[CenoCC][DownLoad][m_fHttpThreadLoad][Thread][while][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载成功]");
                                    }
                                    catch (Exception ex)
                                    {
                                        this.BeginInvoke(new MethodInvoker(() =>
                                        {
                                            listViewItem.ImageIndex = 3;
                                            listViewItem.SubItems["msgTips"].Text = $"下载失败({ex.Message},{ex.StackTrace})";
                                            listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                                            listViewItem.SubItems["progress"].Text = "0.00%";
                                            listViewItem.SubItems["progress"].ForeColor = Color.Red;
                                            listViewItem.SubItems["status"].Text = "3";
                                        }));

                                        Log.Instance.Fail($"[CenoCC][DownLoad][m_fHttpThreadLoad][Thread][while][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载失败,{ex.Message}]");
                                    }
                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Instance.Error($"[CenoCC][DownLoad][m_fHttpThreadLoad][Thread][while][Exception][{ex.Message}]");
                            }
                        }

                        //是否停止提示
                        if (this.m_uThread == ++this.m_uEndThread)
                            this.metroProgressSpinnerdo(false);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][DownLoad][m_fHttpThreadLoad][Thread][Exception][{ex.Message}]");
                    }

                })).Start();
            }
            #endregion
        }
        #endregion

        #region ***拓展文件转换
        private void m_fLoad(ListViewItem listViewItem, string m_sSwitch, string m_sIn, string m_sOut, out string _m_sOut)
        {
            //检测是否需要格式转换
            _m_sOut = string.Empty;
            var _m_sSwitch = m_sSwitch?.ToLower();
            if (!string.IsNullOrWhiteSpace(_m_sSwitch) && !_m_sSwitch.StartsWith("."))
            {
                ///兼容特定的转换
                _m_sOut = $"{m_sOut.Remove(m_sOut.Length - 4)}";
                bool m_bLoadSuccess = Core_v1.m_cFfmpeg.m_fInToOut(m_sIn, _m_sOut, false, string.Empty, _m_sSwitch);

                //目前这里只有下载成功,不判断对与错
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems["msgTips"].Text = "下载成功";
                    listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                    listViewItem.SubItems["progress"].Text = "100.00%";
                    listViewItem.SubItems["progress"].ForeColor = Color.Green;
                    listViewItem.SubItems["status"].Text = "1";
                }));

                Log.Instance.Warn($"[CenoCC][DownLoad][m_fLoad][文件转换:{_m_sOut}]");
            }
            else
            {
                switch (_m_sSwitch)
                {
                    //增加oga
                    case ".oga":
                    case ".pcm":
                    case ".mp3":
                    case ".wav":
                    case ".wma":
                    case ".amr":
                        {
                            if (!Path.GetExtension(m_sIn).Equals(_m_sSwitch, StringComparison.OrdinalIgnoreCase))
                            {
                                _m_sOut = $"{m_sOut.Remove(m_sOut.Length - 4)}{_m_sSwitch}";
                                Core_v1.m_cFfmpeg.m_fInToOut(m_sIn, _m_sOut, true);

                                //目前这里只有下载成功,不判断对与错
                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    listViewItem.ImageIndex = 1;
                                    listViewItem.SubItems["msgTips"].Text = "下载成功";
                                    listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                                    listViewItem.SubItems["progress"].Text = "100.00%";
                                    listViewItem.SubItems["progress"].ForeColor = Color.Green;
                                    listViewItem.SubItems["status"].Text = "1";
                                }));

                                Log.Instance.Warn($"[CenoCC][DownLoad][m_fLoad][文件转换:{_m_sOut}]");
                            }
                            else
                            {
                                Log.Instance.Warn($"[CenoCC][DownLoad][m_fLoad][文件与目标格式一致,无需转换]");
                                _m_sOut = m_sOut;
                                this.m_fLoadThenProgress(listViewItem, m_sIn, m_sOut);
                            }
                        }
                        break;
                    default:
                        {
                            _m_sOut = m_sOut;
                            this.m_fLoadThenProgress(listViewItem, m_sIn, m_sOut);
                        }
                        break;
                }
            }
        }
        #endregion

        #region ***下载文件及反馈下载进度
        private void m_fLoadThenProgress(ListViewItem listViewItem,string m_sIn,string m_sOut)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileCompleted += client_DownloadFileCompleted;
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.DownloadFileAsync(new Uri(m_sIn), m_sOut, listViewItem);
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    listViewItem.ImageIndex = 3;
                    listViewItem.SubItems["msgTips"].Text = $"下载失败({ex.Message},{ex.StackTrace})";
                    listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                    listViewItem.SubItems["progress"].Text = "0.00%";
                    listViewItem.SubItems["progress"].ForeColor = Color.Red;
                    listViewItem.SubItems["status"].Text = "3";
                }));

                Log.Instance.Warn($"[CenoCC][DownLoad][m_fLoadThenProgress][Exception][{ex.Message}]");
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    ListViewItem listViewItem = (ListViewItem)e.UserState;
                    if (listViewItem.SubItems["status"].Text == "1")
                    {
                        listViewItem.SubItems["progress"].Text = $"100.00%";
                    }
                    else
                    {
                        listViewItem.SubItems["progress"].Text = $"{e.ProgressPercentage}.00%";
                    }
                }));
            }
            catch { }
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e == null)
                    return;

                if (e.Error != null)
                {
                    ListViewItem listViewItem = (ListViewItem)e.UserState;

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        listViewItem.ImageIndex = 3;
                        listViewItem.SubItems["msgTips"].Text = $"下载失败({e.Error.Message},{e.Error.StackTrace})";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                        //listViewItem.SubItems["progress"].Text = $"0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Red;
                        listViewItem.SubItems["status"].Text = "1";
                    }));

                    Log.Instance.Fail($"[CenoCC][DownLoad][client_DownloadFileCompleted][Error][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{listViewItem.SubItems["fileName"].Text},{e.Error.Message}]");

                    return;
                }

                if (e.Cancelled)
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        ListViewItem listViewItem = (ListViewItem)e.UserState;
                        listViewItem.ImageIndex = 2;
                        listViewItem.SubItems["msgTips"].Text = "取消下载";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                        //listViewItem.SubItems["progress"].Text = $"0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Red;
                        listViewItem.SubItems["status"].Text = "1";
                    }));

                    Log.Instance.Warn($"[CenoCC][DownLoad][client_DownloadFileCompleted][Cancelled][取消下载]");

                    return;
                }

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    ListViewItem listViewItem = (ListViewItem)e.UserState;
                    listViewItem.ImageIndex = 1;
                    listViewItem.SubItems["msgTips"].Text = "下载成功";
                    listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                    listViewItem.SubItems["progress"].Text = $"100.00%";
                    listViewItem.SubItems["progress"].ForeColor = Color.Green;
                    listViewItem.SubItems["status"].Text = "1";
                }));
            }
            catch { }
        }
        #endregion
    }
}
