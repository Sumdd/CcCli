using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CenoSip;
using System.ComponentModel;
using DataBaseUtil;
using System.Windows.Forms;
using Common;
using System.Collections;
using Core_v1;

namespace CenoCC {
    public class SessionControl {

        #region 注册状态事件
        /// <summary>
        /// is called when sip register state change.
        /// </summary>
        /// <param name="RegState"></param>
        public static void SipRegStateHandle(bool RegState) {
            if(!RegState) {
                if(CCFactory.IsInCall)
                    CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;
                else
                    CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE;
                Log.Instance.Fail($"[CenoCC][SessionControl][SipRegStateHandle][SIP注册失败]");
                return;
            }
            CCFactory.ChInfo[MinChat.CurrentCh].IsSoftDial = true;
            Log.Instance.Success($"[CenoCC][SessionControl][SipRegStateHandle][SIP注册成功]");
            if(CCFactory.IsInCall)
                CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;
            else {
                CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_IDLE, (IntPtr)0, (IntPtr)0);
            }
        }
        #endregion

        #region 呼叫,使用的是本地呼叫
        /// <summary>
        /// start dial
        /// </summary>
        /// <param name="Number"></param>
        public static void Phone_Dial(string Number) {
            if(string.IsNullOrEmpty(Number))
                return;

            CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP;
            MinChat._MinChat.CallStatus_Lbl.Text = "拨号中";
            MinChat._MinChat.Call_Flag_Pnl.Visible = true;
            MinChat._MinChat.Width = 180;
            MinChat._MinChat.PhoneNum_Contact_Lbl.Text = Number;
            QueryContactInfo(Number);

            switch(CCFactory._PhoneType) {
                case GlobalData.PhoneType.SIP_SOFT_PHONE:
                    Dictionary<bool, string> ResultD = SipMain.Dial(Number);

                    foreach(KeyValuePair<bool, string> keys in ResultD)
                        if(keys.Key) {
                            LogFile.Write(typeof(SessionControl), LOGLEVEL.ERROR, "dial fail", new Exception(keys.Value));
                            MinChat._MinChat.CallStatus_Lbl.Text = "拨号失败";
                            MinChat._MinChat.CallInfo_Pnl.Visible = false;
                            MinChat._MinChat.Call_Flag_Pnl.Visible = false;
                            CCFactory.ChInfo[MinChat.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                        } else {

                        }
                    break;
                case GlobalData.PhoneType.SIP_BOARD_PHONE:

                    break;
                case GlobalData.PhoneType.TELEPHONE:

                    break;
                case GlobalData.PhoneType.TELEPHONE_BOX:

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 停止呼叫
        /// <summary>
        /// stop dial
        /// </summary>
        public static void Phone_Temminate(string ABHang) {
            if(CCFactory.ChInfo[MinChat.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                return;

            CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;

            switch (CCFactory._PhoneType) {
                case GlobalData.PhoneType.SIP_SOFT_PHONE:
                    {
                        if (!string.IsNullOrWhiteSpace(ABHang)) WebSocket_v1.InWebSocketMain.Send(CenoSocket.M_Send._bhzt__hang(ABHang));
                        SipMain.Teminate();
                    }
                    break;
                case GlobalData.PhoneType.TELEPHONE:

                    break;
                case GlobalData.PhoneType.TELEPHONE_BOX:

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 查询信息,来电地址
        //query contact and phoneaddress    start
        public static void QueryContactInfo(string PhoneNum) {
            BackgroundWorker QueryContactBW = new BackgroundWorker();
            QueryContactBW.DoWork += new DoWorkEventHandler(QueryContectInfoBW_DoWork);
            QueryContactBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QueryContectInfoBW_RunWorkerCompleted);
            QueryContactBW.RunWorkerAsync(PhoneNum);
        }

        static void QueryContectInfoBW_DoWork(object sender, DoWorkEventArgs e) {
            string _PhoneNum = e.Argument.ToString();
            //string PhoneAddress = Call_PhoneAddressUtil.GetPhoneAddress(_PhoneNum);
            bool m_bIsNeedGetContact = false;
            string m_sDt = string.Empty;
            string m_sCardType = string.Empty;
            string m_sZipCode = string.Empty;
            List<string> m_lStrings = m_cPhone.m_fGetPhoneNumberMemo(_PhoneNum, out m_bIsNeedGetContact, out m_sDt, out m_sCardType, out m_sZipCode);
            string PhoneAddress = m_lStrings[3];
            e.Result = string.IsNullOrEmpty(PhoneAddress) ? "未知" : PhoneAddress;
            e.Result += ";" + Call_PhoneAddressUtil.GetContactName(_PhoneNum);
        }

        static void QueryContectInfoBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if(MinChat._MinChat.PhoneNum_Contact_Lbl.InvokeRequired) {
                MinChat._MinChat.PhoneNum_Contact_Lbl.Invoke(new MethodInvoker(delegate () {
                    MinChat._MinChat.PhoneNum_Contact_Lbl.Text += "(" + e.Result.ToString().Split(';')[0] + ")";
                }));
            } else
                MinChat._MinChat.PhoneNum_Contact_Lbl.Text += "(" + e.Result.ToString().Split(';')[0] + ")";

            //if(MinChat._MinChat.CallStatus_Lbl.InvokeRequired) {
            //    MinChat._MinChat.CallStatus_Lbl.Invoke(new MethodInvoker(delegate () {
            //        MinChat._MinChat.PhoneAddress_TT.SetToolTip(MinChat._MinChat.PhoneNum_Contact_Lbl, e.Result.ToString().Split(';')[0]);
            //    }));
            //} else
            //    MinChat._MinChat.PhoneAddress_TT.SetToolTip(MinChat._MinChat.PhoneNum_Contact_Lbl, e.Result.ToString().Split(';')[0]);

            MinChat._MinChat.PhoneAddress_TT.Show("1231111111", MinChat._MinChat.PhoneNum_Contact_Lbl, 3000);
        }
        //query contact and phoneaddress    stop
        #endregion
    }
}
