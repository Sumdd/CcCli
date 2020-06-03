using Model_v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class CCFactory {
        public static GlobalData.PhoneType _PhoneType;
        public static ChannelInfo.CH_INFO[] ChInfo;
        public static bool isReStartSip = false;
        private IntPtr nextClipboardViewer = (IntPtr)0;
        public static bool IsInCall = false;
        public static int CurrentCh = 0;
        public const int WM_USER = 0x0900;
        public static IntPtr MainHandle;
        public static GlobalData.DropDialData _DropDialData = new GlobalData.DropDialData();
        public static bool ConnectServerSeen;
        public static List<M_kv> RecentNoanswerRecords;
        public static string m_sLoginString = string.Empty;
        public static bool m_bIsEnterNumber = false;
        public static int IsRegister;
    }
}
