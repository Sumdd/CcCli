using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace CenoCC {
    public partial class _index :
        //MetroForm {
        Form {
        public _search searchEntity;
        public Dictionary<string,object> args;
        public EventHandler SearchEvent;
        public bool m_bResetArgs = true;
        public _index() {
            InitializeComponent();
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e) {
            if(this.searchEntity != null && this.searchEntity.senderEntity != null) {
                if(this.searchEntity.SetArgsEvent != null) {
                    this.searchEntity.SetArgsEvent(this, null);
                }
            }
            if(this.SearchEvent != null) {
                this.SearchEvent(this, null);
            }
        }
        protected virtual void btnReset_Click(object sender, EventArgs e) {
            if (m_bResetArgs)
                this.args = null;
            if(this.searchEntity != null) {
                this.searchEntity.IsReset = true;
                if(this.searchEntity.SetArgsEvent != null) {
                    this.searchEntity.SetArgsEvent(this, null);
                }
                this.searchEntity.IsReset = false;
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) {
            if(this.searchEntity != null) {
                this.searchEntity.Close();
                this.searchEntity = null;
            }
            base.OnFormClosed(e);
        }
    }
}
