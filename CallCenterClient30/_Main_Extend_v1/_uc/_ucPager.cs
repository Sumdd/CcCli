using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_v1;
using System.Text.RegularExpressions;

namespace CenoCC {
    public partial class _ucPager : UserControl {
        private Pager m_Pager;
        private int   m_Count;
        private int   m_TotalPage;
        private bool  m_Load = true;
        private bool  m_Enter = false;

        public event EventHandler PageSkipEvent;

        public _ucPager() {
            InitializeComponent();
            this.limitValue.SelectedIndex = 1;
            this.m_Pager = new Pager() { page = 1, limit = Convert.ToInt32(this.limitValue.Text), field = "id", type = "asc", eqli = "=" };
            this.m_Load = false;
        }

        private void limitValue_SelectedIndexChanged(object sender, EventArgs e) {
            if(this.m_Load)
                return;
            this._disabled();
            this.m_Pager.limit = Convert.ToInt32(this.limitValue.Text);
            if((this.m_Pager.page - 1) * this.m_Pager.limit >= m_Count)
                this.m_Pager.page = 1;
            if(this.PageSkipEvent != null) {
                this.PageSkipEvent(this, null);
            }
            this._enabled();
        }

        private void pageUp_Click(object sender, EventArgs e) {
            this._disabled();
            if(this.m_Pager.page > 0) {
                this.m_Pager.page--;
            }
            if(this.PageSkipEvent != null) {
                this.PageSkipEvent(this, null);
            }
            this._enabled();
        }

        private void pageDown_Click(object sender, EventArgs e) {
            this._disabled();
            if(this.m_Pager.page < this.m_TotalPage) {
                this.m_Pager.page++;
            }
            if(this.PageSkipEvent != null) {
                this.PageSkipEvent(this, null);
            }
            this._enabled();
        }

        private void redirectValue_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)Keys.Enter) {
                this.m_Enter = true;
            }
        }

        public Pager pager {
            get {
                return m_Pager;
            }
        }

        public int Count {
            get {
                return m_Count;
            }
        }

        public int PageIndexStart {
            get {
                return (this.m_Pager.page - 1) * this.m_Pager.limit + 1;
            }
        }

        public void Set(int _count) {
            this._disabled();
            this.m_Count = _count;
            this.m_TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m_Count) / Convert.ToDouble(m_Pager.limit)));
            if(this.m_Count > 0 && this.m_Pager.page > this.m_TotalPage) {
                this.m_Pager.page = 1;
                this.PageSkipEvent(this, null);
                return;
            }
            var _pageIndexStart = this.PageIndexStart;
            var _page = this.m_Pager.page;
            int _pageIndexEnd = 0;
            if(this.m_Count == 0) {
                _pageIndexStart = 0;
                _pageIndexEnd = 0;
                _page = 0;
            } else if(this.m_Pager.page == 1) {
                _pageIndexEnd = (this.m_Pager.page - 0) * this.m_Pager.limit;
                this.pageUp.Enabled = false;
                this.pageDown.Enabled = true;
                _enabled();
            } else if(this.m_Pager.page < m_TotalPage) {
                _pageIndexEnd = (this.m_Pager.page - 0) * this.m_Pager.limit;
                this.pageUp.Enabled = true;
                this.pageDown.Enabled = true;
                _enabled();
            } else {
                _pageIndexEnd = this.m_Count;
                this.pageUp.Enabled = true;
                this.pageDown.Enabled = false;
                _enabled();
            }
            if(this.m_TotalPage == 1) {
                _disabled();
                if(this.m_Count > Convert.ToInt32(this.limitValue.Items[0]))
                    this.limitValue.Enabled = true;
                _pageIndexEnd = this.m_Count;
            }
            this.mainTip.Text = $"页码{_page}/{this.m_TotalPage},显示{_pageIndexStart}到{_pageIndexEnd}条,共{this.m_Count}条";
            this.redirectValue.Text = $"{_page}";
        }

        private void _disabled() {
            this.limitValue.Enabled = false;
            this.pageUp.Enabled = false;
            this.pageDown.Enabled = false;
            this.redirectValue.Enabled = false;
        }

        private void _enabled() {
            this.limitValue.Enabled = true;
            this.redirectValue.Enabled = true;
        }

        private void redirectValue_TextChanged(object sender, EventArgs e) {
            if(this.m_Enter) {
                this.m_Enter = false;
                Regex regex = new Regex("^[1-9]\\d*$");
                this.redirectValue.Text = this.redirectValue.Text
                      .Replace("\r\n", "")
                      .Replace(" ", "");
                if(regex.IsMatch(this.redirectValue.Text)) {
                    this._disabled();
                    int page = Convert.ToInt32(this.redirectValue.Text);
                    if(page > this.m_TotalPage) {
                        this.m_Pager.page = this.m_TotalPage;
                    } else if(page < 0) {
                        this.m_Pager.page = 1;
                    } else {
                        this.m_Pager.page = page;
                    }
                    if(this.PageSkipEvent != null) {
                        this.PageSkipEvent(this, null);
                    }
                    this._enabled();
                }
            }
        }
    }
}
