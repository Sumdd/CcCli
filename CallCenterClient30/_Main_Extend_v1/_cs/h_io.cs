using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CenoCC {
    public class h_io {
        public static void creat_dir(string _path) {
            if(string.IsNullOrEmpty(_path))
                throw new ArgumentException("路径");
            string _dir = Path.GetDirectoryName(_path);
            if(!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
        }
        public static bool has_file(string _path) {
            if(string.IsNullOrEmpty(_path))
                return false;
            if(File.Exists(_path))
                return true;
            else
                return false;
        }
        public static void open_path(string _path) {
            Process.Start("Explorer.exe", $@"{h_io.path_fmt(_path)}");
        }
        public static void open_path_select(string _path) {
            Process.Start("Explorer.exe", $@"/select,{h_io.path_fmt(_path)}");
        }
        public static string path_fmt(string _path, string _replace = "\\") {
            return new Regex("[\\\\//]+").Replace(_path, _replace);
        }
        public static void open_file(string _file) {
            Process.Start("notepad.exe", h_io.path_fmt(_file));
        }
    }
}
