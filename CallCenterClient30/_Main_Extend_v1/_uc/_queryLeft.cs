using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CenoCC {
    public partial class _queryLeft : UserControl {
        public _queryLeft() {
            InitializeComponent();
        }
        public void thisDefult(string _name, string _thisName, string _operatorStr, bool _enabled, params string[] removeStrOperators) {
            this.queryName.Text = _name;
            this.queryOperator.Name = $"{_thisName.Remove(_thisName.Length - 3)}Mark";
            this.queryOperator.Items.Clear();
            this.queryOperator.Items.AddRange(new object[] { "=", ">", ">=", "<", "<=", "Like" });
            this.queryOperator.SelectedItem = _operatorStr;
            this.queryOperator.Enabled = _enabled;
            List<int> removeIntOperators = new List<int>();
            foreach(string removeStrOperator in removeStrOperators) {
                removeIntOperators.Add(FindIntOperatorByStr(removeStrOperator));
            }
            this.RemoveOperatorsByInt(removeIntOperators.OrderByDescending(x => x).ToList());
        }
        private int FindIntOperatorByStr(string _operator) {
            if(_operator == "=")
                return 0;
            if(_operator == ">")
                return 1;
            if(_operator == ">=")
                return 2;
            if(_operator == "<")
                return 3;
            if(_operator == "<=")
                return 4;
            if(_operator.Equals("Like", StringComparison.InvariantCultureIgnoreCase))
                return 5;
            return 0;
        }
        private void RemoveOperatorsByInt(List<int> removeIntOperators) {
            foreach(int removeIntOperator in removeIntOperators) {
                this.queryOperator.Items.RemoveAt(removeIntOperator);
            }
        }
    }
}
