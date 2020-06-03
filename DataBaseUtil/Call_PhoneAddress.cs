///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_SocketCommand.cs
// 类名     : Call_PhoneAddressUtil
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 11:43:14
// 版权信息 : 青岛新生代软件有限公司  www.ceno-soft.net
///////////////////////////////////////////////////////////////////////////////////////

using System;
using DataBaseModel;
using System.Data;
using Common;
using MySql.Data.MySqlClient;
using Core_v1;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;

namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>
    public class Call_PhoneAddressUtil {
        public Call_PhoneAddressUtil() {
            DateTime initDatetime = new DateTime(1900, 1, 1);
        }
        public static string GetPhoneAddress(string phoneNubmer) {
            #region 先弃用
            //string Address = string.Empty;
            //try {
            //    string Sql = string.Empty;
            //    if(PhoneNum.StartsWith("*"))
            //        return "内呼";
            //    if(PhoneNum.StartsWith("0")) {
            //        if(PhoneNum.StartsWith("010"))
            //            return Address = "北京";
            //        if(PhoneNum.StartsWith("02"))
            //            Sql = "select CityName from  Call_PhoneAddress where CityCode='" + PhoneNum.Substring(0, 3) + "'";
            //        if(PhoneNum.StartsWith("01") && PhoneNum.Length > 8)
            //            Sql = "select CityName from  Call_PhoneAddress where PhoneNum='" + PhoneNum.Substring(1, 7) + "'";
            //        else
            //            Sql = "select CityName from  Call_PhoneAddress where CityCode='" + PhoneNum.Substring(0, 4) + "'";
            //    } else {
            //        if(PhoneNum.StartsWith("1") && PhoneNum.Length > 7)
            //            Sql = "select CityName from  Call_PhoneAddress where PhoneNum='" + PhoneNum.Substring(0, 7) + "'";
            //    }
            //    if(!string.IsNullOrEmpty(Sql)) {
            //        DataTable Dt = MySQL_Method.BindTable(Sql);
            //        if(Dt.Rows.Count > 0)
            //            Address = Dt.Rows[0][0].ToString();
            //        else {
            //            NetQueryPhoneAddress()
            //        }
            //    }
            //} catch(Exception ex) {
            //    LogFile.Write(typeof(Call_PhoneAddressUtil), LOGLEVEL.ERROR, "查询电话归属地失败", ex);
            //}
            //return Address;
            #endregion

            #region 再弃用

            string result = "";
            try {
                if(phoneNubmer.StartsWith("*"))
                    return "内呼";

                //号码处理
                string temp = phoneNubmer.TrimStart(new char[] { '0' });
                if(temp.Length < 2)
                    return "";
                int startZeroCount = phoneNubmer.Length - temp.Length;
                string number = "";
                if(temp.StartsWith("1") && temp.ToCharArray()[1] >= '3' && temp.Length >= 7) {
                    //手机号码
                    number = temp.Substring(0, 7);
                } else {
                    //固定电话
                    switch(temp.Substring(0, 1)) {
                        case "1":
                        case "2":  //部分省级市
                            number = "0" + temp.Substring(0, 2);
                            break;
                        default:   //省级代码
                            {
                                string num = temp.Substring(0, 3);
                                if(num == "852" || num == "853" || num == "856") {
                                    number = (startZeroCount >= 2 ? "00" : "0") + num;
                                } else {
                                    number = "0" + num;
                                }
                            }
                            break;
                    }
                }

                if(number.Length < 3)
                    return "";

                string _CityName = string.Empty;
                try {
                    _CityName = GetCityCode(number);
                } catch(Exception ex) {
                    Log.Instance.Success($"[Customs_v1][SeoQuery][PhoneAddress][本地查询错误:{ex.Message}]");
                }

                if(!string.IsNullOrWhiteSpace(_CityName)) {
                    result = _CityName;// + " , " + dt.Rows[0][1].ToString();
                } else {
                    if(number.Length == 7 && true) {
                        //联网查询
                        result = m_fGetPhoneAddressByNet(number);
                    }
                    result = "未知归属地";
                }
            } catch(Exception ex) {
                Log.Instance.Success($"[Customs_v1][SeoQuery][PhoneAddress][联网查询错误:{ex.Message}]");
            }
            return result;

            #endregion

            /*
             * 处理一:得到的号码直接进行处理就行
             */

            return "";
        }

        /// <summary>
        /// 查询手机号码归属地
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string m_fGetPhoneAddressByNet(string number) {

            string m_sPhoneAddressStr = "未知";

            if (!Call_ClientParamUtil.IsLinkNet) {
                return m_sPhoneAddressStr;
            }

            System.Data.DataTable resultT = new DataTable();
            resultT.Columns.Add("location");
            DataRow dr = resultT.NewRow();
            try {
                //TODO:确定网络可用
                //有道API接口
                string url = "http://www.youdao.com/smartresult-xml/search.s?type=mobile&q=" + number;
                XmlDocument XMLResponse = new XmlDocument();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                using(WebResponse wr = req.GetResponse()) {
                    using(StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.GetEncoding("gbk"))) {
                        string Response = sr.ReadToEnd();
                        sr.Close();
                        XMLResponse.LoadXml(Response);
                    }
                }
                XmlNode XMLResult = XMLResponse.SelectSingleNode("smartresult");
                if(XMLResult != null) {
                    if(XMLResult.HasChildNodes) {//有结果
                        XmlNode Address = XMLResult.ChildNodes[0].SelectSingleNode("location");
                        if(Address != null) {
                            string str = Address.InnerText.ToString();
                            dr["location"] = str;
                        }
                    } else {//没有结果

                    }
                }
            } catch(Exception ex) {
                Log.Instance.Success($"[Customs_v1][SeoQuery][NetQueryPhoneAddress][联网查询错误:{ex.Message}]");
            }
            resultT.Rows.Add(dr);
            return resultT.Rows[0]["location"].ToString();
        }

        public static string GetCityCode(string PhoneNum) {
            var model = new Call_PhoneAddressModel();
            string sql = "select ID,PhoneNum,CardType,CityCode,CityName,ZipCode,Remark from call_phoneaddress where PhoneNum like CONCAT('%',?PhoneNum,'%') limit 1";
            MySqlParameter[] parameters = {
     new MySqlParameter("?PhoneNum", MySqlDbType.VarChar,50)
                };
            parameters[0].Value = PhoneNum;
            using(var dr = MySQL_Method.ExecuteDataReader(sql, parameters)) {
                if(dr.Read()) {
                    return dr["CityName"].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 增加最后更新时间的返回
        /// </summary>
        public static string m_fGetCityNameByPhoneNumber(string m_sPhoneNumberStr,out string m_sCityCode,out string m_sDt,out string m_sCardType,out string m_sZipCode)
        {
            string sql = "select ID,PhoneNum,CardType,CityCode,CityName,ZipCode,Remark,upt from call_phoneaddress where PhoneNum like CONCAT('%',?PhoneNum,'%') limit 1";
            MySqlParameter[] parameters = {
     new MySqlParameter("?PhoneNum", MySqlDbType.VarChar,50)
                };
            parameters[0].Value = m_sPhoneNumberStr;
            using (var dr = MySQL_Method.ExecuteDataReader(sql, parameters))
            {
                if (dr.Read())
                {
                    m_sCityCode = dr["CityCode"].ToString();
                    m_sDt = dr["upt"]?.ToString();
                    m_sCardType = dr["CardType"]?.ToString();
                    m_sZipCode = dr["ZipCode"]?.ToString();
                    return dr["CityName"].ToString();
                }
            }
            m_sCityCode = string.Empty;
            m_sDt = string.Empty;
            m_sCardType = string.Empty;
            m_sZipCode = string.Empty;
            return "";
        }

        public static string m_fGetCityNameByCityCode(string m_sCityCodeStr,out string m_sCityCode)
        {
            string sql = "select ID,PhoneNum,CardType,CityCode,CityName,ZipCode,Remark from call_phoneaddress where CityCode like CONCAT('%',?CityCode,'%') limit 1";
            MySqlParameter[] parameters = {
     new MySqlParameter("?CityCode", MySqlDbType.VarChar,50)
                };
            parameters[0].Value = m_sCityCodeStr;
            using (var dr = MySQL_Method.ExecuteDataReader(sql, parameters))
            {
                if (dr.Read())
                {
                    m_sCityCode = dr["CityCode"].ToString();
                    return dr["CityName"].ToString();
                }
            }
            m_sCityCode = string.Empty;
            return "";
        }

        public static string GetContactName(string _PhoneNum) {
            return "未知";
            //throw new NotImplementedException();
        }
    }
}
