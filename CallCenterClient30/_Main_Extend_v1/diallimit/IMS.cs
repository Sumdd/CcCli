using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;
using Core_v1;
using Cmn_v1;
using Common;

namespace CenoCC
{
    public partial class IMS : Form
    {
        private bool _update_ = false;

        public EventHandler SearchEvent;
        public diallimit _entity;
        public EventHandler m_fFlushGateway;
        public IMS()
        {
            InitializeComponent();

            ///默认IMS导入路径,不在需要拷贝
            this.txtIMSlocation.Text = $"{Call_ParamUtil.m_sFreeSWITCHPath}/conf/sip_profiles/{Call_ParamUtil.m_sFreeSWITCHUaPath}";

            ///gateway文件格式,支持修改
            this.txtXML.Text = $@"<gateway name=""$number$"">
	<param name=""realm"" value=""$ip$""/>
	<param name=""username"" value=""$number$""/>
	<param name=""password"" value=""$password$""/>
	<param name=""from-user"" value=""$number$""/>
	<param name=""from-domain"" value=""$ip$""/>
	<param name=""caller-id-in-from"" value=""true""/>
	<param name=""register"" value=""true""/>
	<param name=""register-proxy"" value=""$ip$:$port$""/>
	<param name=""outbound-proxy"" value=""$ip$:$port$""/>
	<param name=""expire-seconds"" value=""$seconds$""/>
</gateway>";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this._update_)
                return;

            if (!WebSocket_v1.InWebSocketMain.IsOpen())
            {
                Cmn_v1.Cmn.MsgWran("WebSocket未连接,撤销IMS导入操作!");
                return;
            }

            if (!Cmn.MsgQ("确定要导入IMS吗?"))
                return;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._update_ = true;
                try
                {
                    string m_sModelString = this.txtXML.Text;
                    string m_sErrMsg = string.Empty;

                    //读取表格
                    DataTable m_pDataTable = h_xls.m_fDataTable(txtIMSimport.Text);

                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                    {
                        if (m_pDataTable.Columns.Contains("$number$") && m_pDataTable.Columns.Contains("$password$"))
                        {
                            ///可以导入模板,根据模板是否有$$来进行只能提示即可
                            if (m_sModelString.Contains("$ip$") && !m_pDataTable.Columns.Contains("$ip$"))
                            {
                                MessageBox.Show(this, "请检测是否包含 $ip$ 列");
                                return;
                            }
                            if (m_sModelString.Contains("$port$") && !m_pDataTable.Columns.Contains("$port$"))
                            {
                                MessageBox.Show(this, "请检测是否包含 $port$ 列");
                                return;
                            }

                            ///写网关至数据库
                            h_xml.write(m_pDataTable, m_sModelString, out m_sErrMsg);
                            ///刷新列表
                            if (this.SearchEvent != null)
                                this.SearchEvent(sender, e);
                            ///刷新网关下拉
                            if (this.m_fFlushGateway != null)
                                this.m_fFlushGateway(sender, e);
                            ///处理返回信息
                            string m_sErrMsgShow = string.Empty;
                            if (!string.IsNullOrWhiteSpace(m_sErrMsg))
                            {
                                m_sErrMsg = m_sErrMsg.TrimEnd(new char[] { ',', ';' });
                                m_sErrMsgShow +=
                                m_sErrMsgShow += $"网关XML文件写入情况:\r\n";
                                m_sErrMsgShow += $"{m_sErrMsg}\r\n";
                            }
                            //完成
                            MessageBox.Show(this, $"IMS导入完成,{m_sErrMsgShow}如需要可将网关XML文件拷贝至对应位置\r\n请到“网关管理”手动点击“重载”按钮加载网关,如果为REGED即为注册成功.");
                        }
                        else
                        {
                            MessageBox.Show(this, "请检测是否包含 $number$、$password$ 两列");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "无数据");
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnOK_Click error:{ex.Message}");
                    MessageBox.Show(this, $"IMS导入时出错:{ex.Message}");
                }
                finally
                {
                    this._update_ = false;
                }
            })).Start();
        }

        private void txtIMSlocation_DoubleClick(object sender, EventArgs e)
        {
            return;
            FolderBrowserDialog m_pFolderBrowserDialog = new FolderBrowserDialog();
            if (m_pFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtIMSlocation.Text = m_pFolderBrowserDialog.SelectedPath;
            }
        }

        private void txtIMSimport_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog m_pOpenFileDialog = new OpenFileDialog();
            m_pOpenFileDialog.Filter = "电子表格|*.xls";
            m_pOpenFileDialog.Multiselect = false;
            if (m_pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtIMSimport.Text = m_pOpenFileDialog.FileName;
            }
        }

        private void txtModelLocation_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog m_pOpenFileDialog = new OpenFileDialog();
            m_pOpenFileDialog.Filter = "XML|*.xml";
            m_pOpenFileDialog.Multiselect = false;
            if (m_pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtModelLocation.Text = m_pOpenFileDialog.FileName;

                ///替换XML内容
                this.txtXML.Text = h_txt.read(txtModelLocation.Text);
            }
        }
    }
}
