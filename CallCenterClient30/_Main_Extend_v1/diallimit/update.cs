using Core_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class update : Form
    {
        private bool _ok_ = false;

        public EventHandler SearchEvent;
        public diallimit _entity;
        public update()
        {
            InitializeComponent();

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);
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
        }
        #endregion

        public void m_fSetNumber(string m_sNumber, string m_sTNumber, string m_sOrderNum)
        {
            this.txtNumber.Text = m_sNumber;
            this.txtTNumber.Text = m_sTNumber;
            this.txtOrderNum.Text = m_sOrderNum;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this._ok_)
                return;
            if (this?._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "仅能选择一项进行修改");
                return;
            }
            Regex m_pRegex = new Regex("^[0-9]{3,}$");
            if (!m_pRegex.IsMatch(this.txtNumber.Text.Trim()))
            {
                MessageBox.Show(this, "号码有误,请重新填写");
                return;
            }
            Button btn = (Button)sender;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this._ok_ = true;
                    string m_sID = this?._entity?.list?.SelectedItems?[0].SubItems["id"].Text;
                    bool m_bSame = false;
                    if (btn.Name == "btnOkSame") m_bSame = true;
                    string m_sResult = d_multi.m_fUpdateNumber(m_sID, this.txtNumber.Text.Trim(), m_bSame);
                    Log.Instance.Success($"update btnOk_Click {m_sResult}");
                    MessageBox.Show(this, m_sResult);
                    if (this.SearchEvent != null)
                        this.SearchEvent(sender, e);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"update btnOk_Click error:{ex.Message}");
                }
                finally
                {
                    this._ok_ = false;
                }
            })).Start();
        }

        private void btnTNumberOK_Click(object sender, EventArgs e)
        {
            if (this._ok_)
                return;
            if (this?._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "仅能选择一项进行修改");
                return;
            }
            Regex m_pRegex = new Regex("^[0-9]{3,}$|^$");
            if (!m_pRegex.IsMatch(this.txtTNumber.Text.Trim()))
            {
                MessageBox.Show(this, "真实号码有误,请重新填写");
                return;
            }
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this._ok_ = true;
                    string m_sID = this?._entity?.list?.SelectedItems?[0].SubItems["id"].Text;
                    string m_sResult = d_multi.m_fUpdateTNumber(m_sID, this.txtTNumber.Text.Trim());
                    Log.Instance.Success($"update btnTNumberOK_Click {m_sResult}");
                    MessageBox.Show(this, m_sResult);
                    if (this.SearchEvent != null)
                        this.SearchEvent(sender, e);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"update btnTNumberOK_Click error:{ex.Message}");
                }
                finally
                {
                    this._ok_ = false;
                }
            })).Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this._entity._update = null;
            base.OnFormClosing(e);
        }

        private void btnOrderNum_Click(object sender, EventArgs e)
        {
            if (this._ok_)
                return;
            if (this?._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "仅能选择一项进行修改");
                return;
            }
            Regex m_pRegex = new Regex("^[-+]?[0-9]{1,2}\\.?[0-9]{0,3}$");
            if (!m_pRegex.IsMatch(this.txtOrderNum.Text.Trim()))
            {
                MessageBox.Show(this, "排序数值有误,请重新填写");
                return;
            }

            ///移除首发态值
            if (this.txtOrderNum.Text.Trim() == "-99.999")
            {
                MessageBox.Show(this, "排序数值有不可为首发态值-99.999,请重新填写");
                return;
            }

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this._ok_ = true;
                    string m_sID = this?._entity?.list?.SelectedItems?[0].SubItems["id"].Text;
                    int status = 0;
                    string m_sResult = d_multi.m_fUpdateOrderNum(m_sID, this.txtOrderNum.Text.Trim(), out status);
                    Log.Instance.Success($"update btnOrderNum_Click {m_sResult}");
                    MessageBox.Show(this, m_sResult);
                    if (status == 1)
                        if (this.SearchEvent != null)
                            this.SearchEvent(sender, e);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"update btnOrderNum_Click error:{ex.Message}");
                }
                finally
                {
                    this._ok_ = false;
                }
            })).Start();
        }
    }
}
