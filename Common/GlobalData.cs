using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Common {
    public class GlobalData {
        /* 电话类型 */

        public enum PhoneType {
            TELEPHONE,
            TELEPHONE_BOX,
            SIP_SOFT_PHONE,
            SIP_BOARD_PHONE,
            OTHER,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DropDialData {
            public string DialNum;
            public string PhoneAddress;
            public string ContentName;
        }
    }
}
