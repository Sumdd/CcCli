using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class m_wMemo : Form
    {
        public m_wMemo()
        {
            InitializeComponent();
            FormClass.AnimateWindow(this.Handle, 1000, FormClass.AW_BLEND);
        }
    }
}
