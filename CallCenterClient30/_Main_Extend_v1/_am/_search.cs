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
    public partial class _search :
        MetroForm {
        //Form {
        public string argsKey;
        public _index senderEntity;
        public EventHandler SearchEvent;
        public EventHandler SetArgsEvent;
        public EventHandler defaultArgsEvent;
        public bool IsReset = false;
        public Timer delayTimer;
        public Panel _searchpanel;
        public _search() {
            InitializeComponent();
            this.delayTimer = new Timer();
            this.delayTimer.Interval = 1;
        }
        protected override void OnFormClosed(FormClosedEventArgs e) {
            if(this.senderEntity.searchEntity != null)
                this.senderEntity.searchEntity = null;
            base.OnFormClosed(e);
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e) {
            if(this.senderEntity != null) {
                if(this.SetArgsEvent != null) {
                    this.SetArgsEvent(this, null);
                }
            }
            if(this.SearchEvent != null)
                this.SearchEvent(this, null);
        }
        protected virtual void btnReset_Click(object sender, EventArgs e) {
            if(this.senderEntity != null) {
                if(this.SetArgsEvent != null) {
                    if(this.defaultArgsEvent != null)
                        this.defaultArgsEvent(this, null);
                    else
                        this.senderEntity.args = null;
                    this.IsReset = true;
                    this.SetArgsEvent(this, null);
                }
            }
            this.IsReset = false;
        }
        protected virtual void btnCloseAfterSearch_Click(object sender, EventArgs e) {
            if(this.senderEntity != null) {
                if(this.SetArgsEvent != null) {
                    this.SetArgsEvent(this, null);
                }
            }
            if(this.SearchEvent != null)
                this.SearchEvent(this, null);
            this.Close();
        }
        protected void SetArgsMark() {
            if(this.senderEntity != null && this.senderEntity.searchEntity != null) {
                if(this.senderEntity.args == null) {
                    this.senderEntity.args = new Dictionary<string, object>();
                }
                foreach(Control item in this.senderEntity.searchEntity._searchpanel.Controls) {
                    if(item is _queryLeft) {
                        _queryLeft queryLeftEntity = (_queryLeft)item;
                        this.senderEntity.args.Add(queryLeftEntity.queryOperator.Name, queryLeftEntity.queryOperator.Text.ToString());
                    }
                }
            }
        }
        public void SetQueryMark() {
            if(this.senderEntity != null && this.senderEntity.searchEntity != null) {
                if(this.senderEntity.args != null) {
                    foreach(Control item in this.senderEntity.searchEntity._searchpanel.Controls) {
                        if(item is _queryLeft) {
                            _queryLeft queryLeftEntity = (_queryLeft)item;
                            var argsMark = this.senderEntity.args.FirstOrDefault(x => x.Key == queryLeftEntity.queryOperator.Name);
                            if(argsMark.Key != null && argsMark.Value != null)
                                queryLeftEntity.queryOperator.SelectedItem = argsMark.Value;
                        }
                    }
                }
            }
        }
    }
}
