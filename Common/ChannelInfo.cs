using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Common {
    public class ChannelInfo {
        /// <summary>
        /// 定义通道信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CH_INFO {

            /// <summary>
            /// 设备通道号
            /// </summary>
            public Int16 uChannelID;            //
                                                /// <summary>
                                                /// 呼叫类型 1 去电 2 来电 10自动拨号
                                                /// </summary>
            public Int32 uCallType;
            /// <summary>
            /// 存储主叫方号码
            /// </summary>
            public StringBuilder szCallerId;    //
                                                /// <summary>
                                                /// 存储被叫方号码
                                                /// </summary>
            public StringBuilder szCalleeId;    //
                                                /// <summary>
                                                /// 通道状态
                                                /// </summary>
            public APP_USER_STATUS chStatus;
            /// <summary>
            /// 来电响铃次数
            /// </summary>
            public Int32 CallInRingCount;
            /// <summary>
            /// 播放句柄
            /// </summary>
            public Int32 lPlayFileHandle;
            /// <summary>
            /// 录音句柄
            /// </summary>
            public Int32 lRecFileHandle;
            /// <summary>
            /// 录音信息
            /// </summary>
            public RECORD_INFO lRecInfo;
            /// <summary>
            /// 暂存录音文件名
            /// </summary>
            public string WavRecName;
            /// <summary>
            /// 保存当前操作的提醒窗体
            /// </summary>
            public IntPtr CurrentCallRemindFrm;
            /// <summary>
            /// 是否是软拨号
            /// </summary>
            public bool IsSoftDial;
        }

        /// <summary>
        /// 通道录音结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECORD_INFO {
            /// <summary>
            /// 通话类型
            /// </summary>
            public string CallType;
            /// <summary>
            /// 录音完整路径
            /// </summary>
            public string RecordPath;
            /// <summary>
            /// 录音文件名
            /// </summary>
            public string RecordName;
            /// <summary>
            /// 当前号码
            /// </summary>
            public string Caller;
            /// <summary>
            /// 操作号码
            /// </summary>
            public string PhoneNumber;
            /// <summary>
            /// 当前业务员
            /// </summary>
            public string UserID;
            /// <summary>
            /// 响铃时间
            /// </summary>
            public string RingTime;
            /// <summary>
            /// 通话时间
            /// </summary>
            public string SpeakeTime;
            /// <summary>
            /// 停止时间
            /// </summary>
            public string EndTime;
            /// <summary>
            /// 标识是否可用
            /// </summary>
            public bool IsUsable;
        }

        public enum APP_USER_STATUS {
            /// <summary>
            /// 通道空闲
            /// </summary>
            US_STATUS_IDLE,             //通道空闲

            /// <summary>
            /// 通道不可用
            /// </summary>
            US_STATUS_UNAVAILABLE,
            /// <summary>
            /// 摘机
            /// </summary>
            US_STATUS_PICKUP,           //摘机状态
                                        /// <summary>
                                        /// 等待本地挂机
                                        /// </summary>
            US_WAIT_LOCAL_HUNGDOW,     //等待本地挂机
                                       /// <summary>
                                       /// 正在获取按键
                                       /// </summary>
            US_STATUS_GETDTMF,          //获取按键
                                        /// <summary>
                                        /// 通话中
                                        /// </summary>
            US_STATUS_TALKING,          //通话中
                                        /// <summary>
                                        /// 拨号超时
                                        /// </summary>
            US_STATUS_DIALOUTTIME,      //拨号超时
                                        /// <summary>
                                        /// 回铃声中
                                        /// </summary>
            US_STATUS_RINGBACK,         //回铃
                                        /// <summary>
                                        /// 本地响铃
                                        /// </summary>
            US_STATUS_RINGING,           //响铃
                                         /// <summary>
                                         /// 软拨号
                                         /// </summary>
            US_STATUS_AUTOCALLING,      //自动拨号任务执行中
            /// <summary>
            /// 挂机
            /// </summary>
            US_STATUS_HUNGUP,           //挂机
                                        /// <summary>
                                        /// 拨号失败
                                        /// </summary>
            US_STATUS_DIALFAIL,         //拨号失败
            /// <summary>
            /// 拒接
            /// </summary>
            US_STATUS_REFUSE,           //拒接
            /// <summary>
            /// 呼叫保持中
            /// </summary>
            US_STATUS_HOLD,           //呼叫保持
            /// <summary>
            /// 呼叫保持动作
            /// </summary> 
            US_DO_HOLD,               //呼叫保持动作
            /// <summary>
            /// 获取号码
            /// </summary>
            US_DO_GETPHONE,//获取号码
            /// <summary>
            /// 设置录音ID
            /// </summary>
            US_DO_SETRECID,
            /// <summary>
            /// 来电弹屏
            /// </summary>
            US_DO_OPENURL, //来电弹屏
            /// <summary>
            /// 强制退出
            /// </summary>
            US_DO_EXIT,
            /// <summary>
            /// 拨号
            /// </summary>
            US_DO_DAIL,//拨号
            /// <summary>
            /// 改变状态为空闲中
            /// </summary>
            US_LOAD_STATUS_IDLE,
            /// <summary>
            /// 改变状态为不可用
            /// </summary>
            US_LOAD_STATUS_UNAVAILABLE,
            /// <summary>
            /// 重置SIP协议栈
            /// </summary>
            US_DO_SIP_RESET,
            /// <summary>
            /// 无音频输入设备
            /// </summary>
            US_ERR_NODEVICEIN,
            /// <summary>
            /// 无音频输入设备
            /// </summary>
            US_ERR_NODEVICEOUT,
            /// <summary>
            /// 非注册(话机)
            /// </summary>
            US_ERR_NOTREGISTER,
            /// <summary>
            /// 非注册(IP话机Web调用)
            /// </summary>
            US_WEB_NOTREGISTER
        }
    }
}
