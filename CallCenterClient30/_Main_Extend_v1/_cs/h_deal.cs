using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CenoCC {
    public class h_deal {
        private static Regex regex = new Regex("^[0-9*#]{3,20}$");
        public static m_deal ifmt(string content) {
            m_deal _m_deal = new m_deal();
            var splits = content.Split(new string[] { ",", "，", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if(splits.Length > 0) {
                foreach(var _split in splits) {
                    var split = _split.Replace(" ", "");
                    if(!match(split)) {
                        _m_deal.haserr = true;
                        _m_deal.list.Add($"{split} [-Err 号码有误(长度,仅数字)]");
                    } else {
                        _m_deal.list.Add(split);
                    }
                }
            } else {
                _m_deal.haserr = true;
                _m_deal.list.Clear();
                _m_deal.list.Add("[-Err 数据为空]");
            }
            return _m_deal;
        }

        private static bool match(string ms) {
            if(regex.IsMatch(ms))
                return true;
            return false;
        }
    }

    public class m_deal {
        public m_deal() {
            haserr = false;
            list = new List<string>();
        }
        public bool haserr {
            get; set;
        }
        public List<string> list {
            get; set;
        }
    }
}
