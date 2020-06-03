using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Core_v1;
using DataBaseUtil;
using Newtonsoft.Json;
using Model_v1;
using System.Threading;
using System.Net;
using System.IO;

namespace Update {
    public partial class Main : MetroForm {
        private updateJsonModel m_updateJsonModel;
        private bool            m_breakThread = false;
        private string          m_updatePath;
        public Main() {
            InitializeComponent();
            this.m_updatePath = Call_ParamUtil.SystemUpgradelPath;
            GetListBody();
        }
        /// <summary>
        /// 加载下载列表
        /// </summary>
        private void GetListBody() {
            try {
                this.m_updateJsonModel = _load_update();
                if(m_updateJsonModel != null) {
                    this.Text = this.Text + $"(版本:{this.m_updateJsonModel?.version})";
                    int index = 1;
                    foreach(var file in this.m_updateJsonModel.files) {
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.ImageIndex = string.IsNullOrWhiteSpace(file.filename) ? 2 : 0;
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "index", Text = $"{index}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "msgTips", Text = $"等待下载" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "fileName", Text = $"{file.filename}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "progress", Text = "0.00%" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ID", Text = $"{index++}" });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "status", Text = $"{listViewItem.ImageIndex}" });
                        this.list.Items.Add(listViewItem);
                    }
                    this.DownLoadQueue();
                } else {
                    Log.Instance.Fail($"[Update][Main][GetListBody][获取更新内容失败]");
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[Update][Main][GetListBody][Exception][{ex.Message}]");
            }
        }
        /// <summary>
        /// 下载队列
        /// </summary>
        private void DownLoadQueue() {
            new Thread(new ThreadStart(() => {
                try {
                    this.refreshListdo();
                    this.metroProgressSpinnerdo(false);
                } catch(Exception ex) {
                    Log.Instance.Error($"[Update][Main][DownLoadQueue][Exception][{ex.Message}]");
                }
            })).Start();
        }
        /// <summary>
        /// Invoke伪进度
        /// </summary>
        /// <param name="visible"></param>
        private void metroProgressSpinnerdo(bool visible) {
            if(this.metroProgressSpinner.InvokeRequired) {
                this.metroProgressSpinner.Invoke(new MethodInvoker(() => {
                    this.metroProgressSpinner.Visible = visible;
                }));
            } else {
                this.metroProgressSpinner.Visible = visible;
            }
        }
        /// <summary>
        /// 列表操作并刷新
        /// </summary>
        private void refreshListdo() {
            if(this.list.InvokeRequired) {
                while(!this.list.IsHandleCreated) {
                    continue;
                }
                this.list.Invoke(new MethodInvoker(refreshListdoInvokeRequired));
            } else {
                refreshListdoInvokeRequired();
            }
        }
        /// <summary>
        /// Invoke列表操作并刷新
        /// </summary>
        private void refreshListdoInvokeRequired() {
            var _folder = H_Pro.defaultUri($"client_{m_updateJsonModel?.version}/");
            H_IO.CreateDir(_folder);
            Log.Instance.Success($"[Update][Main][refreshListdoInvokeRequired][创建保存原文件的路径:{_folder}]");
            foreach(ListViewItem listViewItem in this.list.Items) {
                if(this.m_breakThread) {
                    break;
                }
                switch(Convert.ToInt32(listViewItem.SubItems["status"].Text)) {
                    case 0: {
                            string _recordFile = listViewItem.SubItems["fileName"].Text;
                            try {
                                listViewItem.SubItems["msgTips"].Text = "下载中";
                                Log.Instance.Success($"[Update][Main][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},开始下载...]");
                                this._save_download_update(listViewItem);
                                Log.Instance.Success($"[Update][Main][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载成功]");
                            } catch(Exception ex) {
                                listViewItem.ImageIndex = 3;
                                listViewItem.SubItems["msgTips"].Text = $"下载失败({ex.Message})";
                                listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                                listViewItem.SubItems["progress"].Text = "0.00%";
                                listViewItem.SubItems["progress"].ForeColor = Color.Red;
                                listViewItem.SubItems["status"].Text = "3";
                                Log.Instance.Fail($"[Update][Main][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},{_recordFile},下载失败,{ex.Message}]");
                            }
                        }
                        break;
                    case 1:
                        listViewItem.ImageIndex = 1;
                        listViewItem.SubItems["msgTips"].Text = "下载成功";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
                        listViewItem.SubItems["progress"].Text = "100.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Green;
                        Log.Instance.Fail($"[Update][Main][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},录音已存在]");
                        break;
                    case 2:
                        listViewItem.ImageIndex = 2;
                        listViewItem.SubItems["msgTips"].Text = "路径错误";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Orange;
                        listViewItem.SubItems["progress"].Text = "0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Orange;
                        Log.Instance.Fail($"[Update][Main][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},录音路径为空]");
                        break;
                    case 3:
                    default:
                        listViewItem.ImageIndex = 3;
                        listViewItem.SubItems["msgTips"].Text = "无法下载";
                        listViewItem.SubItems["msgTips"].ForeColor = Color.Red;
                        listViewItem.SubItems["progress"].Text = "0.00%";
                        listViewItem.SubItems["progress"].ForeColor = Color.Red;
                        Log.Instance.Fail($"[Update][DownLoad][refreshListdoInvokeRequired][{listViewItem.SubItems["index"].Text},{listViewItem.SubItems["ID"].Text},无效的下载状态]");
                        break;
                }
            }
            H_Json.updateJsonModel.version = this.m_updateJsonModel?.version;
            H_Json.WriteJson();
        }
        /// <summary>
        /// 窗体退出
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e) {
            this.m_breakThread = true;
            base.OnFormClosing(e);
        }
        /// <summary>
        /// 居中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e) {
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            int aw = this.Width;
            int ah = this.Height;
            this.Left = sw / 2 - aw / 2;
            this.Top = sh / 2 - ah / 2 - 50;
        }
        /// <summary>
        /// 获取更新版本信息
        /// </summary>
        /// <returns></returns>
        private updateJsonModel _load_update() {
            var _updateJsonModel = H_Json.DescUpdateJsonModel(H_Web.Get($"{this.m_updatePath}/client_update.json"));
            return _updateJsonModel;
        }
        /// <summary>
        /// 下载更新
        /// </summary>
        private void _save_download_update(ListViewItem listViewItem) {
            string _id = listViewItem.SubItems["ID"].Text;
            string _filename = listViewItem.SubItems["fileName"].Text;
            var _use_filename = H_Pro.defaultUri(_filename);
            Log.Instance.Success($"[Update][Main][_save_download_update][原文件:{_use_filename}]");
            var _save_filename = H_Pro.defaultUri($"client_{this.m_updateJsonModel?.version}/{_filename}");
            Log.Instance.Success($"[Update][Main][_save_download_update][移动至:{_save_filename}]");
            File.Move(_use_filename, _save_filename);
            using(var client = new WebClient()) {
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                var _server_filename = $"{this.m_updatePath}/client_{this.m_updateJsonModel?.version}/{_filename}";
                Log.Instance.Success($"[Update][Main][_save_download_update][正在下载服务器文件:{_server_filename}]");
                client.DownloadFileAsync(new Uri(_server_filename), _use_filename, listViewItem);
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            ListViewItem listViewItem = (ListViewItem)e.UserState;
            listViewItem.SubItems["progress"].Text = $"{e.ProgressPercentage}.00%";
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            ListViewItem listViewItem = (ListViewItem)e.UserState;
            listViewItem.ImageIndex = 1;
            listViewItem.SubItems["msgTips"].Text = "下载成功";
            listViewItem.SubItems["msgTips"].ForeColor = Color.Green;
            listViewItem.SubItems["progress"].ForeColor = Color.Green;
            listViewItem.SubItems["status"].Text = "1";
        }
    }
}
