//using Cmn_v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core_v1 {
    public class H_IO {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="_path">目录</param>
        public static void CreateDir(string _path) {
            if(string.IsNullOrWhiteSpace(_path))
                throw new ArgumentException("路径");
            string _dir = Path.GetDirectoryName(_path);
            if(!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="_path">文件路径</param>
        /// <returns></returns>
        public static bool HasFile(string _path) {
            if(string.IsNullOrWhiteSpace(_path))
                throw new ArgumentException("路径");
            //if(File.Exists(Cmn.PathFmt(_path)))
            if(File.Exists(_path))
                return true;
            else
                return false;
        }
    }
}
