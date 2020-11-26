using Common;
using Core_v1;
using DataBaseUtil;
using Model_v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CenoCC
{
    public class m_cPhone
    {
        public static List<string> m_fGetPhoneNumberMemo(string m_sPhoneNumber, out bool m_bIsNeedGetContact, out string m_sDt, out string m_sCardType, out string m_sZipCode)
        {

            /*
             * 精简一下
             * 解决一:由于多号码的引入,是否需要加0出局要通过真正的线路来决定
             * 问题一:手机号码不需要加0,线路非本地需加0
             * 难点一:有的客户使用的线路不同,需要做兼容
             */

            List<string> m_lStrings = new List<string>();
            string m_sRealPhoneNumberStr = string.Empty;
            string m_sPhoneNumberTemp = m_sPhoneNumber;
            string m_sFirstChar = string.Empty;
            string m_sPrefixStr = string.Empty;
            string m_sPhoneAddressStr = string.Empty;
            string m_sCityCodeStr = string.Empty;
            string m_sDealWithStr = string.Empty;
            m_bIsNeedGetContact = false;
            ///号码归属地最后更新时间,后续为自动更新归属地做准备
            m_sDt = string.Empty;
            ///卡运营商类型
            m_sCardType = string.Empty;
            ///邮编
            m_sZipCode = string.Empty;

            try
            {

                /*
                 * 到底前面的零需不需要去掉需要根据规则来判断
                 * 解决一:这里直接开始处理
                 * 优化一:这里处理了之后,最好后台部分就不需要再做处理了,直接拿来使用即可
                 * 冗余一:前后端对一个号码来回处理多次
                 * 解决二:电话都当作不需要加0的来处理,或者增加一个开关变量,允许自动处理电话,否则电话是什么样子就都必须到后方
                 * 问题一:如特殊命令*57*怎么办呢?加了0是
                 */

                switch (m_sPhoneNumber[0])
                {

                    /*
                     * 由于内呼是*
                     * 解决一:也就是含有业务的必须加0
                     * OK,目前以这个逻辑继续
                     * 问题一:*开头业务,无法操作
                     */

                    case '*':
                        Regex m_rRegex = new Regex("^[*][0-9][0-9][0-9][0-9]$");
                        if (m_rRegex.IsMatch(m_sPhoneNumber))
                        {
                            m_sFirstChar = Special.Star;
                            m_sRealPhoneNumberStr = m_sPhoneNumber.Substring(1);
                            m_sPhoneAddressStr = "内呼";
                            m_sDealWithStr = Special.Complete;
                        }
                        else
                        {
                            m_sFirstChar = Special.Zero;
                            m_sRealPhoneNumberStr = m_sPhoneNumber;
                            m_sPhoneAddressStr = "业务";
                            m_sDealWithStr = Special.Complete;
                        }
                        break;
                    case '0':
                    default:
                        m_sFirstChar = Special.Zero;

                        if (m_sPhoneNumber.Contains(Special.Star) ||
                            m_sPhoneNumber.Contains(Special.Hash))
                        {
                            /*
                             * 业务前面的零需要去掉
                             * 假设没有以0开头的其他业务
                             */

                            m_sRealPhoneNumberStr = m_sPhoneNumber.TrimStart('0');
                            m_sPhoneAddressStr = "业务";
                            m_sDealWithStr = Special.Complete;
                        }
                        else
                        {
                            if ((m_sPhoneNumber.Length == 7 || m_sPhoneNumber.Length == 8) && !m_sPhoneNumber.StartsWith("0"))
                            {
                                /*
                                 * 将此电话直接算作本地电话
                                 */
                                m_sRealPhoneNumberStr = m_sPhoneNumberTemp;
                                m_sPrefixStr = "";
                                m_sPhoneAddressStr = "本地";
                                m_sDealWithStr = Special.Complete;
                                m_bIsNeedGetContact = true;
                            }
                            else
                            {
                                /*
                                 * 移除首个0进行判断
                                 * 后面才可能是区号
                                 */
                                m_sPhoneNumber = m_sPhoneNumber.TrimStart('0');
                                if (m_sPhoneNumber.Length < 7)
                                {
                                    m_sRealPhoneNumberStr = m_sPhoneNumberTemp;
                                    m_sPhoneAddressStr = "特殊号码";
                                    m_sDealWithStr = Special.Complete;
                                }
                                else if (m_sPhoneNumber.Length >= 7 && m_sPhoneNumber[0] == '1' && m_sPhoneNumber[1] >= '3')
                                {
                                    m_sRealPhoneNumberStr = m_sPhoneNumber;
                                    m_sPrefixStr = m_sRealPhoneNumberStr.Substring(0, 7);
                                    m_sPhoneAddressStr = Call_PhoneAddressUtil.m_fGetCityNameByPhoneNumber(m_sPrefixStr, out m_sCityCodeStr, out m_sDt, out m_sCardType, out m_sZipCode);
                                    m_sDealWithStr = Special.Mobile;
                                    m_bIsNeedGetContact = true;
                                }
                                else
                                {
                                    switch (m_sPhoneNumber[0])
                                    {
                                        case '1':
                                        case '2':
                                            m_sRealPhoneNumberStr = $"0{m_sPhoneNumber}";
                                            m_sPrefixStr = m_sRealPhoneNumberStr.Substring(0, 3);
                                            break;
                                        default:
                                            if (m_sPhoneNumber.StartsWith("852") ||
                                                m_sPhoneNumber.StartsWith("853") ||
                                                m_sPhoneNumber.StartsWith("856"))
                                            {
                                                m_sRealPhoneNumberStr = $"00{m_sPhoneNumber}";
                                                m_sPrefixStr = m_sRealPhoneNumberStr.Substring(0, 5);
                                            }
                                            else
                                            {
                                                m_sRealPhoneNumberStr = $"0{m_sPhoneNumber}";
                                                m_sPrefixStr = m_sRealPhoneNumberStr.Substring(0, 4);
                                            }
                                            break;
                                    }

                                    ///特殊处理400、800
                                    if (m_sPhoneNumber.StartsWith("400") || m_sPhoneNumber.StartsWith("800"))
                                    {
                                        m_sRealPhoneNumberStr = $"{m_sPhoneNumber}";
                                        m_sPhoneAddressStr = "特殊";
                                        m_sDealWithStr = Special.Complete;
                                    }
                                    else
                                    {
                                        m_sPhoneAddressStr = Call_PhoneAddressUtil.m_fGetCityNameByCityCode(m_sPrefixStr, out m_sCityCodeStr);
                                        m_sDealWithStr = Special.Telephone;
                                        m_bIsNeedGetContact = true;
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(m_sPhoneAddressStr))
                                    m_sPhoneAddressStr = Call_PhoneAddressUtil.m_fGetPhoneAddressByNet(m_sPrefixStr);

                                if (string.IsNullOrWhiteSpace(m_sPhoneAddressStr))
                                    m_sPhoneAddressStr = "未知";
                            }

                        }
                        break;
                }

                m_lStrings.Add(m_sRealPhoneNumberStr);
                m_lStrings.Add(m_sPhoneNumberTemp);
                m_lStrings.Add(m_sFirstChar.ToString());
                m_lStrings.Add(m_sPhoneAddressStr);
                m_lStrings.Add(m_sCityCodeStr);

                /*
                 * 客户端自定义参数:
                 * 是否自动加拨前缀为0,直接将参数赋值为已处理
                 */

                if (!Call_ClientParamUtil.m_bAutoAddNumDialFlag)
                {
                    m_sDealWithStr = Special.Complete;
                }

                m_lStrings.Add(m_sDealWithStr);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][H_Phone][m_fGetPhoneNumberMemo][{ex.Message} {ex.StackTrace}]");
            }

            return m_lStrings;
        }

        public static void m_fSetShow(string m_sPhoneNumberString, out string m_sPhoneAddress, string m_sExtension = null)
        {
            m_sPhoneAddress = "未知";
            try
            {
                bool m_bIsNeedGetContact = false;
                string m_sDt = string.Empty;
                string m_sCardType = string.Empty;
                string m_sZipCode = string.Empty;
                List<string> m_lStrings = m_cPhone.m_fGetPhoneNumberMemo(m_sPhoneNumberString, out m_bIsNeedGetContact, out m_sDt, out m_sCardType, out m_sZipCode);
                m_lStrings.Insert(0, AgentInfo.AgentID);
                m_sPhoneAddress = m_lStrings[4];
                m_cPhone.m_fSetShow(m_lStrings, m_bIsNeedGetContact, m_sExtension);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][H_Phone][m_fSetShow][{ex.Message}]");
            }
        }

        public static void m_fSetShow(List<string> m_lStrings, bool m_bIsNeedGetContact, string m_sExtension = null)
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    string m_sRealPhoneNumberString = m_lStrings[1];
                    string m_sPhoneNumberString = m_lStrings[2];
                    string m_sRealNameString = "未知";
                    string m_sPhoneAddressString = m_lStrings[4];
                    string m_sShowString = string.Empty;

                    List<string> m_lShowStyleList = Call_ClientParamUtil.ShowStyleString.Split(',').ToList();
                    bool m_bIsShowNumber = m_lShowStyleList[0] == "1";
                    bool m_bIsShowRealName = m_lShowStyleList[1] == "1";
                    bool m_bIsShowAddress = m_lShowStyleList[2] == "1";
                    bool m_bHasSecretNumber = !string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber);

                    if (m_bIsNeedGetContact)
                    {
                        if (m_bIsShowRealName && Call_ParamUtil.m_bUseHomeSearch)
                        {
                            m_cShowStyle.m_mContact _m_mContact = m_cShowStyle.m_fGetContact(m_sRealPhoneNumberString);

                            //时间与联系人姓名一同查询出来
                            if (
                            _m_mContact != null &&
                            _m_mContact.m_dtUpdateTime > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"))
                            )
                            {
                                //无需任何处理
                                m_sRealNameString = _m_mContact.m_sRealNameString;
                            }
                            else
                            {
                                //需要查询催收数据库
                                string _m_sRealNameString = m_cEsySQL.m_fGetContact(m_sRealPhoneNumberString);
                                if (!string.IsNullOrWhiteSpace(_m_sRealNameString))
                                {
                                    m_sRealNameString = _m_sRealNameString;
                                    //更新呼叫中心数据库
                                    m_cShowStyle.m_fSetContact(m_sRealPhoneNumberString, m_sRealNameString);
                                }
                                else
                                {
                                    m_sRealNameString = _m_mContact?.m_sRealNameString ?? "未知";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (m_sPhoneAddressString == "内呼")
                        {
                            m_sRealNameString = Call_AgentUtil.m_fGetAgentName(m_lStrings[1]);
                        }
                    }

                    MinChat._MinChat.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (m_bIsShowNumber)
                        {
                            ///<![CDATA[
                            /// 增加号码隐藏逻辑
                            /// ]]>
                            if (m_bHasSecretNumber)
                                m_sShowString = MinChat.m_sSecretNumber;
                            else
                                m_sShowString = m_sPhoneNumberString;
                        }
                        if (m_bIsShowRealName)
                        {
                            if (m_sRealNameString != "未知")
                            {
                                if (!string.IsNullOrWhiteSpace(m_sShowString))
                                {
                                    m_sShowString += $"({m_sRealNameString})";
                                }
                                else
                                {
                                    m_sShowString = m_sRealNameString;
                                }
                            }
                            else
                            {
                                ///<![CDATA[
                                /// 增加号码隐藏逻辑
                                /// ]]>
                                if (m_bHasSecretNumber)
                                    m_sShowString = MinChat.m_sSecretNumber;
                                else
                                    m_sShowString = m_sPhoneNumberString;
                            }
                        }
                        if (m_bIsShowAddress)
                        {
                            if (m_sPhoneAddressString != "未知")
                            {
                                m_sShowString += $"({m_sPhoneAddressString})";
                            }
                        }

                        MinChat._MinChat.PhoneNum_Contact_Lbl.Text = m_sShowString;
                        StringBuilder m_sbTipMsg = new StringBuilder();

                        string m_sNumberType = "去　电：";
                        if (CCFactory.ChInfo[CCFactory.CurrentCh].uCallType == 2)
                        {
                            m_sNumberType = "来　电：";
                        }
                        if (!string.IsNullOrWhiteSpace(m_sExtension))
                        {
                            m_sbTipMsg.AppendLine($"分机号：{m_sExtension}");
                        }
                        m_sbTipMsg.AppendLine($"{m_sNumberType}{m_sPhoneNumberString}");
                        m_sbTipMsg.AppendLine($"联系人：{m_sRealNameString}");
                        m_sbTipMsg.AppendLine($"归属地：{m_sPhoneAddressString}");
                        MinChat._MinChat.PhoneAddress_TT.SetToolTip(MinChat._MinChat.PhoneNum_Contact_Lbl, m_sbTipMsg.ToString());
                    }));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][H_Phone][m_fSetShow][{ex.Message}]");
                }

            }).Start();
        }
    }
}
