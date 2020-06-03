using Core_v1;
using Model_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC {
    public partial class About : _item {
        public About() {
            InitializeComponent();
            List<M_kv> _list = new List<M_kv>();
            _list.Add(new M_kv() { key = "系统", value = "呼叫中心客户端" });
            _list.Add(new M_kv() { key = "版本", value = H_Json.updateJsonModel?.version });
            _list.Add(new M_kv() { key = "功能一", value = "接听拨打电话" });
            _list.Add(new M_kv() { key = "功能二", value = "来电弹屏、自动拨号" });
            _list.Add(new M_kv() { key = "功能三", value = "拨打量统计" });
            _list.Add(new M_kv() { key = "功能四", value = "自动录音以及下载" });
            _list.Add(new M_kv() { key = "公司", value = "Ceno" });
            _list.Add(new M_kv() { key = "日期", value = "2018/10/01" });
            this.LoadListBody(_list);
        }
    }
}
