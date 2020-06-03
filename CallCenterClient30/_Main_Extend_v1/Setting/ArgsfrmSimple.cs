using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CenoSip;
using LumiSoft.Net.Media;
using Core_v1;
using Model_v1;
using System.Reflection;

namespace CenoCC {
    public partial class ArgsfrmSimple : _form {
        public ArgsfrmSimple() {
            InitializeComponent();
            leftMenu.LostFocus += (s, e) => leftMenu.Update();
            leftMenu.SelectedIndex = 0;
        }

        private void leftMenu_SelectedIndexChanged(object sender, EventArgs e) {
            M_kv m_kv = (M_kv)(this.leftMenu.SelectedItem);
            string _tag = m_kv.tag.ToString();
            if(this.rightPanel.Controls.Count > 0) {
                this.rightPanel.Controls.RemoveAt(0);
            }
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Form _form = (Form)_assembly.CreateInstance($"CenoCC.{_tag}");
            _form.TopMost = false;
            _form.TopLevel = false;
            _form.Parent = this.rightPanel;
            _form.Location = new Point(0, 0);
            _form.Show();
            this.rightPanel.Controls.Add(_form);
        }
    }
}
