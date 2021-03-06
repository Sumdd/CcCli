﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model_v1
{
    public class m_mTree
    {
        public string ID
        {
            get; set;
        }
        public string fID
        {
            get; set;
        }
        public string n
        {
            get; set;
        }
        public string t
        {
            get; set;
        }
    }

    public class m_mOperate
    {
        /// <summary>
        /// 未接来电显示
        /// </summary>
        public const string noanswer_number_show = "noanswer_number_show";
        /// <summary>
        /// 是否全号显示
        /// </summary>
        public static bool m_bSeeNumber = false;

        /// <summary>
        /// 浏览器
        /// </summary>
        public const string browser = "browser";
        #region ***通话记录
        public const string phonerecords = "phonerecords";
        public const string phonerecords_search_share = "phonerecords_search_share";
        public const string phonerecords_search_share_all = "phonerecords_search_share_all";
        public const string phonerecords_listen = "phonerecords_listen";
        public const string phonerecords_download = "phonerecords_download";
        public const string phonerecords_download_page = "phonerecords_download_page";
        public const string phonerecords_download_all = "phonerecords_download_all";
        public const string phonerecords_excel = "phonerecords_excel";
        #endregion
        public const string phonestatistical = "phonestatistical";
        public const string diallimit = "diallimit";
        #region ***拨号限制
        public const string diallimit_search_share = "diallimit_search_share";
        public const string diallimit_number_add_share = "diallimit_number_add_share";
        #endregion
        public const string share_area = "share_area";
        public const string user = "user";
        public const string power = "power";
        #region ***权限
        public const string power_operate = "power_operate";
        public const string power_data = "power_data";
        public const string power_operate_save = "power_operate_save";
        #endregion
        public const string args = "args";
        #region ***网关管理
        public const string diallimit_gateway_XMLedit = "diallimit_gateway_XMLedit";
        #endregion
    }

    public class PopedomArgs
    {
        public string type;
        public List<string> left = new List<string>();
        public bool part { get; set; } = false;
        public string TSQL;
    }

    public class DataPowerType
    {
        /// <summary>
        /// 通话记录查询
        /// </summary>
        public const string _data_phonerecords_search = "data_phonerecords_search";
        /// <summary>
        /// 统计表查询
        /// </summary>
        public const string _data_phonestatistical_search = "data_phonestatistical_search";
        /// <summary>
        /// 限制配置查询
        /// </summary>
        public const string _data_diallimit_limit_search = "data_diallimit_limit_search";
        /// <summary>
        /// 限制配置分配
        /// </summary>
        public const string _data_diallimit_limit_allot = "data_diallimit_limit_allot";
        /// <summary>
        /// 网关查询
        /// </summary>
        public const string _data_gateway_search = "data_gateway_search";
        /// <summary>
        /// 网关分配
        /// </summary>
        public const string _data_gateway_allot = "data_gateway_allot";
        /// <summary>
        /// 用户管理查询
        /// </summary>
        public const string _data_user_search = "data_user_search";
        /// <summary>
        /// 路由查询
        /// </summary>
        public const string _data_route_search = "data_route_search";
        /// <summary>
        /// 黑白名单查询
        /// </summary>
        public const string _data_wblist_search = "data_wblist_search";
        /// <summary>
        /// 黑白名单查询
        /// </summary>
        public const string _data_inrule_search = "data_inrule_search";
    }

    public class DataPower
    {
        public string dcode
        {
            get; set;
        }
        public string duuid
        {
            get; set;
        }
    }

    public class m_mWebSocketJsonPrefix
    {
        public const string _m_sPrefix = "{JSON-AUTO-DIAL-TASK}";
        public const string _m_sHttpCmd = "{JSON-HTTP-CMD}";
        public const string _m_sP2PMsgCmd = "{JSON-P2PMSG-CMD}";
        public const string _m_sFSCmd = "{JSON-FS-CMD}";
        public const string _m_sFileCmd = "{JSON-FILE-CMD}";
    }

    public class m_mSendAsync
    {
        public m_mSendAsync(int _m_uTimeout = 15)
        {
            m_sUUID = Guid.NewGuid().ToString();
            m_pDateTime = DateTime.Now;
            m_iStatus = -2;
            m_uTimeout = _m_uTimeout;
        }
        public string m_sUUID
        {
            get;
            set;
        }
        public object m_oObject
        {
            get;
            set;
        }
        public DateTime m_pDateTime
        {
            get;
            set;
        }
        public int m_uTimeout
        {
            get;
            set;
        }
        public int m_iStatus
        {
            get;
            set;
        }
    }

    public class m_mResponseJSON
    {
        public int status
        {
            get;
            set;
        }

        public string msg
        {
            get;
            set;
        }

        public object result
        {
            get;
            set;
        }
    }

    public class m_mWebSocketJson
    {
        public string m_sUse
        {
            get;
            set;
        }

        public object m_oObject
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 通用的freeswitch命令,后续更丰富
    /// </summary>
    public class m_cFSCmd
    {
        /// <summary>
        /// 查看所有网关状态
        /// </summary>
        public const string m_sCmd_sofia_xmlstatus_gateway = "sofia xmlstatus gateway";
        /// <summary>
        /// 杀死特定网关
        /// </summary>
        public const string m_sCmd_sofia_profile_external_killgw_ = "sofia profile external killgw ";
        /// <summary>
        /// 保护性重启external
        /// </summary>
        public const string m_sCmd_sofia_profile_external_rescan = "sofia profile external rescan";
        /// <summary>
        /// 重启external
        /// </summary>
        public const string m_sCmd_sofia_profile_external_restart = "sofia profile external restart";
    }

    public class m_mGateway
    {
        public string name
        {
            get;
            set;
        }
        public string state
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
    }

    public class m_cFSCmdType
    {
        /// <summary>
        /// 发送并执行freeswitch命令
        /// </summary>
        public const string _m_sFSCmd = "FSCmd";
        /// <summary>
        /// 读取gateway文件
        /// </summary>
        public const string _m_sReadGateway = "ReadGateway";
        /// <summary>
        /// 创建gateway文件
        /// </summary>
        public const string _m_sCreateGateway = "CreateGateway";
        /// <summary>
        /// 写入gateway文件
        /// </summary>
        public const string _m_sWriteGateway = "WriteGateway";
        /// <summary>
        /// 删除gateway文件
        /// </summary>
        public const string _m_sDeleteGateway = "DeleteGateway";

    }

    public class m_cFileCmdType
    {
        /// <summary>
        /// 上传文件的相对目录
        /// </summary>
        public const string _m_sFilePath = "fload";
        /// <summary>
        /// 创建文件
        /// </summary>
        public const string _m_sFileCreate = "FileCreate";
        /// <summary>
        /// 删除文件
        /// </summary>
        public const string _m_sFileDelete = "FileDelete";
    }

    public class m_mGatewayType
    {
        public const string _m_sGateway = "gateway";
        public const string _m_sInternal = "internal";
        public const string _m_sExternal = "external";
    }

    public class m_mShareApi
    {
        public int id
        {
            get; set;
        }
        public string unique_id
        {
            get; set;
        }
        public string gw_name
        {
            get; set;
        }
        public string caller_prefix
        {
            get; set;
        }
        public string caller_number
        {
            get; set;
        }
        public string callee_prefix
        {
            get; set;
        }
        public string call_server
        {
            get; set;
        }
        public string group_id
        {
            get; set;
        }
        public string remark
        {
            get; set;
        }
        public string creator
        {
            get; set;
        }
        public string create_time
        {
            get; set;
        }
    }

    public class m_mBook
    {
        public string inrulebookname { get; set; }
        public string inrulebookfkey { get; set; }
        public string inrulebooktkey { get; set; }
        public float inrulebookordernum { get; set; }
    }

    public class m_mWblist
    {
        public float ordernum { get; set; }
        public string wbname { get; set; }
        public string wbnumber { get; set; }
        public int wblimittype { get; set; }
        public int wbtype { get; set; }
    }

    public class m_mInrule
    {
        public int inruleid { get; set; }
        public string inrulename { get; set; }
        public string inruleip { get; set; }
        public string inrulesuffix { get; set; }
        public float ordernum { get; set; }
        public int inruleport { get; set; }
        public string inruleua { get; set; }
        public int inrulemain { get; set; }
        public List<m_mBook> m_lBook { get; set; }
    }
}
