using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Common;
using WebBrowser;
using System.Runtime.InteropServices;
using Core_v1;
using Cmn_v1;
using DataBaseUtil;

namespace CenoCC {
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Main_Frm : MoveForm {
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        int ControlCount = 0;

        ArrayList _TabControl = new ArrayList();
        public ExtendedWebBrowser tew = null;

        public Main_Frm() {
            InitializeComponent();
        }

        public void AddTab(Form NewForm) {
            foreach(MainFormClass m in _TabControl) {
                if(m.FormName == NewForm.Text) {
                    ActiveTab(m.ControlIndex);
                    return;
                }
            }
            this.Address_Bar_Pan.Visible = false;

            MainFormClass mfc = new MainFormClass();
            mfc._ActiveFlag = true;
            mfc.ControlIndex = ControlCount;
            mfc.IsWebBrowser = false;

            Tab_Title Tab_title = new Tab_Title();
            Tab_title.TitleName = NewForm.Text;
            switch(NewForm.Text) {
                case "通话记录":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.CallRecord;
                    break;
                case "统计表":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.Report;
                    break;
                case "共享域":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.fuwuqi;
                    break;
                case "拨号限制":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.telsettimg2;
                    break;
                case "权限":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.cog;
                    break;
                case "用户管理":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources.user1;
                    break;
                case "网关管理":
                    Tab_title.TitlePic = global::CenoCC.Properties.Resources._40;
                    break;
            }
            Tab_title.Top = 21;
            Tab_title.Left = ControlCount * Tab_title.Width;
            Tab_title.Dock = DockStyle.Left;
            mfc._Tab_Title = Tab_title;
            Tab_title._ClickBrowserTitle += new Tab_Title.ClickBrowserTitle(Tab_title__ClickBrowserTitle);
            this.Title_Bar_Pan.Controls.Add(Tab_title);

            NewForm.Width = this.panel17.Width;
            NewForm.Height = this.panel17.Height;
            NewForm.TopLevel = false;
            NewForm.Dock = DockStyle.Fill;
            this.panel17.Controls.Add(NewForm);
            mfc.Main_Form = NewForm;
            NewForm.Show();

            _TabControl.Add(mfc);
            ControlCount++;

            ActiveTab(mfc.ControlIndex);
        }

        public void AddTab(string _UrlAddress) {
            this.Address_Bar_Pan.Visible = true;
            MainFormClass mfc = new MainFormClass();
            mfc._ActiveFlag = true;
            mfc.ControlIndex = ControlCount;
            mfc.IsWebBrowser = true;

            string MainUrl = _UrlAddress;
            var _mainUrl = MainUrl.ToLower();
            if(!MainUrl.ToLower().StartsWith(@"http://") && !MainUrl.ToLower().StartsWith(@"https://"))
                this.UrlAddress_Txt.Text = @"http://" + MainUrl;
            else
                this.UrlAddress_Txt.Text = MainUrl;
            ExtendedWebBrowser _WebBrowser = new ExtendedWebBrowser();
            tew = _WebBrowser;

            Tab_Title Tab_title = new Tab_Title();
            Tab_title.TitleName = "正在加载中...";
            Tab_title.TitlePic = global::CenoCC.Properties.Resources.ie;
            Tab_title.Top = 21;
            Tab_title.Left = ControlCount * Tab_title.Width;
            Tab_title.Dock = DockStyle.Left;
            Tab_title._ClickBrowserTitle += new Tab_Title.ClickBrowserTitle(Tab_title__ClickBrowserTitle);
            mfc._Tab_Title = Tab_title;
            this.Title_Bar_Pan.Controls.Add(Tab_title);


            _WebBrowser.Url = new Uri(this.UrlAddress_Txt.Text);
            _WebBrowser.ScriptErrorsSuppressed = true;
            _WebBrowser.Dock = DockStyle.Fill;
            _WebBrowser.ObjectForScripting = this;
            _WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            _WebBrowser.ProgressChanged += new WebBrowserProgressChangedEventHandler(_browser_ProgressChanged);
            _WebBrowser.DownloadComplete += new EventHandler(_browser_DownloadComplete);
            _WebBrowser.StatusTextChanged += new EventHandler(_browser_StatusTextChanged);
            _WebBrowser.StartNewWindow += new EventHandler<BrowserExtendedNavigatingEventArgs>(_browser_StartNewWindow);
            _WebBrowser.Show();

            if (this.panel17.Controls.Count > 0)
                this.panel17.Controls.RemoveAt(0);

            this.panel17.Controls.Add(_WebBrowser);
            mfc._WebBrowser = _WebBrowser;

            _TabControl.Add(mfc);
            ControlCount++;

            ActiveTab(mfc.ControlIndex);

        }

        void _browser_StartNewWindow(object sender, BrowserExtendedNavigatingEventArgs e) {
            e.Cancel = true;
            AddTab(e.Url.ToString());
        }

        private void _browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) {
            try {
                this.progressBar1.Visible = true;
                if ((e.CurrentProgress > 0) && (e.MaximumProgress > 0))
                {
                    progressBar1.Maximum = Convert.ToInt32(e.MaximumProgress);//设置正在加载的文档总字节数
                    progressBar1.Step = Convert.ToInt32(e.CurrentProgress);////获取已下载文档的字节数
                    progressBar1.PerformStep();
                }
                else if (((ExtendedWebBrowser)sender).ReadyState == WebBrowserReadyState.Complete)//加载完成后隐藏进度条
                {
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Main_Frm][_browser_ProgressChanged][Exception][浏览器进度变化发生错误:{ex.Message}]");
            }

        }

        void _browser_StatusTextChanged(object sender, EventArgs e) {
            this.StatusTxt_Lbl.Text = ((ExtendedWebBrowser)sender).StatusText != "完成" ? ((ExtendedWebBrowser)sender).StatusText : "";
        }

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            foreach(MainFormClass de in _TabControl) {
                if((ExtendedWebBrowser)(de._WebBrowser) == (ExtendedWebBrowser)sender) {
                    string m_sTitleNameStr = ((ExtendedWebBrowser)(de._WebBrowser)).DocumentTitle;
                    de._Tab_Title.TitleName = m_sTitleNameStr;
                    break;
                }
            }
        }

        void _browser_DownloadComplete(object sender, EventArgs e) {
            try {
                // 检查文档是否可用
                if(((ExtendedWebBrowser)sender).Document != null) {
                    // Subscribe to the Error event
                    ((ExtendedWebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
                    if (((ExtendedWebBrowser)sender).Url != null)
                    {
                        foreach (MainFormClass de in _TabControl)
                        {
                            if ((ExtendedWebBrowser)(de._WebBrowser) == ((ExtendedWebBrowser)sender))
                            {
                                if (de._ActiveFlag)
                                {
                                    string m_sUrlStr = ((ExtendedWebBrowser)sender).Url.ToString();
                                    UpdateAddressBox(m_sUrlStr);
                                    break;
                                }
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                LogFile.Write(typeof(Main_Frm), LOGLEVEL.ERROR, "browser page load complete error", ex);
            }
        }

        private void UpdateAddressBox(string UrlAddress) {
            try {
                this.UrlAddress_Txt.BeginInvoke(new MethodInvoker(delegate () {
                    this.UrlAddress_Txt.Text = UrlAddress;
                }));
            } catch(Exception ex) {
                LogFile.Write(typeof(Main_Frm), LOGLEVEL.ERROR, "update page address error when browser page load complete", ex);
            }

        }

        void Window_Error(object sender, HtmlElementErrorEventArgs e) {
            // We got a script error, record it
            try
            {
                //暂时去掉
                if (false)
                {
                    ScriptErrorManager.Instance.RegisterScriptError(e.Url, e.Description, e.LineNumber);
                }
                Log.Instance.Debug($"{e.Url?.ToString()},{e.Description},{e.LineNumber}");
                // Let the browser know we handled this error.
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Main_Frm][Window_Error][{ex.Message}]");
            }
        }


        void Tab_title__ClickBrowserTitle(Tab_Title bt, bool Flag) {
            try {
                if(_TabControl.Count > 0) {
                    foreach(MainFormClass m in _TabControl) {
                        if(m._Tab_Title == bt) {
                            if(Flag) {
                                if (!m._ActiveFlag)
                                {
                                    ActiveTab(m.ControlIndex);
                                    tew = m._WebBrowser;
                                }
                            } else {
                                DeleteTab(m.ControlIndex);
                            }
                            return;
                        }
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Main_Frm][Tab_title__ClickBrowserTitle][{ex.Message}]");
            }
        }

        private void ActiveTab(int ActiveIndex) {
            try
            { 
                for(int i = 0; i < _TabControl.Count; i++) {
                    MainFormClass mfc = (MainFormClass)_TabControl[i];
                    if(mfc.ControlIndex == ActiveIndex) {
                        mfc._ActiveFlag = true;
                        mfc._Tab_Title.ShowActive(true);
                        if (mfc != null && mfc._WebBrowser != null && mfc._WebBrowser.Url != null)
                        {
                            UpdateAddressBox(mfc._WebBrowser.Url.ToString());
                        }
                        if(this.panel17.Controls.Count > 0)
                            this.panel17.Controls.RemoveAt(0);
                        if(mfc.IsWebBrowser) {
                            this.Address_Bar_Pan.Visible = true;
                            this.panel17.Controls.Add(mfc._WebBrowser);
                        } else {
                            this.Address_Bar_Pan.Visible = false;
                            this.panel17.Controls.Add(mfc.Main_Form);
                        }
                    } else {
                        mfc._Tab_Title.ShowActive(false);
                        mfc._ActiveFlag = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Main_Frm][ActiveTab][{ex.Message}]");
            }
        }
        /// <summary>
        /// 删除面板
        /// <![CDATA[
        /// 问题:关闭之后没有关闭浏览器
        /// 发现时间及原因:2018年10月15日,浏览器打开了一个播放音乐的页面,关闭后还是有音乐在播放
        /// 原因分析:_ActiveFlag可能是没选中面板,直接点击的关闭按钮,没有进入移除逻辑,以后试试
        /// ]]>
        /// </summary>
        /// <param name="ActiveIndex">选中面板</param>
        private void DeleteTab(int ActiveIndex) {
            if (_TabControl.Count > 1)
            {
                ///<![CDATA[
                /// 直接关闭
                /// ]]>

                //if (DialogResult.Yes == Cmn.MsgQuestion("确定关闭此页面?"))
                {
                    bool? m_bIsActive = null;
                    int? m_uIndex = null;
                    for (int i = 0; i < _TabControl.Count; i++)
                    {
                        MainFormClass mfc = (MainFormClass)_TabControl[i];
                        if (mfc.ControlIndex == ActiveIndex)
                        {
                            m_bIsActive = mfc._ActiveFlag;
                            m_uIndex = i;
                            this.Title_Bar_Pan.Controls.Remove(mfc._Tab_Title);
                            if (mfc.IsWebBrowser)
                            {
                                mfc._WebBrowser.Dispose();
                            }
                            mfc._Tab_Title.Dispose();
                            mfc = null;
                            _TabControl.RemoveAt(i);
                            break;
                        }
                    }
                    if (m_bIsActive != null && m_uIndex != null)
                    {
                        bool _m_bIsActive = Convert.ToBoolean(m_bIsActive);
                        int _m_uIndex = Convert.ToInt32(m_uIndex);
                        if (_m_bIsActive)
                        {
                            if (this.panel17.Controls.Count > 0)
                                this.panel17.Controls.RemoveAt(0);
                        }
                        if (_TabControl.Count > 0)
                        {
                            if (_m_uIndex > _TabControl.Count - 1)
                            {
                                _m_uIndex = _TabControl.Count - 1;
                            }
                            ActiveTab(((MainFormClass)_TabControl[_m_uIndex]).ControlIndex);
                        }
                    }
                }
            }
        }

        private void panel15_MouseLeave(object sender, EventArgs e) {
            this.panel15.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Right_Right;
        }

        private void panel15_MouseHover(object sender, EventArgs e) {
            if(this.panel15.BackgroundImage == global::CenoCC.Properties.Resources.Center_Top_Right_Right_Hover)
                return;
            this.panel15.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Right_Right_Hover;
        }

        #region 拨号弹出
        public void MyMessageBox(string msg)
        {
            MinChat.m_sSecretNumber = string.Empty;
            this.m_fGetPhone(msg);
        }

        /// <summary>
        /// 兼容号码隐藏
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="m_sSecretMsg"></param>
        public void MyMessageBoxv2(string msg, string m_sSecretMsg)
        {
            MinChat.m_sSecretNumber = m_sSecretMsg ?? string.Empty;
            this.m_fGetPhone(msg);
        }

        private void m_fGetPhone(string msg)
        {
            try
            {
                string temp = "";
                foreach (char ch in msg.ToCharArray())
                {
                    if (ch >= 48 && ch <= 57 || ch.Equals('#') || ch.Equals('*'))
                    {
                        temp += ch.ToString();
                    }
                }
                msg = temp;
                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_GETPHONE, (IntPtr)0, Marshal.StringToHGlobalAnsi(msg));
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Main_Frm][m_fGetPhone][Exception][提取页面号码错误:{ex.Message}]");
            }
        }

        public void RecordLoad(string recordList) {
            try {
                if(!string.IsNullOrWhiteSpace(recordList)) {

                    //增加多服务器下载

                    if (recordList.Contains(">") || recordList.Contains("<"))
                    {
                        this.m_fRecordLoad(recordList);
                        return;
                    }

                    ///<![CDATA[
                    /// 单服务器下载(暂时)
                    /// ]]>
                    var record_list_split = Cmn.SplitRemoveEmpty(recordList, ",");
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try {
                            List<string> list = new List<string>();
                            foreach(var item in record_list_split) {
                                var item_split = Cmn.SplitRemoveEmpty(item, "|");
                                var item_split_count = item_split.Count();
                                list.Add(item_split[item_split_count - 1]);
                            }
                            var _ds = Call_Record.GetDownLoadRecord(list);
                            this.BeginInvoke(new MethodInvoker(() => {
                                DownLoad _ = new DownLoad(_ds);
                                _.Show(this);
                            }));
                        } catch(Exception ex) {
                            Log.Instance.Error($"[CenoCC][Main_Frm][RecordLoad][thread][Exception][{ex.Message}]");
                        }
                    })).Start();
                } else {
                    Log.Instance.Fail($"[CenoCC][Main_Frm][RecordLoad][录音为空]");
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Main_Frm][RecordLoad][Exception][下载录音失败:{ex.Message}]");
            }
        }

        ///<![CDATA[
        /// 多录音
        /// 多服务器
        /// 多方式
        /// 可转换
        /// 多线程
        /// 略：可调整
        /// ]]>
        public void m_fRecordLoad(string recordList)
        {
            try
            {
                DataSet m_pDataSet = new DataSet();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(string));
                m_pDataTable.Columns.Add("RecordFile", typeof(string));
                m_pDataTable.Columns.Add("m_sType", typeof(string));
                m_pDataTable.Columns.Add("m_sIP", typeof(string));
                m_pDataTable.Columns.Add("m_sPrefix", typeof(string));

                string[] m_lRecord = recordList.Split(',');
                int i = 0;
                foreach (var item in m_lRecord)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        DataRow m_pDataRow = m_pDataTable.NewRow();
                        m_pDataRow["ID"] = $"{i++}";
                        string m_sType = string.Empty;
                        if (item.Contains('>')) m_sType = ">";
                        else if (item.Contains('<')) m_sType = "<";
                        m_pDataRow["m_sType"] = m_sType;
                        string[] m_lTypeString = Cmn.SplitRemoveEmpty(item, m_sType);
                        if (m_lTypeString.Length == 2)
                        {
                            string[] m_lIPString = Cmn.SplitRemoveEmpty(m_lTypeString[1], "|");
                            if (m_lIPString.Length == 1)
                            {
                                m_pDataRow["RecordFile"] = this.m_fGetByRecID(m_lIPString[0]);
                            }
                            else if (m_lIPString.Length == 2)
                            {
                                m_pDataRow["RecordFile"] = this.m_fGetByRecID(m_lIPString[1]);
                                m_pDataRow["m_sIP"] = $"{m_lIPString[0]}";
                            }
                            else
                            {
                                continue;
                            }
                            m_pDataRow["m_sPrefix"] = $"{m_lTypeString[0]}";
                            m_pDataTable.Rows.Add(m_pDataRow);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                m_pDataSet.Tables.Add(m_pDataTable);
                this.BeginInvoke(new MethodInvoker(() => {
                    DownLoad _ = new DownLoad(m_pDataSet);
                    _.Show(this);
                }));
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Main_Frm][m_fRecordLoad][Exception][{ex.Message}]");
            }
        }

        private string m_fGetByRecID(string m_sPath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(m_sPath) && m_sPath.StartsWith("Rec_"))
                {
                    if (m_sPath.Length >= 12)
                    {
                        string yyyy = m_sPath.Substring(4, 4);
                        string MM = m_sPath.Substring(8, 2);
                        string dd = m_sPath.Substring(10, 2);

                        ///<![CDATA[
                        /// 兼容最终转码扩展名称
                        /// ]]>

                        string m_sExt = Call_ParamUtil.m_srec_t;
                        if (!string.IsNullOrWhiteSpace(Call_ParamUtil.m_sEndExt))
                        {
                            m_sExt = Call_ParamUtil.m_sEndExt;
                        }

                        return $"{Call_ParamUtil.SaveRecordPath}/{yyyy}/{yyyy}{MM}/{yyyy}{MM}{dd}/{m_sPath}{m_sExt}";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        public void PlayRecord(string recordList)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(recordList))
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        try
                        {
                            var record_list_split = Cmn.SplitRemoveEmpty(recordList, ",");
                            List<string> list = new List<string>();
                            foreach (var item in record_list_split)
                            {
                                var item_split = Cmn.SplitRemoveEmpty(item, "|");
                                var item_split_count = item_split.Count();
                                list.Add(item_split[item_split_count - 1]);
                            }
                            var _ds = Call_Record.GetDownLoadRecord(list);
                            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                            {
                                string m_sRecordFileString = _ds.Tables[0].Rows[0]["RecordFile"].ToString();
                                if (!string.IsNullOrWhiteSpace(Call_ParamUtil.m_sDialTaskRecDownLoadHTTP))
                                {
                                    string m_sDialTaskRecDownLoadHTTP = Call_ParamUtil.m_sDialTaskRecDownLoadHTTP.TrimEnd('/');
                                    Log.Instance.Success($"[CenoCC][Main_Frm][PlayRecord][录音:{m_sRecordFileString}]");
                                    string m_sFilePathString = Call_ParamUtil.ReplacePath(m_sRecordFileString);
                                    string m_sSureHttpPath = $"{m_sDialTaskRecDownLoadHTTP}/{Cmn.PathFmt(m_sFilePathString, "/").TrimStart('/')}";
                                    this.BeginInvoke(new MethodInvoker(() =>
                                    {
                                        MediaPlayerFrm m_pMediaPlayer = new MediaPlayerFrm(m_sSureHttpPath);
                                        m_pMediaPlayer.BeginSearchFile();
                                        m_pMediaPlayer.Show(this);
                                    }));
                                }
                                else
                                {
                                    Log.Instance.Success($"[CenoCC][Main_Frm][PlayRecord][录音:{m_sRecordFileString}]");
                                    DataTable dt = Call_ServerListUtil.GetFtpServerInfo();
                                    string m_Host = dt.Rows[0]["ServerIP"].ToString();
                                    int m_Port = Convert.ToInt32(dt.Rows[0]["ServerPort"]);
                                    string m_LoginName = dt.Rows[0]["LoginName"].ToString();
                                    string m_Password = dt.Rows[0]["Password"].ToString();
                                    string m_sFtpFilePathString = Call_ParamUtil.ReplacePath(m_sRecordFileString);
                                    string m_sListenTestFtpFilePathString = $@"ftp://{m_LoginName}:{m_Password}@{m_Host}:{m_Port}/{m_sFtpFilePathString}";
                                    this.BeginInvoke(new MethodInvoker(() =>
                                    {
                                        MediaPlayerFrm m_pMediaPlayer = new MediaPlayerFrm(m_sListenTestFtpFilePathString);
                                        m_pMediaPlayer.BeginSearchFile();
                                        m_pMediaPlayer.Show(this);
                                    }));
                                }
                            }
                            else
                            {
                                Log.Instance.Fail($"[CenoCC][Main_Frm][PlayRecord][未找到录音]");
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][Main_Frm][PlayRecord][thread][Exception][{ex.Message}]");
                        }
                    })).Start();
                }
                else
                {
                    Log.Instance.Fail($"[CenoCC][Main_Frm][PlayRecord][录音为空]");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Main_Frm][PlayRecord][Exception][试听录音失败:{ex.Message}]");
            }
        }
        #endregion

        #region 刷新当前页
        private void pRefresh_Click(object sender, EventArgs e) {
            if(_TabControl.Count > 0) {
                for(int i = 0; i < _TabControl.Count; i++) {
                    MainFormClass mfc = (MainFormClass)_TabControl[i];
                    if(mfc._ActiveFlag && mfc.IsWebBrowser) {
                        if(DialogResult.Yes == Cmn.MsgQuestion("确定刷新此页面?")) {
                            mfc._WebBrowser.Navigate(mfc._WebBrowser.Url);
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        #region 回到主页
        private void pHome_Click(object sender, EventArgs e) {
            if(_TabControl.Count > 0) {
                for(int i = 0; i < _TabControl.Count; i++) {
                    MainFormClass mfc = (MainFormClass)_TabControl[i];
                    if(mfc._ActiveFlag && mfc.IsWebBrowser) {
                        if(DialogResult.Yes == Cmn.MsgQuestion("确定跳转至主页?")) {
                            mfc._WebBrowser.Url = new Uri(BrowserParam.HomeUrl);
                            mfc._WebBrowser.Navigate(BrowserParam.HomeUrl);
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        #region 回退
        private void goBack_Click(object sender, EventArgs e) {
            if(tew != null)
                tew.GoBack();
        }
        #endregion

        #region 前进
        private void goForward_Click(object sender, EventArgs e) {
            if(tew != null)
                tew.GoForward();
        }
        #endregion
    }
}
