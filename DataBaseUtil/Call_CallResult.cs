///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_SocketCommand.cs
// 类名     : Call_PhoneAddressModel
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 11:43:14
// 版权信息 : 青岛新生代软件有限公司  www.ceno-soft.net
///////////////////////////////////////////////////////////////////////////////////////

using System;
using DataBaseModel;
using Common;
using System.Data;
namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>
    public class Call_CallResultUtil {
        public static DataTable GetList() {
            string asSQL = "select ID,R_Description AS Name from Call_CallResult";
            DataTable dt = MySQL_Method.BindTable(asSQL);
            return dt;
        }
    }
}
