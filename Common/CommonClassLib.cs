using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class CommonClassLib {
        /// <summary>
        /// check string is number and is not null
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool StringIsNumber(string Str) {
            if(string.IsNullOrEmpty(Str))
                return false;
            foreach(char c in Str) {
                if(c >= 48 && c <= 58)
                    continue;
                else
                    return false;
            }
            return true;
        }

        public static string StringValue(object obj) {
            if(obj == null)
                return string.Empty;
            var str = obj.ToString();
            return str;
        }
    }
}
