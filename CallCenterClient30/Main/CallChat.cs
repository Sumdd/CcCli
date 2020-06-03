using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace CenoCC
{
    public partial class CallChat : Form
    {
        public CallChat()
        {
            InitializeComponent();

            InitForm();
        }

        private void InitForm()
        {
            
        }

        private void Dtmf_Btn_Click(object sender, EventArgs e)
        {
            if (this.PhoneNumber_Lbl.Text == "输入电话号码")
                this.PhoneNumber_Lbl.Text = "";
            switch (((Button)sender).Name)
            {
                case "Dtmf_1_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "1";
                    break;
                case "Dtmf_2_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "2";
                    break;
                case "Dtmf_3_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "3";
                    break;
                case "Dtmf_4_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "4";
                    break;
                case "Dtmf_5_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "5";
                    break;
                case "Dtmf_6_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "6";
                    break;
                case "Dtmf_7_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "7";
                    break;
                case "Dtmf_8_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "8";
                    break;
                case "Dtmf_9_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "9";
                    break;
                case "Dtmf_10_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "0";
                    break;
                case "Dtmf_11_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "*";
                    break;
                case "Dtmf_12_Btn":
                    if (this.PhoneNumber_Lbl.Text.Length >= 12)
                    {
                        MessageBox.Show("号码超出界限");
                        return;
                    }
                    this.PhoneNumber_Lbl.Text += "#";
                    break;
            }
        }

        private void PhoneNumber_Lbl_DoubleClick(object sender, EventArgs e)
        {
            this.PhoneNumber_Lbl.Text = "输入电话号码";
        }

        private void PhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.PhoneNumber_Lbl.Text += e.KeyChar;
        }

        private void CallChat_Load(object sender, EventArgs e)
        {
            FormClass.FormCenter(this);
            FormClass.AnimateWindow(this.Handle, 1000, FormClass.AW_BLEND);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            MinChat mc = new MinChat();
            mc.Show();
        }
    }
}
