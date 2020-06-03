using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace CenoCC {
    public class h_txt {
        public static string read(string path) {
            var content = string.Empty;
            using(StreamReader sr = new StreamReader(path, Encoding.Default)) {
                content = sr.ReadToEnd();
            }
            return content;
        }

        public static void write(m_deal _m_deal, string path) {
            h_io.creat_dir(path);
            var _filepath = $"{path}/错误原因{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            StringBuilder sb = new StringBuilder();
            foreach(var item in _m_deal.list) {
                sb.AppendLine(item);
            }
            using(FileStream fs = new FileStream(_filepath, FileMode.Create)) {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.Write(sb);
                sw.Close();
            }
            if(h_io.has_file(_filepath))
                h_io.open_file(h_io.path_fmt(_filepath));
            else
                h_io.open_path(path);
        }

        public static void write(DataTable _repeat, string path) {
            var _m_deal = new m_deal();
            foreach(DataRow _r in _repeat.Rows) {
                _m_deal.list.Add(_r[0].ToString());
            }
            h_txt.write(_m_deal, path);
        }
    }
}
