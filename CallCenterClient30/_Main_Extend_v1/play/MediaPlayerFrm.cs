using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuartzTypeLib;
using Core_v1;

namespace CenoCC {
    public partial class MediaPlayerFrm : Form {

        private bool _isLocal;
        private string _hostUrl;

        //这里对要使用的变量进行声明
        public OpenFileDialog openFileDialog;
        public IVideoWindow myVideoWindow;
        public IMediaEvent myMediaEvent;
        public IMediaEvent myMediaEventEx;
        public IMediaPosition myMediaPosition;
        public IMediaControl myMediaControl;
        public IBasicAudio myBasicAudio;

        //初始化媒体文件的播放状态
        public string state = "Init";

        //该变量用来保存媒体文件的路径
        public string path;

        public MediaPlayerFrm() {
            InitializeComponent();
            //开启ftp长链,来播放文件试一下
        }

        public MediaPlayerFrm(string filepath) {
            InitializeComponent();
            this.path = filepath;
        }

        private void MediaPlayerFrm_Load(object sender, EventArgs e) {
            //停止播放按钮不可用
            StopBtn.Enabled = false;
            //定时器开始计时
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            //如果播放状态为播放
            if(state == "Play") {
                //更新状态栏
                UpdateStatusBar();

                //播放进度条
                this.PlayProgressBar.Value = (int)myMediaPosition.CurrentPosition;
            }
            //若媒体文件没有播放则不更新状态栏
            else {
                return;
            }
        }

        //更新播放状态栏函数
        private void UpdateStatusBar() {
            try {
                //如果状态不为空 获得播放时间信息
                if(state != "") {
                    //获得当前的播放时间
                    int sec = (int)myMediaPosition.CurrentPosition;
                    //获得小时信息
                    int hour = sec / 3600;
                    //获得分钟信息
                    int min = (sec - (hour * 3600)) / 60;
                    //获得秒信息
                    sec = sec - (hour * 3600 + min * 60);
                    //把当前时间信息显示在状态栏中
                    timeState1.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
                } else {
                    //初始化播放时间
                    timeState.Text = "00:00:00";
                    //初始化当前时间
                    timeState1.Text = "00:00:00";
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][UpdateStatusBar][{ex.Message}]");
            }

        }

        private void MediaPlayerFrm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                //释放DirectShow资源
                myMediaControl.Stop();
                myVideoWindow = null;
                myMediaEvent = null;
                myMediaEventEx = null;
                myMediaPosition = null;
                myMediaControl = null;
                myBasicAudio = null;


            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][MediaPlayerFrm_FormClosing][{ex.Message}]");
            }

        }


        private void PlayBtn_Click(object sender, EventArgs e) {
            try {
                if(string.IsNullOrEmpty(this.path)) {
                    MessageBox.Show("还没有选择播放文件");
                    return;
                }
                if(this.path.ToLower().EndsWith(".mpg") ||
                    this.path.ToLower().EndsWith(".avi") ||
                    this.path.ToLower().EndsWith(".wma") ||
                        this.path.ToLower().EndsWith(".mov") ||
                            this.path.ToLower().EndsWith(".wav") ||
                                this.path.ToLower().EndsWith(".mp2") ||
                                this.path.ToLower().EndsWith(".mp3")) {
                    //如果首次播放
                    if(state == "Init") {
                        //建立FilgraphManager对象来建立媒体文件，并对媒体文件的播放2进行控制
                        FilgraphManager myFilterGraph = new FilgraphManager();
                        //获得媒体文件的路径
                        myFilterGraph.RenderFile(this.path);
                        //建立IBasicAudio用来播放音频
                        myBasicAudio = myFilterGraph as IBasicAudio;
                        try {
                            //从myFilterGraph建立myVideoWindow对象
                            myVideoWindow = myFilterGraph as IVideoWindow;
                            //将myVideoWindow的所有者句柄设为panel1的句柄
                            myVideoWindow.Owner = (int)panel1.Handle;
                            //设置播放窗体的位置和大小
                            myVideoWindow.SetWindowPosition(panel1.ClientRectangle.Left,
                                            panel1.ClientRectangle.Top,
                                            panel1.ClientRectangle.Width,
                                            panel1.ClientRectangle.Height);
                        } catch(Exception ex) {
                            //若生成失败 设置myVideoWindow对象为空
                            myVideoWindow = null;
                        }
                        //建立媒体播放事件的对象
                        myMediaEvent = myFilterGraph as IMediaEvent;

                        //建立媒体播放速率的对象
                        myMediaPosition = myFilterGraph as IMediaPosition;

                        //建立控制媒体播放的对象myMediaControl
                        myMediaControl = myFilterGraph as IMediaControl;
                        //停止播放媒体
                        myMediaControl.Stop();

                        //播放按钮可用
                        PlayBtn.Enabled = true;
                        //暂停播放按钮可用
                        StopBtn.Enabled = true;

                        //获得总的播放时间
                        this.PlayProgressBar.Maximum = (int)myMediaPosition.Duration;
                        int sec = (int)myMediaPosition.Duration;
                        //获得小时信息
                        int hour = sec / 3600;
                        //获得分钟信息
                        int min = (sec - (hour * 3600)) / 60;
                        //获得秒信息
                        sec = sec - (hour * 3600 + min * 60);
                        //将播放时间信息显示在状态栏中
                        timeState.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
                        this.SeekForwardBtn.Enabled = true;
                        this.BackwardBtn.Enabled = true;
                        this.PlayProgressBar.Enabled = true;
                    }
                    //如果按钮为暂停
                    if(PlayBtn.Text == "暂停") {
                        //停止播放文件 
                        myMediaControl.Pause();
                        //设置播放状态为暂停
                        state = "Pause";
                        this.mediaState.Text = "已暂停";
                        //设置按钮为播放按钮
                        PlayBtn.Text = "播放";
                    } else {
                        //播放媒体文件
                        myMediaControl.Run();
                        //设置播放状态为播放
                        state = "Play";
                        this.mediaState.Text = "正在播放";
                        //设置按钮为暂停按钮
                        PlayBtn.Text = "暂停";
                    }
                    return;
                } else {
                    MessageBox.Show(this.path + "  /r/n无效播放文件！");
                }


            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][PlayBtn_Click][{ex.Message}]");
            }


        }

        private void StopBtn_Click(object sender, EventArgs e) {
            try {
                //停止播放媒体文件
                myMediaControl.Stop();
                PlayBtn.Text = "播放";
                this.StopBtn.Enabled = false;
                this.SeekForwardBtn.Enabled = false;
                this.BackwardBtn.Enabled = false;
                this.PlayProgressBar.Enabled = false;
                state = "Init";
                this.mediaState.Text = "已停止";
                //初始化播放时间
                timeState.Text = "00:00:00";
                //初始化当前时间
                timeState1.Text = "00:00:00";
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][StopBtn_Click][{ex.Message}]");
            }

        }

        private void OpenFileBtn_Click(object sender, EventArgs e) {
            try {
                //新建打开文件窗口
                openFileDialog = new OpenFileDialog();
                //设置打开媒体文件的类型
                openFileDialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*";
                //显示该窗口
                openFileDialog.ShowDialog();
                //如果打开文件失败
                if(openFileDialog.FileName == "") {
                    //显示错误对话框
                    MessageBox.Show("Open Error!");
                }
                //获得打开文件的文件名
                pathTBox.Text = openFileDialog.FileName;
                this.path = openFileDialog.FileName;
                return;
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][OpenFileBtn_Click][{ex.Message}]");
            }

        }


        //快进
        private void SeekForwardBtn_Click(object sender, EventArgs e) {
            try {
                if(myMediaPosition.CurrentPosition + 10 < myMediaPosition.Duration) {
                    myMediaPosition.CurrentPosition += 10;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][SeekForwardBtn_Click][{ex.Message}]");
            }


        }

        //快退
        private void BackwardBtn_Click(object sender, EventArgs e) {
            try {
                if(myMediaPosition.CurrentPosition - 10 > 0) {
                    myMediaPosition.CurrentPosition -= 10;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][BackwardBtn_Click][{ex.Message}]");
            }

        }

        private void PlayProgressBar_Scroll(object sender, EventArgs e) {
            try {
                myMediaPosition.CurrentPosition = this.PlayProgressBar.Value;
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MediaPlayerFrm][PlayProgressBar_Scroll][{ex.Message}]");
            }

        }

        //设置播放器播放文件
        public void SetPathOfPlayer(string filepath) {
            this.path = filepath;
        }


        #region ===========获取试听路径===========
        public void BeginSearchFile() {
            this.OpenFileBtn.Enabled = false;
            this.pathTBox.Enabled = false;
            this.OpenFileBtn.Text = "正在加载";
            this.BackwardBtn.Enabled = false;
            this.PlayBtn.Enabled = false;
            this.StopBtn.Enabled = false;
            this.PlayProgressBar.Enabled = false;
            BackgroundWorker _ = new BackgroundWorker();
            _.DoWork += (o, e) => {
                try {
                    e.Result = this.path;
                } catch { }
            };
            _.RunWorkerCompleted += (o, e) => {
                try {
                    this.path = e.Result.ToString();
                    this.OpenFileBtn.Enabled = false;
                    this.pathTBox.Enabled = false;
                    this.pathTBox.Text = System.IO.Path.GetFileName(this.path);
                    this.OpenFileBtn.Text = "浏览";
                    this.BackwardBtn.Enabled = false;
                    this.PlayBtn.Enabled = true;
                    this.StopBtn.Enabled = false;
                    this.PlayProgressBar.Enabled = false;
                } catch { }
            };
            _.RunWorkerAsync();
        }
        #endregion

    }
}
