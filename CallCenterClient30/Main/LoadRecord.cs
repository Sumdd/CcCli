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
    public partial class LoadRecord : Form
    {
        public LoadRecord()
        {
            InitializeComponent();
        }

        private void LoadRecord_Load(object sender, EventArgs e)
        {
            FormClass.AnimateWindow(this.Handle, 1000, FormClass.AW_BLEND);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
