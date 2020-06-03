using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;

namespace CenoCC
{
    public class m_cExcel
    {
        public static string m_fExport(DataSet source, string fileName)
        {
            string defaultFileName = fileName;

            System.IO.StreamWriter excelDoc;
            excelDoc = new System.IO.StreamWriter(defaultFileName);
            const string startExcelXML = "<?xml version=\"1.0\"?>\r\n" +
                  "<?mso-application progid=\"Excel.Sheet\"?>" +
                  "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                  "xmlns:x=\"urn:schemas-microsoft-com:office:" +
                  "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">\r\n <Styles>\r\n " +
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                  "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                  "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                  "\r\n <Protection/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                  "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                  " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                  "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                  "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                  "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                  "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";
            #region 隐藏注释部分
            /*
        <xml version>
        <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
        xmlns:o="urn:schemas-microsoft-com:office:office"
        xmlns:x="urn:schemas-microsoft-com:office:excel"
        xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">
        <Styles>
        <Style ss:ID="Default" ss:Name="Normal">
          <Alignment ss:Vertical="Bottom"/>
          <Borders/>
          <Font/>
          <Interior/>
          <NumberFormat/>
          <Protection/>
        </Style>
        <Style ss:ID="BoldColumn">
          <Font x:Family="Swiss" ss:Bold="1"/>
        </Style>
        <Style ss:ID="StringLiteral">
          <NumberFormat ss:Format="@"/>
        </Style>
        <Style ss:ID="Decimal">
          <NumberFormat ss:Format="0.0000"/>
        </Style>
        <Style ss:ID="Integer">
          <NumberFormat ss:Format="0"/>
        </Style>
        <Style ss:ID="DateLiteral">
          <NumberFormat ss:Format="mm/dd/yyyy;@"/>
        </Style>
        </Styles>
        <Worksheet ss:Name="Sheet1">
        </Worksheet>
        </Workbook>
        */
            #endregion
            //输出Execl文件头
            excelDoc.Write(startExcelXML);
            for (int tableCount = 0; tableCount < source.Tables.Count; tableCount++)
            {
                int rowCount = 0;//每个表行数
                int sheetCount = 1;//每个表页面数
                excelDoc.Write("<Worksheet ss:Name=\"Sheet" + tableCount + sheetCount + "\">");
                #region "输出页面中的表格"
                excelDoc.Write("<Table>");
                excelDoc.Write("<Row>");
                for (int x = 0; x < source.Tables[tableCount].Columns.Count; x++)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(source.Tables[tableCount].Columns[x].ColumnName);
                    excelDoc.Write("</Data></Cell>");
                }
                excelDoc.Write("</Row>");
                foreach (DataRow x in source.Tables[tableCount].Rows)
                {

                    rowCount++;
                    //if the number of rows is > 64000 create a new page to continue output
                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("</Table>");
                        excelDoc.Write(" </Worksheet>");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + tableCount + sheetCount + "\">");
                        excelDoc.Write("<Table>");
                    }
                    excelDoc.Write("<Row>"); //ID=" + rowCount + "
                    for (int y = 0; y < source.Tables[tableCount].Columns.Count; y++)
                    {
                        System.Type rowType;
                        rowType = x[y].GetType();
                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string XMLstring = x[y].ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&");
                                XMLstring = XMLstring.Replace(">", ">");
                                XMLstring = XMLstring.Replace("<", "<");
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by  
                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                                //The Following Code puts the date stored in XMLDate 
                                //to the format above
                                DateTime XMLDate = (DateTime)x[y];
                                string XMLDatetoString = ""; //Excel Converted Date
                                XMLDatetoString = XMLDate.Year.ToString() +
                                     "-" +
                                     (XMLDate.Month < 10 ? "0" +
                                     XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                     "-" +
                                     (XMLDate.Day < 10 ? "0" +
                                     XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                     "T" +
                                     (XMLDate.Hour < 10 ? "0" +
                                     XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                     ":" +
                                     (XMLDate.Minute < 10 ? "0" +
                                     XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                     ":" +
                                     (XMLDate.Second < 10 ? "0" +
                                     XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                     ".000";
                                excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                             "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                            "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                        "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                      "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>");
                                break;
                            default:
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                        }
                    }
                    excelDoc.Write("</Row>");
                }
                excelDoc.Write("</Table>");
                #endregion
                excelDoc.Write(" </Worksheet>");
            }

            //输出Excel末尾
            excelDoc.Write(endExcelXML);
            excelDoc.Close();
            //返回生成的Excel文件的路径和文件名，用于下载
            return defaultFileName;
        }
    }
}
