using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenoSocket
{
    /// <summary>
    /// WebSocket模型
    /// </summary>
    public class M_WebSocket
    {
        /// <summary>
        /// 连接服务器结果
        /// </summary>
        public const string _ljfwqjg = "LJFWQJG";
        #region 连接服务器结果
        /// <summary>
        /// 连接服务器结果:成功
        /// </summary>
        public const string _ljfwqjg_success = "Success";
        /// <summary>
        /// 连接服务器结果:多点登录
        /// </summary>
        public const string _ljfwqjg_more = "More";
        /// <summary>
        /// 连接服务器结果:服务器退出
        /// </summary>
        public const string _ljfwqjg_exit = "Exit";
        #endregion
        /// <summary>
        /// 发送录音
        /// </summary>  
        public const string _fsly = "FSLY";
        /// <summary>
        /// 发送来电号码
        /// </summary>
        public const string _fsldhm = "FSLDHM";
        /// <summary>
        /// 拨号状态    
        /// </summary>
        public const string _bhzt = "BHZT";
        #region 拨号状态
        /// <summary>
        /// 拨号状态:失败
        /// </summary>
        public const string _bhzt_fail = "Fail";
        /// <summary>
        /// 拨号状态:摘机
        /// </summary>
        public const string _bhzt_pick = "Pick";
        /// <summary>
        /// 拨号状态:挂断
        /// </summary>
        public const string _bhzt_hang = "Hang";
        #endregion
        /// <summary>
        /// 自动外呼
        /// </summary>
        public const string _zdwh = "ZDWH";
    }
}
