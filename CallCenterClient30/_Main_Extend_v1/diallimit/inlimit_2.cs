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
    public partial class inlimit_2 : Form
    {
        private bool m_bLoad = true;
        private bool m_bDoing = false;
        private int m_uID = -1;
        public EventHandler SearchEvent;

        public inlimit_2(int _m_uID)
        {
            InitializeComponent();

            m_uID = _m_uID;

            ///跟随线路启用禁用状态即可、模式禁用、尝试次数禁用
            this.inlimit_2use.Enabled = false;
            this.inlimit_2way.Enabled = false;
            this.inlimit_2trycount.Enabled = false;

            if (m_uID == -1)
            {
                this.Text = "呼叫内转配置添加";
                this.btnSave.Text = "添加";

                ///默认勾选所有星期
                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                {
                    this.inlimit_2whatday.SetItemChecked(i, true);
                }

                ///默认仅勾选内转
                for (int i = 0; i < this.inlimit_2way.Items.Count; i++)
                {
                    if ((2 & (int)(Math.Pow(2, i))) > 0)
                        this.inlimit_2way.SetItemChecked(i, true);
                }

                this.inlimit_2starttime.Text = "19:00:00";
                this.inlimit_2endtime.Text = "08:00:00";

                ///默认尝试次数1
                this.inlimit_2trycount.SelectedItem = "1";

                ///默认是
                this.inlimit_2use.SelectedItem = "是";

                this.m_bLoad = false;
            }
            else
            {
                ///先进行查询,是否已经有此呼叫内转配置
                this.HandleCreated += (a, b) =>
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {

                        ///查询出所有的信息,并赋值

                        string m_sSQL = $@"
SELECT
	* 
FROM
	`dial_inlimit_2` 
WHERE
	`inlimit_2id` = {m_uID};
";
                        DataSet ds = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                        if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow m_pDataRow = ds.Tables[0].Rows[0];

                            this.Invoke(new MethodInvoker(() =>
                            {
                                this.inlimit_2starttime.Text = m_pDataRow["inlimit_2starttime"].ToString();
                                this.inlimit_2endtime.Text = m_pDataRow["inlimit_2endtime"].ToString();
                                this.inlimit_2number.Text = m_pDataRow["inlimit_2number"].ToString();

                                ///设定星期几
                                int m_uWhatDay = Convert.ToInt32(m_pDataRow["inlimit_2whatday"]);
                                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                                {
                                    this.inlimit_2whatday.SetItemChecked(i, (m_uWhatDay & (int)(Math.Pow(2, i))) > 0 ? true : false);
                                }

                                ///设定呼叫内转模式
                                int m_uWay = Convert.ToInt32(m_pDataRow["inlimit_2way"]);
                                for (int i = 0; i < this.inlimit_2way.Items.Count; i++)
                                {
                                    this.inlimit_2way.SetItemChecked(i, (m_uWay & (int)(Math.Pow(2, i))) > 0 ? true : false);
                                }

                                this.inlimit_2trycount.SelectedItem = m_pDataRow["inlimit_2trycount"].ToString();

                                this.inlimit_2use.SelectedItem = Convert.ToInt32(m_pDataRow["inlimit_2use"]) == 1 ? "是" : "否";

                                this.inlimit_2id.Text = m_pDataRow["inlimit_2id"].ToString();
                                this.Text = "呼叫内转配置编辑";
                                this.btnSave.Text = "编辑";

                                this.m_bLoad = false;
                            }));
                        }
                        else
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                this.inlimit_2id.Text = m_uID.ToString();
                                this.Text = "呼叫内转配置添加";
                                this.btnSave.Text = "添加";

                                ///默认勾选所有星期
                                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                                {
                                    this.inlimit_2whatday.SetItemChecked(i, true);
                                }

                                ///默认仅勾选内转
                                for (int i = 0; i < this.inlimit_2way.Items.Count; i++)
                                {
                                    if ((2 & (int)(Math.Pow(2, i))) > 0)
                                        this.inlimit_2way.SetItemChecked(i, true);
                                }

                                this.inlimit_2starttime.Text = "19:00:00";
                                this.inlimit_2endtime.Text = "08:00:00";

                                ///默认尝试次数1
                                this.inlimit_2trycount.SelectedItem = "1";

                                ///默认是
                                this.inlimit_2use.SelectedItem = "是";

                                this.m_bLoad = false;
                            }));
                        }

                    })).Start();
                };
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bDoing)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "有任务正在执行,请稍后");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.inlimit_2number.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "呼叫内转号码非空");
                    return;
                }

                if (this.inlimit_2whatday.CheckedItems.Count <= 0)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "呼叫内转星期至少一项");
                    return;
                }

                ///得到选中的星期的代数和
                int m_uWhatDay = 0;
                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                {
                    if (this.inlimit_2whatday.GetItemChecked(i))
                    {
                        m_uWhatDay += (int)(Math.Pow(2, i));
                    }
                }

                if (this.inlimit_2way.CheckedItems.Count <= 0)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "呼叫内转模式至少一项");
                    return;
                }

                ///得到选中的模式的代数和
                int m_uWay = 0;
                for (int i = 0; i < this.inlimit_2way.Items.Count; i++)
                {
                    if (this.inlimit_2way.GetItemChecked(i))
                    {
                        m_uWay += (int)(Math.Pow(2, i));
                    }
                }

                if (string.IsNullOrWhiteSpace(this.inlimit_2trycount.SelectedItem?.ToString()))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "呼叫内转尝试次数非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.inlimit_2use.SelectedItem?.ToString()))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "呼叫内转是否启用非空");
                    return;
                }

                int m_uUse = this.inlimit_2use.SelectedItem.ToString() == "是" ? 1 : 0;

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;

                        ///有则修改无则新增
                        string m_sSQL = $@"
INSERT INTO `dial_inlimit_2` ( `inlimit_2id`, `inlimit_2starttime`, `inlimit_2endtime`, `inlimit_2number`, `inlimit_2whatday`, `inlimit_2way`, `inlimit_2trycount`, `inlimit_2use` )
VALUES
	( '{this.inlimit_2id.Text}', '{this.inlimit_2starttime.Text}', '{this.inlimit_2endtime.Text}', '{this.inlimit_2number.Text}', '{m_uWhatDay}', '{m_uWay}', '{this.inlimit_2trycount.SelectedItem}', '{m_uUse}' ) 
	ON DUPLICATE KEY UPDATE `dial_inlimit_2`.`inlimit_2starttime` = '{this.inlimit_2starttime.Text}',
	`dial_inlimit_2`.`inlimit_2endtime` = '{this.inlimit_2endtime.Text}',
	`dial_inlimit_2`.`inlimit_2number` = '{this.inlimit_2number.Text}',
	`dial_inlimit_2`.`inlimit_2whatday` = '{m_uWhatDay}',
	`dial_inlimit_2`.`inlimit_2way` = '{m_uWay}',
	`dial_inlimit_2`.`inlimit_2trycount` = '{this.inlimit_2trycount.SelectedItem}', 
	`dial_inlimit_2`.`inlimit_2use` = '{m_uUse}';
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"呼叫内转配置{(m_uID == -1 ? "添加" : "编辑")}成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][inlimit_2][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"呼叫内转配置{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][inlimit_2][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"呼叫内转配置{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }
}
