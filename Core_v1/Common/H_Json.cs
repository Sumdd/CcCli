using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model_v1;
using System.IO;

namespace Core_v1 {
    public class H_Json {
        private static updateJsonModel _updateJsonModel = null;
        public static updateJsonModel updateJsonModel {
            set {
                _updateJsonModel = value;
            }
            get {
                if(_updateJsonModel == null)
                    ReadJson();
                return _updateJsonModel;
            }
        }
        private static void ReadJson() {
            try {
                string JsonPath = H_Pro.defaultUri("client_update.json");
                using(StreamReader file = File.OpenText(JsonPath)) {
                    _updateJsonModel = JsonConvert.DeserializeObject<updateJsonModel>(file.ReadToEnd());
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[Core_v1][H_JSON][ReadJson][Exception][{ex.Message}]");
            }
        }

        public static updateJsonModel DescUpdateJsonModel(string _json) {
            return JsonConvert.DeserializeObject<updateJsonModel>(_json);
        }

        public static void WriteJson() {
            try {
                string JsonPath = H_Pro.defaultUri("client_update.json");
                var _up_json_str = JsonConvert.SerializeObject(updateJsonModel);
                using(FileStream fs = new FileStream(JsonPath, FileMode.Create, FileAccess.Write)) {
                    fs.Lock(0, fs.Length);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(_up_json_str);
                    fs.Unlock(0, fs.Length);
                    sw.Flush();
                    sw.Close();
                }

            } catch(Exception ex) {
                Log.Instance.Error($"[Core_v1][H_JSON][WriteJson][Exception][{ex.Message}]");
            }
        }

        /// <summary>
        /// 对象转JSON字符串
        /// </summary>
        /// <param name="m_oObject"></param>
        /// <returns></returns>
        public static string ToJson(object m_oObject)
        {
            return JsonConvert.SerializeObject(m_oObject);
        }
    }
}
