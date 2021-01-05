using Common;
using Core_v1;
using DataBaseUtil;
using Model_v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class ShareNumber : _metorform
    {
        public delegate void TransfDelegate(string m_sNumberType, string m_sTypeUUID, string m_sTag, bool m_bCancel);
        public event TransfDelegate TransfEvent;
        private bool m_bCancel = true;
        private Timer m_pTimer;
        private int m_uWait = 5;
        private System.Threading.Thread m_pThread;
        private int m_uSpecial = 0;
        public ShareNumber()
        {
            InitializeComponent();

            this.m_pTimer = new Timer();
            this.m_pTimer.Tick += (a, b) =>
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.lblWait.Text = $"({--this.m_uWait})";
                }));

                if (this.m_uWait == 0)
                {
                    ///<![CDATA[
                    /// 如果设置了超时自动专线轮呼
                    /// ]]>
                    if (Call_ClientParamUtil.m_bIsUseSpRandomTimeout && Call_ClientParamUtil.m_bIsUseShare && this.m_uSpecial > 0)
                    {
                        this.TransfEvent(Special.Common, string.Empty, string.Empty, this.m_bCancel = false);
                    }
                    this.Close();
                }
            };
            this.m_pTimer.Interval = 1000;

            this.HandleCreated += (a, b) =>
            {
                this.m_fGetHeader();
                this.m_fGetBody();
            };

            if (Call_ClientParamUtil.m_uShareWait > 0)
            {
                this.m_uWait = Call_ClientParamUtil.m_uShareWait;
                this.lblWait.Text = $"({this.m_uWait})";
                this.m_pTimer.Start();
            }
            else
            {
                this.lblWait.Text = $"";
            }
        }

        private void m_fGetHeader()
        {
            this.listView.BeginUpdate();
            this.listView.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.listView.Columns.Add(new ColumnHeader() { Name = "area", Text = "区域", Width = 150 });
            this.listView.Columns.Add(new ColumnHeader() { Name = "tnumber", Text = "号码", Width = 150 });
            //this.listView.Columns.Add(new ColumnHeader() { Name = "number", Text = "号码", Width = 150 });
            this.listView.EndUpdate();
        }

        private void m_fGetBody()
        {
            try
            {
                int j = 0;
                this.listView.ShowGroups = true;
                this.listView.BeginUpdate();

                ///<![CDATA[
                /// 加入一个存储过程,可以直接查询出自己可用的号码有哪些
                /// ]]>

                ListViewGroup m_pLocalListViewGroup = new ListViewGroup("专线号码");
                this.listView.Groups.Add(m_pLocalListViewGroup);

                DataTable m_pLoaclDataTable = m_cEsyMySQL.m_fGetLocalNumberList(AgentInfo.AgentID);
                if (m_pLoaclDataTable != null && m_pLoaclDataTable.Rows.Count > 0)
                {
                    foreach (DataRow item in m_pLoaclDataTable.Rows)
                    {
                        ListViewItem m_pListViewItem = new ListViewItem($"{++j}");
                        string tnumber = item["tnumber"]?.ToString();
                        if (string.IsNullOrWhiteSpace(tnumber)) tnumber = item["number"]?.ToString();
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "area", Text = $"{item["areaname"]}({item["areacode"]})" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tnumber", Text = $"{tnumber}" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = $"{item["number"]}" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = $"{Special.Common}" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tag", Text = $"" });
                        m_pLocalListViewGroup.Items.Add(m_pListViewItem);
                        this.listView.Items.Add(m_pListViewItem);
                        this.m_uSpecial++;
                    }
                }
                this.listView.EndUpdate();

                ///<![CDATA[
                /// 如果使用共享号码
                /// ]]>
                if (Call_ClientParamUtil.m_bIsUseShare)
                {
                    BackgroundWorker m_pBackgroundWorker = new BackgroundWorker();
                    m_pBackgroundWorker.DoWork += (a, b) =>
                    {
                        try
                        {
                            List<share_number> m_lShareNumber = Core_v1.Redis2.m_fGetShareNumberList(AgentInfo.LoginName);
                            if (m_lShareNumber != null && m_lShareNumber.Count > 0)
                            {
                                List<ListViewItem> m_lListViewItem = new List<ListViewItem>();
                                foreach (share_number item in m_lShareNumber)
                                {
                                    ListViewItem m_pListViewItem = new ListViewItem($"{++j}");
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "area", Text = $"{item.areaname}({item.areacode})" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tnumber", Text = $"{(!string.IsNullOrWhiteSpace(item.tnumber) ? item.tnumber : item.number)}" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = $"{item.number}" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = $"{Special.Share}" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tag", Text = $"" });
                                    m_lListViewItem.Add(m_pListViewItem);
                                }

                                if (m_lListViewItem != null && m_lListViewItem.Count > 0)
                                {
                                    b.Result = m_lListViewItem;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Core_v1.Log.Instance.Error($"[CenoCC][ShareNumber][m_fGetBody][Thread][Exception][{ex.Message}]");
                        }
                    };
                    m_pBackgroundWorker.RunWorkerCompleted += (a, b) =>
                    {
                        if (b.Result != null)
                        {
                            ListViewGroup m_pShareListViewGroup = new ListViewGroup("共享号码");
                            ListViewItem[] _m_lListViewItem = (b.Result as List<ListViewItem>).ToArray();
                            this.listView.Groups.Add(m_pShareListViewGroup);
                            m_pShareListViewGroup.Items.AddRange(_m_lListViewItem);
                            this.listView.Items.AddRange(_m_lListViewItem);
                        }
                    };
                    m_pBackgroundWorker.RunWorkerAsync();
                }

                ///<![CDATA[
                /// 独立申请式
                /// ]]>
                {
                    #region ***追加独立服务中的共享号码
                    if (Call_ParamUtil.m_bUseApply && Call_ClientParamUtil.m_bUseApply)
                    {
                        ///执行api加载号码,这里直接走自己的9464接口api,尽可能少调整客户端即可
                        BackgroundWorker m_pBackgroundWorker = new BackgroundWorker();
                        m_pBackgroundWorker.DoWork += (a, b) =>
                        {
                            try
                            {
                                ///得到参数
                                string m_sResultString = H_Web.Get(m_cProfile.m_fUseShareApi(AgentInfo.UniqueID));
                                string m_sErrMsg = "无返回";
                                if (!string.IsNullOrWhiteSpace(m_sResultString))
                                {
                                    m_mResponseJSON _m_mResponseJSON = JsonConvert.DeserializeObject<m_mResponseJSON>(m_sResultString);
                                    if (_m_mResponseJSON.status == 0)
                                    {
                                        m_sErrMsg = string.Empty;
                                        List<m_mShareApi> m_lShareApi = JsonConvert.DeserializeObject<List<m_mShareApi>>(_m_mResponseJSON.result.ToString());
                                        List<ListViewItem> m_lListViewItem = new List<ListViewItem>();

                                        for (int i = 0; i < m_lShareApi.Count; i++)
                                        {
                                            m_mShareApi item = m_lShareApi[i];
                                            if (string.IsNullOrWhiteSpace(item.gw_name)) continue;
                                            ListViewItem m_pListViewItem = new ListViewItem($"{++j}");
                                            m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "area", Text = $"()" });
                                            m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tnumber", Text = $"{item.gw_name}" });
                                            m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = $"{item.gw_name}" });
                                            m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = $"{Special.ApiShare}" });
                                            m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tag", Text = $"{item.call_server}" });
                                            m_lListViewItem.Add(m_pListViewItem);
                                        }

                                        if (m_lListViewItem != null && m_lListViewItem.Count > 0)
                                        {
                                            b.Result = m_lListViewItem;
                                        }
                                    }
                                    else m_sErrMsg = _m_mResponseJSON.msg;
                                }
                                else
                                {
                                    m_sErrMsg = "无返回";
                                }
                                if (!string.IsNullOrWhiteSpace(m_sErrMsg))
                                {
                                    Log.Instance.Warn($"[CenoCC][MinChat][DefWndProc][WM_DRAWCLIPBOARD][ShareApi][{m_sErrMsg}]");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Instance.Error($"[CenoCC][MinChat][DefWndProc][WM_DRAWCLIPBOARD][ShareApi][Exception][{ex.Message}]");
                            }
                        };
                        m_pBackgroundWorker.RunWorkerCompleted += (a, b) =>
                        {
                            if (b.Result != null)
                            {
                                ListViewGroup m_pShareListViewGroup = new ListViewGroup("独立服务");
                                ListViewItem[] _m_lListViewItem = (b.Result as List<ListViewItem>).ToArray();
                                this.listView.Groups.Add(m_pShareListViewGroup);
                                m_pShareListViewGroup.Items.AddRange(_m_lListViewItem);
                                this.listView.Items.AddRange(_m_lListViewItem);
                            }
                        };
                        m_pBackgroundWorker.RunWorkerAsync();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Core_v1.Log.Instance.Error($"[CenoCC][ShareNumber][m_fGetBody][Exception][{ex.Message}]");
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView.SelectedItems.Count == 1)
            {
                ListViewItem m_pListViewItem = this.listView.SelectedItems[0];
                this.TransfEvent(m_pListViewItem.SubItems["isshare"].Text, m_pListViewItem.SubItems["number"].Text, m_pListViewItem.SubItems["tag"].Text, this.m_bCancel = false);
            }
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.m_bCancel)
                this.TransfEvent(string.Empty, string.Empty, string.Empty, true);
            this.m_pTimer.Stop();
            if (this.m_pThread != null && this.m_pThread.IsAlive)
                this.m_pThread?.Abort();
            base.OnFormClosing(e);
        }
    }
}
