using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WebBrowser;

namespace CenoCC
{
    class MainFormClass
    {
        public bool IsWebBrowser;

        public int ControlIndex
        {
            set;
            get;
        }

        public Tab_Title _Tab_Title
        {
            set;
            get;
        }

        public Form Main_Form
        {
            set;
            get;
        }

        public ExtendedWebBrowser _WebBrowser
        {
            set;
            get;
        }

        public string FormName
        {
            get { if (Main_Form != null) return Main_Form.Text; return ""; }
        }

        public bool _ActiveFlag
        {
            set;
            get;
        }
    }
}
