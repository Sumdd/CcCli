///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_SocketCommand.cs
// 类名     : Call_SocketCommand
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 11:43:14
// 版权信息 : 青岛新生代软件有限公司  www.ceno-soft.net
///////////////////////////////////////////////////////////////////////////////////////

using System;
using DataBaseModel;
using System.Data;
using System.Text;

namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>

    public class Call_SocketCommandUtil {
        public Call_SocketCommandUtil() {

        }
        public static string[] GetEndStr() {
            DataTable dt = MySQL_Method.BindTable("select DISTINCT S_EndChar from Call_SocketCommand where S_EndChar<>'' and S_EndChar is not Null");
            if(dt.Rows.Count <= 0)
                return null;
            string[] str = new string[dt.Rows.Count];
            for(int i = 0; i < dt.Rows.Count; i++) {
                str[i] = dt.Rows[i]["S_EndChar"].ToString();
            }
            return str;
        }

        public static string[] GetStartStr() {
            DataTable dt = MySQL_Method.BindTable("select DISTINCT S_StartChar from Call_SocketCommand where S_StartChar<>'' and S_StartChar is not Null");
            if(dt.Rows.Count <= 0)
                return null;
            string[] str = new string[dt.Rows.Count];
            for(int i = 0; i < dt.Rows.Count; i++) {
                str[i] = dt.Rows[i]["S_StartChar"].ToString();
            }
            return str;
        }

        public static string GetConnectServerStr(string[] ConnectParam) {
            string ComStr = string.Empty;
            string SqlStr = "select ID,S_Name,S_StartChar,S_EndChar from Call_SocketCommand where S_NO='LJFWQ'";
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            if(dt.Rows.Count > 0) {
                ComStr += dt.Rows[0]["S_StartChar"].ToString() + dt.Rows[0]["S_Name"].ToString() + "{";
                string EndStr = dt.Rows[0]["S_EndChar"].ToString();
                SqlStr = "select S_Name,S_Value,Rep_OldChar,Rep_NewChar from Call_SocketCommand where S_ParentID=" + dt.Rows[0]["ID"].ToString() + " order by S_Order asc";
                dt = MySQL_Method.BindTable(SqlStr);
                if(dt.Rows.Count > 0 && ConnectParam.Length == dt.Rows.Count) {
                    for(int i = 0; i < dt.Rows.Count; i++) {
                        if(string.IsNullOrEmpty(dt.Rows[i]["S_Name"].ToString()))
                            continue;

                        if(!string.IsNullOrEmpty(dt.Rows[i]["Rep_OldChar"].ToString())) {
                            for(int j = 0; j < dt.Rows[i]["Rep_OldChar"].ToString().Length; j++) {
                                ConnectParam[i] = ConnectParam[i].Replace(dt.Rows[i]["Rep_OldChar"].ToString().ToCharArray()[j], dt.Rows[i]["Rep_NewChar"].ToString().ToCharArray()[j]);
                            }
                        }
                        ComStr += dt.Rows[i]["S_Name"].ToString() + ":" + ConnectParam[i] + ";";
                    }
                    ComStr += "}" + EndStr;
                }
            }
            return ComStr;
        }
        public static string GetStartStr(string S_NO) {
            return MySQL_Method.ExecuteScalar("select DISTINCT S_StartChar from Call_SocketCommand where S_StartChar<>'' and S_StartChar is not Null and S_NO='" + S_NO + "'").ToString();
        }
        public static string GetEndStr(string S_NO) {
            return MySQL_Method.ExecuteScalar("select DISTINCT S_EndChar from Call_SocketCommand where S_EndChar<>'' and S_EndChar is not Null and S_NO='" + S_NO + "'").ToString();
        }
        public static string GetS_NameByS_NO(string S_NO) {
            return MySQL_Method.ExecuteScalar("select S_Name from Call_SocketCommand where S_NO='" + S_NO + "'").ToString();
        }
        public static Call_SocketCommandModel[] GetModelNodeByHeadInfo(string S_NO) {
            DataTable dt = MySQL_Method.BindTable("select * from Call_SocketCommand where S_NO='" + S_NO + "' and S_ParentID<>0 order by S_Order ASC");
            if(dt.Rows.Count >= 0) {
                Call_SocketCommandModel[] _Model = new Call_SocketCommandModel[dt.Rows.Count];
                for(int i = 0; i < dt.Rows.Count; i++) {
                    _Model[i] = new Call_SocketCommandModel();
                    _Model[i].ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    _Model[i].Rep_NewChar = dt.Rows[i]["Rep_NewChar"].ToString();
                    _Model[i].Rep_OldChar = dt.Rows[i]["Rep_OldChar"].ToString();
                    _Model[i].S_EndChar = dt.Rows[i]["S_EndChar"].ToString();
                    _Model[i].S_Name = dt.Rows[i]["S_Name"].ToString();
                    _Model[i].S_NO = dt.Rows[i]["S_NO"].ToString();
                    _Model[i].S_Order = int.Parse(dt.Rows[i]["S_Order"].ToString());
                    _Model[i].S_ParentID = int.Parse(dt.Rows[i]["S_ParentID"].ToString());
                    _Model[i].S_StartChar = dt.Rows[i]["S_StartChar"].ToString();
                    _Model[i].S_Value = dt.Rows[i]["S_Value"].ToString();
                }
                return _Model;
            }
            return null;
        }
        public static string SendCommonStr(string S_NO, params string[] S_Values) {
            StringBuilder CommandStr = new StringBuilder();
            CommandStr.Append(Call_SocketCommandUtil.GetStartStr(S_NO) + Call_SocketCommandUtil.GetS_NameByS_NO(S_NO));
            CommandStr.Append("{");
            Call_SocketCommandModel[] _Model = Call_SocketCommandUtil.GetModelNodeByHeadInfo(S_NO);
            if(_Model == null)
                return CommandStr.Append("}" + Call_SocketCommandUtil.GetEndStr(S_NO)).ToString();

            for(int i = 0; i < _Model.Length; i++) {
                CommandStr.Append(_Model[i].S_Name + $":{S_Values[i]};");
            }
            return CommandStr.Append("}" + Call_SocketCommandUtil.GetEndStr(S_NO)).ToString();
        }

        public static string GetDialInfoStr(params string[] DialParam) {
            string ComStr = string.Empty;
            string SqlStr = "select ID,S_Name,S_StartChar,S_EndChar from Call_SocketCommand where S_NO='BDDH'";
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            if(dt.Rows.Count > 0) {
                ComStr += dt.Rows[0]["S_StartChar"].ToString() + dt.Rows[0]["S_Name"].ToString() + "{";
                string EndStr = dt.Rows[0]["S_EndChar"].ToString();
                SqlStr = "select S_Name,S_Value,Rep_OldChar,Rep_NewChar from Call_SocketCommand where S_ParentID=" + dt.Rows[0]["ID"].ToString() + " order by S_Order asc";
                dt = MySQL_Method.BindTable(SqlStr);
                if(dt.Rows.Count > 0 && DialParam.Length == dt.Rows.Count) {
                    for(int i = 0; i < dt.Rows.Count; i++) {
                        if(string.IsNullOrEmpty(dt.Rows[i]["S_Name"].ToString()))
                            continue;

                        if(!string.IsNullOrEmpty(dt.Rows[i]["Rep_OldChar"].ToString())) {
                            for(int j = 0; j < dt.Rows[i]["Rep_OldChar"].ToString().Length; j++) {
                                DialParam[i] = DialParam[i].Replace(dt.Rows[i]["Rep_OldChar"].ToString().ToCharArray()[j], dt.Rows[i]["Rep_NewChar"].ToString().ToCharArray()[j]);
                            }
                        }
                        ComStr += dt.Rows[i]["S_Name"].ToString() + ":" + DialParam[i] + ";";
                    }
                    ComStr += "}" + EndStr;
                }
            }
            return ComStr;
        }

        public static string GetNewChar(string S_Name) {
            return MySQL_Method.BindTable("select Rep_NewChar from Call_SocketCommand where S_Name='" + S_Name + "'").Rows[0][0].ToString();
        }
        public static string GetOldChar(string S_Name) {
            return MySQL_Method.BindTable("select Rep_OldChar from Call_SocketCommand where S_Name='" + S_Name + "'").Rows[0][0].ToString();
        }

        public static bool HeadInfoContain(string HeadInfo) {
            return MySQL_Method.BindTable("select * from Call_SocketCommand where S_Name='" + HeadInfo + "'").Rows.Count > 0;
        }

        public static string GetHeadInfo(string HeadInfo) {
            return MySQL_Method.BindTable("select S_NO from Call_SocketCommand where S_Name='" + HeadInfo + "'").Rows[0][0].ToString();
        }

        public static string[] GetParamByHeadName(string S_Name) {

            DataTable dt = MySQL_Method.BindTable("select S_Name from Call_SocketCommand where S_ParentID=(SELECT ID from Call_SocketCommand where S_NO='" + S_Name + "' limit 0,1) ORDER BY ID asc");
            if(dt.Rows.Count <= 0)
                return null;
            string[] str = new string[dt.Rows.Count];
            for(int i = 0; i < dt.Rows.Count; i++) {
                str[i] = dt.Rows[i]["S_Name"].ToString();
            }
            return str;
        }
    }
}
