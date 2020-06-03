using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Common
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class MoveForm : Form
    {
        #region 移动窗口
        Point mouseOff;//鼠标移动位置变量      
        bool leftFlag;//标签是否为左键  

        protected MoveForm()
        {
            this.MouseDown += new MouseEventHandler(MoveForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MoveForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MoveForm_MouseUp);
        }

        private void MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void MoveForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void MoveForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        #endregion
    }

    public class FormClass
    {
        public const int AW_HOR_POSITIVE = 0x0001;
        public const int AW_HOR_NEGATIVE = 0x0002;
        public const int AW_VER_POSITIVE = 0x0004;
        public const int AW_VER_NEGATIVE = 0x0008;
        public const int AW_CENTER = 0x0010;
        public const int AW_HIDE = 0x10000;
        public const int AW_ACTIVATE = 0x20000;
        public const int AW_SLIDE = 0x40000;
        public const int AW_BLEND = 0x80000;

        public static string managerid = "1";
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyWindow(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long FlashWindow(IntPtr handle, bool bInvert);

        //显示窗体
        public static void FormShow(Form fm)
        {
            if (Application.OpenForms[fm.Name] != null)
            {
                Application.OpenForms[fm.Name].Activate();
            }
            else
            {
                fm.Show();
            }
        }

        public static void FormCenter(Form frm)
        {
            //获取屏幕分辨率
            int Swidth = Screen.PrimaryScreen.Bounds.Width;
            int Sheight = Screen.PrimaryScreen.Bounds.Height;
            //获取程序的大小
            int Awidth = frm.Width;
            int Aheight = frm.Height;
            //加载程序时使程序居中
            frm.Left = Swidth / 2 - Awidth / 2;
            frm.Top = Sheight / 2 - Aheight / 2 - 50;
        }

    }

}
