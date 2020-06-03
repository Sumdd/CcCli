using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DataBaseUtil;

namespace CenoCC
{
	public partial class SettingFrm : Form
	{
		DataTable FtpServer_DT, CallServer_DT, SipServer_DT;
		public SettingFrm(string showTab)
		{
			InitializeComponent();

			IntiCallServerInfo();
			IntiFtpServerInfo();
			IntiSipServerInfo();

			switch (showTab)
			{
				case "Usual":
					this.UsualSet_Tab.Select();
					break;
				case "Server":
					this.ServerSet_Tab.Select();
					break;
				case "Device":
					this.DeviceSet_Tab.Select();
					break;
				default: break;
			}
		}

		private void IntiCallServerInfo()
		{
			int LvCou = this.CallServer_LV.Items.Count;
			for (int j = 0; j < LvCou; j++)
			{
				this.CallServer_LV.Items.RemoveAt(0);
			}

			//初始化电话服务器
			CallServer_DT = Call_ServerListUtil.GetCallServerInfo();
			if (CallServer_DT.Rows.Count > 0)
			{
				for (int i = 0; i < CallServer_DT.Rows.Count; i++)
				{
					this.CallServer_LV.Items.Add(CallServer_DT.Rows[i]["ServerIndex"].ToString());
					this.CallServer_LV.Items[i].SubItems.Add(CallServer_DT.Rows[i]["ServerName"].ToString());
					this.CallServer_LV.Items[i].SubItems.Add(CallServer_DT.Rows[i]["ServerIP"].ToString());
					this.CallServer_LV.Items[i].SubItems.Add(CallServer_DT.Rows[i]["ServerPort"].ToString());
					this.CallServer_LV.Items[i].SubItems.Add(CallServer_DT.Rows[i]["IsDefault"].ToString());
				}
			}
		}

		private void IntiFtpServerInfo()
		{
			int LvCou = this.FtpServer_LV.Items.Count;
			for (int j = 0; j < LvCou; j++)
			{
				this.FtpServer_LV.Items.RemoveAt(0);
			}

			//初始化FTP服务器
			FtpServer_DT = Call_ServerListUtil.GetFtpServerInfo();
			if (FtpServer_DT.Rows.Count > 0)
			{
				for (int i = 0; i < FtpServer_DT.Rows.Count; i++)
				{
					this.FtpServer_LV.Items.Add(FtpServer_DT.Rows[i]["ServerIndex"].ToString());
					this.FtpServer_LV.Items[i].SubItems.Add(FtpServer_DT.Rows[i]["ServerName"].ToString());
					this.FtpServer_LV.Items[i].SubItems.Add(FtpServer_DT.Rows[i]["ServerIP"].ToString());
					this.FtpServer_LV.Items[i].SubItems.Add(FtpServer_DT.Rows[i]["ServerPort"].ToString());
					this.FtpServer_LV.Items[i].SubItems.Add(FtpServer_DT.Rows[i]["LoginName"].ToString());
					this.FtpServer_LV.Items[i].SubItems.Add(Encrypt.DecryptString(FtpServer_DT.Rows[i]["Password"].ToString(), "cenosoft"));
					this.FtpServer_LV.Items[i].SubItems.Add(FtpServer_DT.Rows[i]["IsDefault"].ToString());
				}
			}

		}

		private void IntiSipServerInfo()
		{
			int LvCou = this.SipServer_LV.Items.Count;
			for (int j = 0; j < LvCou; j++)
			{
				this.SipServer_LV.Items.RemoveAt(0);
			}

			//初始化SIP服务器
			SipServer_DT = Call_ServerListUtil.GetSipServerInfo();
			if (SipServer_DT.Rows.Count > 0)
			{
				for (int i = 0; i < SipServer_DT.Rows.Count; i++)
				{
					this.SipServer_LV.Items.Add(SipServer_DT.Rows[i]["ServerIndex"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["ServerName"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["ServerIP"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["ServerPort"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["DomainName"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["ConnectTime"].ToString());
					this.SipServer_LV.Items[i].SubItems.Add(SipServer_DT.Rows[i]["IsDefault"].ToString());
				}
			}

		}

		private void Server_LV_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			switch (((ListView)sender).Name)
			{
				case "CallServer_LV":
					if (this.CallServer_LV.SelectedItems.Count <= 0)
						return;
					this.CallServerIndex_Txt.Tag = this.CallServerIndex_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[0].Text;
					this.CallServerName_Txt.Tag = this.CallServerName_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[1].Text;
					this.CallServerIp_Txt.Tag = this.CallServerIp_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[2].Text;
					this.CallServerPort_Txt.Tag = this.CallServerPort_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[3].Text;
					this.CallServerDefault_Ckb.Tag = this.CallServerDefault_Ckb.Checked = this.CallServer_LV.SelectedItems[0].SubItems[4].Text == "是";
					break;
				case "FtpServer_LV":
					if (this.FtpServer_LV.SelectedItems.Count <= 0)
						return;
					this.FtpServerIndex_Txt.Tag = this.FtpServerIndex_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[0].Text;
					this.FtpServerName_Txt.Tag = this.FtpServerName_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[1].Text;
					this.FtpServerIp_Txt.Tag = this.FtpServerIp_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[2].Text;
					this.FtpServerPort_Txt.Tag = this.FtpServerPort_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[3].Text;
					this.FtpServerLoginName_Txt.Tag = this.FtpServerLoginName_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[4].Text;
					this.FtpServerLoginPsw_Txt.Tag = this.FtpServerLoginPsw_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[5].Text;
					this.FtpServerDefault_Ckb.Tag = this.FtpServerDefault_Ckb.Checked = this.FtpServer_LV.SelectedItems[0].SubItems[6].Text == "是";
					break;
				case "SipServer_LV":
					if (this.SipServer_LV.SelectedItems.Count <= 0)
						return;
					this.SipServerIndex_Txt.Tag = this.SipServerIndex_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[0].Text;
					this.SipServerName_Txt.Tag = this.SipServerName_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[1].Text;
					this.SipServerIp_Txt.Tag = this.SipServerIp_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[2].Text;
					this.SipServerPort_Txt.Tag = this.SipServerPort_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[3].Text;
					this.SipServerDomain_Txt.Tag = this.SipServerDomain_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[4].Text;
					this.SipServerExpress_Txt.Tag = this.SipServerExpress_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[5].Text;
					this.SipServerDefault_Ckb.Tag = this.SipServerDefault_Ckb.Checked = this.SipServer_LV.SelectedItems[0].SubItems[6].Text == "是";
					break;
				default: break;
			}
		}

		private void Button_Click(object sender, EventArgs e)
		{
			switch (((Button)sender).Name)
			{
				case "CreateCallServer_Btn":
					#region 创建电话服务器
					this.CallServerIndex_Txt.Text = (this.CallServer_LV.Items.Count + 1).ToString();
					this.CallServerName_Txt.Text = this.CallServerPort_Txt.Text = this.CallServerIp_Txt.Text = "";
					this.CallServerDefault_Ckb.Checked = false;
					#endregion
					break;
				case "CreateFtpServer_Btn":
					#region 创建FTP服务器
					this.FtpServerIndex_Txt.Text = (this.FtpServer_LV.Items.Count + 1).ToString();
					this.FtpServerName_Txt.Text = this.FtpServerPort_Txt.Text = this.FtpServerLoginName_Txt.Text = this.FtpServerLoginPsw_Txt.Text = this.FtpServerIp_Txt.Text = "";
					this.FtpServerDefault_Ckb.Checked = false;
					#endregion
					break;
				case "CreateSipServer_Btn":
					#region 创建SIP服务器
					this.SipServerIndex_Txt.Text = (this.SipServer_LV.Items.Count + 1).ToString();
					this.SipServerName_Txt.Text = this.SipServerDomain_Txt.Text = this.SipServerExpress_Txt.Text = this.SipServerIp_Txt.Text = "";
					this.SipServerDefault_Ckb.Checked = false;
					#endregion
					break;
				case "EditCallServer_Btn":
					#region 编辑电话服务器
					if (this.CallServer_LV.SelectedItems.Count <= 0)
						return;
					this.CallServerIndex_Txt.Tag = this.CallServerIndex_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[0].Text;
					this.CallServerName_Txt.Tag = this.CallServerName_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[1].Text;
					this.CallServerIp_Txt.Tag = this.CallServerIp_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[2].Text;
					this.CallServerPort_Txt.Tag = this.CallServerPort_Txt.Text = this.CallServer_LV.SelectedItems[0].SubItems[3].Text;
					this.CallServerDefault_Ckb.Tag = this.CallServerDefault_Ckb.Checked = this.CallServer_LV.SelectedItems[0].SubItems[4].Text == "是";
					#endregion
					break;
				case "EditFtpServer_Btn":
					#region 编辑FTP服务器
					if (this.FtpServer_LV.SelectedItems.Count <= 0)
						return;
					this.FtpServerIndex_Txt.Tag = this.FtpServerIndex_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[0].Text;
					this.FtpServerName_Txt.Tag = this.FtpServerName_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[1].Text;
					this.FtpServerIp_Txt.Tag = this.FtpServerIp_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[2].Text;
					this.FtpServerPort_Txt.Tag = this.FtpServerPort_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[3].Text;
					this.FtpServerLoginName_Txt.Tag = this.FtpServerLoginName_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[4].Text;
					this.FtpServerLoginPsw_Txt.Tag = this.FtpServerLoginPsw_Txt.Text = this.FtpServer_LV.SelectedItems[0].SubItems[5].Text;
					this.FtpServerDefault_Ckb.Tag = this.FtpServerDefault_Ckb.Checked = this.FtpServer_LV.SelectedItems[0].SubItems[6].Text == "是";
					#endregion
					break;
				case "EditSipServer_Btn":
					#region 编辑SIP服务器
					if (this.SipServer_LV.SelectedItems.Count <= 0)
						return;
					this.SipServerIndex_Txt.Tag = this.SipServerIndex_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[0].Text;
					this.SipServerName_Txt.Tag = this.SipServerName_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[1].Text;
					this.SipServerIp_Txt.Tag = this.SipServerIp_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[2].Text;
					this.SipServerDomain_Txt.Tag = this.SipServerDomain_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[3].Text;
					this.SipServerExpress_Txt.Tag = this.SipServerExpress_Txt.Text = this.SipServer_LV.SelectedItems[0].SubItems[4].Text;
					this.SipServerDefault_Ckb.Tag = this.SipServerDefault_Ckb.Checked = this.SipServer_LV.SelectedItems[0].SubItems[5].Text == "是";
					#endregion
					break;
				case "DelCallServer_Btn":
					#region 删除电话服务器
					if (this.CallServer_LV.SelectedItems.Count <= 0)
						return;
					string ServerIndex = this.CallServer_LV.SelectedItems[0].SubItems[0].Text;
					if (this.CallServerIndex_Txt.Text == ServerIndex)
					{
						this.CallServerIndex_Txt.Tag = this.CallServerIndex_Txt.Text = "";
						this.CallServerName_Txt.Tag = this.CallServerName_Txt.Text = "";
						this.CallServerIp_Txt.Tag = this.CallServerIp_Txt.Text = "";
						this.CallServerPort_Txt.Tag = this.CallServerPort_Txt.Text = "";
						this.CallServerDefault_Ckb.Tag = this.CallServerDefault_Ckb.Checked = false;
					}

					Call_ServerListUtil.DeleteServerList(ServerIndex, "CallServer");

					for (int i = 0; i < CallServer_DT.Rows.Count; i++)
					{
						if (int.Parse(ServerIndex) < int.Parse(CallServer_DT.Rows[i]["ServerIndex"].ToString()))
						{
							Call_ServerListUtil.UpdateServerIndex(CallServer_DT.Rows[i]["ServerIndex"].ToString(), "CallServer");
						}
					}

					IntiCallServerInfo();
					#endregion
					break;
				case "DelFtpServer_Btn":
					#region 删除FTP服务器
					if (this.FtpServer_LV.SelectedItems.Count <= 0)
						return;
					string FtpServerIndex = this.FtpServer_LV.SelectedItems[0].SubItems[0].Text;
					if (this.FtpServerIndex_Txt.Text == FtpServerIndex)
					{
						this.FtpServerIndex_Txt.Tag = this.FtpServerIndex_Txt.Text = "";
						this.FtpServerName_Txt.Tag = this.FtpServerName_Txt.Text = "";
						this.FtpServerIp_Txt.Tag = this.FtpServerIp_Txt.Text = "";
						this.FtpServerPort_Txt.Tag = this.FtpServerPort_Txt.Text = "";
						this.FtpServerLoginName_Txt.Tag = this.FtpServerLoginName_Txt.Text = "";
						this.FtpServerLoginPsw_Txt.Tag = this.FtpServerLoginPsw_Txt.Text = "";
						this.FtpServerDefault_Ckb.Tag = this.FtpServerDefault_Ckb.Checked = false;
					}

					Call_ServerListUtil.DeleteServerList(FtpServerIndex, "FtpServer");

					for (int i = 0; i < CallServer_DT.Rows.Count; i++)
					{
						if (int.Parse(FtpServerIndex) < int.Parse(CallServer_DT.Rows[i]["ServerIndex"].ToString()))
						{
							Call_ServerListUtil.UpdateServerIndex(CallServer_DT.Rows[i]["ServerIndex"].ToString(), "FtpServer");
						}
					}

					IntiFtpServerInfo();
					#endregion
					break;
				case "DelSipServer_Btn":
					#region 删除SIP服务器
					if (this.SipServer_LV.SelectedItems.Count <= 0)
						return;
					string SipServerIndex = this.SipServer_LV.SelectedItems[0].SubItems[0].Text;
					if (this.SipServerIndex_Txt.Text == SipServerIndex)
					{
						this.SipServerIndex_Txt.Tag = this.SipServerIndex_Txt.Text = "";
						this.SipServerName_Txt.Tag = this.SipServerName_Txt.Text = "";
						this.SipServerIp_Txt.Tag = this.SipServerIp_Txt.Text = "";
						this.SipServerDomain_Txt.Tag = this.SipServerDomain_Txt.Text = "";
						this.SipServerExpress_Txt.Tag = this.SipServerExpress_Txt.Text = "";
						this.SipServerDefault_Ckb.Tag = this.SipServerDefault_Ckb.Checked = false;
					}

					Call_ServerListUtil.DeleteServerList(SipServerIndex, "SipServer");

					for (int i = 0; i < CallServer_DT.Rows.Count; i++)
					{
						if (int.Parse(SipServerIndex) < int.Parse(CallServer_DT.Rows[i]["ServerIndex"].ToString()))
						{
							Call_ServerListUtil.UpdateServerIndex(CallServer_DT.Rows[i]["ServerIndex"].ToString(), "SipServer");
						}
					}

					IntiSipServerInfo();
					#endregion
					break;
				case "CallServerConfirm_Btn":
					#region 确定电话服务器
					if (this.CallServer_LV.Items.Count >= int.Parse(this.CallServerIndex_Txt.Text))
					{
						if (this.CallServerName_Txt.Text != this.CallServerName_Txt.Tag.ToString())
						{
							this.CallServerName_Txt.Tag = this.CallServerName_Txt.Text;
						}
						if (this.CallServerIp_Txt.Text != this.CallServerIp_Txt.Tag.ToString())
						{
							this.CallServerIp_Txt.Tag = this.CallServerIp_Txt.Text;
						}
						if (this.CallServerPort_Txt.Text != this.CallServerPort_Txt.Tag.ToString())
						{
							this.CallServerPort_Txt.Tag = this.CallServerPort_Txt.Text;
						}
						if (this.CallServerDefault_Ckb.Checked != (bool)this.CallServerDefault_Ckb.Tag)
						{
							this.CallServerDefault_Ckb.Tag = this.CallServerDefault_Ckb.Checked;
						}
						Call_ServerListUtil.UpdateServerInfo(this.CallServerIndex_Txt.Text, this.CallServerName_Txt.Text, this.CallServerIp_Txt.Text, this.CallServerPort_Txt.Text, "", "", "", "", this.CallServerDefault_Ckb.Checked ? "是" : "否", "CallServer");
					}
					else
					{
						Call_ServerListUtil.InsertServerInfo(this.CallServerIndex_Txt.Text, this.CallServerName_Txt.Text, this.CallServerIp_Txt.Text, this.CallServerPort_Txt.Text, "", "", "", "", this.CallServerDefault_Ckb.Checked ? "是" : "否", "CallServer");
					}

					IntiCallServerInfo();
					#endregion
					break;
				case "FtpServerConfirm_Btn":
					#region 确定FTP服务器
					if (this.FtpServer_LV.Items.Count >= int.Parse(this.FtpServerIndex_Txt.Text))
					{
						if (this.FtpServerName_Txt.Text != this.FtpServerName_Txt.Tag.ToString())
						{
							this.CallServerName_Txt.Tag = this.CallServerName_Txt.Text;
						}
						if (this.FtpServerIp_Txt.Text != this.FtpServerIp_Txt.Tag.ToString())
						{
							this.FtpServerIp_Txt.Tag = this.FtpServerIp_Txt.Text;
						}
						if (this.FtpServerPort_Txt.Text != this.FtpServerPort_Txt.Tag.ToString())
						{
							this.FtpServerPort_Txt.Tag = this.FtpServerPort_Txt.Text;
						}
						if (this.FtpServerLoginName_Txt.Text != this.FtpServerLoginName_Txt.Tag.ToString())
						{
							this.FtpServerLoginName_Txt.Tag = this.FtpServerLoginName_Txt.Text;
						}
						if (this.FtpServerLoginPsw_Txt.Text != this.FtpServerLoginPsw_Txt.Tag.ToString())
						{
							this.FtpServerLoginPsw_Txt.Tag = this.FtpServerLoginPsw_Txt.Text;
						}
						if (this.FtpServerDefault_Ckb.Checked != (bool)this.FtpServerDefault_Ckb.Tag)
						{
							this.FtpServerDefault_Ckb.Tag = this.FtpServerDefault_Ckb.Checked;
						}
						Call_ServerListUtil.UpdateServerInfo(this.FtpServerIndex_Txt.Text, this.FtpServerName_Txt.Text, this.FtpServerIp_Txt.Text, this.FtpServerPort_Txt.Text, "", this.FtpServerLoginName_Txt.Text, "", Encrypt.EncryptString(this.FtpServerLoginPsw_Txt.Text), this.FtpServerDefault_Ckb.Checked ? "是" : "否", "FtpServer");
					}
					else
					{
						Call_ServerListUtil.InsertServerInfo(this.FtpServerIndex_Txt.Text, this.FtpServerName_Txt.Text, this.FtpServerIp_Txt.Text, this.FtpServerPort_Txt.Text, "", this.FtpServerLoginName_Txt.Text, "", Encrypt.EncryptString(this.FtpServerLoginPsw_Txt.Text), this.FtpServerDefault_Ckb.Checked ? "是" : "否", "FtpServer");

					}

					IntiFtpServerInfo();
					#endregion
					break;
				case "SipServerConfirm_Btn":
					#region 确定SIP服务器
					if (this.SipServer_LV.Items.Count >= int.Parse(this.SipServerIndex_Txt.Text))
					{
						if (this.SipServerName_Txt.Text != this.SipServerName_Txt.Tag.ToString())
						{
							this.SipServerName_Txt.Tag = this.SipServerName_Txt.Text;
						}
						if (this.SipServerIp_Txt.Text != this.SipServerIp_Txt.Tag.ToString())
						{
							this.SipServerIp_Txt.Tag = this.SipServerIp_Txt.Text;
						}
						if (this.SipServerDomain_Txt.Text != this.SipServerDomain_Txt.Tag.ToString())
						{
							this.SipServerDomain_Txt.Tag = this.SipServerDomain_Txt.Text;
						}
						if (this.SipServerPort_Txt.Text != this.SipServerPort_Txt.Tag.ToString())
						{
							this.SipServerPort_Txt.Tag = this.SipServerPort_Txt.Text;
						}
						if (this.SipServerExpress_Txt.Text != this.SipServerExpress_Txt.Tag.ToString())
						{
							this.SipServerExpress_Txt.Tag = this.SipServerExpress_Txt.Text;
						}
						if (this.SipServerDefault_Ckb.Checked != (bool)this.SipServerDefault_Ckb.Tag)
						{
							this.SipServerDefault_Ckb.Tag = this.SipServerDefault_Ckb.Checked;
						}
						Call_ServerListUtil.UpdateServerInfo(this.SipServerIndex_Txt.Text, this.SipServerName_Txt.Text, this.SipServerIp_Txt.Text, this.SipServerPort_Txt.Text, this.SipServerDomain_Txt.Text, "", this.SipServerExpress_Txt.Text, "", this.SipServerDefault_Ckb.Checked ? "是" : "否", "SipServer");
					}
					else
					{
						Call_ServerListUtil.InsertServerInfo(this.SipServerIndex_Txt.Text, this.SipServerName_Txt.Text, this.SipServerIp_Txt.Text, this.SipServerPort_Txt.Text, this.SipServerDomain_Txt.Text, "", this.SipServerExpress_Txt.Text, "", this.SipServerDefault_Ckb.Checked ? "是" : "否", "SipServer");
					}

					IntiSipServerInfo();
					#endregion
					break;
			}
		}

		private void Tool_Btn_Click(object sender, EventArgs e)
		{
			switch (((Button)sender).Name)
			{
				case "Browser_Tool_Btn":
					Browser_Grp.Focus();
					break;
				case "Popscreen_Tool_Btn":
					Popscreen_Grp.Focus();
					break;
				case "Path_Tool_Btn":
					Path_Grp.Focus();
					break;
				case "Dialrole_Tool_Btn":
					Dialrole_Grp.Focus();
					break;
				case "Other_Tool_Btn":
					Other_Grp.Focus();
					break;
				case "CallServer_Tool_Btn":
					CallServer_Pan.Focus();
					break;
				case "SipServer_Tool_Btn":
					SipServer_Pan.Focus();
					break;
				case "FtpServer_Tool_Btn":
					FtpServer_Pan.Focus();
					break;
				case "Play_Tool_Btn":
					PlayDev_Pan.Focus();
					break;
				case "Mic_Tool_Btn":
					MicDev_Pan.Focus();
					break;
				case "Sound_Tool_Btn":
					SoundDev_Pan.Focus();
					break;
				case "Audio_Tool_Btn":
					AudioCodec_Pan.Focus();
					break;
				default: break;
			}
		}

		private void SipNatMoudle_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
