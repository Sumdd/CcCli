using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using Model_v1;

namespace CenoCC {
    public partial class _item : _metorform {
        public _item() {
            InitializeComponent();
        }
        public void LoadListBody(List<M_kv> _list) {
            foreach(M_kv _kv in _list) {
                ListViewItem listViewItem = new ListViewItem($"{_kv.key}");
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = _kv.value });
                this.list.Items.Add(listViewItem);
            }
        }
    }
}
