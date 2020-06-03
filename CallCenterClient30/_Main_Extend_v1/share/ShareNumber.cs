using Common;
using DataBaseUtil;
using Model_v1;
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
        public delegate void TransfDelegate(string m_sNumberType, string m_sTypeUUID, bool m_bCancel);
        public event TransfDelegate TransfEvent;
        private bool m_bCancel = true;
        private Timer m_pTimer;
        private int m_uWait = 5;
        private System.Threading.Thread m_pThread;
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
                    if (Call_ClientParamUtil.m_bIsUseSpRandomTimeout)
                    {
                        this.TransfEvent(Special.Common, string.Empty, this.m_bCancel = false);
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
                this.lblWait.Text = $"{this.m_uWait}";
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
            this.listView.Columns.Add(new ColumnHeader() { Name = "number", Text = "号码", Width = 150 });
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
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "area", Text = $"{item["areaname"]}({item["areacode"]})" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = $"{item["number"]}" });
                        m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = $"{Special.Common}" });
                        m_pLocalListViewGroup.Items.Add(m_pListViewItem);
                        this.listView.Items.Add(m_pListViewItem);
                    }
                }
                this.listView.EndUpdate();

                ///<![CDATA[
                /// 如果使用共享号码
                /// ]]>
                if (Call_ClientParamUtil.m_bIsUseShare)
                {
                    this.m_pThread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        try
                        {
                            List<share_number> m_lShareNumber = Core_v1.Redis2.m_fGetShareNumberList();
                            if (m_lShareNumber != null && m_lShareNumber.Count > 0)
                            {
                                List<ListViewItem> m_lListViewItem = new List<ListViewItem>();
                                foreach (share_number item in m_lShareNumber)
                                {
                                    ListViewItem m_pListViewItem = new ListViewItem($"{++j}");
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "area", Text = $"{item.areaname}({item.areacode})" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = $"{item.number}" });
                                    m_pListViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = $"{Special.Share}" });
                                    m_lListViewItem.Add(m_pListViewItem);
                                }

                                if (m_lListViewItem != null && m_lListViewItem.Count > 0)
                                {
                                    ListViewGroup m_pShareListViewGroup = new ListViewGroup("共享号码");
                                    ListViewItem[] _m_lListViewItem = m_lListViewItem.ToArray();
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        this.listView.Groups.Add(m_pShareListViewGroup);
                                        m_pShareListViewGroup.Items.AddRange(_m_lListViewItem);
                                        this.listView.Items.AddRange(_m_lListViewItem);
                                    }));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Core_v1.Log.Instance.Error($"[CenoCC][ShareNumber][m_fGetBody][Thread][Exception][{ex.Message}]");
                        }

                    }));
                    this.m_pThread.Start();
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
                this.TransfEvent(m_pListViewItem.SubItems["isshare"].Text, m_pListViewItem.SubItems["number"].Text, this.m_bCancel = false);
            }
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.m_bCancel)
                this.TransfEvent(string.Empty, string.Empty, true);
            this.m_pTimer.Stop();
            if (this.m_pThread != null && this.m_pThread.IsAlive)
                this.m_pThread?.Abort();
            base.OnFormClosing(e);
        }
    }
}
