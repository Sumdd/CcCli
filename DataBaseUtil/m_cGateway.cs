using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBaseUtil
{
    public class m_cGateway
    {
        public static DataTable m_fGatewayList(string m_sID = "", string m_sPopedomSQL = "")
        {
            try
            {
                string m_sSelectSQL = $@"
SELECT
	`id`,
	`uniqueid` AS `uid`,
	`gwtype`,
	`remark`,
	`gw_name` AS `gw`,
CASE
		
		WHEN `remark` IS NULL THEN
		`gw_name` 
		WHEN `remark` = '' THEN
		`gw_name` ELSE CONCAT( `remark`, ' ', `gw_name` ) 
	END AS `rgw`, 
	`call_gateway`.`isinlimit_2`,
	`call_gateway`.`inlimit_2caller`     
FROM
	`call_gateway`
WHERE
	1 = 1
    {(!string.IsNullOrWhiteSpace(m_sID) ? $" AND `uniqueid` = '{m_sID}' LIMIT 1 " : "")}
    {(!string.IsNullOrWhiteSpace(m_sID) ? "" : m_sPopedomSQL)}
;";
                return MySQL_Method.BindTable(m_sSelectSQL);
            }
            catch
            {
                return null;
            }
        }

        public static int m_fSetGateway(string m_sGatewayUniqueID, List<string> m_lStrings)
        {
            try
            {
                string m_sInsertSQL = $@"
update dial_limit
set gwuid = '{m_sGatewayUniqueID}'
where id in ({string.Join(",", m_lStrings.ToArray())});
";
                return MySQL_Method.ExecuteNonQuery(m_sInsertSQL);
            }
            catch
            {
                return 0;
            }
        }
    }
}
