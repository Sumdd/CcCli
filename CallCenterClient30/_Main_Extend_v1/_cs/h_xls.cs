using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core_v1;
using System.Data.OleDb;
using System.Data;

namespace CenoCC
{
    public class h_xls
    {
        public static System.Data.DataTable m_fDataTable(string m_sPath)
        {
            System.Data.DataTable m_pDataTable = null;
            try
            {
                DataTable table;
                //连接字符串  
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + m_sPath + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection myConn = new OleDbConnection(sConnectionString);
                string strCom = " SELECT * FROM [Sheet1$]";
                myConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                table = new DataTable();
                myCommand.Fill(table);
                myConn.Close();
                return table;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"h_xls m_fDataTable error:{ex.Message}");
            }
            return m_pDataTable;
        }
    }
}
