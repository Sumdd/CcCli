using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC {
    public partial class PhoneNumPanel : UserControl {
        public delegate bool PhoneNumDownEvent(string phoneNum);
        public event PhoneNumDownEvent PhoneNumDown = null;

        public delegate void PhoneNumDelEvent();
        public event PhoneNumDelEvent PhoneNumDel = null;

        public delegate void PhoneNumDialEvent();
        public event PhoneNumDialEvent PhoneNumDial = null;

        public delegate void PhoneNumHungUpEvent();
        public event PhoneNumHungUpEvent PhoneNumHungUp = null;

        public PhoneNumPanel() {
            InitializeComponent();
        }

        private void PhoneNum_Click(object sender, EventArgs e) {
            if(this.PhoneNumDown != null) {
                var button = (Button)sender;
                this.PhoneNumDown(button.Tag.ToString());
            }
        }

        private void btn_del_Click(object sender, EventArgs e) {
            if(this.PhoneNumDel != null) {
                this.PhoneNumDel();
            }
        }

        private void btn_dial_Click(object sender, EventArgs e) {
            if(this.PhoneNumDial != null) {
                this.PhoneNumDial();
            }
        }

        private void btn_hungup_Click(object sender, EventArgs e) {
            if(this.PhoneNumHungUp != null) {
                this.PhoneNumHungUp();
            }
        }
    }
}
