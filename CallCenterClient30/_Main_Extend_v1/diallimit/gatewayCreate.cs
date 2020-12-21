using Core_v1;
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
    public partial class gatewayCreate : Form
    {
        private bool m_bDoing = true;

        private string _m_sUUID = string.Empty;
        public EventHandler m_fFlushGateway;
        public gatewayCreate(string m_sUUID = "")
        {
            InitializeComponent();

            this.txtPassword.Enabled = false;
            this.txtIP.Enabled = false;
            this.txtPort.Enabled = false;
            this.txtSeconds.Enabled = false;

            ///默认网关ua
            this.cboGwType.SelectedItem = Model_v1.m_mGatewayType._m_sExternal;

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);

            if (string.IsNullOrWhiteSpace(m_sUUID))
            {
                this.Text = "新增网关";
                this.cboGwType.SelectedValue = 0;
                this.m_bDoing = false;
            }
            else
            {
                this.Text = "编辑网关";
                //可以编辑网关
                _m_sUUID = m_sUUID;
                //读取网关配置
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        DataTable m_pDataTable = DataBaseUtil.m_cGateway.m_fGatewayList(m_sUUID);
                        string gwType = string.Empty;
                        if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                        {
                            DataRow m_pDataRow = m_pDataTable.Rows[0];
                            this.txtName.Text = m_pDataRow["remark"].ToString();
                            this.txtGwName.Text = m_pDataRow["gw"].ToString();
                            gwType = m_pDataRow["gwtype"].ToString();
                            if (new string[] { "gateway", "internal", "external" }.Contains(gwType))
                            {
                                this.cboGwType.SelectedItem = gwType;
                            }
                            else
                            {
                                this.txtGwOName.Text = m_pDataRow["gwType"].ToString();
                            }

                            ///呼叫转移
                            this.cbxisinlimit_2.Checked = Convert.ToInt32(m_pDataRow["isinlimit_2"]) == 1;
                            this.txtinlimit_2number.Text = m_pDataRow["inlimit_2caller"].ToString();
                        }

                        if (gwType == Model_v1.m_mGatewayType._m_sGateway)
                        {
                            this.cboGwType.Enabled = false;
                            ///发送命令,查询出对应的网关XML内容并展示
                            Model_v1.m_mResponseJSON _m_mResponseJSON = WebSocket_v1.InWebSocketMain.SendAsyncObject(this.txtGwName.Text, Model_v1.m_cFSCmdType._m_sReadGateway);
                            if (_m_mResponseJSON.status == 0)
                            {
                                if (_m_mResponseJSON.result != null && !_m_mResponseJSON.result.ToString().StartsWith("-ERR"))
                                {
                                    this.txtXML.Text = _m_mResponseJSON.result.ToString();
                                }
                            }
                            else
                            {
                                this.txtXML.Text = $"{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}";
                                if (!this.txtXML.Text.StartsWith("-ERR")) this.txtXML.Text = $"-ERR {this.txtXML.Text}";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"gatewayCreate edit error:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }

                })).Start();
            }
        }

        #region ***操作权限
        private void m_fLoadOperatePower(Control.ControlCollection m_lControls)
        {
            foreach (var item in m_lControls)
            {
                if (item.GetType() == typeof(Button))
                {
                    Button m_pButton = (Button)item;
                    if (m_pButton.Tag == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(m_pButton.Tag.ToString()))
                        continue;
                    if (m_cPower.Has(m_pButton.Tag.ToString()))
                        m_pButton.Enabled = true;
                    else
                        m_pButton.Enabled = false;
                }
                else if (item.GetType() == typeof(Panel))
                {
                    Panel m_pPanel = (Panel)item;
                    this.m_fLoadOperatePower(m_pPanel.Controls);
                }
            }

            ///XML默认不可操作
            this.btnXML.Enabled = false;
        }
        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            ///简单的判断是否正确
            string m_sXML = this.txtXML.Text;
            if (this.cboGwType.Text == Model_v1.m_mGatewayType._m_sGateway)
            {
                ///添加时主动先替换XML的占位符内容
                if (string.IsNullOrWhiteSpace(this._m_sUUID))
                {
                    ///网关名称
                    string m_sArgs = this.txtGwName.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(m_sArgs))
                    {
                        m_sXML = m_sXML.Replace("$number$", m_sArgs);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran("请填写网关IP及端口($number$)");
                        return;
                    }
                    ///密码
                    m_sArgs = this.txtPassword.Text;
                    if (!string.IsNullOrWhiteSpace(m_sArgs))
                    {
                        m_sXML = m_sXML.Replace("$password$", m_sArgs);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran("请填写密码($password$)");
                        return;
                    }
                    ///IP
                    m_sArgs = this.txtIP.Text;
                    if (!string.IsNullOrWhiteSpace(m_sArgs))
                    {
                        m_sXML = m_sXML.Replace("$ip$", m_sArgs);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran("请填写IP($ip$)");
                        return;
                    }
                    ///端口
                    m_sArgs = this.txtPort.Text;
                    if (!string.IsNullOrWhiteSpace(m_sArgs))
                    {
                        m_sXML = m_sXML.Replace("$port$", m_sArgs);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran("请填写端口($port$)");
                        return;
                    }
                    ///过期时间
                    m_sArgs = this.txtSeconds.Text;
                    if (!string.IsNullOrWhiteSpace(m_sArgs))
                    {
                        m_sXML = m_sXML.Replace("$seconds$", m_sArgs);
                    }
                    else
                    {
                        this.txtSeconds.Text = "75";
                        m_sXML = m_sXML.Replace("$seconds$", "75");
                    }
                }

                if (m_sXML.Contains("$"))
                {
                    Cmn_v1.Cmn.MsgWran("请替换其中所有$$内容");
                    return;
                }
                else if (m_sXML.Contains("-ERR"))
                {
                    Cmn_v1.Cmn.MsgWran("请修正网关XML内容");
                    return;
                }
            }

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    //参数非空判断
                    if (
                        string.IsNullOrWhiteSpace(this.txtGwName.Text.Trim())
                        ||
                        (
                        string.IsNullOrWhiteSpace(this.cboGwType.Text.Trim())
                        &&
                        string.IsNullOrWhiteSpace(this.txtGwOName.Text.Trim())
                        )
                        )
                    {
                        Cmn_v1.Cmn.MsgWran("请填写网关参数");
                        return;
                    }
                    string m_sErrMsg = string.Empty;
                    if (d_multi.gatewayadd(this.txtGwName.Text, this.cboGwType.Text, this.txtGwOName.Text, out m_sErrMsg, _m_sUUID, this.txtName.Text, m_sXML, (this.cbxisinlimit_2.Checked ? 1 : 0), this.txtinlimit_2number.Text) > 0)
                    {
                        var msg = $"网关{this.txtGwName.Text}成功";
                        MessageBox.Show(this, msg);
                        Log.Instance.Success(msg.Replace("\r\n", ";"));
                        if (this.m_fFlushGateway != null)
                        {
                            this.m_fFlushGateway(sender, e);
                        }
                    }
                    else
                    {
                        var msg = $"网关{this.txtGwName.Text}失败";
                        if (!string.IsNullOrWhiteSpace(m_sErrMsg)) msg += $":{m_sErrMsg}";
                        MessageBox.Show(this, msg);
                        Log.Instance.Success(msg.Replace("\r\n", ";"));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gatewayCreate][btnGateway_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }

            })).Start();
        }

        private void cboGwType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboGwType.Text == Model_v1.m_mGatewayType._m_sGateway)
            {
                this.txtXML.Enabled = true;
                ///编辑时
                if (!string.IsNullOrWhiteSpace(this._m_sUUID) && m_cPower.Has(Model_v1.m_mOperate.diallimit_gateway_XMLedit)) this.btnXML.Enabled = true;
                ///新增时
                if (string.IsNullOrWhiteSpace(this._m_sUUID) && string.IsNullOrWhiteSpace(this.txtXML.Text))
                {
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

                this.txtGwOName.Enabled = false;

                if (string.IsNullOrWhiteSpace(this._m_sUUID))
                {
                    this.txtPassword.Enabled = true;
                    this.txtIP.Enabled = true;
                    this.txtPort.Enabled = true;
                    this.txtSeconds.Enabled = true;
                }
                else
                {
                    this.txtPassword.Enabled = false;
                    this.txtIP.Enabled = false;
                    this.txtPort.Enabled = false;
                    this.txtSeconds.Enabled = false;
                }
            }
            else
            {
                this.txtXML.Enabled = false;
                this.btnXML.Enabled = false;

                this.txtGwOName.Enabled = true;

                this.txtPassword.Enabled = false;
                this.txtIP.Enabled = false;
                this.txtPort.Enabled = false;
                this.txtSeconds.Enabled = false;
            }
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            this.m_bDoing = true;

            string m_sName = this.txtGwName.Text;
            string m_sXML = this.txtXML.Text;
            string m_sErrMsg = string.Empty;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    ///向服务器发送添加XML的命令,如果有则报错
                    object m_oObject = new
                    {
                        m_sName = m_sName,
                        m_sXML = m_sXML
                    };
                    Model_v1.m_mResponseJSON _m_mResponseJSON = WebSocket_v1.InWebSocketMain.SendAsyncObject(H_Json.ToJson(m_oObject), Model_v1.m_cFSCmdType._m_sWriteGateway);
                    if (_m_mResponseJSON.status == 0)
                    {
                        m_sErrMsg = $"修改网关XML文件完成:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}";
                    }
                    else
                    {
                        m_sErrMsg = $"修改网关XML文件失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}";
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gatewayCreate][btnXML_Click][Thread][Exception][{ex.Message}]");
                    m_sErrMsg = $"修改网关XML文件出错:{ex.Message}";
                }
                finally
                {
                    this.m_bDoing = false;
                }
                Cmn_v1.Cmn.MsgWran(m_sErrMsg);
                Log.Instance.Warn($"[CenoCC][gatewayCreate][btnXML_Click][Thread][{m_sErrMsg},{m_sXML}]");

            })).Start();
        }

        private void txtXML_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog m_pOpenFileDialog = new OpenFileDialog();
            m_pOpenFileDialog.Filter = "XML|*.xml";
            m_pOpenFileDialog.Multiselect = false;
            if (m_pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ///替换XML内容
                this.txtXML.Text = h_txt.read(m_pOpenFileDialog.FileName);
            }
        }
    }
}
