using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;
using DataBaseModel;
using Common;

namespace CenoCC {
    public partial class CallRecord : Form {

        public string MaxDateStr = DateTime.Now.ToString("yyyy-MM-dd");
        public string MinDateStr = DateTime.Now.ToString("yyyy-MM-dd");
        public string MaxTimeStr = DateTime.Now.ToString("HH:mm:ss");
        public string MinTimeStr = "00:00:00";
        public List<string> CallType = new List<string>() { "1" ,"2","3","4","5","6","7","8" };
        public int AgentID = 0;
        public string OrderBy = "RecordIndex";

        public CallRecord() {
            InitializeComponent();
            memberList();
        }
        public CallRecord(int Width, int Height) {
            InitializeComponent();
            memberList();
            this.Width = Width;
            this.Height = Height;
        }

        private void memberList() {
            cbbMember.DataSource = Call_AgentUtil.GetList();
            cbbMember.DisplayMember = "AgentName";
            cbbMember.ValueMember = "ID";
        }

        private void timerButton1_Click(object sender, EventArgs e) {
            this.totlepage_lbl.Text = "0";
            this.currentpage_lbl.Text = "1";
            this.JumpPage_Txt.Text = "1";
            BackgroundWorker QueryCallRecord = new BackgroundWorker();
            QueryCallRecord.DoWork += new DoWorkEventHandler(QueryCallRecord_DoWork);
            QueryCallRecord.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QueryCallRecord_RunWorkerCompleted);
            QueryCallRecord.RunWorkerAsync();
        }

        void QueryCallRecord_DoWork(object sender, DoWorkEventArgs e) {
            if(this.LoadingData_PB.InvokeRequired)
                this.LoadingData_PB.BeginInvoke(new MethodInvoker(delegate () {
                    this.LoadingData_PB.Visible = true;
                }));
            else
                this.LoadingData_PB.Visible = true;

            Dictionary<string, object> QueryParam = new Dictionary<string, object>();
            if(!string.IsNullOrEmpty(this.phonenumber_txt.Text.Trim()))
                QueryParam.Add("T_PhoneNum", this.phonenumber_txt.Text);

            int PageIndex = int.Parse(this.JumpPage_Txt.Text);
            QueryParam.Add("T_PhoneNum_Like", checkBox1.Checked ? 1 : 0);
            QueryParam.Add("LimitParam", ((PageIndex - 1) * 50).ToString() + ",50");
            QueryParam.Add("MaxDate", MaxDateStr);
            QueryParam.Add("MinDate", MinDateStr);
            QueryParam.Add("MaxTime", this.MaxTime_Txt.Text);
            QueryParam.Add("MinTime", this.MinTime_Txt.Text);
            QueryParam.Add("Speaks", this.Speaks.Text);
            QueryParam.Add("Speake", this.Speake.Text);
            QueryParam.Add("Waits", this.Waits.Text);
            QueryParam.Add("Waite", this.Waite.Text);
            QueryParam.Add("AgentID", AgentID);
            QueryParam.Add("OrderBy", OrderBy);

            e.Result = Call_Record.GetData(QueryParam);
        }

        void QueryCallRecord_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            DataSet RecordDataSet = (DataSet)e.Result;
            int totlecount = Common.CommonClassLib.StringIsNumber(RecordDataSet.Tables[0].Rows[0][0].ToString()) ? int.Parse(RecordDataSet.Tables[0].Rows[0][0].ToString()) : 0;

            this.totlepage_lbl.Text = (totlecount / 50 + 1).ToString();
            this.TotleCount_lbl.Text = totlecount.ToString();

            this.currentpage_lbl.Text = this.JumpPage_Txt.Text;

            if(this.currentpage_lbl.Text == "1")
                this.PreviousPage_LLbl.Enabled = false;
            else
                this.PreviousPage_LLbl.Enabled = true;

            if(this.currentpage_lbl.Text == this.totlepage_lbl.Text)
                this.NextPage_Llbl.Enabled = false;
            else
                this.NextPage_Llbl.Enabled = true;

            this.DIndex.DataPropertyName = "ID";
            this.Dcalltype.DataPropertyName = "TypeName";
            this.DPhoneNum.DataPropertyName = "T_PhoneNum";
            this.DC_PhoneNum.DataPropertyName = "C_PhoneNum";
            this.DLocalNum.DataPropertyName = "LocalNum";
            this.DDTMF.DataPropertyName = "DtmfNum";
            this.Dphoneaddress.DataPropertyName = "PhoneAddress";
            this.DAgentName.DataPropertyName = "AgentName";
            this.Dcustomname.DataPropertyName = "ContactID";
            this.Dcallprice.DataPropertyName = "CallPrice";
            this.DDate.DataPropertyName = "C_Date";
            this.Dstarttime.DataPropertyName = "C_StartTime";
            this.Dringtime.DataPropertyName = "C_RingTime";
            this.Dspeaktime.DataPropertyName = "C_AnswerTime";
            this.Dendtime.DataPropertyName = "C_EndTime";
            this.Dspeaklength.DataPropertyName = "C_SpeakTime";
            this.Dwaitlength.DataPropertyName = "C_WaitTime";
            this.Disforward.DataPropertyName = "CallForwordFlag";
            this.Dforwardnum.DataPropertyName = "ChNum";
            this.DSerOp_ID.DataPropertyName = "ServiceName";
            this.DSerOp_DTMF.DataPropertyName = "EffectPress";
            this.DSerOp_LeaveRec.DataPropertyName = "LeaveRecordFile";
            this.DCallresult.DataPropertyName = "R_Description";
            this.DDetail.DataPropertyName = "Detail";
            this.Dremark.DataPropertyName = "Remark";


            this.dataGridView1.DataSource = RecordDataSet.Tables[1].DefaultView;
            TimerButton.RunComplete(this.timerButton1);

            if(this.LoadingData_PB.InvokeRequired)
                this.LoadingData_PB.BeginInvoke(new MethodInvoker(delegate () {
                    this.LoadingData_PB.Visible = false;
                }));
            else
                this.LoadingData_PB.Visible = false;
        }

        private void CallRecord_FormClosing(object sender, FormClosingEventArgs e) {
            if(this.axWindowsMediaPlayer1 != null)
                this.axWindowsMediaPlayer1.Dispose();
        }

        private void JumpPage_Txt_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar != 13)
                return;

            if(!CommonClassLib.StringIsNumber(this.JumpPage_Txt.Text))
                return;

            BackgroundWorker QueryCallRecord = new BackgroundWorker();
            QueryCallRecord.DoWork += new DoWorkEventHandler(QueryCallRecord_DoWork);
            QueryCallRecord.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QueryCallRecord_RunWorkerCompleted);
            QueryCallRecord.RunWorkerAsync();
        }

        private void PreviousPage_LLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.JumpPage_Txt.Text = (int.Parse(this.JumpPage_Txt.Text) - 1).ToString();
            BackgroundWorker QueryCallRecord = new BackgroundWorker();
            QueryCallRecord.DoWork += new DoWorkEventHandler(QueryCallRecord_DoWork);
            QueryCallRecord.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QueryCallRecord_RunWorkerCompleted);
            QueryCallRecord.RunWorkerAsync();
        }

        private void NextPage_Llbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.JumpPage_Txt.Text = (int.Parse(this.JumpPage_Txt.Text) + 1).ToString();
            BackgroundWorker QueryCallRecord = new BackgroundWorker();
            QueryCallRecord.DoWork += new DoWorkEventHandler(QueryCallRecord_DoWork);
            QueryCallRecord.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QueryCallRecord_RunWorkerCompleted);
            QueryCallRecord.RunWorkerAsync();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e) {
            RadioButton rb = (RadioButton)sender;
            switch(rb.Name) {
                case "Today_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = false;
                    this.MinDateStr = this.MaxDateStr = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "Toweek_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = false;
                    switch(DateTime.Now.DayOfWeek) {
                        case DayOfWeek.Tuesday:
                            this.MinDateStr = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                            break;
                        case DayOfWeek.Wednesday:
                            this.MinDateStr = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                            break;
                        case DayOfWeek.Thursday:
                            this.MinDateStr = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                            break;
                        case DayOfWeek.Friday:
                            this.MinDateStr = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd");
                            break;
                        case DayOfWeek.Saturday:
                            this.MinDateStr = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
                            break;
                        case DayOfWeek.Sunday:
                            this.MinDateStr = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                            break;
                    }
                    break;
                case "ThisMonth_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = false;
                    this.MinDateStr = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1).ToString("yyyy-MM-dd");
                    break;
                case "ThisQuarter_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = false;
                    switch(DateTime.Now.Month) {
                        case 1:
                        case 2:
                        case 3:
                            this.MinDateStr = DateTime.Now.ToString("yyyy-01-01");
                            break;
                        case 4:
                        case 5:
                        case 6:
                            this.MinDateStr = DateTime.Now.ToString("yyyy-04-01");
                            break;
                        case 7:
                        case 8:
                        case 9:
                            this.MinDateStr = DateTime.Now.ToString("yyyy-07-01");
                            break;
                        case 10:
                        case 11:
                        case 12:
                            this.MinDateStr = DateTime.Now.ToString("yyyy-10-01");
                            break;
                    }
                    break;
                case "ThisYear_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = false;
                    this.MinDateStr = DateTime.Now.ToString("yyyy-01-01");
                    break;
                case "Custom_Rb":
                    this.MinDate_Dtp.Enabled = this.MinTime_Txt.Enabled = this.MaxDate_Dtp.Enabled = this.MaxTime_Txt.Enabled = true;
                    this.MaxDateStr = this.MaxDate_Dtp.Value.ToString("yyyy-MM-dd");
                    this.MinDateStr = this.MinDate_Dtp.Value.ToString("yyyy-MM-dd");
                    break;
            }
        }

        private void ExportAll_Btn_Click(object sender, EventArgs e) {
            if(this.LoadingData_PB.InvokeRequired)
                this.LoadingData_PB.BeginInvoke(new MethodInvoker(delegate () {
                    this.LoadingData_PB.Visible = true;
                }));
            else
                this.LoadingData_PB.Visible = true;

            Dictionary<string, object> QueryParam = new Dictionary<string, object>();
            if(!string.IsNullOrEmpty(this.phonenumber_txt.Text.Trim()))
                QueryParam.Add("T_PhoneNum", this.phonenumber_txt.Text);

            int PageIndex = int.Parse(this.JumpPage_Txt.Text);
            QueryParam.Add("LimitParam", "0,99999999");

            QueryParam.Add("MaxDate", MaxDateStr);
            QueryParam.Add("MinDate", MaxDateStr);

            DataSet ds = Call_Record.GetData(QueryParam);
            if(ds.Tables.Count > 1) {
                DataTable dt = ds.Tables[1].Copy();
                dt.Columns["ID"].ColumnName = "序号";
                dt.Columns["TypeName"].ColumnName = "呼叫类型";
                dt.Columns["T_PhoneNum"].ColumnName = "原呼叫号码";
                dt.Columns["C_PhoneNum"].ColumnName = "出局号码";
                dt.Columns["LocalNum"].ColumnName = "本地号码";
                dt.Columns["DtmfNum"].ColumnName = "按键";
                dt.Columns["PhoneAddress"].ColumnName = "电话归属地";
                dt.Columns["AgentName"].ColumnName = "业务员";
                dt.Columns["CallPrice"].ColumnName = "费用";
                dt.Columns["C_Date"].ColumnName = "日期";
                dt.Columns["C_StartTime"].ColumnName = "开始时间";
                dt.Columns["C_RingTime"].ColumnName = "响铃时间";
                dt.Columns["C_AnswerTime"].ColumnName = "应答时间";
                dt.Columns["C_EndTime"].ColumnName = "结束时间";
                dt.Columns["C_SpeakTime"].ColumnName = "通话时长";
                dt.Columns["C_WaitTime"].ColumnName = "等待时长";
                dt.Columns["CallForwordFlag"].ColumnName = "是否转接";
                dt.Columns["ChNum"].ColumnName = "转接号码";
                dt.Columns["ServiceName"].ColumnName = "服务名称";
                dt.Columns["EffectPress"].ColumnName = "服务结果";
                dt.Columns["LeaveRecordFile"].ColumnName = "服务录音";
                dt.Columns["R_Description"].ColumnName = "描述";
                dt.Columns["Detail"].ColumnName = "详情";
                dt.Columns["Remark"].ColumnName = "备注";
                ds = new DataSet();
                ds.Tables.Add(dt);
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件(*.xls)|*.xls";
            sfd.FileName = "通话记录-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            sfd.AddExtension = true;
            if(sfd.ShowDialog() == DialogResult.OK) {
                Common.ExcelOperate.exportToExcel(ds, sfd.FileName);
            }
        }

        private void MaxDate_Dtp_ValueChanged(object sender, EventArgs e) {
            if(DateTime.Compare(MaxDate_Dtp.Value, MinDate_Dtp.Value) < 0) {
                this.MaxDate_Dtp.Value = DateTime.Now;
            }
            this.MaxDateStr = this.MaxDate_Dtp.Value.ToString("yyyy-MM-dd");
        }

        private void MinDate_Dtp_ValueChanged(object sender, EventArgs e) {
            this.MinDateStr = this.MinDate_Dtp.Value.ToString("yyyy-MM-dd");
        }

        private void rbOutTime_CheckedChanged(object sender, EventArgs e) {
            if(((CheckBox)sender).Checked)
                OrderBy = "R.C_StartTime";

        }

        private void rbMember_CheckedChanged(object sender, EventArgs e) {
            if(((CheckBox)sender).Checked)
                OrderBy = "R.AgentID";
        }

        private void rbSpeakTime_CheckedChanged(object sender, EventArgs e) {
            if(((CheckBox)sender).Checked)
                OrderBy = "R.C_SpeakTime";
        }

        private void cbbMember_SelectedIndexChanged(object sender, EventArgs e) {
            //AgentID = Convert.ToInt32(this.cbbMember.SelectedItem.ToString());
            if(this.cbbMember.SelectedItem != null)
                AgentID = Convert.ToInt32(((System.Data.DataRowView)this.cbbMember.SelectedItem).Row["ID"]);
        }
    }
}
